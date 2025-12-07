using FluentValidation;
using Streetcode.BLL.DTO.AdditionalContent.Tag;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
    /// <summary>
    /// Validator for CreateTagDto.
    /// </summary>
    public class CreateTagDtoValidator : AbstractValidator<CreateTagDto>
    {
        public CreateTagDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Назва тегу є обов'язковою")
                .MaximumLength(ValidationConstants.Tag.TitleMaxLength)
                .WithMessage($"Назва тегу не може перевищувати {ValidationConstants.Tag.TitleMaxLength} символів")
                .Matches(@"^[а-яА-ЯіІїЇєЄґҐa-zA-Z0-9\s\-]+$")
                .WithMessage("Назва тегу може містити лише літери, цифри, пробіли та дефіси");
        }
    }
}
