namespace Streetcode.XUnitTest.MediatR.RelatedTerm.Update
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Streetcode.TextContent;
    using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Update;
    using Streetcode.BLL.Util.Validators;
    using Xunit;

    public class UpdateRelatedTermDtoValidatorTests
    {
        private readonly UpdateRelatedTermDtoValidator _validator;

        public UpdateRelatedTermDtoValidatorTests()
        {
            _validator = new UpdateRelatedTermDtoValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Word_Is_Empty(string word)
        {
            var dto = new RelatedTermDto { Word = word };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Word)
                  .WithErrorMessage("Слово є обов'язковим");
        }

        [Fact]
        public void Should_Have_Error_When_Word_Is_Too_Long()
        {
            var dto = new RelatedTermDto
            {
                Word = new string('a', ValidationConstants.RelatedTerm.WordMaxLength + 1)
            };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Word)
                  .WithErrorMessage($"Слово не може перевищувати {ValidationConstants.RelatedTerm.WordMaxLength} символів");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_TermId_Is_Invalid(int termId)
        {
            var dto = new RelatedTermDto { TermId = termId };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.TermId)
                  .WithErrorMessage("ID терміну має бути більше 0");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Dto_Is_Valid()
        {
            var dto = new RelatedTermDto
            {
                Word = "Valid Word",
                TermId = 1
            };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}