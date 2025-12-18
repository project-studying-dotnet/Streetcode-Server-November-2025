namespace Streetcode.XUnitTest.MediatR.Streetcodes.Update
{
    using System.Text.Json;
    using FluentValidation.TestHelper;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.Update;
    using Xunit;

    public class UpdateStreetcodeCommandValidatorTests
    {
        private readonly UpdateStreetcodeCommandValidator _validator;

        public UpdateStreetcodeCommandValidatorTests()
        {
            _validator = new UpdateStreetcodeCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            // Arrange
            var json = JsonDocument.Parse("{}").RootElement;
            var command = new UpdateStreetcodeCommand(0, json);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.id)
                  .WithErrorMessage("Id має бути більше 0");
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Negative()
        {
            // Arrange
            var json = JsonDocument.Parse("{}").RootElement;
            var command = new UpdateStreetcodeCommand(-1, json);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.id)
                  .WithErrorMessage("Id має бути більше 0");
        }

        [Fact]
        public void Should_Pass_When_Id_Is_Positive()
        {
            // Arrange
            var json = JsonDocument.Parse("{}").RootElement;
            var command = new UpdateStreetcodeCommand(1, json);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.id);
        }

        [Fact]
        public void Should_Have_Error_When_Json_Is_Empty()
        {
            // Arrange
            var emptyJson = default(JsonElement);
            var command = new UpdateStreetcodeCommand(1, emptyJson);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.rawJsonUpdateDTO)
                  .WithErrorMessage("Дані стріткоду є обов'язковими");
        }

        [Fact]
        public void Should_Have_Error_When_Json_Is_Array()
        {
            // Arrange
            var jsonArray = JsonDocument.Parse("[]").RootElement;
            var command = new UpdateStreetcodeCommand(1, jsonArray);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.rawJsonUpdateDTO)
                  .WithErrorMessage("Невірна структура JSON");
        }

        [Fact]
        public void Should_Have_Error_When_Json_Is_Value()
        {
            // Arrange
            var jsonValue = JsonDocument.Parse("\"Just a string\"").RootElement;
            var command = new UpdateStreetcodeCommand(1, jsonValue);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.rawJsonUpdateDTO)
                  .WithErrorMessage("Невірна структура JSON");
        }

        [Fact]
        public void Should_Pass_Structural_Validation_When_Json_Is_Object()
        {
            // Arrange
            var jsonObject = JsonDocument.Parse("{\"Title\": \"Test\", \"Id\": 1}").RootElement;
            var command = new UpdateStreetcodeCommand(1, jsonObject);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.rawJsonUpdateDTO);
        }

        [Fact]
        public void Should_Trigger_Dto_Validator_When_Json_Is_Object()
        {
            // Arrange
            var invalidDtoJson = JsonDocument.Parse("{}").RootElement;
            var command = new UpdateStreetcodeCommand(1, invalidDtoJson);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            Assert.True(result.Errors.Count > 0);
        }
    }
}
