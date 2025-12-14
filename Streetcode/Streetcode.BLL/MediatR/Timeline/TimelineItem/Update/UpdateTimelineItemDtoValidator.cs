using FluentValidation;
using Streetcode.BLL.DTO.Timeline;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Update
{
    public class UpdateTimelineItemDtoValidator : AbstractValidator<UpdateTimelineItemDto>
    {
        public UpdateTimelineItemDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(28).WithMessage("Title cannot exceed 28 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(400).WithMessage("Description cannot exceed 400 characters");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required");

            RuleFor(x => x.DateViewPattern)
                .IsInEnum().WithMessage("Invalid DateViewPattern value");

            RuleFor(x => x.StreetcodeId)
                .GreaterThan(0).WithMessage("StreetcodeId must be greater than 0");

            RuleForEach(x => x.HistoricalContextIds)
                .GreaterThan(0).WithMessage("HistoricalContextId must be greater than 0");
        }
    }
}
