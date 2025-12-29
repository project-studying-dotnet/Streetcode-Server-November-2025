namespace Streetcode.XUnitTest.MediatR.RelatedTerm.Delete
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.MediatR.RelatedTerm.Delete;
 using global::Streetcode.BLL.Util.Validators;
    using Xunit;

    public class DeleteRelatedTermCommandValidatorTests
    {
        private const int TermId = 1;
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
            var command = new DeleteRelatedTermCommand(word, TermId);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.word)
                  .WithErrorMessage(ErrorMessages.RelatedTermWordForDeletionRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Word_Is_Too_Long()
        {
            var longWord = new string('a', ValidationConstants.RelatedTerm.WordMaxLength + 1);
            var command = new DeleteRelatedTermCommand(longWord, TermId);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.word)
                  .WithErrorMessage(string.Format(ErrorMessages.RelatedTermWordTooLong, ValidationConstants.RelatedTerm.WordMaxLength));
        }

        [Fact]
        public void Should_Not_Have_Error_When_Word_Is_Valid()
        {
            var command = new DeleteRelatedTermCommand("ValidWord", TermId);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.word);
        }

        [Fact]
        public void Should_Have_Error_When_TermId_Is_Invalid()
        {
            var command = new DeleteRelatedTermCommand("ValidWord", 0);
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.termId)
                  .WithErrorMessage(ErrorMessages.RelatedTermIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid_Command()
        {
            var command = new DeleteRelatedTermCommand("ValidWord", TermId);
            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.word);
            result.ShouldNotHaveValidationErrorFor(x => x.termId);
        }
    }
}