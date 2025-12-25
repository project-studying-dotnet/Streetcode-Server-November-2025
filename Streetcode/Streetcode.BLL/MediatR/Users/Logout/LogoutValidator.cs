using FluentValidation;

namespace Streetcode.BLL.MediatR.Users.Logout
{
	public class LogoutValidator : AbstractValidator<LogoutCommand>
	{
		public LogoutValidator()
		{
			RuleFor(x => x.LogoutRequest.RefreshToken)
				.NotEmpty()
				.WithMessage("Refresh token is required.");

			RuleFor(x => x.UserId)
				.GreaterThan(0)
				.WithMessage("Invalid User ID.");
		}
	}
}