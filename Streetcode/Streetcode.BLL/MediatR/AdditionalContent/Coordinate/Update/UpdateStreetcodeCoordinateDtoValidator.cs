using FluentValidation;
using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update
{
    /// <summary>
    /// Validator for StreetcodeCoordinateDto in update context.
    /// </summary>
    public class UpdateStreetcodeCoordinateDtoValidator : BaseStreetcodeCoordinateDtoValidator
    {
        public UpdateStreetcodeCoordinateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.CoordinateIdMustBeGreaterThanZero);

            ConfigureSharedRules();
        }
    }
}
