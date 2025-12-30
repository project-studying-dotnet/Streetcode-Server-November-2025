namespace Streetcode.XUnitTest.MediatR.Media.Audio.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Media.Audio;
 using global::Streetcode.BLL.MediatR.Media.Audio.Create;
 using global::Streetcode.BLL.Util.Validators;
    using Xunit;

    public class AudioFileBaseCreateDtoValidatorTests
    {
        private readonly AudioFileBaseCreateDtoValidator _validator;

        public AudioFileBaseCreateDtoValidatorTests()
        {
            _validator = new AudioFileBaseCreateDtoValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_BaseFormat_Is_Empty(string baseFormat)
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto { BaseFormat = baseFormat };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BaseFormat);
        }

        [Fact]
        public void Should_Have_Error_When_BaseFormat_Is_Not_Valid_Base64()
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto { BaseFormat = "Not_A_Base_64_String_!@#" };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BaseFormat)
                  .WithErrorMessage(ErrorMessages.AudioBase64Invalid);
        }

        [Fact]
        public void Should_Have_Error_When_BaseFormat_Is_Too_Large()
        {
            // Arrange
            long limitInBytes = ValidationConstants.Media.MaxAudioSizeInBytes;
            long requiredChars = (long)Math.Ceiling(limitInBytes * 4.0 / 3.0);
            int length = (int)(requiredChars + 100);
            length = length + (4 - length % 4);
            string hugeBase64 = new string('A', length);

            var dto = new AudioFileBaseCreateDto { BaseFormat = hugeBase64 };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BaseFormat)
                  .WithErrorMessage(string.Format(ErrorMessages.AudioSizeExceeded, ValidationConstants.Media.MaxAudioSizeInBytes / 1024 / 1024));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Extension_Is_Empty(string extension)
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto { Extension = extension };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Extension);
        }

        [Fact]
        public void Should_Have_Error_When_Extension_Is_Not_Allowed()
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto { Extension = "exe" };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Extension);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Extension_Is_Allowed()
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto { Extension = "mp3" };
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
            var dto = new AudioFileBaseCreateDto
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
            var dto = new AudioFileBaseCreateDto
            {
                Title = new string('a', ValidationConstants.Media.TitleMaxLength + 1),
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Is_Too_Long()
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto
            {
                Description = new string('a', ValidationConstants.Media.DescriptionMaxLength + 1),
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Fact]
        public void Should_Be_Valid_With_Correct_Data()
        {
            // Arrange
            var dto = new AudioFileBaseCreateDto
            {
                BaseFormat = "U3RyZWV0Y29kZQ==",
                Extension = "mp3",
                MimeType = "audio/mp3",
                Title = "Test Audio",
                Description = "Test Description",
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}