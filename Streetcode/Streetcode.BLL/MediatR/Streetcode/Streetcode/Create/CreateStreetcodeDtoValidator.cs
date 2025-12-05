using System;
using System.Linq;
using System.Text.Json;
using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Create
{
    /// <summary>
    /// Validator for Streetcode DTO JSON structure.
    /// </summary>
    public class CreateStreetcodeDtoValidator : AbstractValidator<JsonElement>
    {
        private static readonly string[] ValidStreetcodeTypes = { "Event", "Person" };

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStreetcodeDtoValidator"/> class.
        /// </summary>
        public CreateStreetcodeDtoValidator()
        {
            RuleFor(x => x)
                .Must(HaveRequiredProperty("Index"))
                .WithMessage("Index is required");

            RuleFor(x => x)
                .Must(HaveIntegerProperty("Index"))
                .WithMessage("Index must be an integer")
                .When(x => HaveRequiredProperty("Index")(x));

            RuleFor(x => x)
                .Must(HaveRequiredProperty("StreetcodeType"))
                .WithMessage("StreetcodeType is required");

            RuleFor(x => x)
                .Must(HaveValidStreetcodeType)
                .WithMessage($"StreetcodeType must be one of: {string.Join(", ", ValidStreetcodeTypes)}")
                .When(x => HaveRequiredProperty("StreetcodeType")(x));

            RuleFor(x => x)
                .Must(HaveRequiredProperty("Title"))
                .WithMessage("Title is required");

            RuleFor(x => x)
                .Must(HaveNonEmptyStringProperty("Title"))
                .WithMessage("Title cannot be empty")
                .When(x => HaveRequiredProperty("Title")(x));

            RuleFor(x => x)
                .Must(HaveStringPropertyWithMaxLength("Title", 255))
                .WithMessage("Title must not exceed 255 characters")
                .When(x => HaveRequiredProperty("Title")(x));

            RuleFor(x => x)
                .Must(HaveStringPropertyWithMaxLength("Alias", 50))
                .WithMessage("Alias must not exceed 50 characters")
                .When(x => HaveProperty("Alias")(x));

            RuleFor(x => x)
                .Must(HaveRequiredProperty("EventStartOrPersonBirthDate"))
                .WithMessage("EventStartOrPersonBirthDate is required");

            RuleFor(x => x)
                .Must(HaveDateTimeProperty("EventStartOrPersonBirthDate"))
                .WithMessage("EventStartOrPersonBirthDate must be a valid date")
                .When(x => HaveRequiredProperty("EventStartOrPersonBirthDate")(x));

            RuleFor(x => x)
                .Must(HaveDateTimeProperty("EventEndOrPersonDeathDate"))
                .WithMessage("EventEndOrPersonDeathDate must be a valid date")
                .When(x => HaveProperty("EventEndOrPersonDeathDate")(x));

            RuleFor(x => x)
                .Must(HaveValidDateRange)
                .WithMessage("EventEndOrPersonDeathDate must be after EventStartOrPersonBirthDate")
                .When(x => HaveProperty("EventEndOrPersonDeathDate")(x) && HaveDateTimeProperty("EventEndOrPersonDeathDate")(x));

            RuleFor(x => x)
                .Must(HaveStringPropertyWithMaxLength("ShortDescription", 33))
                .WithMessage("ShortDescription must not exceed 33 characters")
                .When(x => HaveProperty("ShortDescription")(x));

            RuleFor(x => x)
                .Must(HaveStringPropertyWithMaxLength("DateString", 50))
                .WithMessage("DateString must not exceed 50 characters")
                .When(x => HaveProperty("DateString")(x));

            RuleFor(x => x)
                .Must(HaveStringPropertyWithMaxLength("Teaser", 520))
                .WithMessage("Teaser must not exceed 520 characters")
                .When(x => HaveProperty("Teaser")(x));

            RuleFor(x => x)
                .Must(HaveRequiredProperty("TransliterationUrl"))
                .WithMessage("TransliterationUrl is required");

            RuleFor(x => x)
                .Must(HaveNonEmptyStringProperty("TransliterationUrl"))
                .WithMessage("TransliterationUrl cannot be empty")
                .When(x => HaveRequiredProperty("TransliterationUrl")(x));

            RuleFor(x => x)
                .Must(HaveStringPropertyWithMaxLength("TransliterationUrl", 100))
                .WithMessage("TransliterationUrl must not exceed 100 characters")
                .When(x => HaveRequiredProperty("TransliterationUrl")(x));

            RuleFor(x => x)
                .Must(HaveIntegerProperty("AudioId"))
                .WithMessage("AudioId must be an integer")
                .When(x => HaveProperty("AudioId")(x));

            RuleFor(x => x)
                .Must(HavePositiveIntegerProperty("AudioId"))
                .WithMessage("AudioId must be greater than 0")
                .When(x => HaveProperty("AudioId")(x) && HaveIntegerProperty("AudioId")(x));
        }

        private static Func<JsonElement, bool> HaveRequiredProperty(string propertyName)
        {
            return json => json.TryGetProperty(propertyName, out var property) && property.ValueKind != JsonValueKind.Null;
        }

        private static Func<JsonElement, bool> HaveProperty(string propertyName)
        {
            return json => json.TryGetProperty(propertyName, out _);
        }

        private static Func<JsonElement, bool> HaveIntegerProperty(string propertyName)
        {
            return json =>
            {
                if (json.TryGetProperty(propertyName, out var property))
                {
                    return property.ValueKind == JsonValueKind.Number && property.TryGetInt32(out _);
                }

                return false;
            };
        }

        private static Func<JsonElement, bool> HavePositiveIntegerProperty(string propertyName)
        {
            return json =>
            {
                if (json.TryGetProperty(propertyName, out var property) && property.TryGetInt32(out var value))
                {
                    return value > 0;
                }

                return false;
            };
        }

        private static Func<JsonElement, bool> HaveNonEmptyStringProperty(string propertyName)
        {
            return json =>
            {
                if (json.TryGetProperty(propertyName, out var property) && property.ValueKind == JsonValueKind.String)
                {
                    return !string.IsNullOrWhiteSpace(property.GetString());
                }

                return false;
            };
        }

        private static Func<JsonElement, bool> HaveStringPropertyWithMaxLength(string propertyName, int maxLength)
        {
            return json =>
            {
                if (json.TryGetProperty(propertyName, out var property))
                {
                    if (property.ValueKind == JsonValueKind.Null)
                    {
                        return true;
                    }

                    if (property.ValueKind == JsonValueKind.String)
                    {
                        var value = property.GetString();
                        return value == null || value.Length <= maxLength;
                    }

                    return false;
                }

                return true;
            };
        }

        private static Func<JsonElement, bool> HaveDateTimeProperty(string propertyName)
        {
            return json =>
            {
                if (json.TryGetProperty(propertyName, out var property))
                {
                    if (property.ValueKind == JsonValueKind.Null)
                    {
                        return true;
                    }

                    if (property.ValueKind == JsonValueKind.String)
                    {
                        return DateTime.TryParse(property.GetString(), out _);
                    }

                    return false;
                }

                return false;
            };
        }

        private static bool HaveValidStreetcodeType(JsonElement json)
        {
            if (json.TryGetProperty("StreetcodeType", out var property) && property.ValueKind == JsonValueKind.String)
            {
                var value = property.GetString();
                return ValidStreetcodeTypes.Contains(value, StringComparer.OrdinalIgnoreCase);
            }

            return false;
        }

        private static bool HaveValidDateRange(JsonElement json)
        {
            if (json.TryGetProperty("EventStartOrPersonBirthDate", out var startProperty) &&
                json.TryGetProperty("EventEndOrPersonDeathDate", out var endProperty) &&
                startProperty.ValueKind == JsonValueKind.String &&
                endProperty.ValueKind == JsonValueKind.String)
            {
                if (DateTime.TryParse(startProperty.GetString(), out var startDate) &&
                    DateTime.TryParse(endProperty.GetString(), out var endDate))
                {
                    return endDate > startDate;
                }
            }

            return true;
        }
    }
}
