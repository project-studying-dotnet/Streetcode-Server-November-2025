using FluentValidation;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Delete
{
    /// <summary>
    /// Validator for DeleteRelatedTermCommand.
    /// </summary>
    public class DeleteRelatedTermCommandValidator : AbstractValidator<DeleteRelatedTermCommand>
    {
        public DeleteRelatedTermCommandValidator()
        {
            RuleFor(x => x.word)
                .NotEmpty()
                .WithMessage("Слово для видалення є обов'язковим")
                .MaximumLength(ValidationConstants.RelatedTerm.WordMaxLength)
                .WithMessage($"Слово не може перевищувати {ValidationConstants.RelatedTerm.WordMaxLength} символів");
        }
    }
}
