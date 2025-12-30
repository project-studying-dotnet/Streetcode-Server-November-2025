namespace Streetcode.XUnitTest.MediatR.Comment.GetById
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL;
    using Streetcode.BLL.MediatR.Streetcode.Comments.GetById;
    using Xunit;

    public class GetCommentByIdQueryValidatorTests
    {
        private readonly GetCommentByIdQueryValidator validator;

        public GetCommentByIdQueryValidatorTests()
        {
            this.validator = new GetCommentByIdQueryValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Zero()
        {
            var query = new GetCommentByIdQuery(0);

            var result = this.validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.id)
                  .WithErrorMessage(ErrorMessages.CommentIdMustBeGreaterThanZero);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Positive()
        {
            var query = new GetCommentByIdQuery(1);

            var result = this.validator.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(x => x.id);
        }
    }
}