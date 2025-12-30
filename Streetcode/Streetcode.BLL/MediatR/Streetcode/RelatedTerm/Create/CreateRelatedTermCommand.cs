using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.TextContent;

namespace Streetcode.BLL.MediatR.RelatedTerm.Create
{
    public record CreateRelatedTermCommand(RelatedTermDto RelatedTerm) : IRequest<Result<RelatedTermDto>>
    {
    }
}
