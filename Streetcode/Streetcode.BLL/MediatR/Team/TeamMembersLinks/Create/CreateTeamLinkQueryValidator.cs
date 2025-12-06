using FluentValidation;
using Streetcode.BLL.DTO.Team;

namespace Streetcode.BLL.MediatR.Team.TeamMembersLinks.Create
{
    /// <summary>
    /// Validator for CreateTeamLinkQuery.
    /// </summary>
    public class CreateTeamLinkQueryValidator : AbstractValidator<CreateTeamLinkQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTeamLinkQueryValidator"/> class.
        /// </summary>
        public CreateTeamLinkQueryValidator()
        {
            RuleFor(x => x.teamMember)
                .NotNull()
                .WithMessage("Посилання на члена команди є обов'язковим")
                .SetValidator(new TeamMemberLinkDtoValidator());
        }
    }
}
