using Streetcode.Auth.Domain.Entities.Users;
using Streetcode.BuildingBlocks.Repositories.Interfaces.Base;

namespace Streetcode.Auth.Infrastructure.Repositories.Interfaces.Users
{
    public interface IUserRepository : IRepositoryBase<User>
    {
    }
}
