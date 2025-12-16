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
                .WithMessage(ErrorMessages.ImageBase64Required)
                .Must(Base64Validator.IsValidBase64)
                .WithMessage(ErrorMessages.ImageBase64Invalid)
                .Must(base64 => Base64Validator.IsWithinSizeLimit(base64, ValidationConstants.Media.MaxImageSizeInBytes))
                .WithMessage(string.Format(
                    ErrorMessages.ImageSizeExceeded,
                    ValidationConstants.Media.MaxImageSizeInBytes / 1024 / 1024));

            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage(ErrorMessages.ImageExtensionRequired)
                .Must(ext => FileExtensionValidator.IsValidExtension(ext, FileExtensionValidator.AllowedImageExtensions))
                .WithMessage(string.Format(
                    ErrorMessages.ImageExtensionInvalid,
                    string.Join(", ", FileExtensionValidator.AllowedImageExtensions)));

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

            RuleFor(x => x.Alt)
                .MaximumLength(ValidationConstants.Media.AltMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Alt))
                .WithMessage(string.Format(
                    ErrorMessages.AltTextTooLong,
                    ValidationConstants.Media.AltMaxLength));
        }
    }
}
