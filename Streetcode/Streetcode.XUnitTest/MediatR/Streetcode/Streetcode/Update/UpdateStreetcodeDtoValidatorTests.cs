namespace Streetcode.XUnitTest.MediatR.Streetcodes.Update
{
    using System.Text.Json;
    using FluentValidation.TestHelper;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.Update;
    using Xunit;

    public class UpdateStreetcodeDtoValidatorTests
    {
        private readonly UpdateStreetcodeDtoValidator _validator;

        public UpdateStreetcodeDtoValidatorTests()
        {
            _validator = new UpdateStreetcodeDtoValidator();
        }

        private static JsonElement Parse(string json)
            => JsonDocument.Parse(json).RootElement;

        [Fact]
        public void Should_Have_Error_When_Id_Is_Missing()
        {
            // Arrange
            var json = Parse(@"{
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""Title"": ""Test"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url""
            }");

            // Act
            var result = _validator.TestValidate(json);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage("Id є обов'язковим");
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Not_Integer()
        {
            // Arrange
            var json = Parse(@"{
                ""Id"": ""abc"",
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""Title"": ""Test"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url""
            }");

            // Act
            var result = _validator.TestValidate(json);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage("Id має бути цілим числом");
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            // Arrange
            var json = Parse(@"{
                ""Id"": 0,
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""Title"": ""Test"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url""
            }");

            // Act
            var result = _validator.TestValidate(json);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage("Id має бути більше 0");
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Negative()
        {
            // Arrange
            var json = Parse(@"{
                ""Id"": -5,
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""Title"": ""Test"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url""
            }");

            // Act
            var result = _validator.TestValidate(json);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage("Id має бути більше 0");
        }

        [Fact]
        public void Should_Pass_When_All_Data_Is_Valid()
        {
            // Arrange
            var json = Parse(@"{
                ""Id"": 1,
                ""Index"": 10,
                ""StreetcodeType"": ""Person"",
                ""Title"": ""Valid title"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""EventEndOrPersonDeathDate"": ""2020-01-01"",
                ""TransliterationUrl"": ""valid-url"",
                ""AudioId"": 5
            }");

            // Act
            var result = _validator.TestValidate(json);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
