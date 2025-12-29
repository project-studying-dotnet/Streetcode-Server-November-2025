using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.Update
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.comment)
                .NotNull().WithMessage(ErrorMessages.CommentDataRequired)
                .SetValidator(new UpdateCommentDtoValidator());
        }
    }
}
