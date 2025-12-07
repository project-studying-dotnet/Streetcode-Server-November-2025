using FluentValidation;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Text
{
    /// <summary>
    /// Base validator containing shared validation rules for Text DTOs.
    /// </summary>
    public abstract class BaseTextDtoValidator<T> : AbstractValidator<T>
    {
        protected void ConfigureSharedRules()
        {
            RuleFor(x => GetTitle(x))
                .NotEmpty()
                .WithMessage("Заголовок тексту є обов'язковим")
                .MaximumLength(ValidationConstants.Text.TitleMaxLength)
                .WithMessage($"Заголовок тексту не може перевищувати {ValidationConstants.Text.TitleMaxLength} символів");

            RuleFor(x => GetTextContent(x))
                .NotEmpty()
                .WithMessage("Зміст тексту є обов'язковим")
                .MaximumLength(ValidationConstants.Text.ContentMaxLength)
                .WithMessage($"Зміст тексту не може перевищувати {ValidationConstants.Text.ContentMaxLength} символів");

            RuleFor(x => GetAdditionalText(x))
                .MaximumLength(ValidationConstants.Text.AdditionalTextMaxLength)
                .When(x => !string.IsNullOrEmpty(GetAdditionalText(x)))
                .WithMessage($"Додатковий текст не може перевищувати {ValidationConstants.Text.AdditionalTextMaxLength} символів");

            RuleFor(x => GetVideoUrl(x))
                .Matches(ValidationConstants.RegexPatterns.YouTubeUrl)
                .When(x => !string.IsNullOrEmpty(GetVideoUrl(x)))
                .WithMessage("Відео повинно бути з YouTube");
        }

        protected abstract string GetTitle(T dto);
        protected abstract string GetTextContent(T dto);
        protected abstract string? GetAdditionalText(T dto);
        protected abstract string? GetVideoUrl(T dto);
    }
}
