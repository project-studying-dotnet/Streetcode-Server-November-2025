using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Streetcode.Auth.Application.Dtos.Auth;
using Streetcode.Auth.Application.Interfaces.Token;
using Streetcode.Auth.Application.Repositories.Interfaces.ResfreshTokens;
using Streetcode.Auth.Common.Configurations;
using Streetcode.Auth.Domain.Entities.Auth;
using Streetcode.Auth.Domain.Entities.Users;
using Streetcode.BuildingBlocks.Interfaces.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Streetcode.Auth.Infrastructure.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings  _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILoggerService _logger;
        private readonly UserManager<User> _userManager;

        public TokenService(
            IOptions<JwtSettings> jwtOptions,
            IRefreshTokenRepository refreshTokenRepository,
            UserManager<User> userManager,
            ILoggerService logger)
        {
            _jwtSettings = jwtOptions.Value;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<Result<TokenResponseDto>> GenerateTokensAsync(User user, CancellationToken cancellationToken)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var utcNow = DateTime.UtcNow;
            var accessTokenExpiry = utcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);

            var accessToken = CreateJwtAccessToken(user, userRoles, accessTokenExpiry);
            var refreshTokenValue = GenerateRefreshTokenString();
            var refreshTokenExpiry = utcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshTokenValue,
                ExpiresOn = refreshTokenExpiry,
                UserId = user.Id,
                IsRevoked = false,
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

            var changesSaved = await _refreshTokenRepository.SaveChangesAsync(cancellationToken) > 0;
            
            if (changesSaved)
            {
                var tokenResponseDto = new TokenResponseDto
                {
                    AccessToken = accessToken,
                    AccessTokenExpiresAt = accessTokenExpiry,

                    RefreshToken = refreshTokenValue,
                    RefreshTokenExpiresAt = refreshTokenExpiry
                };

                return Result.Ok(tokenResponseDto);
            }
            else
            {
                return Result.Fail<TokenResponseDto>("Failed to save RefreshToken.");
            }
        }

        public async Task<Result<TokenResponseDto>> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Result.Fail<TokenResponseDto>("Refresh token is null or empty.");
            }

            var existingRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken);

            if (existingRefreshToken == null)
            {
                return Result.Fail<TokenResponseDto>("Invalid refresh token.");
            }

            if (existingRefreshToken.IsRevoked)
            {
                return Result.Fail<TokenResponseDto>("Refresh token has been revoked.");
            }

            if (existingRefreshToken.ExpiresOn < DateTime.UtcNow)
            {
                await RevokeRefreshTokenAsync(existingRefreshToken.Token, existingRefreshToken.UserId, cancellationToken);
                return Result.Fail<TokenResponseDto>("Refresh token has expired.");
            }

            var user = existingRefreshToken.User!;
            var utcNow = DateTime.UtcNow;
            var accessTokenExpiry = utcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);
            var userRoles = await _userManager.GetRolesAsync(user);

            var accessToken = CreateJwtAccessToken(user, userRoles, accessTokenExpiry);

            var tokenResponseDto = new TokenResponseDto
            {
                AccessToken = accessToken,
                AccessTokenExpiresAt = accessTokenExpiry,

                RefreshToken = existingRefreshToken.Token,
                RefreshTokenExpiresAt = existingRefreshToken.ExpiresOn
            };

            return Result.Ok(tokenResponseDto);
        }

        public async Task<Result<bool>> RevokeRefreshTokenAsync(string refreshToken, int expectedUserId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Result.Fail<bool>("Refresh token is null or empty.");
            }

            var refreshTokenEntity = await _refreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken);

            // Idempotent logout: token absent => already logged out
            if (refreshTokenEntity == null)
            {
                return Result.Ok(true);
            }

            // Ownership check: do not revoke other user's token
            if (refreshTokenEntity.UserId != expectedUserId)
            {
                return Result.Ok(true);
            }

            // Idempotent: already revoked => ok
            if (refreshTokenEntity.IsRevoked)
            {
                return Result.Ok(true);
            }

            refreshTokenEntity.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(refreshTokenEntity, cancellationToken);

            var changesSaved = await _refreshTokenRepository.SaveChangesAsync(cancellationToken) > 0;
            return changesSaved ? Result.Ok(true) : Result.Fail<bool>("Failed to save revoke RefreshToken.");
        }

        public async Task<Result<int>> RevokeExpiredRefreshTokensAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            _logger.LogInformation($"Starting bulk revoke of expired refresh tokens at {now}");

            var revokedCount = await _refreshTokenRepository
                .BulkRevokeExpiredTokensAsync(now, cancellationToken);

            if (revokedCount == 0)
            {
                _logger.LogInformation("No expired refresh tokens found to revoke.");
            }
            else
            {
                _logger.LogInformation($"Completed bulk revoke of expired refresh tokens. Total revoked: {revokedCount}");
            }

            return Result.Ok(revokedCount);
        }

        public async Task<Result<int>> DeleteRevokedRefreshTokensAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting bulk delete of revoked refresh tokens at {DateTime.UtcNow}");

            var deletedCount = await _refreshTokenRepository
                .BulkDeleteRevokedTokensAsync(cancellationToken);

            if (deletedCount == 0)
            {
                _logger.LogInformation("No revoked refresh tokens found to delete.");
            }
            else
            {
                _logger.LogInformation($"Completed bulk delete of revoked refresh tokens. Total deleted: {deletedCount}");
            }

            return Result.Ok(deletedCount);
        }

        private string CreateJwtAccessToken(User user, IEnumerable<string> userRoles, DateTime expiresAt)
        {
            var utcNow = DateTime.UtcNow;
            var tokenClaims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (ClaimTypes.Name, user.UserName ?? string.Empty)
            };

            foreach (var userRole in userRoles)
            {
                tokenClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: tokenClaims,
                notBefore: utcNow,
                expires: expiresAt,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
