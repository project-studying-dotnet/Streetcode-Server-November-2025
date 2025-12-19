namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.Create
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.AdditionalContent.Tag;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.Create;
    using Streetcode.BLL.Util.Validators;
    using Xunit;

    public class CreateTagDtoValidatorTests
    {
        private readonly CreateTagDtoValidator _validator;

        public CreateTagDtoValidatorTests()
        {
            _validator = new CreateTagDtoValidator();
        }

        [Theory]
        [InlineData("History")]
        [InlineData("Історія України")]
        [InlineData("IT-Technology")]
        [InlineData("1990 рік")]
        public void Should_Not_Have_Error_When_Title_Is_Valid(string title)
        {
            // Arrange
            var dto = new CreateTagDto { Title = title };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void Should_Have_Error_When_Title_Is_Empty(string title)
        {
            // Arrange
            var dto = new CreateTagDto { Title = title };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage("Назва тегу є обов'язковою");
        }

        [Fact]
        public void Should_Have_Error_When_Title_Exceeds_MaxLength()
        {
            // Arrange
            int maxLength = ValidationConstants.Tag.TitleMaxLength;
            string longTitle = new string('a', maxLength + 1);

            var dto = new CreateTagDto { Title = longTitle };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage($"Назва тегу не може перевищувати {maxLength} символів");
        }

        [Theory]
        [InlineData("Tag!")]
        [InlineData("Tag_Name")]
        [InlineData("Tag@Name")]
        [InlineData("Tag.Name")]
        public void Should_Have_Error_When_Title_Contains_Invalid_Characters(string title)
        {
            // Arrange
            var dto = new CreateTagDto { Title = title };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage("Назва тегу може містити лише літери, цифри, пробіли та дефіси");
        }
    }
}