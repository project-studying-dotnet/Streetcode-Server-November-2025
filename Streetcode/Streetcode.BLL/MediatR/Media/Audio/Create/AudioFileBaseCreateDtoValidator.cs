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
                .WithMessage(ErrorMessages.AudioBase64Required)
                .Must(Base64Validator.IsValidBase64)
                .WithMessage(ErrorMessages.AudioBase64Invalid)
                .Must(base64 => Base64Validator.IsWithinSizeLimit(base64, ValidationConstants.Media.MaxAudioSizeInBytes))
                .WithMessage(string.Format(
                    ErrorMessages.AudioSizeExceeded,
                    ValidationConstants.Media.MaxAudioSizeInBytes / 1024 / 1024));

            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage(ErrorMessages.AudioExtensionRequired)
                .Must(ext => FileExtensionValidator.IsValidExtension(ext, FileExtensionValidator.AllowedAudioExtensions))
                .WithMessage(string.Format(
                    ErrorMessages.AudioExtensionInvalid,
                    string.Join(", ", FileExtensionValidator.AllowedAudioExtensions)));

            RuleFor(x => x.MimeType)
                .MaximumLength(ValidationConstants.Common.MimeTypeMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.MimeType))
                .WithMessage(string.Format(
                    ErrorMessages.MimeTypeTooLong,
                    ValidationConstants.Common.MimeTypeMaxLength));

            RuleFor(x => x.Title)
                .MaximumLength(ValidationConstants.Media.TitleMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Title))
                .WithMessage(string.Format(
                    ErrorMessages.TitleTooLong,
                    ValidationConstants.Media.TitleMaxLength));

            RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.Media.DescriptionMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage(string.Format(
                    ErrorMessages.DescriptionTooLong,
                    ValidationConstants.Media.DescriptionMaxLength));
        }
    }
}
