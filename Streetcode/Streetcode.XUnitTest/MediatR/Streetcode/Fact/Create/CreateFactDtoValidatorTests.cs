namespace Streetcode.XUnitTest.MediatR.Fact.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
 using global::Streetcode.BLL.MediatR.Streetcode.Fact.Create;
 using global::Streetcode.BLL.Util.Validators;
    using Xunit;

    public class CreateFactDtoValidatorTests
    {
        private readonly CreateFactDtoValidator _validator;

        public CreateFactDtoValidatorTests()
        {
            _validator = new CreateFactDtoValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_StreetcodeId_Is_Invalid(int id)
        {
            var dto = CreateValidDto();
            dto.StreetcodeId = id;

            var result = _validator.TestValidate(dto);

            result.ShouldHaveValidationErrorFor(x => x.StreetcodeId)
                  .WithErrorMessage(ErrorMessages.StreetcodeIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var dto = CreateValidDto();
            dto.Title = string.Empty;

            var result = _validator.TestValidate(dto);

            Assert.Contains(result.Errors, e => e.ErrorMessage == ErrorMessages.FactTitleRequired);
        }

        [Fact]
        public void Should_Have_Error_When_FactContent_Is_Empty()
        {
            var dto = CreateValidDto();
            dto.FactContent = string.Empty;

            var result = _validator.TestValidate(dto);

            Assert.Contains(result.Errors, e => e.ErrorMessage == ErrorMessages.FactContentRequired);
        }

        [Fact]
        public void Should_Have_Error_When_ImageId_Is_Invalid()
        {
            var dto = CreateValidDto();
            dto.ImageId = 0;

            var result = _validator.TestValidate(dto);

            Assert.Contains(result.Errors, e => e.ErrorMessage == ErrorMessages.FactImageIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Dto_Is_Valid()
        {
            var dto = CreateValidDto();
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        private CreateFactDto CreateValidDto()
        {
            return new CreateFactDto
            {
                Title = "Valid Title",
                FactContent = "Valid Content",
                ImageId = 1,
                StreetcodeId = 1,
            };
        }
    }
}