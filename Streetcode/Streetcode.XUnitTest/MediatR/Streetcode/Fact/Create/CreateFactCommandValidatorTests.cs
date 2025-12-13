namespace Streetcode.XUnitTest.MediatR.Fact.Create
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
    using Streetcode.BLL.MediatR.Streetcode.Fact.Create;
    using Xunit;

    public class CreateFactCommandValidatorTests
    {
        private readonly CreateFactCommandValidator _validator;

        public CreateFactCommandValidatorTests()
        {
            _validator = new CreateFactCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Fact_Is_Null()
        {
            // Arrange
            var command = new CreateFactCommand(null);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.newFact)
                  .WithErrorMessage("Дані факту не можуть бути порожніми");
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            // Arrange
            var invalidDto = new CreateFactDto
            {
                Title = string.Empty,
                FactContent = "Valid Content",
                ImageId = 1,
                StreetcodeId = 1,
            };
            var command = new CreateFactCommand(invalidDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            Assert.Contains(result.Errors, e => e.ErrorMessage == "Заголовок факту є обов'язковим");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Fact_Is_Valid()
        {
            // Arrange
            var validDto = new CreateFactDto
            {
                Title = "Valid Title",
                FactContent = "Valid Content",
                ImageId = 1,
                StreetcodeId = 1,
            };
            var command = new CreateFactCommand(validDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}