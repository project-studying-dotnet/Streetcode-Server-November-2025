using FluentResults;
using MediatR;
using Streetcode.Auth.Application.Dtos.Users;

namespace Streetcode.Auth.Application.MediatR.Register
{
    public record RegisterCommand(RegisterUserDto newUser) : IRequest<Result<RegisterUserResponseDto>>;
}
