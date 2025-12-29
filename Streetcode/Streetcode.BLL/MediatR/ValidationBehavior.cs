using FluentValidation;
using MediatR;
using Streetcode.BLL.Exceptions.CustomExceptions;

namespace Streetcode.BLL.MediatR
{
    /// <summary>
    /// MediatR pipeline behavior that validates requests using FluentValidation.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">Collection of validators for the request type.</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Handles the request by running all validators before proceeding to the next behavior or handler.
        /// </summary>
        /// <param name="request">The request to validate.</param>
        /// <param name="next">The next behavior or handler in the pipeline.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The response from the handler.</returns>
        /// <exception cref="ValidationException">Thrown when validation fails.</exception>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Skip validation if no validators registered
            if (!_validators.Any())
            {
                return await next();
            }

            // Create validation context
            var context = new ValidationContext<TRequest>(request);

            // Run all validators and collect results
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Collect all validation failures
            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            // If there are validation errors, throw ValidationException
            if (failures.Any())
            {
                throw new Exceptions.CustomExceptions.ValidationException(failures);
            }

            // Proceed to the next behavior or handler
            return await next();
        }
    }
}
