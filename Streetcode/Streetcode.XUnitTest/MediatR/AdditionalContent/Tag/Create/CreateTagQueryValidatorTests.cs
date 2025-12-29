namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.AdditionalContent.Tag;
 using global::Streetcode.BLL.MediatR.AdditionalContent.Tag.Create;
    using Xunit;

    public class CreateTagQueryValidatorTests
    {
        private readonly CreateTagQueryValidator _validator;

        public CreateTagQueryValidatorTests()
        {
            _validator = new CreateTagQueryValidator();
        }

        [Fact]
        public void Should_Have_Error_When_TagDto_Is_Null()
        {
            // Arrange
            var query = new CreateTagCommand(null);

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.tag)
                  .WithErrorMessage(ErrorMessages.TagDataCantBeEmpty);
        }

        [Fact]
        public void Should_Not_Have_Error_When_TagDto_Is_Valid()
        {
            // Arrange
            var validDto = new CreateTagDto { Title = "Valid Title" };
            var query = new CreateTagCommand(validDto);

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.tag);
        }

        [Fact]
        public void Should_Have_Error_When_Nested_TagDto_Is_Invalid()
        {
            // Arrange
            var invalidDto = new CreateTagDto { Title = string.Empty };
            var query = new CreateTagCommand(invalidDto);

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.tag.Title)
                  .WithErrorMessage(ErrorMessages.TagNameIsRequired);
        }
    }
}