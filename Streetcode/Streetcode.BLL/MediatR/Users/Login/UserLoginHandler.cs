using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.DTO.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Users;
using MediatR;
using Streetcode.BLL.Interfaces.Users;

namespace Streetcode.BLL.MediatR.Users.Login
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, Result<LoginResultDto>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ILoggerService _logger;

        public UserLoginHandler(
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILoggerService logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
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

                var accessToken = _tokenService.GenerateJWTToken(user);
                var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);

                var result = new LoginResultDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    UserId = user.Id,
                    AccessTokenExpiresAt = accessToken.ValidTo,
                    RefreshTokenExpiresAt = DateTime.Now, // TODO: change on right time
                };

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user login"); // TODO: Use constant from resx
                return Result.Fail<LoginResultDto>("An error occurred during login"); // TODO: Use constant from resx
            }
        }
    }
}