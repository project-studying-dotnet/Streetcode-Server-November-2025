namespace Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.Delete
{
    using global::Streetcode.BLL;
    using global::Streetcode.BLL.MediatR.Timeline.HistoricalContext.Delete;
    using FluentValidation.TestHelper;
    using Xunit;

    public class DeleteHistoricalContextCommandValidatorTests
    {
        private readonly DeleteHistoricalContextCommandValidator validator;

        public DeleteHistoricalContextCommandValidatorTests()
        {
            this.validator = new DeleteHistoricalContextCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            // Arrange
            var command = new DeleteHistoricalContextCommand(0);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.HistoricalContextIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Negative()
        {
            // Arrange
            var command = new DeleteHistoricalContextCommand(-1);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage(ErrorMessages.HistoricalContextIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Pass_Validation_When_Id_Is_Positive()
        {
            // Arrange
            var command = new DeleteHistoricalContextCommand(1);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(42)]
        [InlineData(100)]
        [InlineData(999)]
        public void Should_Pass_Validation_For_Various_Positive_Ids(int id)
        {
            // Arrange
            var command = new DeleteHistoricalContextCommand(id);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Should_Have_Error_For_Various_Invalid_Ids(int id)
        {
            // Arrange
            var command = new DeleteHistoricalContextCommand(id);

            // Act
            var result = this.validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
        }
    }
}
