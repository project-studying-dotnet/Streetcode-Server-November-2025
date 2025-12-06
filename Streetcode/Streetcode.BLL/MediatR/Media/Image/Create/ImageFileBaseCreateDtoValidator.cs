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
                .WithMessage("Image Base64 data is required")
                .Must(Base64Validator.IsValidBase64)
                .WithMessage("BaseFormat must be valid Base64 string")
                .Must(base64 => Base64Validator.IsWithinSizeLimit(base64, MediaValidationConstants.MaxImageSizeInBytes))
                .WithMessage($"Image size must not exceed {MediaValidationConstants.MaxImageSizeInBytes / 1024 / 1024}MB when decoded");

            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage("Image extension is required")
                .Must(ext => FileExtensionValidator.IsValidExtension(ext, FileExtensionValidator.AllowedImageExtensions))
                .WithMessage($"Image extension must be one of: {string.Join(", ", FileExtensionValidator.AllowedImageExtensions)}");

            RuleFor(x => x.MimeType)
                .MaximumLength(ValidationConstants.Common.MimeTypeMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.MimeType))
                .WithMessage($"MimeType must not exceed {ValidationConstants.Common.MimeTypeMaxLength} characters");

            RuleFor(x => x.Title)
                .MaximumLength(ValidationConstants.Media.TitleMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Title))
                .WithMessage($"Title must not exceed {ValidationConstants.Media.TitleMaxLength} characters");

            RuleFor(x => x.Alt)
                .MaximumLength(ValidationConstants.Media.AltMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Alt))
                .WithMessage($"Alt text must not exceed {ValidationConstants.Media.AltMaxLength} characters");
        }
    }
}
