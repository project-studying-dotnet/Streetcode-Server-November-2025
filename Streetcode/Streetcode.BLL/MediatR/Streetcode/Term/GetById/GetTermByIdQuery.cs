using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.TextContent;

namespace Streetcode.BLL.MediatR.Term.GetById;

public record GetTermByIdQuery(int Id) : IRequest<Result<TermDto>>;
