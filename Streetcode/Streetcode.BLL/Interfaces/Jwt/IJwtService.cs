using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentResults;
using Streetcode.BLL.DTO.Users;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Interfaces.Jwt
{
    public interface IJwtService
    {
        Task<TokenResultDto> GenerateAccessTokenAsync(User user);
        ClaimsPrincipal? ValidateToken(string token);
        Task<Result<TokenResultDto>> RefreshTokenAsync(string token);
        Task<TokenResultDto> GenerateRefreshTokenAsync(User user);
    }
}
