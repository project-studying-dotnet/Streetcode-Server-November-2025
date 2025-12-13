using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.Users;
using Streetcode.BLL.MediatR.Users.Register;

namespace Streetcode.WebApi.Controllers.Users;

public class UsersController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
    {
        return HandleResult(await Mediator.Send(new RegisterUserCommand(user)));
    }
}