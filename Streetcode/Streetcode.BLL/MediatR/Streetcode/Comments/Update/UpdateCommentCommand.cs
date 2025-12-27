using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.Comments;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.Update
{
    public record UpdateCommentCommand(UpdateCommentDto comment) : IRequest<Result<CommentDto>>;
}
