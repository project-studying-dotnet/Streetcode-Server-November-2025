using FluentValidation;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete
{
    /// <summary>
    /// Validator for DeleteCoordinateCommand.
    /// </summary>
    public class DeleteCoordinateCommandValidator : AbstractValidator<DeleteCoordinateCommand>
    {
        public DeleteCoordinateCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("ID координат має бути більше 0");
        }
    }
}
