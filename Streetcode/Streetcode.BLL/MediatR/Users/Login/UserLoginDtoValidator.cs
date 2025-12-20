using FluentValidation;
using Streetcode.BLL.DTO.Users;

namespace Streetcode.BLL.MediatR.Users.Login
{
    /// <summary>
    /// Validator for UserLoginDto.
    /// </summary>
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login is required")
                .MaximumLength(20)
                .WithMessage("Login cannot exceed 20 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MaximumLength(20)
                .WithMessage("Password cannot exceed 20 characters")
                .MinimumLength(3)
                .WithMessage("Password must be at least 3 characters");
        }
    }
}
