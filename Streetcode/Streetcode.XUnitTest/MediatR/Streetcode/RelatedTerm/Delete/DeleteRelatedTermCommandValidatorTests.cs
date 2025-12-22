namespace Streetcode.XUnitTest.MediatR.RelatedTerm.Delete
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Delete;
    using Streetcode.BLL.Util.Validators;
    using Xunit;

    public class DeleteRelatedTermCommandValidatorTests
    {
        private readonly DeleteRelatedTermCommandValidator _validator;

        public DeleteRelatedTermCommandValidatorTests()
        {
            _validator = new DeleteRelatedTermCommandValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Have_Error_When_Word_Is_Empty(string word)
        {
            var command = new DeleteRelatedTermCommand(word);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.word)
                  .WithErrorMessage("Слово для видалення є обов'язковим");
        }

        [Fact]
        public void Should_Have_Error_When_Word_Is_Too_Long()
        {
            var longWord = new string('a', ValidationConstants.RelatedTerm.WordMaxLength + 1);
            var command = new DeleteRelatedTermCommand(longWord);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.word)
                  .WithErrorMessage($"Слово не може перевищувати {ValidationConstants.RelatedTerm.WordMaxLength} символів");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Word_Is_Valid()
        {
            var command = new DeleteRelatedTermCommand("ValidWord");
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.word);
        }
    }
}