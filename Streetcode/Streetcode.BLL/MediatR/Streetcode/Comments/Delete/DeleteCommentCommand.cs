using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.Delete
{
    public record DeleteCommentCommand(int CommentId) : IRequest<Result<Unit>>;
}
