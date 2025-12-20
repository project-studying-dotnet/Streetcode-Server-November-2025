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
                .WithMessage(ErrorMessages.RelatedTermWordForDeletionRequired)
                .MaximumLength(ValidationConstants.RelatedTerm.WordMaxLength)
                .WithMessage($"Слово не може перевищувати {ValidationConstants.RelatedTerm.WordMaxLength} символів");

            RuleFor(x => x.termId)
                .GreaterThan(0)
                .WithMessage("Ідентифікатор терміну повинен бути більше 0")
                .WithMessage(string.Format(
                    ErrorMessages.RelatedTermWordTooLong,
                    ValidationConstants.RelatedTerm.WordMaxLength));
        }
    }
}
