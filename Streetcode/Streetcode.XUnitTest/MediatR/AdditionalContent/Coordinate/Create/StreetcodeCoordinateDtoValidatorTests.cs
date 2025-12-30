namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Coordinate.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
 using global::Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
 using global::Streetcode.BLL.Util.Validators;
    using Xunit;

    public class StreetcodeCoordinateDtoValidatorTests
    {
        private readonly StreetcodeCoordinateDtoValidator _validator;

        public StreetcodeCoordinateDtoValidatorTests()
        {
            _validator = new StreetcodeCoordinateDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_StreetcodeId_Is_Invalid()
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { StreetcodeId = ValidationConstants.Common.MinPositiveValue };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeId);
        }

        [Fact]
        public void Should_Have_Error_When_Latitude_Is_Too_Low()
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { Latitude = ValidationConstants.Coordinate.MinLatitude - 0.0001m };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Latitude);
        }

        [Fact]
        public void Should_Have_Error_When_Latitude_Is_Too_High()
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { Latitude = ValidationConstants.Coordinate.MaxLatitude + 0.0001m };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Latitude);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Dto_Is_Valid()
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto
            {
                StreetcodeId = 1,
                Latitude = 0,
                Longtitude = 0,
                Id = 1
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}