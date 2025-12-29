using FluentValidation;
using Streetcode.BLL.MediatR.Streetcode.Comments.Create;

namespace Streetcode.BLL.MediatR.Comments.Create
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.newComment)
                .NotNull()
                .WithMessage(ErrorMessages.CommentDataRequired)
                .SetValidator(new CreateCommentDtoValidator());
        }
    }
}
