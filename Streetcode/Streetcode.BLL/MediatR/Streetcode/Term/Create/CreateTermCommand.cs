using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.TextContent;

namespace Streetcode.BLL.MediatR.Term.Create;

public record CreateTermCommand(TermDto Term) : IRequest<Result<TermDto>>;