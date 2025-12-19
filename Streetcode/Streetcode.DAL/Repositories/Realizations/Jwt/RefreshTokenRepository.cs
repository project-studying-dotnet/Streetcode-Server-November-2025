using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streetcode.DAL.Entities.Jwt;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.Jwt;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.Jwt
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(StreetcodeDbContext context)
            : base(context)
        {
        }
    }
}
