namespace Streetcode.XUnitTest.MediatR.Fact.Update
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
 using global::Streetcode.BLL.MediatR.Streetcode.Fact.Update;
    using Xunit;

    public class UpdateFactCommandValidatorTests
    {
        private readonly UpdateFactCommandValidator _validator;

        public UpdateFactCommandValidatorTests()
        {
            _validator = new UpdateFactCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_UpdateFact_Is_Null()
        {
            // Arrange
            var command = new UpdateFactCommand(null);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.updateFact)
                  .WithErrorMessage(ErrorMessages.FactDataRequired);
        }

        [Fact]
        public void Should_Not_Have_Error_When_UpdateFact_Is_Valid()
        {
            // Arrange
            var validDto = new UpdateFactDto
            {
                Id = 1,
                ImageId = 1,
                Title = "Valid Title",
                FactContent = "Valid Content",
            };

            var command = new UpdateFactCommand(validDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.updateFact);
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            // Arrange
            var invalidDto = new UpdateFactDto
            {
                Title = string.Empty,
            };

            var command = new UpdateFactCommand(invalidDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.updateFact);
        }
    }
}