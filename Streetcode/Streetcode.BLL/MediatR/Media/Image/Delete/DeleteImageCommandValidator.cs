using FluentValidation;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Media.Image.Delete
{
    /// <summary>
    /// Validator for DeleteImageCommand.
    /// </summary>
    public class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteImageCommandValidator"/> class.
        /// </summary>
        public DeleteImageCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(ValidationConstants.Common.MinId - 1)
                .WithMessage("Id зображення має бути більше 0");
        }
    }
}
