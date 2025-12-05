using System;
using FluentValidation;
using Streetcode.BLL.DTO.Team;

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
                .Must(BeAValidUrl)
                .WithMessage("TargetUrl must be a valid URL");

            RuleFor(x => x.TeamMemberId)
                .GreaterThan(0)
                .WithMessage("TeamMemberId must be greater than 0");
        }

        private static bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
