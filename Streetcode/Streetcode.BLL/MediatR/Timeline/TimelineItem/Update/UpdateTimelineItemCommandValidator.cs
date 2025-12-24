using FluentValidation;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Update
{
    public class UpdateTimelineItemCommandValidator : AbstractValidator<UpdateTimelineItemCommand>
    {
        public UpdateTimelineItemCommandValidator()
        {
            RuleFor(x => x.TimelineItem).NotNull().SetValidator(new UpdateTimelineItemDtoValidator());
        }
    }
}
