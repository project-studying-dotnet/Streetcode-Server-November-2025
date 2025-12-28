using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.Team;
using Streetcode.BLL.MediatR.Team.Create;
using Streetcode.BLL.MediatR.Team.Position.GetAll;
using Streetcode.DAL.Enums;

namespace Streetcode.WebApi.Controllers.Team
{
    public class PositionController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllPositionsQuery()));
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> Create([FromBody] PositionDto position)
        {
            return HandleResult(await Mediator.Send(new CreatePositionQuery(position)));
        }
    }
}
