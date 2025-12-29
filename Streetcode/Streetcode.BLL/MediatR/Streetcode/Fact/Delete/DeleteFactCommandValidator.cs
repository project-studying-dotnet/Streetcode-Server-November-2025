using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Delete
{
    /// <summary>
    /// Validator for DeleteFactCommand.
    /// </summary>
    public class DeleteFactCommandValidator : AbstractValidator<DeleteFactCommand>
    {
        public DeleteFactCommandValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.FactIdMustBeGreaterThanZero);
        }
    }
}
