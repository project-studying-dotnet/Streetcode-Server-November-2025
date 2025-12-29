using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.TextContent;

namespace Streetcode.BLL.MediatR.Term.GetAll
{
    public record GetAllTermsQuery : IRequest<Result<IEnumerable<TermDto>>>;
}
