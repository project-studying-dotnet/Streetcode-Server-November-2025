using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Toponyms.Delete
{
    public record DeleteStreetcodeToponymCommand(int StreetcodeId, int ToponymId)
        : IRequest<Result<Unit>>;
}
