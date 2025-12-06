using FluentValidation;
using Streetcode.BLL.DTO.Team;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Team.TeamMembersLinks.Create
{
    /// <summary>
    /// Validator for TeamMemberLinkDto.
    /// </summary>
    public class TeamMemberLinkDtoValidator : AbstractValidator<TeamMemberLinkDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberLinkDtoValidator"/> class.
        /// </summary>
        public TeamMemberLinkDtoValidator()
        {
            RuleFor(x => x.LogoType)
                .NotNull()
                .WithMessage("LogoType is required");

            RuleFor(x => x.TargetUrl)
                .NotEmpty()
                .WithMessage("TargetUrl is required")
                .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: true))
                .WithMessage("TargetUrl must be a valid URL");

            RuleFor(x => x.TeamMemberId)
                .GreaterThan(ValidationConstants.Common.MinId - 1)
                .WithMessage("TeamMemberId must be greater than 0");
        }
    }
}
