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
    public async Task<IActionResult> Create(
        [FromBody] StreetcodeCoordinateDto streetcodeCoordinate)
    {
        return HandleResult(await Mediator.Send(new CreateCoordinateCommand(streetcodeCoordinate)));
    }

    [HttpPut("{coordinateId:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int coordinateId,
        [FromBody] StreetcodeCoordinateDto dto)
    {
        dto.Id = coordinateId;

        return HandleResult(await Mediator.Send(new UpdateCoordinateCommand(dto)));
    }

    [HttpDelete("{coordinateId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int coordinateId)
    {
        return HandleResult(await Mediator.Send(new DeleteCoordinateCommand(coordinateId)));
    }
}