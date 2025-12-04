using FluentValidation.Results;

namespace Streetcode.BLL.Exceptions.CustomExceptions
{
    /// <summary>
    /// Exception thrown when validation fails.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="failures">Collection of validation failures.</param>
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="propertyName">The property name that failed validation.</param>
        /// <param name="errorMessage">The validation error message.</param>
        public ValidationException(string propertyName, string errorMessage)
            : this()
        {
            Errors = new Dictionary<string, string[]>
            {
                { propertyName, new[] { errorMessage } }
            };
        }

        /// <summary>
        /// Gets a dictionary of validation errors, keyed by property name.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; }
    }
}
