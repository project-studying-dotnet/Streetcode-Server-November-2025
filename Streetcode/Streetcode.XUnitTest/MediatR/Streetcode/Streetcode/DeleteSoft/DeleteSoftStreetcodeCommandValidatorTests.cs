namespace Streetcode.XUnitTest.MediatR.Streetcodes.DeleteSoft
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.DeleteSoft;
    using Xunit;

    public class DeleteSoftStreetcodeCommandValidatorTests
    {
        private readonly DeleteSoftStreetcodeCommandValidator _validator;

        public DeleteSoftStreetcodeCommandValidatorTests()
        {
            _validator = new DeleteSoftStreetcodeCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            // Arrange
            var command = new DeleteSoftStreetcodeCommand(0);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Id має бути більше 0");
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Negative()
        {
            // Arrange
            var command = new DeleteSoftStreetcodeCommand(-1);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Id має бути більше 0");
        }

        [Fact]
        public void Should_Pass_When_Id_Is_Positive()
        {
            // Arrange
            var command = new DeleteSoftStreetcodeCommand(1);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}
