using FluentValidation;
using Streetcode.BLL.Util.Validators;

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
                .MustBeValidId("ID аудіо має бути більше 0");
        }
    }
}
