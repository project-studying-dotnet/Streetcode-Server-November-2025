using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Delete
{
    /// <summary>
    /// Validator for DeleteRelatedFigureCommand.
    /// </summary>
    public class DeleteRelatedFigureCommandValidator : AbstractValidator<DeleteRelatedFigureCommand>
    {
        public DeleteRelatedFigureCommandValidator()
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
