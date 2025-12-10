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
                .WithMessage("ID стріткоду-спостерігача має бути більше 0")
                .NotEqual(x => x.TargetId)
                .WithMessage("Стріткод не може бути пов'язаний сам з собою");

            RuleFor(x => x.TargetId)
                .GreaterThan(0)
                .WithMessage("ID цільового стріткоду має бути більше 0");
        }
    }
}
