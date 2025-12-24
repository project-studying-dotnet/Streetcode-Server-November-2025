using Microsoft.EntityFrameworkCore;
using Streetcode.Auth.Application.Repositories.Interfaces.ResfreshTokens;
using Streetcode.Auth.Domain.Entities.Auth;
using Streetcode.Auth.Infrastructure.Data;
using Streetcode.BuildingBlocks.Repositories.Realizations.Base;

namespace Streetcode.Auth.Infrastructure.Repositories.Realizations.RefreshTokens
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        private readonly UsersDbContext _dbContext;

        public RefreshTokenRepository(UsersDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken)
        {
            return await _dbContext.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        }

        public async Task<int> BulkRevokeExpiredTokensAsync(DateTime now, CancellationToken cancellationToken)
        {
            return await _dbContext.RefreshTokens
                .Where(rt => !rt.IsRevoked && rt.ExpiresOn < now)
                .ExecuteUpdateAsync(updates => updates
                        .SetProperty(rt => rt.IsRevoked, rt => true),
                    cancellationToken);
        }

        public Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            _dbContext.RefreshTokens.Update(refreshToken);

            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> BulkDeleteRevokedTokensAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshTokens
                .Where(rt => rt.IsRevoked)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
