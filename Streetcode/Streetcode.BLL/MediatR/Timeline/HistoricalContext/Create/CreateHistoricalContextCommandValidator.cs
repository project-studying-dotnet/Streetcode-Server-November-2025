using FluentValidation;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create
{
    public class CreateHistoricalContextCommandValidator : AbstractValidator<CreateHistoricalContextCommand>
    {
        public CreateHistoricalContextCommandValidator()
        {
            RuleFor(x => x.HistoricalContext).NotNull().SetValidator(new CreateHistoricalContextDtoValidator());
        }
    }
}
