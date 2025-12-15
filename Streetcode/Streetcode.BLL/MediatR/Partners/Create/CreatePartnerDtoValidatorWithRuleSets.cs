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
                    .WithMessage("Назва партнера є обов'язковою");

                RuleFor(x => x.LogoId)
                    .GreaterThan(ValidationConstants.Common.MinPositiveValue)
                    .WithMessage("LogoId має бути більше 0");

                RuleFor(x => x.Streetcodes)
                    .NotNull()
                    .WithMessage("Список стріткодів є обов'язковим");
            });

            // Length constraints validation
            RuleSet("LengthConstraints", () =>
            {
                RuleFor(x => x.Title)
                    .MaximumLength(ValidationConstants.Partner.TitleMaxLength)
                    .WithMessage($"Назва партнера не може перевищувати {ValidationConstants.Partner.TitleMaxLength} символів");

                RuleFor(x => x.Description)
                    .MaximumLength(ValidationConstants.Partner.DescriptionMaxLength)
                    .When(x => !string.IsNullOrWhiteSpace(x.Description))
                    .WithMessage($"Опис не може перевищувати {ValidationConstants.Partner.DescriptionMaxLength} символів");

                RuleFor(x => x.UrlTitle)
                    .MaximumLength(ValidationConstants.Partner.UrlTitleMaxLength)
                    .When(x => !string.IsNullOrWhiteSpace(x.UrlTitle))
                    .WithMessage($"UrlTitle не може перевищувати {ValidationConstants.Partner.UrlTitleMaxLength} символів");
            });

            // Format validation
            RuleSet("FormatValidation", () =>
            {
                RuleFor(x => x.TargetUrl)
                    .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: false))
                    .WithMessage("TargetUrl має бути дійсною абсолютною URL-адресою");
            });
        }
    }
}
