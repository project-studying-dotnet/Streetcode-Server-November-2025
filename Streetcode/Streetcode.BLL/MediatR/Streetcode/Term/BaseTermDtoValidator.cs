using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Term;

public class BaseTermDtoValidator : AbstractValidator<TermDto>
{
    protected void ConfigureSharedRules()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Назва терміну є обов'язковою")
            .MaximumLength(ValidationConstants.Term.TitleMaxLength)
            .WithMessage($"Назва терміну не може перевищувати {ValidationConstants.Term.TitleMaxLength} символів")
            .Matches(@"^[а-яА-ЯіІїЇєЄґҐa-zA-Z0-9\s\-]+$")
            .WithMessage("Назва терміну може містити лише літери, цифри, пробіли та дефіси");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Назва опису є обов'язковою")
            .MaximumLength(ValidationConstants.Term.DescriptionMaxLength)
            .WithMessage($"Назва опису не може перевищувати {ValidationConstants.Term.DescriptionMaxLength} символів")
            .Matches(@"^[а-яА-ЯіІїЇєЄґҐa-zA-Z0-9\s\-]+$")
            .WithMessage("Назва опису може містити лише літери, цифри, пробіли та дефіси");
        RuleForEach(x => x.RelatedTerms)
            .GreaterThan(0)
            .WithMessage("ID пов'язаного терміну має бути більше 0");
    }
}