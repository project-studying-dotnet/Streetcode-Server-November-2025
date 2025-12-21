using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Users;

namespace Streetcode.BLL.MediatR.Users.Logout
{
	public record LogoutCommand(LogoutRequestDto LogoutRequestDto) : IRequest<Result<Unit>>;
}