using FluentValidation;
using MediatR;
using Streetcode.BLL.Exceptions.CustomExceptions;

namespace Streetcode.BLL.MediatR
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

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
