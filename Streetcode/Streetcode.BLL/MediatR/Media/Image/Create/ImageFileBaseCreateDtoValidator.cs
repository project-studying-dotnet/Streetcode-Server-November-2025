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
        private const long MaxImageSizeInBytes = 5 * 1024 * 1024; // 5MB

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFileBaseCreateDtoValidator"/> class.
        /// </summary>
        public ImageFileBaseCreateDtoValidator()
        {
            RuleFor(x => x.BaseFormat)
                .NotEmpty()
                .WithMessage("Image Base64 data is required")
                .Must(Base64Validator.IsValidBase64)
                .WithMessage("BaseFormat must be valid Base64 string")
                .Must(base64 => Base64Validator.IsWithinSizeLimit(base64, MaxImageSizeInBytes))
                .WithMessage($"Image size must not exceed {MaxImageSizeInBytes / 1024 / 1024}MB when decoded");

            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage("Image extension is required")
                .Must(ext => FileExtensionValidator.IsValidExtension(ext, FileExtensionValidator.AllowedImageExtensions))
                .WithMessage($"Image extension must be one of: {string.Join(", ", FileExtensionValidator.AllowedImageExtensions)}");

            RuleFor(x => x.MimeType)
                .MaximumLength(10)
                .When(x => !string.IsNullOrWhiteSpace(x.MimeType))
                .WithMessage("MimeType must not exceed 10 characters");

            RuleFor(x => x.Title)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.Title))
                .WithMessage("Title must not exceed 100 characters");

            RuleFor(x => x.Alt)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.Alt))
                .WithMessage("Alt text must not exceed 200 characters");
        }
    }
}
