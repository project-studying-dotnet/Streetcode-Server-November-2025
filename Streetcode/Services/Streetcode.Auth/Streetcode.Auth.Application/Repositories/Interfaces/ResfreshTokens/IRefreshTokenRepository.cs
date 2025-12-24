using Streetcode.Auth.Domain.Entities.Auth;
using Streetcode.BuildingBlocks.Repositories.Interfaces.Base;

namespace Streetcode.Auth.Application.Repositories.Interfaces.ResfreshTokens
{
    public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken>
    {
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);

        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken);

        Task<int> BulkRevokeExpiredTokensAsync(DateTime now, CancellationToken cancellationToken);

        Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> BulkDeleteRevokedTokensAsync(CancellationToken cancellationToken = default);
    }
}
