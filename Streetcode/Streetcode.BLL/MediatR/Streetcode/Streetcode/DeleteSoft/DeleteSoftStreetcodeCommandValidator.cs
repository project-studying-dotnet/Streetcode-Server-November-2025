using FluentValidation;

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
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0");
        }
    }
}
