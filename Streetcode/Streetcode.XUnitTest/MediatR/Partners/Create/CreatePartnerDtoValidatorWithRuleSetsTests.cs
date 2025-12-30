namespace Streetcode.XUnitTest.MediatR.Partners.Create
{
    using FluentValidation;
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL.DTO.Partners;
 using global::Streetcode.BLL.DTO.Streetcode;
 using global::Streetcode.BLL.MediatR.Partners.Create;
 using global::Streetcode.BLL.Util.Validators;
    using Xunit;

    public class CreatePartnerDtoValidatorWithRuleSetsTests
    {
        private readonly CreatePartnerDtoValidatorWithRuleSets _validator;

        public CreatePartnerDtoValidatorWithRuleSetsTests()
        {
            _validator = new CreatePartnerDtoValidatorWithRuleSets();
        }

        [Fact]
        public void Should_Validate_RequiredFields_RuleSet()
        {
            var dto = new CreatePartnerDto
            {
                Title = string.Empty,
                LogoId = 0,
                Streetcodes = null,
            };

            var result = _validator.TestValidate(dto, options => options.IncludeRuleSets("RequiredFields"));

            result.ShouldHaveValidationErrorFor(x => x.Title);
            result.ShouldHaveValidationErrorFor(x => x.LogoId);
            result.ShouldHaveValidationErrorFor(x => x.Streetcodes);
        }

        [Fact]
        public void Should_Validate_LengthConstraints_RuleSet()
        {
            var dto = new CreatePartnerDto
            {
                Title = new string('a', ValidationConstants.Partner.TitleMaxLength + 1),
                Description = new string('a', ValidationConstants.Partner.DescriptionMaxLength + 1),
                UrlTitle = new string('a', ValidationConstants.Partner.UrlTitleMaxLength + 1),
            };

            var result = _validator.TestValidate(dto, options => options.IncludeRuleSets("LengthConstraints"));

            result.ShouldHaveValidationErrorFor(x => x.Title);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.UrlTitle);
        }

        [Fact]
        public void Should_Validate_FormatValidation_RuleSet()
        {
            var dto = new CreatePartnerDto
            {
                TargetUrl = "invalid-url",
            };

            var result = _validator.TestValidate(dto, options => options.IncludeRuleSets("FormatValidation"));

            result.ShouldHaveValidationErrorFor(x => x.TargetUrl);
        }

        [Fact]
        public void Should_Pass_All_RuleSets_When_Valid()
        {
            var dto = new CreatePartnerDto
            {
                Title = "Valid",
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto>(),
                TargetUrl = "https://valid.com",
                Description = "Desc",
                UrlTitle = "UrlTitle",
            };

            var result = _validator.TestValidate(dto, options => options.IncludeAllRuleSets());

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}