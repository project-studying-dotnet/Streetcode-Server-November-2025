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
                    .WithMessage("Index is required");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty("StreetcodeType"))
                    .WithMessage("StreetcodeType is required");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty("Title"))
                    .WithMessage("Title is required");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty("EventStartOrPersonBirthDate"))
                    .WithMessage("EventStartOrPersonBirthDate is required");

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveRequiredProperty("TransliterationUrl"))
                    .WithMessage("TransliterationUrl is required");
            });

            // Data type validation
            RuleSet("DataTypes", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveIntegerProperty("Index"))
                    .WithMessage("Index must be an integer")
                    .When(x => JsonElementValidator.HaveRequiredProperty("Index")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithAllowedValues("StreetcodeType", ValidationConstants.Streetcode.ValidTypes))
                    .WithMessage($"StreetcodeType must be one of: {string.Join(", ", ValidationConstants.Streetcode.ValidTypes)}")
                    .When(x => JsonElementValidator.HaveRequiredProperty("StreetcodeType")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveDateTimeProperty("EventStartOrPersonBirthDate"))
                    .WithMessage("EventStartOrPersonBirthDate must be a valid date")
                    .When(x => JsonElementValidator.HaveRequiredProperty("EventStartOrPersonBirthDate")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveDateTimeProperty("EventEndOrPersonDeathDate"))
                    .WithMessage("EventEndOrPersonDeathDate must be a valid date")
                    .When(x => JsonElementValidator.HaveProperty("EventEndOrPersonDeathDate")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveIntegerProperty("AudioId"))
                    .WithMessage("AudioId must be an integer")
                    .When(x => JsonElementValidator.HaveProperty("AudioId")(x));
            });

            // String content validation
            RuleSet("StringContent", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveNonEmptyStringProperty("Title"))
                    .WithMessage("Title cannot be empty")
                    .When(x => JsonElementValidator.HaveRequiredProperty("Title")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveNonEmptyStringProperty("TransliterationUrl"))
                    .WithMessage("TransliterationUrl cannot be empty")
                    .When(x => JsonElementValidator.HaveRequiredProperty("TransliterationUrl")(x));
            });

            // Length constraints validation
            RuleSet("LengthConstraints", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Title", ValidationConstants.Streetcode.TitleMaxLength))
                    .WithMessage($"Title must not exceed {ValidationConstants.Streetcode.TitleMaxLength} characters")
                    .When(x => JsonElementValidator.HaveRequiredProperty("Title")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Alias", ValidationConstants.Streetcode.AliasMaxLength))
                    .WithMessage($"Alias must not exceed {ValidationConstants.Streetcode.AliasMaxLength} characters")
                    .When(x => JsonElementValidator.HaveProperty("Alias")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("ShortDescription", ValidationConstants.Streetcode.ShortDescriptionMaxLength))
                    .WithMessage($"ShortDescription must not exceed {ValidationConstants.Streetcode.ShortDescriptionMaxLength} characters")
                    .When(x => JsonElementValidator.HaveProperty("ShortDescription")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("DateString", ValidationConstants.Streetcode.DateStringMaxLength))
                    .WithMessage($"DateString must not exceed {ValidationConstants.Streetcode.DateStringMaxLength} characters")
                    .When(x => JsonElementValidator.HaveProperty("DateString")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Teaser", ValidationConstants.Streetcode.TeaserMaxLength))
                    .WithMessage($"Teaser must not exceed {ValidationConstants.Streetcode.TeaserMaxLength} characters")
                    .When(x => JsonElementValidator.HaveProperty("Teaser")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("TransliterationUrl", ValidationConstants.Streetcode.TransliterationUrlMaxLength))
                    .WithMessage($"TransliterationUrl must not exceed {ValidationConstants.Streetcode.TransliterationUrlMaxLength} characters")
                    .When(x => JsonElementValidator.HaveRequiredProperty("TransliterationUrl")(x));
            });

            // Business rules validation
            RuleSet("BusinessRules", () =>
            {
                RuleFor(x => x)
                    .Must(JsonElementValidator.HaveValidDateRange("EventStartOrPersonBirthDate", "EventEndOrPersonDeathDate"))
                    .WithMessage("EventEndOrPersonDeathDate must be after EventStartOrPersonBirthDate")
                    .When(x => JsonElementValidator.HaveProperty("EventEndOrPersonDeathDate")(x) && JsonElementValidator.HaveDateTimeProperty("EventEndOrPersonDeathDate")(x));

                RuleFor(x => x)
                    .Must(JsonElementValidator.HavePositiveIntegerProperty("AudioId"))
                    .WithMessage($"AudioId must be greater than {ValidationConstants.Common.MinId - 1}")
                    .When(x => JsonElementValidator.HaveProperty("AudioId")(x) && JsonElementValidator.HaveIntegerProperty("AudioId")(x));
            });

            // Include all rule sets by default
            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("Index"))
                .WithMessage("Index is required");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveIntegerProperty("Index"))
                .WithMessage("Index must be an integer")
                .When(x => JsonElementValidator.HaveRequiredProperty("Index")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("StreetcodeType"))
                .WithMessage("StreetcodeType is required");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithAllowedValues("StreetcodeType", ValidationConstants.Streetcode.ValidTypes))
                .WithMessage($"StreetcodeType must be one of: {string.Join(", ", ValidationConstants.Streetcode.ValidTypes)}")
                .When(x => JsonElementValidator.HaveRequiredProperty("StreetcodeType")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("Title"))
                .WithMessage("Title is required");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveNonEmptyStringProperty("Title"))
                .WithMessage("Title cannot be empty")
                .When(x => JsonElementValidator.HaveRequiredProperty("Title")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Title", ValidationConstants.Streetcode.TitleMaxLength))
                .WithMessage($"Title must not exceed {ValidationConstants.Streetcode.TitleMaxLength} characters")
                .When(x => JsonElementValidator.HaveRequiredProperty("Title")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Alias", ValidationConstants.Streetcode.AliasMaxLength))
                .WithMessage($"Alias must not exceed {ValidationConstants.Streetcode.AliasMaxLength} characters")
                .When(x => JsonElementValidator.HaveProperty("Alias")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("EventStartOrPersonBirthDate"))
                .WithMessage("EventStartOrPersonBirthDate is required");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveDateTimeProperty("EventStartOrPersonBirthDate"))
                .WithMessage("EventStartOrPersonBirthDate must be a valid date")
                .When(x => JsonElementValidator.HaveRequiredProperty("EventStartOrPersonBirthDate")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveDateTimeProperty("EventEndOrPersonDeathDate"))
                .WithMessage("EventEndOrPersonDeathDate must be a valid date")
                .When(x => JsonElementValidator.HaveProperty("EventEndOrPersonDeathDate")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveValidDateRange("EventStartOrPersonBirthDate", "EventEndOrPersonDeathDate"))
                .WithMessage("EventEndOrPersonDeathDate must be after EventStartOrPersonBirthDate")
                .When(x => JsonElementValidator.HaveProperty("EventEndOrPersonDeathDate")(x) && JsonElementValidator.HaveDateTimeProperty("EventEndOrPersonDeathDate")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("ShortDescription", ValidationConstants.Streetcode.ShortDescriptionMaxLength))
                .WithMessage($"ShortDescription must not exceed {ValidationConstants.Streetcode.ShortDescriptionMaxLength} characters")
                .When(x => JsonElementValidator.HaveProperty("ShortDescription")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("DateString", ValidationConstants.Streetcode.DateStringMaxLength))
                .WithMessage($"DateString must not exceed {ValidationConstants.Streetcode.DateStringMaxLength} characters")
                .When(x => JsonElementValidator.HaveProperty("DateString")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("Teaser", ValidationConstants.Streetcode.TeaserMaxLength))
                .WithMessage($"Teaser must not exceed {ValidationConstants.Streetcode.TeaserMaxLength} characters")
                .When(x => JsonElementValidator.HaveProperty("Teaser")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("TransliterationUrl"))
                .WithMessage("TransliterationUrl is required");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveNonEmptyStringProperty("TransliterationUrl"))
                .WithMessage("TransliterationUrl cannot be empty")
                .When(x => JsonElementValidator.HaveRequiredProperty("TransliterationUrl")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveStringPropertyWithMaxLength("TransliterationUrl", ValidationConstants.Streetcode.TransliterationUrlMaxLength))
                .WithMessage($"TransliterationUrl must not exceed {ValidationConstants.Streetcode.TransliterationUrlMaxLength} characters")
                .When(x => JsonElementValidator.HaveRequiredProperty("TransliterationUrl")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveIntegerProperty("AudioId"))
                .WithMessage("AudioId must be an integer")
                .When(x => JsonElementValidator.HaveProperty("AudioId")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HavePositiveIntegerProperty("AudioId"))
                .WithMessage($"AudioId must be greater than {ValidationConstants.Common.MinId - 1}")
                .When(x => JsonElementValidator.HaveProperty("AudioId")(x) && JsonElementValidator.HaveIntegerProperty("AudioId")(x));
        }
    }
}
