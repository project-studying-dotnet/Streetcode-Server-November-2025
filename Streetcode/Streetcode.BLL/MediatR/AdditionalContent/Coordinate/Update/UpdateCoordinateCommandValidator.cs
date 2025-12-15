using FluentValidation;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update
{
    /// <summary>
    /// Validator for UpdateCoordinateCommand.
    /// </summary>
    public class UpdateCoordinateCommandValidator : AbstractValidator<UpdateCoordinateCommand>
    {
        public UpdateCoordinateCommandValidator()
        {
            RuleFor(x => x.StreetcodeCoordinate)
                .NotNull()
                .WithMessage("Дані координат не можуть бути порожніми")
                .SetValidator(new UpdateStreetcodeCoordinateDtoValidator());
        }
    }
}
