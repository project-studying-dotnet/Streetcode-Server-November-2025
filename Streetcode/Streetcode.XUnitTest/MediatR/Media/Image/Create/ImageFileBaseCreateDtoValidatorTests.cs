namespace Streetcode.XUnitTest.MediatR.Media.Image.Create
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.Media.Images;
    using Streetcode.BLL.MediatR.Media.Image.Create;
    using Streetcode.BLL.Util.Validators;
    using Xunit;

    public class ImageFileBaseCreateDtoValidatorTests
    {
        private readonly ImageFileBaseCreateDtoValidator _validator;

        public ImageFileBaseCreateDtoValidatorTests()
        {
            _validator = new ImageFileBaseCreateDtoValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_BaseFormat_Is_Empty(string baseFormat)
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto { BaseFormat = baseFormat };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BaseFormat);
        }

        [Fact]
        public void Should_Have_Error_When_BaseFormat_Is_Not_Valid_Base64()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto { BaseFormat = "Invalid_Base64_String!!!" };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BaseFormat)
                  .WithErrorMessage(ErrorMessages.ImageBase64Invalid);
        }

        [Fact]
        public void Should_Have_Error_When_BaseFormat_Is_Too_Large()
        {
            // Arrange
            long limitInBytes = ValidationConstants.Media.MaxImageSizeInBytes;
            long requiredChars = (long)Math.Ceiling(limitInBytes * 4.0 / 3.0);
            int length = (int)(requiredChars + 100);
            length = length + (4 - length % 4);

            string hugeBase64 = new string('A', length);
            var dto = new ImageFileBaseCreateDto { BaseFormat = hugeBase64 };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BaseFormat)
                  .WithErrorMessage(string.Format(ErrorMessages.ImageSizeExceeded, ValidationConstants.Media.MaxImageSizeInBytes / 1024 / 1024));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Extension_Is_Empty(string extension)
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto { Extension = extension };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Extension);
        }

        [Fact]
        public void Should_Have_Error_When_Extension_Is_Not_Allowed()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto { Extension = "txt" };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Extension);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Extension_Is_Allowed()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto { Extension = "png" };
            dto.BaseFormat = "U3RyZWV0Y29kZQ==";

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Extension);
        }

        [Fact]
        public void Should_Have_Error_When_MimeType_Is_Too_Long()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto
            {
                MimeType = new string('a', ValidationConstants.Common.MimeTypeMaxLength + 1),
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.MimeType);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Too_Long()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto
            {
                Title = new string('a', ValidationConstants.Media.TitleMaxLength + 1),
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Alt_Is_Too_Long()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto
            {
                Alt = new string('a', ValidationConstants.Media.AltMaxLength + 1),
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Alt);
        }

        [Fact]
        public void Should_Be_Valid_With_Correct_Data()
        {
            // Arrange
            var dto = new ImageFileBaseCreateDto
            {
                BaseFormat = "U3RyZWV0Y29kZQ==",
                Extension = "jpeg",
                MimeType = "image/jpeg",
                Title = "Test Image",
                Alt = "Test Alt Text",
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}