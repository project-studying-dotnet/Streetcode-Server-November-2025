using FluentValidation;

namespace Streetcode.BLL.MediatR.Term.Create;

    /// <summary>
    /// Validator for CreateTermCommand.
    /// </summary>
    public class CreateTermCommandValidator : AbstractValidator<CreateTermCommand>
    {
        public CreateTermCommandValidator()
        {
            RuleFor(x => x.Term)
                .NotNull()
                .WithMessage("Дані терміну не можуть бути порожніми")
                .SetValidator(new CreateTermDtoValidator());
        }
    }