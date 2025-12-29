namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Create
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL.DTO.Toponyms;
 using global::Streetcode.BLL.MediatR.Toponyms.Create;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="StreetcodeToponymDtoValidator"/>.
    /// Covers validation rules for StreetcodeId and ToponymId to ensure
    /// both fields are not empty and greater than zero.
    /// </summary>
    public class StreetcodeToponymDtoValidatorTests
    {
        private readonly StreetcodeToponymDtoValidator validator = new StreetcodeToponymDtoValidator();

        /// <summary>
        /// Tests that the StreetcodeId field validates correctly based on value constraints.
        /// StreetcodeId must not be empty and must be greater than zero.
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
            var dto = new StreetcodeToponymDto { StreetcodeId = streetcodeId, ToponymId = 1 };

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
            var dto = new StreetcodeToponymDto { StreetcodeId = 1, ToponymId = toponymId };

            // Act
            var result = this.validator.TestValidate(dto);

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
    }
}