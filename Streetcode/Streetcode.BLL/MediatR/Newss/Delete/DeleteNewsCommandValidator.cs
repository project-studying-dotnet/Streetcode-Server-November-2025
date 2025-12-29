using FluentValidation;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Newss.Delete
{
    /// <summary>
    /// Validator for DeleteNewsCommand.
    /// </summary>
    public class DeleteNewsCommandValidator : AbstractValidator<DeleteNewsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteNewsCommandValidator"/> class.
        /// </summary>
        public DeleteNewsCommandValidator()
        {
            RuleFor(x => x.id)
                .MustBeValidId(ErrorMessages.NewsIdMustBeGreaterThanZero);
        }
    }
}
