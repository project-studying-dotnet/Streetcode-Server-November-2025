namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Delete
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.MediatR.Toponyms.Delete;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="DeleteStreetcodeToponymCommandValidator"/>.
    /// Covers validation rules for StreetcodeId and ToponymId to ensure
    /// both fields are greater than zero.
    /// </summary>
    public class DeleteStreetcodeToponymCommandValidatorTests
    {
        private readonly DeleteStreetcodeToponymCommandValidator validator = new DeleteStreetcodeToponymCommandValidator();

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
            var command = new DeleteStreetcodeToponymCommand(streetcodeId, 1);

            // Act
            var result = this.validator.TestValidate(command);

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
        /// Tests that the ToponymId field validates correctly based on value constraints.
        /// ToponymId must be greater than zero.
        /// </summary>
        /// <param name="toponymId">The toponym ID to validate.</param>
        /// <param name="isValid">Expected validation result.</param>
        [Theory]
        [InlineData(1, true)]
        [InlineData(100, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void ToponymId_Should_Validate_Value(int toponymId, bool isValid)
        {
            // Arrange
            var command = new DeleteStreetcodeToponymCommand(1, toponymId);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            if (isValid)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.ToponymId);
            }
            else
            {
                result.ShouldHaveValidationErrorFor(x => x.ToponymId);
            }
        }

        /// <summary>
        /// Tests that validation fails when both StreetcodeId and ToponymId are invalid.
        /// Ensures that validation errors are returned for both fields.
        /// </summary>
        [Fact]
        public void Should_HaveError_When_BothIdsAreInvalid()
        {
            // Arrange
            var command = new DeleteStreetcodeToponymCommand(0, 0);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeId);
            result.ShouldHaveValidationErrorFor(x => x.ToponymId);
        }

        /// <summary>
        /// Tests that validation succeeds when both StreetcodeId and ToponymId are valid.
        /// Ensures that no validation errors are returned for valid input.
        /// </summary>
        [Fact]
        public void Should_NotHaveError_When_BothIdsAreValid()
        {
            // Arrange
            var command = new DeleteStreetcodeToponymCommand(1, 2);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.StreetcodeId);
            result.ShouldNotHaveValidationErrorFor(x => x.ToponymId);
        }
    }
}