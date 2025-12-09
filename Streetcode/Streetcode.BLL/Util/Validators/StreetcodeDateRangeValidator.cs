using System;

namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Cross-field validator for date range validation.
    /// Ensures end dates are after start dates and handles nullable dates correctly.
    /// </summary>
    public static class StreetcodeDateRangeValidator
    {
        /// <summary>
        /// Validates that the end date is after the start date.
        /// If both dates are provided, ensures DateTo > DateFrom.
        /// Handles nullable dates correctly - returns true if end date is null.
        /// </summary>
        /// <param name="startDate">The start date (DateFrom, EventStartOrPersonBirthDate).</param>
        /// <param name="endDate">The end date (DateTo, EventEndOrPersonDeathDate), can be null.</param>
        /// <returns>True if the date range is valid (end date is after start date or null), false otherwise.</returns>
        public static bool IsValidDateRange(DateTime startDate, DateTime? endDate)
        {
            // If end date is not provided, it's valid
            if (!endDate.HasValue)
            {
                return true;
            }

            // End date must be after start date
            return endDate.Value > startDate;
        }

        /// <summary>
        /// Validates that both dates form a valid range when both are provided.
        /// If both DateFrom and DateTo provided, ensure DateTo > DateFrom.
        /// Handles nullable dates correctly - returns true if either date is null.
        /// </summary>
        /// <param name="dateFrom">The start date (nullable).</param>
        /// <param name="dateTo">The end date (nullable).</param>
        /// <returns>True if the date range is valid or either date is null, false otherwise.</returns>
        public static bool IsValidDateRange(DateTime? dateFrom, DateTime? dateTo)
        {
            // If either date is not provided, it's valid
            if (!dateFrom.HasValue || !dateTo.HasValue)
            {
                return true;
            }

            // End date must be after start date
            return dateTo.Value > dateFrom.Value;
        }

        /// <summary>
        /// Validates that the end date is after the start date with tolerance for same-day events.
        /// </summary>
        /// <param name="startDate">The start date (EventStartOrPersonBirthDate).</param>
        /// <param name="endDate">The end date (EventEndOrPersonDeathDate), can be null.</param>
        /// <param name="allowSameDay">If true, allows end date to be the same as start date.</param>
        /// <returns>True if the date range is valid, false otherwise.</returns>
        public static bool IsValidDateRange(DateTime startDate, DateTime? endDate, bool allowSameDay)
        {
            // If end date is not provided, it's valid
            if (!endDate.HasValue)
            {
                return true;
            }

            // Check if same day is allowed
            if (allowSameDay)
            {
                return endDate.Value >= startDate;
            }

            // End date must be after start date
            return endDate.Value > startDate;
        }

        /// <summary>
        /// Validates that start date is not in the future.
        /// </summary>
        /// <param name="startDate">The start date to validate.</param>
        /// <returns>True if the start date is not in the future, false otherwise.</returns>
        public static bool IsStartDateNotInFuture(DateTime startDate)
        {
            return startDate <= DateTime.UtcNow;
        }

        /// <summary>
        /// Validates that end date is not in the future (if provided).
        /// </summary>
        /// <param name="endDate">The end date to validate, can be null.</param>
        /// <returns>True if the end date is not in the future or null, false otherwise.</returns>
        public static bool IsEndDateNotInFuture(DateTime? endDate)
        {
            if (!endDate.HasValue)
            {
                return true;
            }

            return endDate.Value <= DateTime.UtcNow;
        }
    }
}
