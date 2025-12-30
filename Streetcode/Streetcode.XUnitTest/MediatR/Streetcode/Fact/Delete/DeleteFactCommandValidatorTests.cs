namespace Streetcode.XUnitTest.MediatR.Fact.Delete
{
    using FluentValidation.TestHelper;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.MediatR.Streetcode.Fact.Delete;
    using Xunit;

    public class DeleteFactCommandValidatorTests
    {
        private readonly DeleteFactCommandValidator _validator;

        public DeleteFactCommandValidatorTests()
        {
            _validator = new DeleteFactCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_Id_Is_Invalid(int id)
        {
            // Arrange
            var command = new DeleteFactCommand(id);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.id)
                  .WithErrorMessage(ErrorMessages.FactIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            // Arrange
            var command = new DeleteFactCommand(1);

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.id);
        }
    }
}