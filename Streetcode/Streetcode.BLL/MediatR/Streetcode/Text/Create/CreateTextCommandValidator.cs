using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Create
{
    /// <summary>
    /// Validator for CreateTextCommand.
    /// </summary>
    public class CreateTextCommandValidator : AbstractValidator<CreateTextCommand>
    {
        public CreateTextCommandValidator()
        {
            RuleFor(x => x.Text)
                .NotNull()
                .WithMessage("Дані тексту не можуть бути порожніми")
                .SetValidator(new TextCreateDtoValidator());
        }
    }
}
