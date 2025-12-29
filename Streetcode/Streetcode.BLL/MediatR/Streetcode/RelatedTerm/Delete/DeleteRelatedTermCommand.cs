using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.TextContent;

namespace Streetcode.BLL.MediatR.RelatedTerm.Delete
{
    public record DeleteRelatedTermCommand(string word, int termId) : IRequest<Result<RelatedTermDto>>;
}
