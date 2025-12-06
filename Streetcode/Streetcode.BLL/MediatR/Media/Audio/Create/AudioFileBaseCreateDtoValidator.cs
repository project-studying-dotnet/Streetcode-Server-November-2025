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
                .WithMessage("Audio Base64 data is required")
                .Must(Base64Validator.IsValidBase64)
                .WithMessage("BaseFormat must be valid Base64 string")
                .Must(base64 => Base64Validator.IsWithinSizeLimit(base64, MediaValidationConstants.MaxAudioSizeInBytes))
                .WithMessage($"Audio size must not exceed {MediaValidationConstants.MaxAudioSizeInBytes / 1024 / 1024}MB when decoded");

            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage("Audio extension is required")
                .Must(ext => FileExtensionValidator.IsValidExtension(ext, FileExtensionValidator.AllowedAudioExtensions))
                .WithMessage($"Audio extension must be one of: {string.Join(", ", FileExtensionValidator.AllowedAudioExtensions)}");

            RuleFor(x => x.MimeType)
                .MaximumLength(ValidationConstants.Common.MimeTypeMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.MimeType))
                .WithMessage($"MimeType must not exceed {ValidationConstants.Common.MimeTypeMaxLength} characters");

            RuleFor(x => x.Title)
                .MaximumLength(ValidationConstants.Media.TitleMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Title))
                .WithMessage($"Title must not exceed {ValidationConstants.Media.TitleMaxLength} characters");

            RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.Media.DescriptionMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage($"Description must not exceed {ValidationConstants.Media.DescriptionMaxLength} characters");
        }
    }
}
