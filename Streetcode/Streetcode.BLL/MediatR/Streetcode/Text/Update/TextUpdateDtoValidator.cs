using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Update
{
    /// <summary>
    /// Validator for TextUpdateDto.
    /// </summary>
    public class TextUpdateDtoValidator : AbstractValidator<TextUpdateDto>
    {
        public TextUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Заголовок тексту є обов'язковим")
                .MaximumLength(ValidationConstants.Text.TitleMaxLength)
                .WithMessage($"Заголовок тексту не може перевищувати {ValidationConstants.Text.TitleMaxLength} символів");

            RuleFor(x => x.TextContent)
                .NotEmpty()
                .WithMessage("Зміст тексту є обов'язковим")
                .MaximumLength(ValidationConstants.Text.ContentMaxLength)
                .WithMessage($"Зміст тексту не може перевищувати {ValidationConstants.Text.ContentMaxLength} символів");

            RuleFor(x => x.AdditionalText)
                .MaximumLength(ValidationConstants.Text.AdditionalTextMaxLength)
                .When(x => !string.IsNullOrEmpty(x.AdditionalText))
                .WithMessage($"Додатковий текст не може перевищувати {ValidationConstants.Text.AdditionalTextMaxLength} символів");

            RuleFor(x => x.VideoUrl)
                .Matches(@"^(https?://)?(www\.)?(youtube\.com/(watch\?v=|embed/|v/)|youtu\.be/)[\w\-]+")
                .When(x => !string.IsNullOrEmpty(x.VideoUrl))
                .WithMessage("Відео повинно бути з YouTube");
        }
    }
}
