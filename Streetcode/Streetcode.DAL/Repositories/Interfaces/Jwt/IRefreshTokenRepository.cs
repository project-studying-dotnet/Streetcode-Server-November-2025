using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Streetcode.DAL.Entities.Jwt;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.DAL.Repositories.Interfaces.Jwt
{
    public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken>
    {
    }
}
