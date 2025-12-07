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
                    .WithMessage("Index є обов'язковим");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty(PropertyStreetcodeType))
                    .WithMessage("StreetcodeType є обов'язковим");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty(PropertyTitle))
                    .WithMessage("Назва є обов'язковою");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty(PropertyEventStartOrPersonBirthDate))
                    .WithMessage("EventStartOrPersonBirthDate є обов'язковим");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty(PropertyTransliterationUrl))
                    .WithMessage("TransliterationUrl є обов'язковим");
            });

            // Data type validation
            RuleSet("DataTypes", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveIntegerProperty(PropertyIndex))
                    .WithMessage("Index має бути цілим числом")
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyIndex)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithAllowedValues(PropertyStreetcodeType, ValidationConstants.Streetcode.ValidTypes))
                    .WithMessage($"StreetcodeType має бути одним з: {string.Join(", ", ValidationConstants.Streetcode.ValidTypes)}")
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyStreetcodeType)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveDateTimeProperty(PropertyEventStartOrPersonBirthDate))
                    .WithMessage("EventStartOrPersonBirthDate має бути дійсною датою")
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyEventStartOrPersonBirthDate)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveDateTimeProperty(PropertyEventEndOrPersonDeathDate))
                    .WithMessage("EventEndOrPersonDeathDate має бути дійсною датою")
                    .When(x => JsonElementValidator.HaveProperty(PropertyEventEndOrPersonDeathDate)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveIntegerProperty(PropertyAudioId))
                    .WithMessage("AudioId має бути цілим числом")
                    .When(x => JsonElementValidator.HaveProperty(PropertyAudioId)(x));
            });

            // String content validation
            RuleSet("StringContent", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveNonEmptyStringProperty(PropertyTitle))
                    .WithMessage("Назва не може бути порожньою")
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyTitle)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveNonEmptyStringProperty(PropertyTransliterationUrl))
                    .WithMessage("TransliterationUrl не може бути порожнім")
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyTransliterationUrl)(x));
            });

            // Length constraints validation
            RuleSet("LengthConstraints", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyTitle, ValidationConstants.Streetcode.TitleMaxLength))
                    .WithMessage($"Назва не може перевищувати {ValidationConstants.Streetcode.TitleMaxLength} символів")
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyTitle)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyAlias, ValidationConstants.Streetcode.AliasMaxLength))
                    .WithMessage($"Alias не може перевищувати {ValidationConstants.Streetcode.AliasMaxLength} символів")
                    .When(x => JsonElementValidator.HaveProperty(PropertyAlias)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyShortDescription, ValidationConstants.Streetcode.ShortDescriptionMaxLength))
                    .WithMessage($"ShortDescription не може перевищувати {ValidationConstants.Streetcode.ShortDescriptionMaxLength} символів")
                    .When(x => JsonElementValidator.HaveProperty(PropertyShortDescription)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyDateString, ValidationConstants.Streetcode.DateStringMaxLength))
                    .WithMessage($"DateString не може перевищувати {ValidationConstants.Streetcode.DateStringMaxLength} символів")
                    .When(x => JsonElementValidator.HaveProperty(PropertyDateString)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyTeaser, ValidationConstants.Streetcode.TeaserMaxLength))
                    .WithMessage($"Teaser не може перевищувати {ValidationConstants.Streetcode.TeaserMaxLength} символів")
                    .When(x => JsonElementValidator.HaveProperty(PropertyTeaser)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength(PropertyTransliterationUrl, ValidationConstants.Streetcode.TransliterationUrlMaxLength))
                    .WithMessage($"TransliterationUrl не може перевищувати {ValidationConstants.Streetcode.TransliterationUrlMaxLength} символів")
                    .When(x => JsonElementValidator.HaveRequiredProperty(PropertyTransliterationUrl)(x));
            });

            // Business rules validation
            RuleSet("BusinessRules", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveValidDateRange(PropertyEventStartOrPersonBirthDate, PropertyEventEndOrPersonDeathDate))
                    .WithMessage("EventEndOrPersonDeathDate має бути пізніше ніж EventStartOrPersonBirthDate")
                    .When(x => JsonElementValidator.HaveProperty(PropertyEventEndOrPersonDeathDate)(x) && JsonElementValidator.HaveDateTimeProperty(PropertyEventEndOrPersonDeathDate)(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HavePositiveIntegerProperty(PropertyAudioId))
                    .WithMessage($"AudioId має бути більше {ValidationConstants.Common.MinPositiveValue}")
                    .When(x => JsonElementValidator.HaveProperty(PropertyAudioId)(x) && JsonElementValidator.HaveIntegerProperty(PropertyAudioId)(x));
            });
        }
    }
}
