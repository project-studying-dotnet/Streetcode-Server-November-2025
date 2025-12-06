using FluentValidation;
using Microsoft.Extensions.Localization;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Partners.Create
{
    /// <summary>
    /// Localized validator for CreatePartnerDto (example implementation).
    /// </summary>
    public class CreatePartnerDtoValidatorLocalized : AbstractValidator<CreatePartnerDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePartnerDtoValidatorLocalized"/> class.
        /// </summary>
        /// <param name="localizer">The string localizer for validation messages.</param>
        public CreatePartnerDtoValidatorLocalized(IStringLocalizer<Resources.ValidationMessages> localizer)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(localizer["Partner_TitleRequired"])
                .MaximumLength(ValidationConstants.Partner.TitleMaxLength)
                .WithMessage(string.Format(localizer["Partner_TitleMaxLength"], ValidationConstants.Partner.TitleMaxLength));

            RuleFor(x => x.TargetUrl)
                .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: false))
                .WithMessage(localizer["Partner_TargetUrlInvalid"]);

            RuleFor(x => x.LogoId)
                .GreaterThan(ValidationConstants.Common.MinId - 1)
                .WithMessage(localizer["Partner_LogoIdInvalid"]);

            RuleFor(x => x.Streetcodes)
                .NotNull()
                .WithMessage(localizer["Partner_StreetcodesRequired"]);

            RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.Partner.DescriptionMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage(string.Format(localizer["Partner_DescriptionMaxLength"], ValidationConstants.Partner.DescriptionMaxLength));

            RuleFor(x => x.UrlTitle)
                .MaximumLength(ValidationConstants.Partner.UrlTitleMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.UrlTitle))
                .WithMessage(string.Format(localizer["Partner_UrlTitleMaxLength"], ValidationConstants.Partner.UrlTitleMaxLength));
        }
    }
}
