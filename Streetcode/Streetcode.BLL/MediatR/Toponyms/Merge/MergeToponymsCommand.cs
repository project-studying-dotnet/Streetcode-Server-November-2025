using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Toponyms;

namespace Streetcode.BLL.MediatR.Toponyms.Merge
{
    public record MergeToponymsCommand(MergeToponymsDto MergeRequest)
        : IRequest<Result<ToponymDto>>;
}
