using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Update
{
    public record UpdateFactCommand(UpdateFactDto updateFact) : IRequest<Result<FactDto>>;
}
