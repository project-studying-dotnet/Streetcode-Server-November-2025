using FluentValidation;
using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create
{
    /// <summary>
    /// Validator for StreetcodeCoordinateDto.
    /// </summary>
    public class StreetcodeCoordinateDtoValidator : AbstractValidator<StreetcodeCoordinateDto>
    {
        public StreetcodeCoordinateDtoValidator()
        {
            RuleFor(x => x.Latitude)
                .InclusiveBetween(ValidationConstants.Coordinate.MinLatitude, ValidationConstants.Coordinate.MaxLatitude)
                .WithMessage($"Широта має бути в межах від {ValidationConstants.Coordinate.MinLatitude} до {ValidationConstants.Coordinate.MaxLatitude} градусів");

            RuleFor(x => x.Longtitude)
                .InclusiveBetween(ValidationConstants.Coordinate.MinLongitude, ValidationConstants.Coordinate.MaxLongitude)
                .WithMessage($"Довгота має бути в межах від {ValidationConstants.Coordinate.MinLongitude} до {ValidationConstants.Coordinate.MaxLongitude} градусів");

            RuleFor(x => x.StreetcodeId)
                .GreaterThan(0)
                .WithMessage("ID стріткоду має бути більше 0");
        }
    }
}
