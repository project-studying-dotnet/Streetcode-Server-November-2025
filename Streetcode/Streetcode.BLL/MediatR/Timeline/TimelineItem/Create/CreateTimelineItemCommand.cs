using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Timeline;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Create
{
    public record CreateTimelineItemCommand(CreateTimelineItemDto TimelineItem) : IRequest<Result<TimelineItemDto>>;
}
