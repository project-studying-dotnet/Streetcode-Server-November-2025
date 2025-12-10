using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create
{
    /// <summary>
    /// Validator for CreateFactCommand.
    /// </summary>
    public class CreateFactCommandValidator : AbstractValidator<CreateFactCommand>
    {
        public CreateFactCommandValidator()
        {
            RuleFor(x => x.newFact)
                .NotNull()
                .WithMessage("Дані факту не можуть бути порожніми")
                .SetValidator(new CreateFactDtoValidator());
        }
    }
}
