using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.DeleteFull
{
    public record DeleteFullStreetcodeCommand(int Id) : IRequest<Result<Unit>>;
}
