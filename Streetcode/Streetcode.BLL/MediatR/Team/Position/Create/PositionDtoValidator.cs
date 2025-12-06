using FluentValidation;
using Streetcode.BLL.DTO.Team;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Team.Create
{
    /// <summary>
    /// Validator for PositionDto.
    /// </summary>
    public class PositionDtoValidator : AbstractValidator<PositionDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PositionDtoValidator"/> class.
        /// </summary>
        public PositionDtoValidator()
        {
            RuleFor(x => x.Position)
                .NotEmpty()
                .WithMessage("Position name is required")
                .MaximumLength(ValidationConstants.Team.NameMaxLength)
                .WithMessage($"Position name must not exceed {ValidationConstants.Team.NameMaxLength} characters");
        }
    }
}
