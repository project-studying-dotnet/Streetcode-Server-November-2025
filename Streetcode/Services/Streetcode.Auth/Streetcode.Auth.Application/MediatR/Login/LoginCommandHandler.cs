using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.Auth.Application.Dtos.Auth;
using Streetcode.Auth.Domain.Entities.Users;
using Streetcode.BuildingBlocks.Interfaces.Logging;

namespace Streetcode.Auth.Application.MediatR.Login
{
    public class UserLoginHandler : IRequestHandler<LoginCommand, Result<TokenResponseDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
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
        public Task<Result<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}