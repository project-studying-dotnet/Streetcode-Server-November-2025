using FluentValidation;

namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Extension methods for FluentValidation to provide reusable validation rules.
    /// </summary>
    public static class FluentValidationExtensions
    {
        /// <summary>
        /// Validates that an ID is greater than zero.
        /// </summary>
        /// <typeparam name="T">The type being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="errorMessage">Custom error message. If null, uses default Ukrainian message.</param>
        /// <returns>The rule builder for chaining.</returns>
        public static IRuleBuilderOptions<T, int> MustBeValidId<T>(
            this IRuleBuilder<T, int> ruleBuilder,
            string? errorMessage = null)
        {
            return ruleBuilder
                .GreaterThan(ValidationConstants.Common.MinPositiveValue)
                .WithMessage(errorMessage ?? "ID має бути більше 0");
        }

        /// <summary>
        /// Validates that a nullable ID is greater than zero when it has a value.
        /// </summary>
        /// <typeparam name="T">The type being validated.</typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <param name="errorMessage">Custom error message. If null, uses default Ukrainian message.</param>
        /// <returns>The rule builder for chaining.</returns>
        public static IRuleBuilderOptions<T, int?> MustBeValidId<T>(
            this IRuleBuilder<T, int?> ruleBuilder,
            string? errorMessage = null)
        {
            return ruleBuilder
                .GreaterThan(ValidationConstants.Common.MinPositiveValue)
                .When(x => ruleBuilder.GetType().GetProperty("CurrentValue")?.GetValue(ruleBuilder) is int?)
                .WithMessage(errorMessage ?? "ID має бути більше 0");
        }
    }
}
