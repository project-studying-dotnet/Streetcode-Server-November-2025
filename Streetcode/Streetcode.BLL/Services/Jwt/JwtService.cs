using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Streetcode.BLL.DTO.Users;
using Streetcode.BLL.Interfaces.Jwt;
using Streetcode.DAL.Entities.Jwt;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.Services.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly UserManager<User> _userManager;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _accessTokenExpirationMinutes;
        private readonly int _refreshTokenExpirationMinutes;

        public JwtService(string secretKey, string issuer, string audience, IRepositoryWrapper repository, UserManager<User> userManager, int accessTokenExpirationMinutes = 15, int refreshTokenExpirationMinutes = 600)
        {
            _repository = repository;
            _userManager = userManager;
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
            _accessTokenExpirationMinutes = accessTokenExpirationMinutes;
            _refreshTokenExpirationMinutes = refreshTokenExpirationMinutes;
        }

        public async Task<TokenResultDto> GenerateAccessTokenAsync(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("token_type", "access")
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new TokenResultDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresAt
            };
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            try
            {
                var principal = tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _issuer,
                        ValidAudience = _audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TokenResultDto> GenerateRefreshTokenAsync(User user)
        {
            string tokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var refreshToken = new RefreshToken
            {
                Token = tokenValue,
                UserId = user.Id,

                ExpiresOn = DateTime.UtcNow.AddMinutes(_refreshTokenExpirationMinutes),
                IsRevoked = false,
            };

            await _repository.RefreshTokenRepository.CreateAsync(refreshToken);
            await _repository.SaveChangesAsync();

            return new TokenResultDto
            {
                Token = refreshToken.Token,
                ExpiresAt = refreshToken.ExpiresOn
            };
        }

        public async Task<Result<TokenResultDto>> RefreshTokenAsync(string token)
        {
            RefreshToken? refreshToken =
                await _repository.RefreshTokenRepository
                .GetFirstOrDefaultAsync(t => t.Token == token);

            if (refreshToken is null)
            {
                string errorMsg = "refreshToken token doesn't exist";
                return Result.Fail<TokenResultDto>(new Error(errorMsg));
            }

            if (refreshToken.IsRevoked || refreshToken.ExpiresOn < DateTime.UtcNow)
            {
                string errorMsg = "refreshToken token expiered";
                refreshToken.IsRevoked = true;
                await _repository.SaveChangesAsync();
                return Result.Fail<TokenResultDto>(new Error(errorMsg));
            }

            User user = refreshToken.User;

            refreshToken.IsRevoked = true;

            await _repository.SaveChangesAsync();

            var newAccessToken = await GenerateAccessTokenAsync(user);

            return Result.Ok(newAccessToken);
        }
    }
}