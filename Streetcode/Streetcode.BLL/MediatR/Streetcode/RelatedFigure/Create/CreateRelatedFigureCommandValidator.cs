using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Create
{
    /// <summary>
    /// Validator for CreateRelatedFigureCommand.
    /// </summary>
    public class CreateRelatedFigureCommandValidator : AbstractValidator<CreateRelatedFigureCommand>
    {
        public CreateRelatedFigureCommandValidator()
        {
            RuleFor(x => x.ObserverId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.RelatedFigureObserverIdMustBeGreaterThanZero)
                .NotEqual(x => x.TargetId)
                .WithMessage(ErrorMessages.RelatedFigureSelfReferenceNotAllowed);

            RuleFor(x => x.TargetId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.RelatedFigureTargetIdMustBeGreaterThanZero);
        }
    }
}
