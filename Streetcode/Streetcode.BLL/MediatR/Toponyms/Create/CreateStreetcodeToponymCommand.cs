using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Toponyms;

namespace Streetcode.BLL.MediatR.Toponyms.Create
{
    public record CreateStreetcodeToponymCommand(StreetcodeToponymDto StreetcodeToponym)
        : IRequest<Result<StreetcodeToponymDto>>;
}
