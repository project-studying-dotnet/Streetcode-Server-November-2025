namespace Streetcode.XUnitTest.MediatR.Partners.Delete
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.MediatR.Partners.Delete;
    using Xunit;

    public class DeletePartnerQueryValidatorTests
    {
        private readonly DeletePartnerQueryValidator _validator;

        public DeletePartnerQueryValidatorTests()
        {
            _validator = new DeletePartnerQueryValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Should_Have_Error_When_Id_Is_Invalid(int id)
        {
            var query = new DeletePartnerQuery(id);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var query = new DeletePartnerQuery(1);
            var result = _validator.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(x => x.id);
        }
    }
}