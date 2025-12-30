using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.Comments;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.GetById
{
    public record GetCommentByIdQuery(int id) : IRequest<Result<CommentDto>>;
}
