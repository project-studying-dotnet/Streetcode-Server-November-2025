using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Update
{
    /// <summary>
    /// Validator for RelatedTermDto in update context.
    /// </summary>
    public class UpdateRelatedTermDtoValidator : AbstractValidator<RelatedTermDto>
    {
        public UpdateRelatedTermDtoValidator()
        {
            RuleFor(x => x.Word)
                .NotEmpty()
                .WithMessage(ErrorMessages.RelatedTermWordRequired)
                .MaximumLength(ValidationConstants.RelatedTerm.WordMaxLength)
                .WithMessage(string.Format(
                    ErrorMessages.RelatedTermWordTooLong,
                    ValidationConstants.RelatedTerm.WordMaxLength));

            RuleFor(x => x.TermId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.RelatedTermTermIdMustBeGreaterThanZero);
        }
    }
}
