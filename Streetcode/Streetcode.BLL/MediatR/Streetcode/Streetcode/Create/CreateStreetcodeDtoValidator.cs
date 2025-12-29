using System.Text.Json;
using FluentValidation;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Create
{
    /// <summary>
    /// Validator for Streetcode DTO JSON structure.
    /// </summary>
    public class CreateStreetcodeDtoValidator : AbstractValidator<JsonElement>
    {
        private const string PropertyIndex = "Index";
        private const string PropertyStreetcodeType = "StreetcodeType";
        private const string PropertyTitle = "Title";
        private const string PropertyEventStartOrPersonBirthDate = "EventStartOrPersonBirthDate";
        private const string PropertyEventEndOrPersonDeathDate = "EventEndOrPersonDeathDate";
        private const string PropertyTransliterationUrl = "TransliterationUrl";
        private const string PropertyAlias = "Alias";
        private const string PropertyShortDescription = "ShortDescription";
        private const string PropertyDateString = "DateString";
        private const string PropertyTeaser = "Teaser";
        private const string PropertyAudioId = "AudioId";

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStreetcodeDtoValidator"/> class.
        /// </summary>
        public CreateStreetcodeDtoValidator()
        {
            // Required fields validation
            RuleSet("RequiredFields", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty(PropertyIndex))
                    .WithMessage(ErrorMessages.StreetcodeIndexRequired);

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty(PropertyStreetcodeType))
                    .WithMessage(ErrorMessages.StreetcodeTypeRequired);

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty(PropertyTitle))
                    .WithMessage(ErrorMessages.StreetcodeTitleRequired);

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty(PropertyEventStartOrPersonBirthDate))
                    .WithMessage(ErrorMessages.StreetcodeEventStartOrPersonBirthDateRequired);

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty(PropertyTransliterationUrl))
                    .WithMessage(ErrorMessages.StreetcodeTransliterationUrlRequired);
            });

            // Data type validation
            RuleSet("DataTypes", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveIntegerProperty(PropertyIndex))
                    .WithMessage(ErrorMessages.StreetcodeIndexMustBeInteger)
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyIndex)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithAllowedValues(PropertyStreetcodeType, ValidationConstants.Streetcode.ValidTypes))
                    .WithMessage(string.Format(
                        ErrorMessages.StreetcodeTypeInvalid,
                        string.Join(", ", ValidationConstants.Streetcode.ValidTypes)))
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyStreetcodeType)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveDateTimeProperty(PropertyEventStartOrPersonBirthDate))
                    .WithMessage(ErrorMessages.StreetcodeEventStartOrPersonBirthDateInvalid)
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyEventStartOrPersonBirthDate)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveDateTimeProperty(PropertyEventEndOrPersonDeathDate))
                    .WithMessage(ErrorMessages.StreetcodeEventEndOrPersonDeathDateInvalid)
                    .When(x => JsonElementValidator.HaveProperty(PropertyEventEndOrPersonDeathDate)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveIntegerProperty(PropertyAudioId))
                    .WithMessage(ErrorMessages.StreetcodeAudioIdMustBeInteger)
                    .When(x => JsonElementValidator.HaveProperty(PropertyAudioId)(x));
            });

            // String content validation
            RuleSet("StringContent", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveNonEmptyStringProperty(PropertyTitle))
                    .WithMessage(ErrorMessages.StreetcodeTitleCannotBeEmpty)
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyTitle)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveNonEmptyStringProperty(PropertyTransliterationUrl))
                    .WithMessage(ErrorMessages.StreetcodeTransliterationUrlCannotBeEmpty)
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyTransliterationUrl)(x));
            });

            // Length constraints validation
            RuleSet("LengthConstraints", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyTitle, ValidationConstants.Streetcode.TitleMaxLength))
                    .WithMessage(string.Format(
                        ErrorMessages.StreetcodeTitleTooLong,
                        ValidationConstants.Streetcode.TitleMaxLength))
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyTitle)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyAlias, ValidationConstants.Streetcode.AliasMaxLength))
                    .WithMessage(string.Format(
                        ErrorMessages.StreetcodeAliasTooLong,
                        ValidationConstants.Streetcode.AliasMaxLength))
                    .When(x => JsonElementValidator.HaveProperty(PropertyAlias)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyShortDescription, ValidationConstants.Streetcode.ShortDescriptionMaxLength))
                    .WithMessage(string.Format(
                        ErrorMessages.StreetcodeShortDescriptionTooLong,
                        ValidationConstants.Streetcode.ShortDescriptionMaxLength))
                    .When(x => JsonElementValidator.HaveProperty(PropertyShortDescription)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyDateString, ValidationConstants.Streetcode.DateStringMaxLength))
                    .WithMessage(string.Format(
                        ErrorMessages.StreetcodeDateStringTooLong,
                        ValidationConstants.Streetcode.DateStringMaxLength))
                    .When(x => JsonElementValidator.HaveProperty(PropertyDateString)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyTeaser, ValidationConstants.Streetcode.TeaserMaxLength))
                    .WithMessage(string.Format(
                        ErrorMessages.StreetcodeTeaserTooLong,
                        ValidationConstants.Streetcode.TeaserMaxLength))
                    .When(x => JsonElementValidator.HaveProperty(PropertyTeaser)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyTransliterationUrl, ValidationConstants.Streetcode.TransliterationUrlMaxLength))
                    .WithMessage(string.Format(
                        ErrorMessages.StreetcodeTransliterationUrlTooLong,
                        ValidationConstants.Streetcode.TransliterationUrlMaxLength))
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyTransliterationUrl)(x));
            });

            // Business rules validation
            RuleSet("BusinessRules", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveValidDateRange(PropertyEventStartOrPersonBirthDate, PropertyEventEndOrPersonDeathDate))
                    .WithMessage(ErrorMessages.StreetcodeDateRangeInvalid)
                    .When(x => JsonElementValidator.HaveProperty(PropertyEventEndOrPersonDeathDate)(x) && JsonElementValidator.HaveDateTimeProperty(PropertyEventEndOrPersonDeathDate)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HavePositiveIntegerProperty(PropertyAudioId))
                    .WithMessage(string.Format(
                        ErrorMessages.StreetcodeAudioIdMustBePositive,
                        ValidationConstants.Common.MinPositiveValue))
                    .When(x => JsonElementValidator.HaveProperty(PropertyAudioId)(x) && JsonElementValidator.HaveIntegerProperty(PropertyAudioId)(x));
            });
        }
    }
}
