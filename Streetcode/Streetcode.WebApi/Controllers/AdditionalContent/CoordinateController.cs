using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.DTO.AdditionalContent.Subtitles;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.GetByStreetcodeId;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update;

namespace Streetcode.WebApi.Controllers.AdditionalContent;

public class CoordinateController : BaseApiController
{
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetCoordinatesByStreetcodeIdQuery(streetcodeId)));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] StreetcodeCoordinateDto dto)
    {
        return HandleResult(await Mediator.Send(new CreateCoordinateCommand(dto)));
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] StreetcodeCoordinateDto dto)
    {
        return HandleResult(await Mediator.Send(new UpdateCoordinateCommand(dto)));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new DeleteCoordinateCommand(id)));
    }
}