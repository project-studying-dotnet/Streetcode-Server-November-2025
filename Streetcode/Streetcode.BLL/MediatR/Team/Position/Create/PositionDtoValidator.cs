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
                .WithMessage("Назва посади є обов'язковою")
                .MaximumLength(ValidationConstants.Team.NameMaxLength)
                .WithMessage($"Назва посади не може перевищувати {ValidationConstants.Team.NameMaxLength} символів");
        }
    }
}
