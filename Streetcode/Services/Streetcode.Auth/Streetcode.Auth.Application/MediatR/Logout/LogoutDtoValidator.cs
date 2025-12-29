using FluentValidation;

namespace Streetcode.Auth.Application.MediatR.Logout
{
    public class LogoutDtoValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutDtoValidator()
        {
            RuleFor(x => x.LogoutRequestDto.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token is required");
        }
    }
}
