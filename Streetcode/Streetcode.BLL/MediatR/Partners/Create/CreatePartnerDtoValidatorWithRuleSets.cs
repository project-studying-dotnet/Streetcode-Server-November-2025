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
                    .WithMessage(ErrorMessages.PartnerTitleRequired);

                RuleFor(x => x.LogoId)
                    .GreaterThan(ValidationConstants.Common.MinPositiveValue)
                    .WithMessage(ErrorMessages.PartnerLogoIdMustBeGreaterThanZero);

                RuleFor(x => x.Streetcodes)
                    .NotNull()
                    .WithMessage(ErrorMessages.PartnerStreetcodesRequired);
            });

            // Length constraints validation
            RuleSet("LengthConstraints", () =>
            {
                RuleFor(x => x.Title)
                    .MaximumLength(ValidationConstants.Partner.TitleMaxLength)
                    .WithMessage(string.Format(
                        ErrorMessages.PartnerTitleTooLong,
                        ValidationConstants.Partner.TitleMaxLength));

                RuleFor(x => x.Description)
                    .MaximumLength(ValidationConstants.Partner.DescriptionMaxLength)
                    .When(x => !string.IsNullOrWhiteSpace(x.Description))
                    .WithMessage(string.Format(
                        ErrorMessages.PartnerDescriptionTooLong,
                        ValidationConstants.Partner.DescriptionMaxLength));

                RuleFor(x => x.UrlTitle)
                    .MaximumLength(ValidationConstants.Partner.UrlTitleMaxLength)
                    .When(x => !string.IsNullOrWhiteSpace(x.UrlTitle))
                    .WithMessage(string.Format(
                        ErrorMessages.PartnerUrlTitleTooLong,
                        ValidationConstants.Partner.UrlTitleMaxLength));
            });

            // Format validation
            RuleSet("FormatValidation", () =>
            {
                RuleFor(x => x.TargetUrl)
                    .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: false))
                    .WithMessage(ErrorMessages.PartnerTargetUrlInvalid);
            });
        }
    }
}
