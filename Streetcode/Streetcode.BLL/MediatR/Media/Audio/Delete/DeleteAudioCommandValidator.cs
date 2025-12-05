using FluentValidation;

namespace Streetcode.BLL.MediatR.Media.Audio.Delete
{
    /// <summary>
    /// Validator for DeleteAudioCommand.
    /// </summary>
    public class DeleteAudioCommandValidator : AbstractValidator<DeleteAudioCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAudioCommandValidator"/> class.
        /// </summary>
        public DeleteAudioCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Audio Id must be greater than 0");
        }
    }
}
