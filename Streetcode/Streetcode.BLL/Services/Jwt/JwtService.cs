using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Interfaces.Users;
using Streetcode.DAL.Entities.Jwt;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.Services.Jwt
{
    public class JwtService : ITokenService
    {
        private readonly IRepositoryWrapper _repository;

        public JwtService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public JwtSecurityToken GenerateJWTToken(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateRefreshTokenAsync(User user)
        {
            string tokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var refreshToken = new RefreshToken
            {
                Token = tokenValue,
                UserId = user.Id,

                // TODO gotta add variable in appsettings for this
                ExpiresOn = DateTime.UtcNow.AddDays(14),
                IsRevoked = false,
            };

            await _repository.RefreshTokenRepository.CreateAsync(refreshToken);
            await _repository.SaveChangesAsync();

            return refreshToken.Token;
        }

        public async Task<Result<JwtSecurityToken>> RefreshTokenAsync(string token)
        {
            RefreshToken? refreshToken =
                await _repository.RefreshTokenRepository
                .GetFirstOrDefaultAsync(t => t.Token == token);

            if (refreshToken is null)
            {
                string errorMsg = "refreshToken token doesn't exist";
                return Result.Fail<JwtSecurityToken>(new Error(errorMsg));
            }

            if (refreshToken.IsRevoked || refreshToken.ExpiresOn < DateTime.UtcNow)
            {
                string errorMsg = "refreshToken token expiered";
                refreshToken.IsRevoked = true;
                await _repository.SaveChangesAsync();
                return Result.Fail<JwtSecurityToken>(new Error(errorMsg));
            }

            User user = refreshToken.User;

            refreshToken.IsRevoked = true;

            await _repository.SaveChangesAsync();

            var newAccessToken = GenerateJWTToken(user);

            return Result.Ok(newAccessToken);
        }
    }
}
