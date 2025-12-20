namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Coordinate.Update
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
    using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update;
    using Xunit;

    public class UpdateCoordinateCommandValidatorTests
    {
        private readonly UpdateCoordinateCommandValidator _validator;

        public UpdateCoordinateCommandValidatorTests()
        {
            _validator = new UpdateCoordinateCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_StreetcodeCoordinate_Is_Null()
        {
            // Arrange
            var command = new UpdateCoordinateCommand(null);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeCoordinate)
                  .WithErrorMessage(ErrorMessages.CoordinateCantBeEmpty);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Dto_Is_Valid()
        {
            // Arrange
            var validDto = new StreetcodeCoordinateDto { Id = 1 };
            var command = new UpdateCoordinateCommand(validDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.StreetcodeCoordinate);
        }

        [Fact]
        public void Should_Have_Error_When_Child_Dto_Is_Invalid()
        {
            // Arrange
            var invalidDto = new StreetcodeCoordinateDto { Id = 0 };
            var command = new UpdateCoordinateCommand(invalidDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeCoordinate.Id)
                  .WithErrorMessage(ErrorMessages.CoordinateIdMustBeGreaterThanZero);
        }
    }
}