using FluentValidation;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Delete
{
    public class DeleteHistoricalContextCommandValidator : AbstractValidator<DeleteHistoricalContextCommand>
    {
        public DeleteHistoricalContextCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
