using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.Auth.Application.Dtos.Auth;
using Streetcode.Auth.Application.Interfaces.Token;
using Streetcode.Auth.Domain.Entities.Users;
using Streetcode.BuildingBlocks.Interfaces.Logging;

namespace Streetcode.Auth.Application.MediatR.Login
{
    public class UserLoginHandler : IRequestHandler<LoginCommand, Result<TokenResponseDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _jwtService;
        private readonly ILoggerService _logger;

        public UserLoginHandler(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService jwtService,
            ILoggerService logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _logger = logger;
        }
        public async Task<Result<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.loginRequestDto.Email);

                if (user is null)
                {
                    _logger.LogError(request, "ErrorMessages.UserEmailOrPasswordInvalid");
                    return Result.Fail("ErrorMessages.UserEmailOrPasswordInvalid");
                }

                var passwordValid = await _signInManager.CheckPasswordSignInAsync(user, request.loginRequestDto.Password, false);
                if (!passwordValid.Succeeded)
                {
                    _logger.LogError(request, "ErrorMessages.UserEmailOrPasswordInvalid");
                    return Result.Fail("ErrorMessages.UserEmailOrPasswordInvalid");
                }

                var result = await _jwtService.GenerateTokensAsync(user, default);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ErrorMessages.LoginFailure");
                return Result.Fail<TokenResponseDto>("ErrorMessages.LoginFailure");
            }
        }
    }
}