using FluentResults;
using Streetcode.Auth.Application.Dtos.Auth;
using Streetcode.Auth.Application.Interfaces.Token;
using Streetcode.Auth.Domain.Entities.Users;

namespace Streetcode.Auth.Application.Services.Token
{
    public class TokenService : ITokenService
    {
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
