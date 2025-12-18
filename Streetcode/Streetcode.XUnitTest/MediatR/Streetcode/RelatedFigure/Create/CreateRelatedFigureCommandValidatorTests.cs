namespace Streetcode.XUnitTest.MediatR.RelatedFigure.Create
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL;
    using Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Create;
    using Xunit;

    public class CreateRelatedFigureCommandValidatorTests
    {
        private readonly CreateRelatedFigureCommandValidator _validator;

        public CreateRelatedFigureCommandValidatorTests()
        {
            _validator = new CreateRelatedFigureCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_ObserverId_Is_Invalid(int observerId)
        {
            // Arrange
            var command = new CreateRelatedFigureCommand(observerId, 10);

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
            var command = new CreateRelatedFigureCommand(id, id);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ObserverId)
                  .WithErrorMessage("Стріткод не може бути пов'язаний сам з собою");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_TargetId_Is_Invalid(int targetId)
        {
            // Arrange
            var command = new CreateRelatedFigureCommand(10, targetId);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.TargetId)
                  .WithErrorMessage("ID цільового стріткоду має бути більше 0");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Ids_Are_Valid_And_Different()
        {
            // Arrange
            var command = new CreateRelatedFigureCommand(1, 2);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}