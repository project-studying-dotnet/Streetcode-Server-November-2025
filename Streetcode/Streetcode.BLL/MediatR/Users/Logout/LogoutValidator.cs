using FluentValidation;

namespace Streetcode.BLL.MediatR.Users.Logout
{
	public class LogoutValidator : AbstractValidator<LogoutCommand>
	{
		public LogoutValidator()
		{
			RuleFor(x => x.LogoutRequestDto.RefreshToken)
				.NotEmpty()
				.WithMessage("Refresh token is required");
		}
	}
}