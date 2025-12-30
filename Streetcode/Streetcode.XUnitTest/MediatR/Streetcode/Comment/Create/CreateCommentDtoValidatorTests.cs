namespace Streetcode.XUnitTest.MediatR.Comments.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL.DTO.Streetcode.Comments;
 using global::Streetcode.BLL.MediatR.Streetcode.Comments.Create;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="CreateCommentDtoValidator"/>.
    /// Covers validation rules for Content, AuthorName, StreetcodeId, and ParentCommentId
    /// to ensure all fields meet their respective constraints.
    /// </summary>
    public class CreateCommentDtoValidatorTests
    {
        private readonly CreateCommentDtoValidator validator = new CreateCommentDtoValidator();

        /// <summary>
        /// Tests that the Content field validates correctly based on value constraints.
        /// Content must not be empty and must not exceed the maximum length.
        /// </summary>
        /// <param name="content">The content to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData("Valid content", true)]
        [InlineData("A", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void Content_Should_Validate_Value(string content, bool isValid)
        {
            // Arrange
            var dto = new CreateCommentDto
            {
                Content = content,
                AuthorName = "John Doe",
                StreetcodeId = 1,
                CreatedAt = DateTime.Now,
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.Content);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.Content);
            }
        }

        /// <summary>
        /// Tests that the Content field fails validation when exceeding maximum length.
        /// </summary>
        [Fact]
        public void Content_Should_HaveError_When_ExceedsMaxLength()
        {
            // Arrange
            var dto = new CreateCommentDto
            {
                Content = new string('A', 5001), // Assuming max length is around 5000
                AuthorName = "John Doe",
                StreetcodeId = 1,
                CreatedAt = DateTime.Now,
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        /// <summary>
        /// Tests that the AuthorName field validates correctly based on value constraints.
        /// AuthorName must not be empty and must not exceed the maximum length.
        /// </summary>
        /// <param name="authorName">The author name to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData("John Doe", true)]
        [InlineData("A", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void AuthorName_Should_Validate_Value(string authorName, bool isValid)
        {
            // Arrange
            var dto = new CreateCommentDto
            {
                Content = "Valid content",
                AuthorName = authorName,
                StreetcodeId = 1,
                CreatedAt = DateTime.Now,
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.AuthorName);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.AuthorName);
            }
        }

        /// <summary>
        /// Tests that the AuthorName field fails validation when exceeding maximum length.
        /// </summary>
        [Fact]
        public void AuthorName_Should_HaveError_When_ExceedsMaxLength()
        {
            // Arrange
            var dto = new CreateCommentDto
            {
                Content = "Valid content",
                AuthorName = new string('A', 101), // Assuming max length is around 100
                StreetcodeId = 1,
                CreatedAt = DateTime.Now,
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.AuthorName);
        }

        /// <summary>
        /// Tests that the StreetcodeId field validates correctly based on value constraints.
        /// StreetcodeId must be greater than zero.
        /// </summary>
        /// <param name="streetcodeId">The streetcode ID to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData(1, true)]
        [InlineData(100, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void StreetcodeId_Should_Validate_Value(int streetcodeId, bool isValid)
        {
            // Arrange
            var dto = new CreateCommentDto
            {
                Content = "Valid content",
                AuthorName = "John Doe",
                StreetcodeId = streetcodeId,
                CreatedAt = DateTime.Now,
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.StreetcodeId);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.StreetcodeId);
            }
        }

        /// <summary>
        /// Tests that the ParentCommentId field validates correctly when it has a value.
        /// ParentCommentId must be greater than zero when provided.
        /// </summary>
        /// <param name="parentCommentId">The parent comment ID to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData(1, true)]
        [InlineData(100, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void ParentCommentId_Should_Validate_Value_When_HasValue(int parentCommentId, bool isValid)
        {
            // Arrange
            var dto = new CreateCommentDto
            {
                Content = "Valid content",
                AuthorName = "John Doe",
                StreetcodeId = 1,
                CreatedAt = DateTime.Now,
                ParentCommentId = parentCommentId,
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.ParentCommentId);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.ParentCommentId);
            }
        }

        /// <summary>
        /// Tests that the ParentCommentId field does not trigger validation errors when null.
        /// Validation should only apply when ParentCommentId has a value.
        /// </summary>
        [Fact]
        public void ParentCommentId_Should_NotHaveError_When_Null()
        {
            // Arrange
            var dto = new CreateCommentDto
            {
                Content = "Valid content",
                AuthorName = "John Doe",
                StreetcodeId = 1,
                CreatedAt = DateTime.Now,
                ParentCommentId = null,
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ParentCommentId);
        }
    }
}