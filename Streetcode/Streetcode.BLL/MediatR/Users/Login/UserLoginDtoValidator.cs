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
            // Email - required, robust format (standard regex), and max length
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserEmailRequired)
                .EmailAddress()
                .WithMessage(ErrorMessages.UserEmailInvalidFormat);

            // Password - required, min/max, Identity-like composition
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserPasswordRequired);
        }
    }
}