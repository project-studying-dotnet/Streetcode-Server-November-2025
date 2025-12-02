using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Create
{
    public record CreateStreetcodeCommand(JsonElement rawJsonCreateDTO) : IRequest<Result<JsonElement>>
    {
    }
}
