namespace Streetcode.XUnitTest.MediatR.Text.Create
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
    using Streetcode.BLL.MediatR.Streetcode.Text.Create;
    using Xunit;

    public class CreateTextCommandValidatorTests
    {
        private readonly CreateTextCommandValidator _validator;

        public CreateTextCommandValidatorTests()
        {
            _validator = new CreateTextCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Text_Is_Null()
        {
            // Arrange
            var command = new CreateTextCommand(null);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Text)
                  .WithErrorMessage("Дані тексту не можуть бути порожніми");
        }

        [Fact]
        public void Should_Pass_When_Text_Is_Valid_Without_Video()
        {
            // Arrange
            var dto = new TextCreateDto
            {
                Title = "Test title",
                TextContent = "Some content",
                AdditionalText = "Additional",
                VideoUrl = null,
                StreetcodeId = 1,
            };

            var command = new CreateTextCommand(dto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Pass_When_Text_Is_Valid_With_Youtube_Video()
        {
            // Arrange
            var dto = new TextCreateDto
            {
                Title = "Test title",
                TextContent = "Some content",
                VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                StreetcodeId = 1,
            };

            var command = new CreateTextCommand(dto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
