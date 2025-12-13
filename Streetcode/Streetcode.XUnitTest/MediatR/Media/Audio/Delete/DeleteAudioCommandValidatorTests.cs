namespace Streetcode.XUnitTest.MediatR.Media.Audio.Delete
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.MediatR.Media.Audio.Delete;
    using Xunit;

    public class DeleteAudioCommandValidatorTests
    {
        private readonly DeleteAudioCommandValidator _validator;

        public DeleteAudioCommandValidatorTests()
        {
            _validator = new DeleteAudioCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Should_Have_Error_When_Id_Is_Invalid(int id)
        {
            // Arrange
            var command = new DeleteAudioCommand(id);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("ID аудіо має бути більше 0");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        public void Should_Not_Have_Error_When_Id_Is_Valid(int id)
        {
            // Arrange
            var command = new DeleteAudioCommand(id);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}