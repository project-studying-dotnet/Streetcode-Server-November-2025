using FluentValidation;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete
{
    public class DeleteTimelineItemCommandValidator : AbstractValidator<DeleteTimelineItemCommand>
    {
        public DeleteTimelineItemCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
