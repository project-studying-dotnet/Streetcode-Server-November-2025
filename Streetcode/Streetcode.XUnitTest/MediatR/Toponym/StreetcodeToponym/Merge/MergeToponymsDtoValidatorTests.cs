namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Merge
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.BLL.MediatR.Toponyms.Merge;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="MergeToponymsDtoValidator"/>.
    /// Covers validation rules for TargetToponymId, SourceToponymIds collection,
    /// and the business rule that target cannot be in the source list.
    /// </summary>
    public class MergeToponymsDtoValidatorTests
    {
        private readonly MergeToponymsDtoValidator validator = new MergeToponymsDtoValidator();

        /// <summary>
        /// Tests that the TargetToponymId field validates correctly based on value constraints.
        /// TargetToponymId must not be empty and must be greater than zero.
        /// </summary>
        /// <param name="targetToponymId">The target toponym ID to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData(1, true)]
        [InlineData(100, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void TargetToponymId_Should_Validate_Value(int targetToponymId, bool isValid)
        {
            // Arrange
            var dto = new MergeToponymsDto
            {
                TargetToponymId = targetToponymId,
                SourceToponymIds = new List<int> { 2, 3 },
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.TargetToponymId);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.TargetToponymId);
            }
        }

        /// <summary>
        /// Tests that the SourceToponymIds collection validates correctly for null or empty scenarios.
        /// SourceToponymIds must not be null or empty.
        /// </summary>
        [Fact]
        public void SourceToponymIds_Should_HaveError_When_Null()
        {
            // Arrange
            var dto = new MergeToponymsDto
            {
                TargetToponymId = 1,
                SourceToponymIds = null!,
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SourceToponymIds);
        }

        /// <summary>
        /// Tests that the SourceToponymIds collection validates correctly for empty collection.
        /// SourceToponymIds must not be empty.
        /// </summary>
        [Fact]
        public void SourceToponymIds_Should_HaveError_When_Empty()
        {
            // Arrange
            var dto = new MergeToponymsDto
            {
                TargetToponymId = 1,
                SourceToponymIds = new List<int>(),
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SourceToponymIds);
        }

        /// <summary>
        /// Tests that each element in SourceToponymIds must be greater than zero.
        /// Ensures validation fails when any source ID is zero or negative.
        /// </summary>
        /// <param name="sourceIds">Array of source toponym IDs to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData(new[] { 2, 3 }, true)]
        [InlineData(new[] { 1, 100, 50 }, true)]
        [InlineData(new[] { 0, 3 }, false)]
        [InlineData(new[] { 2, -1 }, false)]
        [InlineData(new[] { -5, -10 }, false)]
        public void SourceToponymIds_Should_Validate_EachElement(int[] sourceIds, bool isValid)
        {
            // Arrange
            var dto = new MergeToponymsDto
            {
                TargetToponymId = 1,
                SourceToponymIds = sourceIds.ToList(),
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.SourceToponymIds);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.SourceToponymIds);
            }
        }

        /// <summary>
        /// Tests that the TargetToponymId cannot be present in the SourceToponymIds collection.
        /// Ensures validation fails when target ID appears in source list.
        /// </summary>
        [Fact]
        public void Should_HaveError_When_TargetIsInSourceList()
        {
            // Arrange
            var dto = new MergeToponymsDto
            {
                TargetToponymId = 1,
                SourceToponymIds = new List<int> { 1, 2, 3 },
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x);
        }

        /// <summary>
        /// Tests that validation succeeds when TargetToponymId is not in the SourceToponymIds collection.
        /// Ensures no validation error when target and source lists are properly separated.
        /// </summary>
        [Fact]
        public void Should_NotHaveError_When_TargetIsNotInSourceList()
        {
            // Arrange
            var dto = new MergeToponymsDto
            {
                TargetToponymId = 1,
                SourceToponymIds = new List<int> { 2, 3, 4 },
            };

            // Act
            var result = this.validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x);
        }
    }
}