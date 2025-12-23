using Streetcode.Auth.Infrastructure.Data;
using Streetcode.BuildingBlocks.Repositories.Realizations.Base;
using Streetcode.Auth.Domain.Entities.Users;
using Streetcode.Auth.Infrastructure.Repositories.Interfaces.Users;

namespace Streetcode.Auth.Infrastructure.Repositories.Realizations.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(UsersDbContext context)
            : base(context)
        {
        }
    }
}
