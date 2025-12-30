namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Coordinate.Update
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
 using global::Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update;
    using Xunit;

    public class UpdateStreetcodeCoordinateDtoValidatorTests
    {
        private readonly UpdateStreetcodeCoordinateDtoValidator _validator;

        public UpdateStreetcodeCoordinateDtoValidatorTests()
        {
            _validator = new UpdateStreetcodeCoordinateDtoValidator();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(123)]
        public void Should_Not_Have_Error_When_Id_Is_Valid(int id)
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { Id = id };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_Id_Is_Invalid(int id)
        {
            // Arrange
            var dto = new StreetcodeCoordinateDto { Id = id };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.CoordinateIdMustBeGreaterThanZero);
        }
    }
}