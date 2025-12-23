namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Merge
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.BLL.MediatR.Toponyms.Merge;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="MergeToponymsCommandValidator"/>.
    /// Covers validation rules for <see cref="MergeToponymsCommand"/> to ensure
    /// that the MergeRequest property is not null and is properly populated.
    /// </summary>
    public class MergeToponymsCommandValidatorTests
    {
        private readonly MergeToponymsCommandValidator validator = new MergeToponymsCommandValidator();

        /// <summary>
        /// Tests that validation fails when the MergeRequest property is null.
        /// Ensures that a validation error is returned for the MergeRequest field.
        /// </summary>
        [Fact]
        public void Should_HaveError_When_MergeRequestIsNull()
        {
            // Arrange
            var command = new MergeToponymsCommand(null!);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.MergeRequest);
        }

        /// <summary>
        /// Tests that validation succeeds when the MergeRequest property is properly populated.
        /// Ensures that no validation error is returned when a valid <see cref="MergeToponymsDto"/> is provided.
        /// </summary>
        [Fact]
        public void Should_NotHaveError_When_MergeRequestIsPopulated()
        {
            // Arrange
            var dto = new MergeToponymsDto
            {
                TargetToponymId = 1,
                SourceToponymIds = new List<int> { 2, 3 },
            };
            var command = new MergeToponymsCommand(dto);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.MergeRequest);
        }
    }
}