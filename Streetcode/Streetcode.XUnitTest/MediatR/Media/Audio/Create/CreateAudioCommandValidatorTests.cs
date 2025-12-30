namespace Streetcode.XUnitTest.MediatR.Media.Audio.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL.DTO.Media.Audio;
 using global::Streetcode.BLL.MediatR.Media.Audio.Create;
    using Xunit;

    public class CreateAudioCommandValidatorTests
    {
        private readonly CreateAudioCommandValidator _validator;

        public CreateAudioCommandValidatorTests()
        {
            _validator = new CreateAudioCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Audio_Is_Null()
        {
            // Arrange
            var command = new CreateAudioCommand(null);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Audio);
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            // Arrange
            var invalidDto = new AudioFileBaseCreateDto
            {
                BaseFormat = string.Empty,
                Extension = "mp3",
            };
            var command = new CreateAudioCommand(invalidDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Audio.BaseFormat);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Audio_Is_Valid()
        {
            // Arrange
            var validDto = new AudioFileBaseCreateDto
            {
                BaseFormat = "U3RyZWV0Y29kZQ==",
                Extension = "mp3",
                Title = "Valid Title"
            };
            var command = new CreateAudioCommand(validDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}