namespace Streetcode.XUnitTest.MediatR.Partners.Create
{
    using System.Collections.Generic;
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Partners;
    using Streetcode.BLL.DTO.Streetcode;
    using Streetcode.BLL.MediatR.Partners.Create;
    using Xunit;

    public class CreatePartnerQueryValidatorTests
    {
        private readonly CreatePartnerQueryValidator _validator;

        public CreatePartnerQueryValidatorTests()
        {
            _validator = new CreatePartnerQueryValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Partner_Is_Null()
        {
            var query = new CreatePartnerCommand(null);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.newPartner);
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            var invalidDto = new CreatePartnerDto { Title = string.Empty };
            var query = new CreatePartnerCommand(invalidDto);

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.newPartner.Title);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Partner_Is_Valid()
        {
            var validDto = new CreatePartnerDto
            {
                Title = "Valid Partner",
                TargetUrl = "https://google.com",
                LogoId = 1,
                Streetcodes = new List<StreetcodeShortDto> { new StreetcodeShortDto { Id = 1 } },
            };
            var query = new CreatePartnerCommand(validDto);

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}