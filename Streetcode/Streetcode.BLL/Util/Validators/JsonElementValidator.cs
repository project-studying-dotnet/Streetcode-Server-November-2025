using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.Json;

namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Utility class providing reusable validation helpers for JsonElement objects.
    /// Uses cached compiled functions for optimal performance.
    /// </summary>
    public static class JsonElementValidator
    {
        private static readonly ConcurrentDictionary<string, Func<JsonElement, bool>> RequiredPropertyCache = new();
        private static readonly ConcurrentDictionary<string, Func<JsonElement, bool>> PropertyCache = new();
        private static readonly ConcurrentDictionary<string, Func<JsonElement, bool>> IntegerPropertyCache = new();
        private static readonly ConcurrentDictionary<string, Func<JsonElement, bool>> PositiveIntegerPropertyCache = new();
        private static readonly ConcurrentDictionary<string, Func<JsonElement, bool>> NonEmptyStringPropertyCache = new();
        private static readonly ConcurrentDictionary<string, Func<JsonElement, bool>> DateTimePropertyCache = new();
        private static readonly ConcurrentDictionary<string, Func<JsonElement, bool>> MaxLengthCache = new();
        private static readonly ConcurrentDictionary<string, Func<JsonElement, bool>> AllowedValuesCache = new();
        private static readonly ConcurrentDictionary<string, Func<JsonElement, bool>> DateRangeCache = new();

        /// <summary>
        /// Creates a cached validator function that checks if a required property exists and is not null.
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <returns>A cached function that validates the property existence.</returns>
        public static Func<JsonElement, bool> HaveRequiredProperty(string propertyName)
        {
            return RequiredPropertyCache.GetOrAdd(
                propertyName,
                key => json => json.TryGetProperty(key, out var property) && property.ValueKind != JsonValueKind.Null);
        }

        /// <summary>
        /// Creates a cached validator function that checks if a property exists (can be null).
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <returns>A cached function that validates the property existence.</returns>
        public static Func<JsonElement, bool> HaveProperty(string propertyName)
        {
            return PropertyCache.GetOrAdd(
                propertyName,
                key => json => json.TryGetProperty(key, out _));
        }

        /// <summary>
        /// Creates a cached validator function that checks if a property is a valid integer.
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <returns>A cached function that validates the property is an integer.</returns>
        public static Func<JsonElement, bool> HaveIntegerProperty(string propertyName)
        {
            return IntegerPropertyCache.GetOrAdd(
                propertyName,
                key => json =>
                {
                    if (json.TryGetProperty(key, out var property))
                    {
                        return property.ValueKind == JsonValueKind.Number && property.TryGetInt32(out _);
                    }

                    return false;
                });
        }

        /// <summary>
        /// Creates a cached validator function that checks if a property is a positive integer.
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <returns>A cached function that validates the property is a positive integer.</returns>
        public static Func<JsonElement, bool> HavePositiveIntegerProperty(string propertyName)
        {
            return PositiveIntegerPropertyCache.GetOrAdd(
                propertyName,
                key => json =>
                {
                    if (json.TryGetProperty(key, out var property) && property.TryGetInt32(out var value))
                    {
                        return value > ValidationConstants.Common.MinId - 1;
                    }

                    return false;
                });
        }

        /// <summary>
        /// Creates a cached validator function that checks if a string property is not empty.
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <returns>A cached function that validates the property is a non-empty string.</returns>
        public static Func<JsonElement, bool> HaveNonEmptyStringProperty(string propertyName)
        {
            return NonEmptyStringPropertyCache.GetOrAdd(
                propertyName,
                key => json =>
                {
                    if (json.TryGetProperty(key, out var property) && property.ValueKind == JsonValueKind.String)
                    {
                        return !string.IsNullOrWhiteSpace(property.GetString());
                    }

                    return false;
                });
        }

        /// <summary>
        /// Creates a cached validator function that checks if a string property does not exceed maximum length.
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <returns>A cached function that validates the property length.</returns>
        public static Func<JsonElement, bool> HaveStringPropertyWithMaxLength(string propertyName, int maxLength)
        {
            var cacheKey = $"{propertyName}:{maxLength}";
            return MaxLengthCache.GetOrAdd(
                cacheKey,
                _ => json =>
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
                });
        }

        /// <summary>
        /// Creates a cached validator function that checks if a property is a valid DateTime.
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <returns>A cached function that validates the property is a valid DateTime.</returns>
        public static Func<JsonElement, bool> HaveDateTimeProperty(string propertyName)
        {
            return DateTimePropertyCache.GetOrAdd(
                propertyName,
                key => json =>
                {
                    if (json.TryGetProperty(key, out var property))
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
                });
        }

        /// <summary>
        /// Creates a cached validator function that checks if a string property matches one of the allowed values.
        /// </summary>
        /// <param name="propertyName">The name of the property to check.</param>
        /// <param name="allowedValues">The array of allowed values.</param>
        /// <param name="ignoreCase">Whether to ignore case when comparing values.</param>
        /// <returns>A cached function that validates the property value is in the allowed set.</returns>
        public static Func<JsonElement, bool> HaveStringPropertyWithAllowedValues(
            string propertyName,
            string[] allowedValues,
            bool ignoreCase = true)
        {
            var cacheKey = $"{propertyName}:{string.Join(",", allowedValues)}:{ignoreCase}";
            return AllowedValuesCache.GetOrAdd(
                cacheKey,
                _ =>
                {
                    var comparer = ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;
                    return json =>
                    {
                        if (json.TryGetProperty(propertyName, out var property) && property.ValueKind == JsonValueKind.String)
                        {
                            var value = property.GetString();
                            return allowedValues.Contains(value, comparer);
                        }

                        return false;
                    };
                });
        }

        /// <summary>
        /// Creates a cached validator function that checks if two DateTime properties form a valid date range.
        /// Uses StreetcodeDateRangeValidator for consistent validation logic.
        /// </summary>
        /// <param name="startPropertyName">The name of the start date property.</param>
        /// <param name="endPropertyName">The name of the end date property.</param>
        /// <returns>A cached function that validates the date range.</returns>
        public static Func<JsonElement, bool> HaveValidDateRange(string startPropertyName, string endPropertyName)
        {
            var cacheKey = $"{startPropertyName}:{endPropertyName}";
            return DateRangeCache.GetOrAdd(
                cacheKey,
                _ => json =>
                {
                    if (json.TryGetProperty(startPropertyName, out var startProperty) &&
                        json.TryGetProperty(endPropertyName, out var endProperty) &&
                        startProperty.ValueKind == JsonValueKind.String &&
                        endProperty.ValueKind == JsonValueKind.String)
                    {
                        if (DateTime.TryParse(startProperty.GetString(), out var startDate) &&
                            DateTime.TryParse(endProperty.GetString(), out var endDate))
                        {
                            return StreetcodeDateRangeValidator.IsValidDateRange(startDate, endDate);
                        }
                    }

                    return true;
                });
        }
    }
}
