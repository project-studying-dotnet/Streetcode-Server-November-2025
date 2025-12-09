using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.Users;
using Streetcode.BLL.MediatR.Users.Login;

namespace Streetcode.WebApi.Controllers.Users
{
    public class UserController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            return HandleResult(await Mediator.Send(new UserLoginCommand(user)));
        }
    }
}