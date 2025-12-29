using FluentValidation;
using Streetcode.Auth.Application.Dtos.Auth;

namespace Streetcode.Auth.Application.MediatR.Login
{
    public class LoginDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginDtoValidator()
        {
            // Email - required, robust format (standard regex), and max length
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email є обов'язковий")
                .EmailAddress()
                .WithMessage("Email має не вірний формат");

            // Password - required, min/max, Identity-like composition
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Пароль є обов'язковий");
        }
    }
}
