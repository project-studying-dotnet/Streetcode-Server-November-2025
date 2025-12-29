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
                .WithMessage(ErrorMessages.TeamMemberLinkLogoTypeRequired);

            RuleFor(x => x.TargetUrl)
                .NotEmpty()
                .WithMessage(ErrorMessages.TeamMemberLinkTargetUrlRequired)
                .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: true))
                .WithMessage(ErrorMessages.TeamMemberLinkTargetUrlInvalid);

            RuleFor(x => x.TeamMemberId)
                .MustBeValidId(ErrorMessages.TeamMemberIdMustBeGreaterThanZero);
        }
    }
}
