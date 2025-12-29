namespace Streetcode.XUnitTest.MediatR.RelatedFigure.Delete
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Delete;
    using Xunit;

    public class DeleteRelatedFigureCommandValidatorTests
    {
        private readonly DeleteRelatedFigureCommandValidator _validator;

        public DeleteRelatedFigureCommandValidatorTests()
        {
            _validator = new DeleteRelatedFigureCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_ObserverId_Is_Invalid(int observerId)
        {
            // Arrange
            var command = new DeleteRelatedFigureCommand(observerId, 10);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ObserverId)
                  .WithErrorMessage(ErrorMessages.RelatedFigureObserverIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Have_Error_When_ObserverId_Equals_TargetId()
        {
            // Arrange
            int id = 5;
            var command = new DeleteRelatedFigureCommand(id, id);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ObserverId)
                  .WithErrorMessage(ErrorMessages.RelatedFigureSelfReferenceNotAllowed);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_TargetId_Is_Invalid(int targetId)
        {
            // Arrange
            var command = new DeleteRelatedFigureCommand(10, targetId);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TargetId)
                  .WithErrorMessage(ErrorMessages.RelatedFigureTargetIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Ids_Are_Valid_And_Different()
        {
            // Arrange
            var command = new DeleteRelatedFigureCommand(1, 2);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}