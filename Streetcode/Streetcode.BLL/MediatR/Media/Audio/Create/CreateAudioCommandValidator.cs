using System;
using FluentValidation;
using Streetcode.BLL.DTO.Media.Audio;

namespace Streetcode.BLL.MediatR.Media.Audio.Create
{
    /// <summary>
    /// Validator for CreateAudioCommand.
    /// </summary>
    public class CreateAudioCommandValidator : AbstractValidator<CreateAudioCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAudioCommandValidator"/> class.
        /// </summary>
        public CreateAudioCommandValidator()
        {
            RuleFor(x => x.Audio)
                .NotNull()
                .WithMessage("Audio data is required")
                .SetValidator(new AudioFileBaseCreateDtoValidator());
        }
    }
}
