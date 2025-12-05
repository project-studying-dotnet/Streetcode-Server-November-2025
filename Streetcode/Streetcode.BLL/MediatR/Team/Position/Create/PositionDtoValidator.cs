using FluentValidation;
using Streetcode.BLL.DTO.Team;

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
                .MaximumLength(50)
                .WithMessage("Position name must not exceed 50 characters");
        }
    }
}
