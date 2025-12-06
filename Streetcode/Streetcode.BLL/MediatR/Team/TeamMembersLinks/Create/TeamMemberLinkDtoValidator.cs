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
                .WithMessage("LogoType є обов'язковим");

            RuleFor(x => x.TargetUrl)
                .NotEmpty()
                .WithMessage("TargetUrl є обов'язковим")
                .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: true))
                .WithMessage("TargetUrl має бути дійсною URL-адресою");

            RuleFor(x => x.TeamMemberId)
                .GreaterThan(ValidationConstants.Common.MinId - 1)
                .WithMessage("TeamMemberId має бути більше 0");
        }
    }
}
