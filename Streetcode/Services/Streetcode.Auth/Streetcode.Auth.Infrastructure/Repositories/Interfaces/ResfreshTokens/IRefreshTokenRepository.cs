using Streetcode.Auth.Domain.Entities.Auth;
using Streetcode.BuildingBlocks.Repositories.Interfaces.Base;

namespace Streetcode.Auth.Infrastructure.Repositories.Interfaces.ResfreshTokens
{
    public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken>
    {
    }
}
