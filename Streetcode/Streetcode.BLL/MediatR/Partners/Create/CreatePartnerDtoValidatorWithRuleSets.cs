using FluentValidation;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Partners.Create
{
    /// <summary>
    /// Validator for CreatePartnerDto with organized RuleSets.
    /// </summary>
    public class CreatePartnerDtoValidatorWithRuleSets : AbstractValidator<CreatePartnerDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePartnerDtoValidatorWithRuleSets"/> class.
        /// </summary>
        public CreatePartnerDtoValidatorWithRuleSets()
        {
            // Required fields validation
            RuleSet("RequiredFields", () =>
            {
                RuleFor(x => x.Title)
                    .NotEmpty()
                    .WithMessage("Partner title is required");

                RuleFor(x => x.LogoId)
                    .GreaterThan(ValidationConstants.Common.MinId - 1)
                    .WithMessage("LogoId must be greater than 0");

                RuleFor(x => x.Streetcodes)
                    .NotNull()
                    .WithMessage("Streetcodes list is required");
            });

            // Length constraints validation
            RuleSet("LengthConstraints", () =>
            {
                RuleFor(x => x.Title)
                    .MaximumLength(ValidationConstants.Partner.TitleMaxLength)
                    .WithMessage($"Partner title cannot exceed {ValidationConstants.Partner.TitleMaxLength} characters");

                RuleFor(x => x.Description)
                    .MaximumLength(ValidationConstants.Partner.DescriptionMaxLength)
                    .When(x => !string.IsNullOrWhiteSpace(x.Description))
                    .WithMessage($"Description cannot exceed {ValidationConstants.Partner.DescriptionMaxLength} characters");

                RuleFor(x => x.UrlTitle)
                    .MaximumLength(ValidationConstants.Partner.UrlTitleMaxLength)
                    .When(x => !string.IsNullOrWhiteSpace(x.UrlTitle))
                    .WithMessage($"UrlTitle cannot exceed {ValidationConstants.Partner.UrlTitleMaxLength} characters");
            });

            // Format validation
            RuleSet("FormatValidation", () =>
            {
                RuleFor(x => x.TargetUrl)
                    .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: false))
                    .WithMessage("TargetUrl must be a valid absolute URL");
            });

            // Default rules (always executed)
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Partner title is required")
                .MaximumLength(ValidationConstants.Partner.TitleMaxLength)
                .WithMessage($"Partner title cannot exceed {ValidationConstants.Partner.TitleMaxLength} characters");

            RuleFor(x => x.TargetUrl)
                .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: false))
                .WithMessage("TargetUrl must be a valid absolute URL");

            RuleFor(x => x.LogoId)
                .GreaterThan(ValidationConstants.Common.MinId - 1)
                .WithMessage("LogoId must be greater than 0");

            RuleFor(x => x.Streetcodes)
                .NotNull()
                .WithMessage("Streetcodes list is required");

            RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.Partner.DescriptionMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage($"Description cannot exceed {ValidationConstants.Partner.DescriptionMaxLength} characters");

            RuleFor(x => x.UrlTitle)
                .MaximumLength(ValidationConstants.Partner.UrlTitleMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.UrlTitle))
                .WithMessage($"UrlTitle cannot exceed {ValidationConstants.Partner.UrlTitleMaxLength} characters");
        }
    }
}
