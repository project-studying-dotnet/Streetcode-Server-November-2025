using FluentValidation;
using Streetcode.BLL.DTO.Media.Audio;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Media.Audio.Create
{
    /// <summary>
    /// Validator for AudioFileBaseCreateDto.
    /// </summary>
    public class AudioFileBaseCreateDtoValidator : AbstractValidator<AudioFileBaseCreateDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileBaseCreateDtoValidator"/> class.
        /// </summary>
        public AudioFileBaseCreateDtoValidator()
        {
            RuleFor(x => x.BaseFormat)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Дані аудіо в Base64 є обов'язковими")
                .Must(Base64Validator.IsValidBase64)
                .WithMessage("BaseFormat має бути дійсним рядком Base64")
                .Must(base64 => Base64Validator.IsWithinSizeLimit(base64, MediaValidationConstants.MaxAudioSizeInBytes))
                .WithMessage($"Розмір аудіо не може перевищувати {MediaValidationConstants.MaxAudioSizeInBytes / 1024 / 1024}МБ після декодування");

            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage("Розширення аудіо є обов'язковим")
                .Must(ext => FileExtensionValidator.IsValidExtension(ext, FileExtensionValidator.AllowedAudioExtensions))
                .WithMessage($"Розширення аудіо має бути одним з: {string.Join(", ", FileExtensionValidator.AllowedAudioExtensions)}");

            RuleFor(x => x.MimeType)
                .MaximumLength(ValidationConstants.Common.MimeTypeMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.MimeType))
                .WithMessage($"MimeType не може перевищувати {ValidationConstants.Common.MimeTypeMaxLength} символів");

            RuleFor(x => x.Title)
                .MaximumLength(ValidationConstants.Media.TitleMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Title))
                .WithMessage($"Назва не може перевищувати {ValidationConstants.Media.TitleMaxLength} символів");

            RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.Media.DescriptionMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage($"Опис не може перевищувати {ValidationConstants.Media.DescriptionMaxLength} символів");
        }
    }
}
