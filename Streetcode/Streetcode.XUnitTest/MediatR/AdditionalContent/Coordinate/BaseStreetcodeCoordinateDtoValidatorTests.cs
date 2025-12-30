namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Coordinate
{
    using FluentValidation.TestHelper;
    using global::Streetcode.BLL;
    using global::Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
    using global::Streetcode.BLL.MediatR.AdditionalContent.Coordinate;
    using global::Streetcode.BLL.Util.Validators;
    using Xunit;

    public class BaseStreetcodeCoordinateDtoValidatorTests
    {
        private readonly TestableBaseStreetcodeCoordinateDtoValidator _validator;

        public BaseStreetcodeCoordinateDtoValidatorTests()
        {
            _validator = new TestableBaseStreetcodeCoordinateDtoValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(90)]
        [InlineData(-90)]
        [InlineData(45.5)]
        public void Should_Not_Have_Error_When_Latitude_Is_Valid(decimal latitude)
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { Latitude = latitude };

            // Act
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Latitude);
        }

        [Theory]
        [InlineData(90.0001)]
        [InlineData(-90.0001)]
        [InlineData(100)]
        [InlineData(-100)]
        public void Should_Have_Error_When_Latitude_Is_Invalid(decimal latitude)
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { Latitude = latitude };

            // Act
            var result = _validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.Latitude)
                  .WithErrorMessage(string.Format(
                      ErrorMessages.CoordinateWidthError,
                      ValidationConstants.Coordinate.MinLatitude,
                      ValidationConstants.Coordinate.MaxLatitude));
        }

        // 2. Тести для Longtitude (Довгота)
        [Theory]
        [InlineData(0)]
        [InlineData(180)]
        [InlineData(-180)]
        [InlineData(50.123)]
        public void Should_Not_Have_Error_When_Longtitude_Is_Valid(decimal longtitude)
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { Longtitude = longtitude };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Longtitude);
        }

        [Theory]
        [InlineData(180.0001)]
        [InlineData(-180.0001)]
        [InlineData(200)]
        public void Should_Have_Error_When_Longtitude_Is_Invalid(decimal longtitude)
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { Longtitude = longtitude };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Longtitude)
                  .WithErrorMessage(string.Format(
                      ErrorMessages.CoordinateHeightError,
                      ValidationConstants.Coordinate.MinLongitude,
                      ValidationConstants.Coordinate.MaxLongitude));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void Should_Not_Have_Error_When_StreetcodeId_Is_Valid(int id)
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { StreetcodeId = id };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.StreetcodeId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_StreetcodeId_Is_Invalid(int id)
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { StreetcodeId = id };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeId)
                  .WithErrorMessage(ErrorMessages.StreetcodeIdMustBeGreaterThanZero);
        }

        /// <summary>
        /// Stub class for testing the abstract validator.
        /// It inherits the abstract class and makes the rule call public.
        /// </summary>
        private class TestableBaseStreetcodeCoordinateDtoValidator : BaseStreetcodeCoordinateDtoValidator
        {
            public TestableBaseStreetcodeCoordinateDtoValidator()
            {
                ConfigureSharedRules();
            }
        }
    }
}