using Microsoft.AspNetCore.Mvc;
using Streetcode.Auth.Application.Dtos.Auth;
using Streetcode.Auth.Application.Dtos.Users;

namespace Streetcode.Auth.Api.Controllers.Users
{
    public class UsersController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
        {
            return HandleResult(await Mediator.Send(new RegisterUserCommand(user)));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            return HandleResult(await Mediator.Send(new UserLoginCommand(request)));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto request)
        {
            return HandleResult(await Mediator.Send(new LogoutCommand(request)));
        }
    }
}
