namespace Streetcode.XUnitTest.MediatR.Text.Update
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
    using Streetcode.BLL.MediatR.Streetcode.Text.Update;
    using Xunit;

    public class UpdateTextCommandValidatorTests
    {
        private readonly UpdateTextCommandValidator _validator;

        public UpdateTextCommandValidatorTests()
        {
            _validator = new UpdateTextCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_Id_Is_Invalid(int id)
        {
            // Arrange
            var command = new UpdateTextCommand(id, null!);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.TextIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Have_Error_When_Text_Is_Null()
        {
            // Arrange
            var command = new UpdateTextCommand(1, null!);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Text)
                  .WithErrorMessage(ErrorMessages.TextDataRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            // Arrange
            var invalidDto = new TextUpdateDto
            {
                Title = "",
                TextContent = "Some content",
            };
            var command = new UpdateTextCommand(1, invalidDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Text);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            // Arrange
            var validDto = new TextUpdateDto
            {
                Title = "Valid Title",
                TextContent = "Some valid content",
                AdditionalText = null,
                VideoUrl = null,
            };
            var command = new UpdateTextCommand(1, validDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}