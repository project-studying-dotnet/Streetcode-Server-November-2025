using Streetcode.BLL.DTO.Streetcode.TextContent;
using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Update;
using FluentValidation.TestHelper;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.RelatedTerm.Update
{
    public class UpdateRelatedTermCommandValidatorTests
    {
        private readonly UpdateRelatedTermCommandValidator _validator;

        public UpdateRelatedTermCommandValidatorTests()
        {
            _validator = new UpdateRelatedTermCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_Id_Is_Invalid(int id)
        {
            // Arrange
            var command = new UpdateRelatedTermCommand(id, null!);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.id)
                  .WithErrorMessage("ID пов'язаного терміну має бути більше 0");
        }

        [Fact]
        public void Should_Have_Error_When_RelatedTerm_Is_Null()
        {
            // Arrange
            var command = new UpdateRelatedTermCommand(1, null!);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.RelatedTerm)
                  .WithErrorMessage("Дані пов'язаного терміну не можуть бути порожніми");
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            // Arrange
            var invalidDto = new RelatedTermDto()
            {
                Word = string.Empty,
                TermId = 1,
            };
            var command = new UpdateRelatedTermCommand(1, invalidDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.RelatedTerm.Word);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            // Arrange
            var validDto = new RelatedTermDto
            {
                Id = 1,
                Word = "Valid Word",
                TermId = 1,
            };
            var command = new UpdateRelatedTermCommand(1, validDto);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}