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
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStreetcodeDtoValidator"/> class.
        /// </summary>
        public CreateStreetcodeDtoValidator()
        {
            // Required fields validation
            RuleSet("RequiredFields", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty("Index"))
                    .WithMessage("Index є обов'язковим");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty("StreetcodeType"))
                    .WithMessage("StreetcodeType є обов'язковим");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty("Title"))
                    .WithMessage("Назва є обов'язковою");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty("EventStartOrPersonBirthDate"))
                    .WithMessage("EventStartOrPersonBirthDate є обов'язковим");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty("TransliterationUrl"))
                    .WithMessage("TransliterationUrl є обов'язковим");
            });

            // Data type validation
            RuleSet("DataTypes", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveIntegerProperty("Index"))
                    .WithMessage("Index має бути цілим числом")
                    .When(x => JsonElementValidator.HaveRequiredProperty("Index")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithAllowedValues("StreetcodeType", ValidationConstants.Streetcode.ValidTypes))
                    .WithMessage($"StreetcodeType має бути одним з: {string.Join(", ", ValidationConstants.Streetcode.ValidTypes)}")
                    .When(x => JsonElementValidator.HaveRequiredProperty("StreetcodeType")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveDateTimeProperty("EventStartOrPersonBirthDate"))
                    .WithMessage("EventStartOrPersonBirthDate має бути дійсною датою")
                    .When(x => JsonElementValidator.HaveRequiredProperty("EventStartOrPersonBirthDate")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveDateTimeProperty("EventEndOrPersonDeathDate"))
                    .WithMessage("EventEndOrPersonDeathDate має бути дійсною датою")
                    .When(x => JsonElementValidator.HaveProperty("EventEndOrPersonDeathDate")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveIntegerProperty("AudioId"))
                    .WithMessage("AudioId має бути цілим числом")
                    .When(x => JsonElementValidator.HaveProperty("AudioId")(x));
            });

            // String content validation
            RuleSet("StringContent", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveNonEmptyStringProperty("Title"))
                    .WithMessage("Назва не може бути порожньою")
                    .When(x => JsonElementValidator.HaveRequiredProperty("Title")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveNonEmptyStringProperty("TransliterationUrl"))
                    .WithMessage("TransliterationUrl не може бути порожнім")
                    .When(x => JsonElementValidator.HaveRequiredProperty("TransliterationUrl")(x));
            });

            // Length constraints validation
            RuleSet("LengthConstraints", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Title", ValidationConstants.Streetcode.TitleMaxLength))
                    .WithMessage($"Назва не може перевищувати {ValidationConstants.Streetcode.TitleMaxLength} символів")
                    .When(x => JsonElementValidator.HaveRequiredProperty("Title")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Alias", ValidationConstants.Streetcode.AliasMaxLength))
                    .WithMessage($"Alias не може перевищувати {ValidationConstants.Streetcode.AliasMaxLength} символів")
                    .When(x => JsonElementValidator.HaveProperty("Alias")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("ShortDescription", ValidationConstants.Streetcode.ShortDescriptionMaxLength))
                    .WithMessage($"ShortDescription не може перевищувати {ValidationConstants.Streetcode.ShortDescriptionMaxLength} символів")
                    .When(x => JsonElementValidator.HaveProperty("ShortDescription")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("DateString", ValidationConstants.Streetcode.DateStringMaxLength))
                    .WithMessage($"DateString не може перевищувати {ValidationConstants.Streetcode.DateStringMaxLength} символів")
                    .When(x => JsonElementValidator.HaveProperty("DateString")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Teaser", ValidationConstants.Streetcode.TeaserMaxLength))
                    .WithMessage($"Teaser не може перевищувати {ValidationConstants.Streetcode.TeaserMaxLength} символів")
                    .When(x => JsonElementValidator.HaveProperty("Teaser")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("TransliterationUrl", ValidationConstants.Streetcode.TransliterationUrlMaxLength))
                    .WithMessage($"TransliterationUrl не може перевищувати {ValidationConstants.Streetcode.TransliterationUrlMaxLength} символів")
                    .When(x => JsonElementValidator.HaveRequiredProperty("TransliterationUrl")(x));
            });

            // Business rules validation
            RuleSet("BusinessRules", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveValidDateRange("EventStartOrPersonBirthDate", "EventEndOrPersonDeathDate"))
                    .WithMessage("EventEndOrPersonDeathDate має бути пізніше ніж EventStartOrPersonBirthDate")
                    .When(x => JsonElementValidator.HaveProperty("EventEndOrPersonDeathDate")(x) && JsonElementValidator.HaveDateTimeProperty("EventEndOrPersonDeathDate")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HavePositiveIntegerProperty("AudioId"))
                    .WithMessage($"AudioId має бути більше {ValidationConstants.Common.MinId - 1}")
                    .When(x => JsonElementValidator.HaveProperty("AudioId")(x) && JsonElementValidator.HaveIntegerProperty("AudioId")(x));
            });

            // Include all rule sets by default
            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("Index"))
                .WithMessage("Index є обов'язковим");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveIntegerProperty("Index"))
                .WithMessage("Index має бути цілим числом")
                .When(x => JsonElementValidator.HaveRequiredProperty("Index")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("StreetcodeType"))
                .WithMessage("StreetcodeType є обов'язковим");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithAllowedValues("StreetcodeType", ValidationConstants.Streetcode.ValidTypes))
                .WithMessage($"StreetcodeType має бути одним з: {string.Join(", ", ValidationConstants.Streetcode.ValidTypes)}")
                .When(x => JsonElementValidator.HaveRequiredProperty("StreetcodeType")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("Title"))
                .WithMessage("Назва є обов'язковою");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveNonEmptyStringProperty("Title"))
                .WithMessage("Назва не може бути порожньою")
                .When(x => JsonElementValidator.HaveRequiredProperty("Title")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Title", ValidationConstants.Streetcode.TitleMaxLength))
                .WithMessage($"Назва не може перевищувати {ValidationConstants.Streetcode.TitleMaxLength} символів")
                .When(x => JsonElementValidator.HaveRequiredProperty("Title")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Alias", ValidationConstants.Streetcode.AliasMaxLength))
                .WithMessage($"Alias не може перевищувати {ValidationConstants.Streetcode.AliasMaxLength} символів")
                .When(x => JsonElementValidator.HaveProperty("Alias")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("EventStartOrPersonBirthDate"))
                .WithMessage("EventStartOrPersonBirthDate є обов'язковим");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveDateTimeProperty("EventStartOrPersonBirthDate"))
                .WithMessage("EventStartOrPersonBirthDate має бути дійсною датою")
                .When(x => JsonElementValidator.HaveRequiredProperty("EventStartOrPersonBirthDate")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveDateTimeProperty("EventEndOrPersonDeathDate"))
                .WithMessage("EventEndOrPersonDeathDate має бути дійсною датою")
                .When(x => JsonElementValidator.HaveProperty("EventEndOrPersonDeathDate")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveValidDateRange("EventStartOrPersonBirthDate", "EventEndOrPersonDeathDate"))
                .WithMessage("EventEndOrPersonDeathDate має бути пізніше ніж EventStartOrPersonBirthDate")
                .When(x => JsonElementValidator.HaveProperty("EventEndOrPersonDeathDate")(x) && JsonElementValidator.HaveDateTimeProperty("EventEndOrPersonDeathDate")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("ShortDescription", ValidationConstants.Streetcode.ShortDescriptionMaxLength))
                .WithMessage($"ShortDescription не може перевищувати {ValidationConstants.Streetcode.ShortDescriptionMaxLength} символів")
                .When(x => JsonElementValidator.HaveProperty("ShortDescription")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("DateString", ValidationConstants.Streetcode.DateStringMaxLength))
                .WithMessage($"DateString не може перевищувати {ValidationConstants.Streetcode.DateStringMaxLength} символів")
                .When(x => JsonElementValidator.HaveProperty("DateString")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Teaser", ValidationConstants.Streetcode.TeaserMaxLength))
                .WithMessage($"Teaser не може перевищувати {ValidationConstants.Streetcode.TeaserMaxLength} символів")
                .When(x => JsonElementValidator.HaveProperty("Teaser")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("TransliterationUrl"))
                .WithMessage("TransliterationUrl є обов'язковим");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveNonEmptyStringProperty("TransliterationUrl"))
                .WithMessage("TransliterationUrl не може бути порожнім")
                .When(x => JsonElementValidator.HaveRequiredProperty("TransliterationUrl")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("TransliterationUrl", ValidationConstants.Streetcode.TransliterationUrlMaxLength))
                .WithMessage($"TransliterationUrl не може перевищувати {ValidationConstants.Streetcode.TransliterationUrlMaxLength} символів")
                .When(x => JsonElementValidator.HaveRequiredProperty("TransliterationUrl")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveIntegerProperty("AudioId"))
                .WithMessage("AudioId має бути цілим числом")
                .When(x => JsonElementValidator.HaveProperty("AudioId")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HavePositiveIntegerProperty("AudioId"))
                .WithMessage($"AudioId має бути більше {ValidationConstants.Common.MinId - 1}")
                .When(x => JsonElementValidator.HaveProperty("AudioId")(x) && JsonElementValidator.HaveIntegerProperty("AudioId")(x));
        }
    }
}
