using System.IdentityModel.Tokens.Jwt;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Interfaces.Users
{
    public interface ITokenService
    {
        public JwtSecurityToken GenerateJWTToken(User user);

        public Task<Result<JwtSecurityToken>> RefreshTokenAsync(string token);

        public Task<string> GenerateRefreshTokenAsync(User user);
    }
}
