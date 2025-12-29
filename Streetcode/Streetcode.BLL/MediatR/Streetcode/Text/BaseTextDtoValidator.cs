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
                .WithMessage(ErrorMessages.TextTitleRequired)
                .MaximumLength(ValidationConstants.Text.TitleMaxLength)
                .WithMessage(string.Format(
                    ErrorMessages.TextTitleTooLong,
                    ValidationConstants.Text.TitleMaxLength));

            RuleFor(x => GetTextContent(x))
                .NotEmpty()
                .WithMessage(ErrorMessages.TextContentRequired)
                .MaximumLength(ValidationConstants.Text.ContentMaxLength)
                .WithMessage(string.Format(
                    ErrorMessages.TextContentTooLong,
                    ValidationConstants.Text.ContentMaxLength));

            RuleFor(x => GetAdditionalText(x))
                .MaximumLength(ValidationConstants.Text.AdditionalTextMaxLength)
                .When(x => !string.IsNullOrEmpty(GetAdditionalText(x)))
                .WithMessage(string.Format(
                    ErrorMessages.TextAdditionalTextTooLong,
                    ValidationConstants.Text.AdditionalTextMaxLength));

            RuleFor(x => GetVideoUrl(x))
                .Matches(ValidationConstants.RegexPatterns.YouTubeUrl)
                .When(x => !string.IsNullOrEmpty(GetVideoUrl(x)))
                .WithMessage(ErrorMessages.TextVideoMustBeYouTube);
        }

        protected abstract string GetTitle(T dto);
        protected abstract string GetTextContent(T dto);
        protected abstract string? GetAdditionalText(T dto);
        protected abstract string? GetVideoUrl(T dto);
    }
}
