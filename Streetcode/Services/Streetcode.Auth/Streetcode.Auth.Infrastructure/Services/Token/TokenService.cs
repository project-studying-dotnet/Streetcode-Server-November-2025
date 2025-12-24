using FluentResults;
using Streetcode.Auth.Application.Dtos.Auth;
using Streetcode.Auth.Application.Interfaces.Token;
using Streetcode.Auth.Application.Repositories.Interfaces.ResfreshTokens;
using Streetcode.Auth.Common.Configurations;
using Streetcode.Auth.Domain.Entities.Users;
using Streetcode.BuildingBlocks.Interfaces.Logging;

namespace Streetcode.Auth.Infrastructure.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings  jwtSettings;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly ILoggerService logger;

        public Task<Result<TokenResponseDto>> GenerateTokensAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<TokenResponseDto>> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> RevokeExpiredRefreshTokensAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> DeleteRevokedRefreshTokensAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
