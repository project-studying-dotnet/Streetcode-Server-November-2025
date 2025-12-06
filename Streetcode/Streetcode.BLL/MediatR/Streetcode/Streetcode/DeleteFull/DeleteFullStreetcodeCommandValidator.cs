using FluentValidation;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.DeleteFull
{
    /// <summary>
    /// Validator for DeleteFullStreetcodeCommand.
    /// </summary>
    public class DeleteFullStreetcodeCommandValidator : AbstractValidator<DeleteFullStreetcodeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFullStreetcodeCommandValidator"/> class.
        /// </summary>
        public DeleteFullStreetcodeCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(ValidationConstants.Common.MinId - 1)
                .WithMessage("Id must be greater than 0");
        }
    }
}
