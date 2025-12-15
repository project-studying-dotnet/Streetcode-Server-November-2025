using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create
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
                .WithMessage("Слово є обов'язковим")
                .MaximumLength(ValidationConstants.RelatedTerm.WordMaxLength)
                .WithMessage($"Слово не може перевищувати {ValidationConstants.RelatedTerm.WordMaxLength} символів");

            RuleFor(x => x.TermId)
                .GreaterThan(0)
                .WithMessage("ID терміну має бути більше 0");
        }
    }
}
