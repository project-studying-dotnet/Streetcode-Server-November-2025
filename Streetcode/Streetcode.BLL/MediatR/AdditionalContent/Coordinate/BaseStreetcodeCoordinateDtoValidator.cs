using FluentValidation;
using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate
{
    /// <summary>
    /// Base validator containing shared validation rules for StreetcodeCoordinate DTOs.
    /// </summary>
    public abstract class BaseStreetcodeCoordinateDtoValidator : AbstractValidator<StreetcodeCoordinateDto>
    {
        protected void ConfigureSharedRules()
        {
            RuleFor(x => x.Latitude)
                .InclusiveBetween(ValidationConstants.Coordinate.MinLatitude, ValidationConstants.Coordinate.MaxLatitude)
                .WithMessage(string.Format(
                      ErrorMessages.CoordinateWidthError,
                      ValidationConstants.Coordinate.MinLatitude,
                      ValidationConstants.Coordinate.MaxLatitude));

            RuleFor(x => x.Longtitude)
                .InclusiveBetween(ValidationConstants.Coordinate.MinLongitude, ValidationConstants.Coordinate.MaxLongitude)
                .WithMessage(string.Format(
                      ErrorMessages.CoordinateHeightError,
                      ValidationConstants.Coordinate.MinLongitude,
                      ValidationConstants.Coordinate.MaxLongitude));

            RuleFor(x => x.StreetcodeId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.StreetcodeIdMustBeGreaterThanZero);
        }
    }
}
