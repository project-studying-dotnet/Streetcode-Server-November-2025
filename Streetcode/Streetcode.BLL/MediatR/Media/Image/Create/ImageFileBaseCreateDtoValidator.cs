using FluentValidation;
using Streetcode.BLL.DTO.Media.Images;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Media.Image.Create
{
    /// <summary>
    /// Validator for ImageFileBaseCreateDto.
    /// </summary>
    public class ImageFileBaseCreateDtoValidator : AbstractValidator<ImageFileBaseCreateDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFileBaseCreateDtoValidator"/> class.
        /// </summary>
        public ImageFileBaseCreateDtoValidator()
        {
            RuleFor(x => x.BaseFormat)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Дані зображення в Base64 є обов'язковими")
                .Must(Base64Validator.IsValidBase64)
                .WithMessage("BaseFormat має бути дійсним рядком Base64")
                .Must(base64 => Base64Validator.IsWithinSizeLimit(base64, ValidationConstants.Media.MaxImageSizeInBytes))
                .WithMessage($"Розмір зображення не може перевищувати {ValidationConstants.Media.MaxImageSizeInBytes / 1024 / 1024}МБ після декодування");

            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage("Розширення зображення є обов'язковим")
                .Must(ext => FileExtensionValidator.IsValidExtension(ext, FileExtensionValidator.AllowedImageExtensions))
                .WithMessage($"Розширення зображення має бути одним з: {string.Join(", ", FileExtensionValidator.AllowedImageExtensions)}");

            RuleFor(x => x.MimeType)
                .MaximumLength(ValidationConstants.Common.MimeTypeMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.MimeType))
                .WithMessage($"MimeType не може перевищувати {ValidationConstants.Common.MimeTypeMaxLength} символів");

            RuleFor(x => x.Title)
                .MaximumLength(ValidationConstants.Media.TitleMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Title))
                .WithMessage($"Назва не може перевищувати {ValidationConstants.Media.TitleMaxLength} символів");

            RuleFor(x => x.Alt)
                .MaximumLength(ValidationConstants.Media.AltMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Alt))
                .WithMessage($"Alt текст не може перевищувати {ValidationConstants.Media.AltMaxLength} символів");
        }
    }
}
