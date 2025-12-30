using FluentValidation;
using Streetcode.BLL.DTO.TextContent;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.RelatedTerm.Create
{
    /// <summary>
    /// Validator for RelatedTermDto.
    /// </summary>
    public class RelatedTermDtoValidator : AbstractValidator<RelatedTermDto>
    {
        public RelatedTermDtoValidator()
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
                .WithMessage(ErrorMessages.RelatedTermIdMustBeGreaterThanZero);
        }
    }
}
