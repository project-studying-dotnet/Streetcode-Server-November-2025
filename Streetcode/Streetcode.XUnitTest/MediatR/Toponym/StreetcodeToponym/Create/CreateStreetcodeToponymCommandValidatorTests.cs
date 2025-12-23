namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Create
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.BLL.MediatR.Toponyms.Create;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="CreateStreetcodeToponymCommandValidator"/>.
    /// Covers validation rules for <see cref="CreateStreetcodeToponymCommand"/> to ensure
    /// that the StreetcodeToponym property is not null and is properly populated.
    /// </summary>
    public class CreateStreetcodeToponymCommandValidatorTests
    {
        private readonly CreateStreetcodeToponymCommandValidator validator = new CreateStreetcodeToponymCommandValidator();

        /// <summary>
        /// Tests that validation fails when the StreetcodeToponym property is null.
        /// Ensures that a validation error is returned for the StreetcodeToponym field.
        /// </summary>
        [Fact]
        public void Should_HaveError_When_StreetcodeToponymIsNull()
        {
            // Arrange
            var command = new CreateStreetcodeToponymCommand(null!);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetcodeToponym);
        }

        /// <summary>
        /// Tests that validation succeeds when the StreetcodeToponym property is properly populated.
        /// Ensures that no validation error is returned when a valid <see cref="StreetcodeToponymDto"/> is provided.
        /// </summary>
        [Fact]
        public void Should_NotHaveError_When_StreetcodeToponymIsPopulated()
        {
            // Arrange
            var dto = new StreetcodeToponymDto
            {
                StreetcodeId = 1,
                ToponymId = 1,
            };
            var command = new CreateStreetcodeToponymCommand(dto);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.StreetcodeToponym);
        }
    }
}