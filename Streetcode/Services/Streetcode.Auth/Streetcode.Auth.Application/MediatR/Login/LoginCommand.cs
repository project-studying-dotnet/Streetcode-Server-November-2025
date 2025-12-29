using FluentResults;
using Streetcode.Auth.Application.Dtos.Auth;
using MediatR;

namespace Streetcode.Auth.Application.MediatR.Login
{
    public record LoginCommand(LoginRequestDto loginRequestDto) : IRequest<Result<TokenResponseDto>>;
}
