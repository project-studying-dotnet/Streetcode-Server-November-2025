using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.DTO.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Users;
using MediatR;

namespace Streetcode.BLL.MediatR.Users.Login
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, Result<LoginResultDto>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
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
                    return Result.Fail("Invalid email or password");
                }

                var passwordValid = await _signInManager.CheckPasswordSignInAsync(user, request.userLoginDto.Password, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user login");
            }
        }
    }
}