namespace Streetcode.XUnitTest.MediatR.Media.Image.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL.DTO.Media.Images;
 using global::Streetcode.BLL.MediatR.Media.Image.Create;
    using Xunit;

    public class CreateImageCommandValidatorTests
    {
        private readonly CreateImageCommandValidator _validator;

        public CreateImageCommandValidatorTests()
        {
            _validator = new CreateImageCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Image_Is_Null()
        {
            // Arrange
            var command = new CreateImageCommand(null);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Image);
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            // Arrange
            var invalidDto = new ImageFileBaseCreateDto
            {
                BaseFormat = string.Empty,
                Extension = "png",
            };
            var command = new CreateImageCommand(invalidDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Image.BaseFormat);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Image_Is_Valid()
        {
            // Arrange
            var validDto = new ImageFileBaseCreateDto
            {
                BaseFormat = "U3RyZWV0Y29kZQ==",
                Extension = "png",
                Title = "Valid Title",
            };
            var command = new CreateImageCommand(validDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}