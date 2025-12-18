using FluentValidation.TestHelper;
using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Coordinate.Create
{
    public class CreateCoordinateCommandValidatorTests
    {
        private readonly CreateCoordinateCommandValidator _validator;

        public CreateCoordinateCommandValidatorTests()
        {
            _validator = new CreateCoordinateCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Dto_Is_Null()
        {
            var command = new CreateCoordinateCommand(null);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeCoordinate);
        }

        [Fact]
        public void Should_Have_Error_When_Dto_Is_Invalid()
        {
            var invalidDto = new StreetcodeCoordinateDto { StreetcodeId = 0 };
            var command = new CreateCoordinateCommand(invalidDto);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.StreetcodeCoordinate.StreetcodeId);
        }
    }
}