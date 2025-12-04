using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Update
{
    public record class UpdateStreetcodeCommand(int id, JsonElement rawJsonUpdateDTO) : IRequest<Result<JsonElement>>;
}
