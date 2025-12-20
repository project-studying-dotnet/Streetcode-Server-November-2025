namespace Streetcode.XUnitTest.MediatR.Newss.Delete
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL;
    using Streetcode.BLL.MediatR.Newss.Delete;
    using Xunit;

    public class DeleteNewsCommandValidatorTests
    {
        private readonly DeleteNewsCommandValidator _validator;

        public DeleteNewsCommandValidatorTests()
        {
            _validator = new DeleteNewsCommandValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_Id_Is_Invalid(int id)
        {
            var command = new DeleteNewsCommand(id);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.id)
                  .WithErrorMessage(ErrorMessages.NewsIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var command = new DeleteNewsCommand(1);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.id);
        }
    }
}