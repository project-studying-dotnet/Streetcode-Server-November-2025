using FluentValidation;
using Streetcode.BLL.DTO.Team;

namespace Streetcode.BLL.MediatR.Team.Create
{
    /// <summary>
    /// Validator for CreatePositionQuery.
    /// </summary>
    public class CreatePositionQueryValidator : AbstractValidator<CreatePositionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePositionQueryValidator"/> class.
        /// </summary>
        public CreatePositionQueryValidator()
        {
            RuleFor(x => x.position)
                .NotNull()
                .WithMessage(ErrorMessages.PositionNameRequired)
                .SetValidator(new PositionDtoValidator());
        }
    }
}
