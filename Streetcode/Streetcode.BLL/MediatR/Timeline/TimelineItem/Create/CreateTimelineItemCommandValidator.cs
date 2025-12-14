using FluentValidation;
using Streetcode.BLL.DTO.Timeline;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Create
{
    public class CreateTimelineItemCommandValidator : AbstractValidator<CreateTimelineItemCommand>
    {
        public CreateTimelineItemCommandValidator()
        {
            RuleFor(x => x.TimelineItem).NotNull().SetValidator(new CreateTimelineItemDtoValidator());
        }
    }
}
