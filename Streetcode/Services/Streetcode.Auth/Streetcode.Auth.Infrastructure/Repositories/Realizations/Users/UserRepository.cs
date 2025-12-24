using Streetcode.Auth.Application.Repositories.Interfaces.Users;
using Streetcode.Auth.Infrastructure.Data;
using Streetcode.BuildingBlocks.Repositories.Realizations.Base;
using Streetcode.Auth.Domain.Entities.Users;

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
