using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.TextContent;

namespace Streetcode.BLL.MediatR.RelatedTerm.GetAllByTermId
{
    public record GetAllRelatedTermsByTermIdQuery(int id) : IRequest<Result<IEnumerable<RelatedTermDto>>>
    {
    }
}
