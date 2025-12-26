using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.Auth.Application.Dtos.Auth;
using Streetcode.Auth.Application.Dtos.Users;
using Streetcode.Auth.Application.MediatR.Login;
using Streetcode.Auth.Application.MediatR.Logout;
using Streetcode.Auth.Application.MediatR.Register;

namespace Streetcode.Auth.Api.Controllers.Users
{
    public class UsersController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
        {
            return HandleResult(await Mediator.Send(new RegisterCommand(user)));
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            return HandleResult(await Mediator.Send(new LoginCommand(request)));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto request)
        {
            return HandleResult(await Mediator.Send(new LogoutCommand(request)));
        }
    }
}
