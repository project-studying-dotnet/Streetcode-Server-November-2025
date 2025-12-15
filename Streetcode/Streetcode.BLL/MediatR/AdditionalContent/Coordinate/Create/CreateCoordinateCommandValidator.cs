using FluentValidation;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create
{
    /// <summary>
    /// Validator for CreateCoordinateCommand.
    /// </summary>
    public class CreateCoordinateCommandValidator : AbstractValidator<CreateCoordinateCommand>
    {
        public CreateCoordinateCommandValidator()
        {
            RuleFor(x => x.StreetcodeCoordinate)
                .NotNull()
                .WithMessage("Дані координат не можуть бути порожніми")
                .SetValidator(new StreetcodeCoordinateDtoValidator());
        }
    }
}
