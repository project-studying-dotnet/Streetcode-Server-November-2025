using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.TextContent;

namespace Streetcode.BLL.MediatR.RelatedTerm.Update
{
    public record UpdateRelatedTermCommand(int id, RelatedTermDto RelatedTerm) : IRequest<Result<RelatedTermDto>>
    {
    }
}
