using FluentValidation;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.RelatedTerm.Delete
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
                .WithMessage(string.Format(ErrorMessages.RelatedTermWordTooLong, ValidationConstants.RelatedTerm.WordMaxLength));

            RuleFor(x => x.termId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.RelatedTermIdMustBeGreaterThanZero);
        }
    }
}
