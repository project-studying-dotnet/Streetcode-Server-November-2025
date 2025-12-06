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
        private const long MaxAudioSizeInBytes = 10 * 1024 * 1024; // 10MB

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileBaseCreateDtoValidator"/> class.
        /// </summary>
        public AudioFileBaseCreateDtoValidator()
        {
            RuleFor(x => x.BaseFormat)
                .NotEmpty()
                .WithMessage("Audio Base64 data is required")
                .Must(Base64Validator.IsValidBase64)
                .WithMessage("BaseFormat must be valid Base64 string")
                .Must(base64 => Base64Validator.IsWithinSizeLimit(base64, MaxAudioSizeInBytes))
                .WithMessage($"Audio size must not exceed {MaxAudioSizeInBytes / 1024 / 1024}MB when decoded");

            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage("Audio extension is required")
                .Must(ext => FileExtensionValidator.IsValidExtension(ext, FileExtensionValidator.AllowedAudioExtensions))
                .WithMessage($"Audio extension must be one of: {string.Join(", ", FileExtensionValidator.AllowedAudioExtensions)}");

            RuleFor(x => x.MimeType)
                .MaximumLength(10)
                .When(x => !string.IsNullOrWhiteSpace(x.MimeType))
                .WithMessage("MimeType must not exceed 10 characters");

            RuleFor(x => x.Title)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.Title))
                .WithMessage("Title must not exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage("Description must not exceed 500 characters");
        }
    }
}
