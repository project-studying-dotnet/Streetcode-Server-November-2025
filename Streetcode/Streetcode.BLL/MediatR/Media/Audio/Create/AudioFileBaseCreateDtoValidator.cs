using System;
using System.Linq;
using FluentValidation;
using Streetcode.BLL.DTO.Media.Audio;

namespace Streetcode.BLL.MediatR.Media.Audio.Create
{
    /// <summary>
    /// Validator for AudioFileBaseCreateDto.
    /// </summary>
    public class AudioFileBaseCreateDtoValidator : AbstractValidator<AudioFileBaseCreateDto>
    {
        private static readonly string[] AllowedExtensions = { "mp3", "wav", "ogg", "m4a" };

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFileBaseCreateDtoValidator"/> class.
        /// </summary>
        public AudioFileBaseCreateDtoValidator()
        {
            RuleFor(x => x.BaseFormat)
                .NotEmpty()
                .WithMessage("Audio Base64 data is required")
                .Must(BeValidBase64)
                .WithMessage("BaseFormat must be valid Base64 string");

            RuleFor(x => x.Extension)
                .NotEmpty()
                .WithMessage("Audio extension is required")
                .Must(ext => AllowedExtensions.Contains(ext?.ToLower()))
                .WithMessage($"Audio extension must be one of: {string.Join(", ", AllowedExtensions)}");

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
