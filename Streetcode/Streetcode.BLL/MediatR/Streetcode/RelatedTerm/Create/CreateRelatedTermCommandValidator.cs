using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create
{
    /// <summary>
    /// Validator for CreateRelatedTermCommand.
    /// </summary>
    public class CreateRelatedTermCommandValidator : AbstractValidator<CreateRelatedTermCommand>
    {
        public CreateRelatedTermCommandValidator()
        {
            RuleFor(x => x.RelatedTerm)
                .NotNull()
                .WithMessage("Дані пов'язаного терміну не можуть бути порожніми")
                .SetValidator(new RelatedTermDtoValidator());
        }
    }
}
