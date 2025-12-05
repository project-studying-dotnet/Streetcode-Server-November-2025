using FluentValidation;
using Streetcode.BLL.DTO.Partners;

namespace Streetcode.BLL.MediatR.Partners.Create
{
    /// <summary>
    /// Validator for CreatePartnerDto.
    /// </summary>
    public class CreatePartnerDtoValidator : AbstractValidator<CreatePartnerDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePartnerDtoValidator"/> class.
        /// </summary>
        public CreatePartnerDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Partner title is required")
                .MaximumLength(255)
                .WithMessage("Partner title cannot exceed 255 characters");

            RuleFor(x => x.TargetUrl)
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.TargetUrl))
                .WithMessage("TargetUrl must be a valid absolute URL");

            RuleFor(x => x.LogoId)
                .GreaterThan(0)
                .WithMessage("LogoId must be greater than 0");

            RuleFor(x => x.Streetcodes)
                .NotNull()
                .WithMessage("Streetcodes list is required");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.UrlTitle)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.UrlTitle))
                .WithMessage("UrlTitle cannot exceed 255 characters");
        }

        private static bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return true;
            }

            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
