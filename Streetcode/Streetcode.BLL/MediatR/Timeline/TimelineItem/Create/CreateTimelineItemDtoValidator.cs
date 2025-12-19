using FluentValidation;
using Streetcode.BLL.DTO.Timeline;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Create
{
    public class CreateTimelineItemDtoValidator : AbstractValidator<CreateTimelineItemDto>
    {
        public CreateTimelineItemDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ErrorMessages.TimelineItemTitleRequired)
                .MaximumLength(28).WithMessage(string.Format(ErrorMessages.TimelineItemTitleTooLong, 28));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ErrorMessages.TimelineItemDescriptionRequired)
                .MaximumLength(400).WithMessage(string.Format(ErrorMessages.TimelineItemDescriptionTooLong, 400));

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(ErrorMessages.TimelineItemDateRequired);

            RuleFor(x => x.DateViewPattern)
                .IsInEnum().WithMessage(ErrorMessages.TimelineItemDateViewPatternInvalid);

            RuleFor(x => x.StreetcodeId)
                .GreaterThan(0).WithMessage(ErrorMessages.TimelineItemStreetcodeIdMustBeGreaterThanZero);

            RuleForEach(x => x.HistoricalContextIds)
                .GreaterThan(0).WithMessage(ErrorMessages.TimelineItemHistoricalContextIdMustBeGreaterThanZero);
        }
    }
}
