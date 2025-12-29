using FluentValidation;
using Streetcode.BLL.DTO.TextContent;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Term;

public class BaseTermDtoValidator : AbstractValidator<TermDto>
{
    protected void ConfigureSharedRules()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(ErrorMessages.RelatedTermWordRequired)
            .MaximumLength(ValidationConstants.Term.TitleMaxLength)
            .WithMessage(string.Format(
                ErrorMessages.RelatedTermWordTooLong,
                ValidationConstants.Term.TitleMaxLength));

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Назва опису є обов'язковою")
            .MaximumLength(ValidationConstants.Term.DescriptionMaxLength)
            .WithMessage($"Назва опису не може перевищувати {ValidationConstants.Term.DescriptionMaxLength} символів")
            .Matches(@"^[а-яА-ЯіІїЇєЄґҐa-zA-Z0-9\s\-]+$")
            .WithMessage("Опис може містити лише літери, цифри, пробіли та дефіси");
    }
}