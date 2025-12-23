using Streetcode.Auth.Domain.Entities.Auth;
using Streetcode.Auth.Infrastructure.Data;
using Streetcode.Auth.Infrastructure.Repositories.Interfaces.ResfreshTokens;
using Streetcode.BuildingBlocks.Repositories.Realizations.Base;

namespace Streetcode.Auth.Infrastructure.Repositories.Realizations.RefreshTokens
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(UsersDbContext context)
            : base(context)
        {
        }
    }
}
