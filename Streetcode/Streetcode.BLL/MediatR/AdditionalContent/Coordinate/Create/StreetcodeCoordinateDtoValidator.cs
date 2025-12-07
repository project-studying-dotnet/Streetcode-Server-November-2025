using FluentValidation;
using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create
{
    /// <summary>
    /// Validator for StreetcodeCoordinateDto.
    /// </summary>
    public class StreetcodeCoordinateDtoValidator : BaseStreetcodeCoordinateDtoValidator
    {
        public StreetcodeCoordinateDtoValidator()
        {
            ConfigureSharedRules();
        }
    }
}
