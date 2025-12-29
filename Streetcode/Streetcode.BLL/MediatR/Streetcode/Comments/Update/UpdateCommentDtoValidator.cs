using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.Comments;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.Update
{
    public class UpdateCommentDtoValidator : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(ErrorMessages.CommentIdMustBePositive);

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(ErrorMessages.CommentContentRequired)
                .MaximumLength(500).WithMessage(ErrorMessages.CommentContentTooLong);

            RuleFor(x => x.AuthorName)
                .NotEmpty().WithMessage(ErrorMessages.AuthorNameRequired)
                .MaximumLength(50).WithMessage(ErrorMessages.AuthorNameTooLong);
        }
    }
}
