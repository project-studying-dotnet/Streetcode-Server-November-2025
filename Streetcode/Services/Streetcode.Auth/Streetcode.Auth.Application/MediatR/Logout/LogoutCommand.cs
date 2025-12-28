using FluentResults;
using MediatR;
using Streetcode.Auth.Application.Dtos.Auth;

namespace Streetcode.Auth.Application.MediatR.Logout
{
    public record LogoutCommand(LogoutRequestDto LogoutRequestDto) : IRequest<Result<Unit>>; 
}
