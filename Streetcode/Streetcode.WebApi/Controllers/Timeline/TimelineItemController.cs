using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.Timeline;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.Create;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetAll;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetById;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetByStreetcodeId;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.Update;
using Streetcode.DAL.Enums;

namespace Streetcode.WebApi.Controllers.Timeline;

public class TimelineItemController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllTimelineItemsQuery()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetTimelineItemByIdQuery(id)));
    }

    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetTimelineItemsByStreetcodeIdQuery(streetcodeId)));
    }

    [Authorize(Roles = nameof(UserRole.Administrator))]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTimelineItemDto timelineItem)
    {
        return HandleResult(await Mediator.Send(new CreateTimelineItemCommand(timelineItem)));
    }

    [Authorize(Roles = nameof(UserRole.Administrator))]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTimelineItemDto timelineItem)
    {
        return HandleResult(await Mediator.Send(new UpdateTimelineItemCommand(timelineItem)));
    }

    [Authorize(Roles = nameof(UserRole.Administrator))]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new DeleteTimelineItemCommand(id)));
    }
}