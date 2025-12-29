using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Users;

namespace Streetcode.BLL.MediatR.Users.Register
{
  public record RegisterUserCommand(RegisterUserDto newUser) : IRequest<Result<RegisterUserResponseDto>>;
}
