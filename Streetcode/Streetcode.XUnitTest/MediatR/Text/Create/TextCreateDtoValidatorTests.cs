namespace Streetcode.XUnitTest.MediatR.Text.Create
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
    using Streetcode.BLL.MediatR.Streetcode.Text.Create;
    using Xunit;

    public class TextCreateDtoValidatorTests
    {
        private readonly TextCreateDtoValidator _validator;

        public TextCreateDtoValidatorTests()
        {
            _validator = new TextCreateDtoValidator();
        }

        private static TextCreateDto CreateValidDto() =>
            new TextCreateDto
            {
                Title = "Valid title",
                TextContent = "Valid text content",
                AdditionalText = "Additional text",
                VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                StreetcodeId = 1,
            };

        [Fact]
        public void Should_Have_Error_When_StreetcodeId_Is_Zero()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.StreetcodeId = 0;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeId)
                  .WithErrorMessage("ID стріткоду має бути більше 0");
        }

        [Fact]
        public void Should_Have_Error_When_StreetcodeId_Is_Negative()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.StreetcodeId = -10;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeId)
                  .WithErrorMessage("ID стріткоду має бути більше 0");
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Null()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.Title = null!;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            Assert.Contains(
                result.Errors,
                e => e.ErrorMessage == "Заголовок тексту є обов'язковим");
        }

        [Fact]
        public void Should_Have_Error_When_TextContent_Is_Null()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.TextContent = null!;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            Assert.True(result.Errors.Count > 0);
        }

        [Fact]
        public void Should_Pass_When_All_Data_Is_Valid()
        {
            // Arrange
            var dto = CreateValidDto();

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Pass_When_VideoUrl_Is_Null()
        {
            // Arrange
            var dto = CreateValidDto();
            dto.VideoUrl = null;

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
