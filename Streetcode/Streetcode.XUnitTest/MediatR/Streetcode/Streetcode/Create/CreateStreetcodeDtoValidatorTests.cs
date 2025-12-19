namespace Streetcode.XUnitTest.MediatR.Streetcodes.Create
{
    using System.Text.Json;
    using FluentValidation.TestHelper;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
    using Xunit;

    public class CreateStreetcodeDtoValidatorTests
    {
        private readonly CreateStreetcodeDtoValidator _validator;

        public CreateStreetcodeDtoValidatorTests()
        {
            _validator = new CreateStreetcodeDtoValidator();
        }

        private static JsonElement Parse(string json)
            => JsonDocument.Parse(json).RootElement;

        [Fact]
        public void Should_Have_Error_When_Index_Is_Missing()
        {
            var json = Parse(@"{
                ""Title"": ""Test"",
                ""StreetcodeType"": ""Person"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url""
            }");

            var result = _validator.TestValidate(json, options => options.IncludeRuleSets("RequiredFields"));

            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage("Index є обов'язковим");
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Missing()
        {
            var json = Parse(@"{
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url""
            }");

            var result = _validator.TestValidate(json, options => options.IncludeRuleSets("RequiredFields"));

            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage("Назва є обов'язковою");
        }

        [Fact]
        public void Should_Have_Error_When_Index_Is_Not_Integer()
        {
            var json = Parse(@"{
                ""Index"": ""abc"",
                ""StreetcodeType"": ""Person"",
                ""Title"": ""Test"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url""
            }");

            var result = _validator.TestValidate(json, options => options.IncludeRuleSets("DataTypes"));

            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage("Index має бути цілим числом");
        }

        [Fact]
        public void Should_Have_Error_When_StreetcodeType_Is_Invalid()
        {
            var json = Parse(@"{
                ""Index"": 1,
                ""StreetcodeType"": ""Invalid"",
                ""Title"": ""Test"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url""
            }");

            var result = _validator.TestValidate(json, options => options.IncludeRuleSets("DataTypes"));

            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage(result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var json = Parse(@"{
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""Title"": """",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url""
            }");

            var result = _validator.TestValidate(json, options => options.IncludeRuleSets("StringContent"));

            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage("Назва не може бути порожньою");
        }

        [Fact]
        public void Should_Have_Error_When_TransliterationUrl_Is_Empty()
        {
            var json = Parse(@"{
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""Title"": ""Test"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": """"
            }");

            var result = _validator.TestValidate(json, options => options.IncludeRuleSets("StringContent"));

            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage("TransliterationUrl не може бути порожнім");
        }

        [Fact]
        public void Should_Have_Error_When_Title_Too_Long()
        {
            var longTitle = new string('a', 300);

            var json = Parse($@"{{
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""Title"": ""{longTitle}"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url""
            }}");

            var result = _validator.TestValidate(json, options => options.IncludeRuleSets("LengthConstraints"));

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Should_Have_Error_When_EndDate_Is_Before_StartDate()
        {
            var json = Parse(@"{
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""Title"": ""Test"",
                ""EventStartOrPersonBirthDate"": ""2020-01-01"",
                ""EventEndOrPersonDeathDate"": ""2010-01-01"",
                ""TransliterationUrl"": ""test-url""
            }");

            var result = _validator.TestValidate(json, options => options.IncludeRuleSets("BusinessRules"));

            result.ShouldHaveValidationErrorFor(x => x)
                  .WithErrorMessage("EventEndOrPersonDeathDate має бути пізніше ніж EventStartOrPersonBirthDate");
        }

        [Fact]
        public void Should_Have_Error_When_AudioId_Is_Not_Positive()
        {
            var json = Parse(@"{
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""Title"": ""Test"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""TransliterationUrl"": ""test-url"",
                ""AudioId"": 0
            }");

            var result = _validator.TestValidate(json, options => options.IncludeRuleSets("BusinessRules"));

            result.ShouldHaveValidationErrorFor(x => x);
        }

        [Fact]
        public void Should_Pass_All_Validations_For_Valid_Json()
        {
            var json = Parse(@"{
                ""Index"": 1,
                ""StreetcodeType"": ""Person"",
                ""Title"": ""Valid title"",
                ""EventStartOrPersonBirthDate"": ""2000-01-01"",
                ""EventEndOrPersonDeathDate"": ""2020-01-01"",
                ""TransliterationUrl"": ""valid-url"",
                ""AudioId"": 10
            }");

            var result = _validator.TestValidate(
                json,
                options => options.IncludeRuleSets(
                    "RequiredFields",
                    "DataTypes",
                    "StringContent",
                    "LengthConstraints",
                    "BusinessRules"));

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
