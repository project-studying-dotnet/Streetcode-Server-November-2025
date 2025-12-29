namespace Streetcode.XUnitTest.MediatR.RelatedTerm.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.TextContent;
 using global::Streetcode.BLL.MediatR.RelatedTerm.Create;
    using Xunit;

    public class CreateRelatedTermCommandValidatorTests
    {
        private readonly CreateRelatedTermCommandValidator _validator;

        public CreateRelatedTermCommandValidatorTests()
        {
            _validator = new CreateRelatedTermCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_RelatedTerm_Is_Null()
        {
            // Arrange
            var command = new CreateRelatedTermCommand(null);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.RelatedTerm)
                  .WithErrorMessage(ErrorMessages.RelatedTermDataRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            // Arrange
            var invalidDto = new RelatedTermDto
            {
                Word = string.Empty,
                TermId = 1,
            };
            var command = new CreateRelatedTermCommand(invalidDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.RelatedTerm.Word);
        }

        [Fact]
        public void Should_Not_Have_Error_When_RelatedTerm_Is_Valid()
        {
            // Arrange
            var validDto = new RelatedTermDto
            {
                Word = "Valid Word",
                TermId = 1,
            };
            var command = new CreateRelatedTermCommand(validDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}