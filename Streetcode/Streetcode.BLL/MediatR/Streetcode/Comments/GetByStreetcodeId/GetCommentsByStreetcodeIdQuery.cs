using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.Comments;
using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.GetByStreetcodeId
{
    public record GetCommentsByStreetcodeIdQuery(int streetcodeId) : IRequest<Result<IEnumerable<CommentDto>>>;
}
