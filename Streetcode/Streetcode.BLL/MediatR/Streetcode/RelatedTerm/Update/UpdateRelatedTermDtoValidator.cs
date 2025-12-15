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
                .WithMessage("Слово є обов'язковим")
                .MaximumLength(ValidationConstants.RelatedTerm.WordMaxLength)
                .WithMessage($"Слово не може перевищувати {ValidationConstants.RelatedTerm.WordMaxLength} символів");

            RuleFor(x => x.TermId)
                .GreaterThan(0)
                .WithMessage("ID терміну має бути більше 0");
        }
    }
}
