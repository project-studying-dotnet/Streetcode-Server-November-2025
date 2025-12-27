using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.Delete
{
    public class DeleteCommentCommandValidator
            : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(x => x.CommentId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.CommentIdMustBePositive);
        }
    }
}
