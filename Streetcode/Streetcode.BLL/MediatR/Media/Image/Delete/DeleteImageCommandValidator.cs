using FluentValidation;

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
                .GreaterThan(0)
                .WithMessage("Image Id must be greater than 0");
        }
    }
}
