using FluentValidation;
namespace Streetcode.BLL.MediatR.Users.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.newUser)
                .NotNull()
                .WithMessage(ErrorMessages.UserDataRequired)
                .SetValidator(new RegisterUserDtoValidator());
        }
    }
}
