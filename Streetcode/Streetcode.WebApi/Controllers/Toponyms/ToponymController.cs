using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.Toponyms;
using Streetcode.BLL.MediatR.Toponyms.Create;
using Streetcode.BLL.MediatR.Toponyms.Delete;
using Streetcode.BLL.MediatR.Toponyms.GetAll;
using Streetcode.BLL.MediatR.Toponyms.GetById;
using Streetcode.BLL.MediatR.Toponyms.GetByStreetcodeId;
using Streetcode.BLL.MediatR.Toponyms.Merge;
using Streetcode.DAL.Enums;

namespace Streetcode.WebApi.Controllers.Toponyms;

public class ToponymController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllToponymsRequestDto request)
    {
        return HandleResult(await Mediator.Send(new GetAllToponymsQuery(request)));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetToponymByIdQuery(id)));
    }

    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetToponymsByStreetcodeIdQuery(streetcodeId)));
    }

    [HttpPost("streetcode-toponym")]
    [Authorize(Roles = nameof(UserRole.Administrator))]
    public async Task<IActionResult> CreateStreetcodeToponym([FromBody] StreetcodeToponymDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateStreetcodeToponymCommand(dto)));
    }

    [HttpDelete("streetcode-toponym/{streetcodeId:int}/{toponymId:int}")]
    [Authorize(Roles = nameof(UserRole.Administrator))]
    public async Task<IActionResult> DeleteStreetcodeToponym(
        [FromRoute] int streetcodeId,
        [FromRoute] int toponymId)
    {
        return HandleResult(await Mediator.Send(new DeleteStreetcodeToponymCommand(streetcodeId, toponymId)));
    }

    [HttpPost("merge")]
    [Authorize(Roles = nameof(UserRole.Administrator))]
    public async Task<IActionResult> MergeToponyms([FromBody] MergeToponymsDto dto)
    {
        return HandleResult(await Mediator.Send(new MergeToponymsCommand(dto)));
    }
}