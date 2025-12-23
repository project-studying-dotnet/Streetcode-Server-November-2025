using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.Comments;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.Create
{
    /// <summary>
    /// Validator for CreateCommentDto.
    /// </summary>
    public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommentDtoValidator"/> class.
        /// </summary>
        public CreateCommentDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage(ErrorMessages.CommentContentRequired)
                .MaximumLength(ValidationConstants.Comment.ContentMaxLength)
                .WithMessage(string.Format(
                    ErrorMessages.CommentContentTooLong,
                    ValidationConstants.Comment.ContentMaxLength));

            RuleFor(x => x.AuthorName)
                .NotEmpty()
                .WithMessage(ErrorMessages.AuthorNameRequired)
                .MaximumLength(ValidationConstants.Comment.AuthorNameMaxLength)
                .WithMessage(string.Format(
                    ErrorMessages.AuthorNameTooLong,
                    ValidationConstants.Comment.AuthorNameMaxLength));

            RuleFor(x => x.StreetcodeId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.StreetcodeIdMustBeGreaterThanZero);

            RuleFor(x => x.ParentCommentId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.ParentCommentIdMustBeGreaterThanZero)
                .When(x => x.ParentCommentId.HasValue);
        }
    }
}
