using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.GetById
{
    public class GetCommentByIdQueryValidator : AbstractValidator<GetCommentByIdQuery>
    {
        public GetCommentByIdQueryValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.CommentIdMustBeGreaterThanZero);
        }
    }
}
