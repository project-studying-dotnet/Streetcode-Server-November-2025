namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Coordinate.Delete
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete;
    using Xunit;

    public class DeleteCoordinateCommandValidatorTests
    {
        private readonly DeleteCoordinateCommandValidator _validator;

        public DeleteCoordinateCommandValidatorTests()
        {
            _validator = new DeleteCoordinateCommandValidator();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(int.MaxValue)]
        public void Should_Not_Have_Error_When_Id_Is_Valid(int id)
        {
            // Arrange
            var command = new DeleteCoordinateCommand(id);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Should_Have_Error_When_Id_Is_Invalid(int id)
        {
            // Arrange
            var command = new DeleteCoordinateCommand(id);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.CoordinateIdMustBeGreaterThanZero);
        }
    }
}