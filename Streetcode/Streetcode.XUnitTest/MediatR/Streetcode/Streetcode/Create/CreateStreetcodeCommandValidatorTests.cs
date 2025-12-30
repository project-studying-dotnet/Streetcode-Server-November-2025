namespace Streetcode.XUnitTest.MediatR.Streetcodes.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
    using System.Text.Json;
    using Xunit;

    public class CreateStreetcodeCommandValidatorTests
    {
        private readonly CreateStreetcodeCommandValidator _validator;

        public CreateStreetcodeCommandValidatorTests()
        {
            _validator = new CreateStreetcodeCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Json_Is_Empty()
        {
            // Arrange
            var emptyJson = default(JsonElement);
            var command = new CreateStreetcodeCommand(emptyJson);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.rawJsonCreateDTO)
                  .WithErrorMessage(ErrorMessages.StreetcodeDataRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Json_Is_Array()
        {
            // Arrange
            var jsonArray = JsonDocument.Parse("[]").RootElement;
            var command = new CreateStreetcodeCommand(jsonArray);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.rawJsonCreateDTO)
                  .WithErrorMessage(ErrorMessages.WrongJSONStructure);
        }

        [Fact]
        public void Should_Have_Error_When_Json_Is_Value()
        {
            // Arrange
            var jsonValue = JsonDocument.Parse("\"Just a string\"").RootElement;
            var command = new CreateStreetcodeCommand(jsonValue);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.rawJsonCreateDTO)
                  .WithErrorMessage(ErrorMessages.WrongJSONStructure);
        }

        [Fact]
        public void Should_Pass_Structural_Validation_When_Json_Is_Object()
        {
            // Arrange
            var jsonObject = JsonDocument.Parse("{\"Title\": " + "\"Test Title\"" + "}").RootElement;
            var command = new CreateStreetcodeCommand(jsonObject);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.rawJsonCreateDTO);
        }
    }
}