namespace Streetcode.XUnitTest.MediatR.Comments.Create
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Streetcode.Comments;
    using Streetcode.BLL.MediatR.Comments.Create;
    using Streetcode.BLL.MediatR.Streetcode.Comments.Create;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="CreateCommentCommandValidator"/>.
    /// Covers validation rules for <see cref="CreateCommentCommand"/> to ensure
    /// that the newComment property is not null and is properly populated.
    /// </summary>
    public class CreateCommentCommandValidatorTests
    {
        private readonly CreateCommentCommandValidator validator = new CreateCommentCommandValidator();

        /// <summary>
        /// Tests that validation fails when the newComment property is null.
        /// Ensures that a validation error is returned for the newComment field.
        /// </summary>
        [Fact]
        public void Should_HaveError_When_NewCommentIsNull()
        {
            // Arrange
            var command = new CreateCommentCommand(null!);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.newComment);
        }

        /// <summary>
        /// Tests that validation succeeds when the newComment property is properly populated.
        /// Ensures that no validation error is returned when a valid <see cref="CreateCommentDto"/> is provided.
        /// </summary>
        [Fact]
        public void Should_NotHaveError_When_NewCommentIsPopulated()
        {
            // Arrange
            var dto = new CreateCommentDto
            {
                Content = "Valid comment content",
                AuthorName = "John Doe",
                StreetcodeId = 1,
                CreatedAt = DateTime.Now,
            };
            var command = new CreateCommentCommand(dto);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.newComment);
        }
    }
}