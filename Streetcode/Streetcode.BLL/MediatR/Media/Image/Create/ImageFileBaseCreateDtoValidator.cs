using System;
using System.Linq;
using FluentValidation;
using Streetcode.BLL.DTO.Media.Images;

namespace Streetcode.BLL.MediatR.Media.Image.Create
{
    /// <summary>
    /// Validator for ImageFileBaseCreateDto.
    /// </summary>
    public class ImageFileBaseCreateDtoValidator : AbstractValidator<ImageFileBaseCreateDto>
    {
        private static readonly string[] AllowedExtensions = { "png", "jpg", "jpeg", "webp" };

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageFileBaseCreateDtoValidator"/> class.
        /// </summary>
        public ImageFileBaseCreateDtoValidator()
        {
            RuleFor(x => x.BaseFormat)
                .NotEmpty()
                .WithMessage("Image Base64 data is required")
                .Must(BeValidBase64)
                .WithMessage("BaseFormat must be valid Base64 string");

            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage("Image extension is required")
                .Must(ext => AllowedExtensions.Contains(ext?.ToLower()))
                .WithMessage($"Image extension must be one of: {string.Join(", ", AllowedExtensions)}");

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

        private static bool BeValidBase64(string? base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
