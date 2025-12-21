using FluentResults;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.DTO.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Users;
using MediatR;
using Streetcode.BLL.Interfaces.Jwt;

namespace Streetcode.BLL.MediatR.Users.Login
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, Result<LoginResultDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly ILoggerService _logger;

        public UserLoginHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwtService jwtService,
            ILoggerService logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<Result<LoginResultDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.userLoginDto.Email);
                if (user is null)
                {
                    return Result.Fail(ErrorMessages.UserEmailOrPasswordInvalid);
                }

                var passwordValid = await _signInManager.CheckPasswordSignInAsync(user, request.userLoginDto.Password, false);
                if (!passwordValid.Succeeded)
                {
                    return Result.Fail(ErrorMessages.UserEmailOrPasswordInvalid);
                }

                var accessToken = await _jwtService.GenerateAccessTokenAsync(user);
                var refreshToken = await _jwtService.GenerateRefreshTokenAsync(user);

                var result = new LoginResultDto
                {
                    UserId = user.Id,
                    AccessToken = accessToken.Token,
                    RefreshToken = refreshToken.Token,
                    AccessTokenExpiresAt = accessToken.ExpiresAt,
                    RefreshTokenExpiresAt = refreshToken.ExpiresAt,
                };

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.LoginFailure);
                return Result.Fail<LoginResultDto>(ErrorMessages.LoginFailure);
            }
        }
    }
}