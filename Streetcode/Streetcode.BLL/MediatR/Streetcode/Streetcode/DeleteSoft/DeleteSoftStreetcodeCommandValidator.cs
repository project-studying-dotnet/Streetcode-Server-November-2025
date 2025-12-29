using FluentValidation;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.DeleteSoft
{
    /// <summary>
    /// Validator for DeleteSoftStreetcodeCommand.
    /// </summary>
    public class DeleteSoftStreetcodeCommandValidator : AbstractValidator<DeleteSoftStreetcodeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSoftStreetcodeCommandValidator"/> class.
        /// </summary>
        public DeleteSoftStreetcodeCommandValidator()
        {
            RuleFor(x => x.Id)
                .MustBeValidId(ErrorMessages.IdMustBeGreaterThan);
        }
    }
}
