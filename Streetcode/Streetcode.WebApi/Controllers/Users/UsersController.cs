using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.Users;
using Streetcode.BLL.MediatR.Users.Login;
using Streetcode.BLL.MediatR.Users.Logout;
using Streetcode.BLL.MediatR.Users.Register;

namespace Streetcode.WebApi.Controllers.Users;

public class UsersController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
    {
        return HandleResult(await Mediator.Send(new RegisterUserCommand(user)));
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginDto user)
    {
        return HandleResult(await Mediator.Send(new UserLoginCommand(user)));
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequestDto token)
	{
		return HandleResult(await Mediator.Send(new LogoutCommand(token)));
	}
}