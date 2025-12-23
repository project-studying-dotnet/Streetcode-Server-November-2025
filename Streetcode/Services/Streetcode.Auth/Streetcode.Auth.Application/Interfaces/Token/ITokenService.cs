using FluentResults;
using Streetcode.Auth.Application.Dtos.Auth;
using Streetcode.Auth.Domain.Entities.Users;

namespace Streetcode.Auth.Application.Interfaces.Token
{
    public interface ITokenService
    {
        Task<Result<TokenResponseDto>> GenerateTokensAsync(User user, CancellationToken cancellationToken);
        Task<Result<TokenResponseDto>> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<Result<bool>> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<Result<int>> RevokeExpiredRefreshTokensAsync(CancellationToken cancellationToken);
        Task<Result<int>> DeleteRevokedRefreshTokensAsync(CancellationToken cancellationToken);
    }
}
