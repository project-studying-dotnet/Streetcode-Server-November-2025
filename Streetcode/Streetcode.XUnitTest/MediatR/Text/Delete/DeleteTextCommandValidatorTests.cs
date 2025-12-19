namespace Streetcode.XUnitTest.MediatR.Text.Delete
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.MediatR.Streetcode.Text.Delete;
    using Xunit;

    public class DeleteTextCommandValidatorTests
    {
        private readonly DeleteTextCommandValidator _validator;

        public DeleteTextCommandValidatorTests()
        {
            _validator = new DeleteTextCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            // Arrange
            var command = new DeleteTextCommand(0);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("ID тексту має бути більше 0");
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Negative()
        {
            // Arrange
            var command = new DeleteTextCommand(-5);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("ID тексту має бути більше 0");
        }

        [Fact]
        public void Should_Pass_When_Id_Is_Positive()
        {
            // Arrange
            var command = new DeleteTextCommand(1);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}
