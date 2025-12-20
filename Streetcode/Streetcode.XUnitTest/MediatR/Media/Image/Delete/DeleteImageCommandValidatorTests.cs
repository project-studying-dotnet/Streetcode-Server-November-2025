namespace Streetcode.XUnitTest.MediatR.Media.Image.Delete
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL;
    using Streetcode.BLL.MediatR.Media.Image.Delete;
    using Xunit;

    public class DeleteImageCommandValidatorTests
    {
        private readonly DeleteImageCommandValidator _validator;

        public DeleteImageCommandValidatorTests()
        {
            _validator = new DeleteImageCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void Should_Have_Error_When_Id_Is_Invalid(int id)
        {
            // Arrange
            var command = new DeleteImageCommand(id);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.ImageIdMustBeGreaterThanZero);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void Should_Not_Have_Error_When_Id_Is_Valid(int id)
        {
            // Arrange
            var command = new DeleteImageCommand(id);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}