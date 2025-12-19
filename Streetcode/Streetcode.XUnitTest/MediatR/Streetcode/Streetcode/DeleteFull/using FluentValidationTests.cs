namespace Streetcode.XUnitTest.MediatR.Streetcodes.DeleteFull
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.DeleteFull;
    using Xunit;

    public class DeleteFullStreetcodeCommandValidatorTests
    {
        private readonly DeleteFullStreetcodeCommandValidator _validator;

        public DeleteFullStreetcodeCommandValidatorTests()
        {
            _validator = new DeleteFullStreetcodeCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            // Arrange
            var command = new DeleteFullStreetcodeCommand(0);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.IdMustBeGreaterThan);
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Negative()
        {
            // Arrange
            var command = new DeleteFullStreetcodeCommand(-1);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.IdMustBeGreaterThan);
        }

        [Fact]
        public void Should_Pass_When_Id_Is_Positive()
        {
            // Arrange
            var command = new DeleteFullStreetcodeCommand(1);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}
