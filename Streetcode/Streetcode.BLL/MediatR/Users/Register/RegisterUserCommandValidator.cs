using FluentValidation;
namespace Streetcode.BLL.MediatR.Users.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.newUser)
                .NotNull()
                .WithMessage("Дані користувача є обов'язковими")
                .SetValidator(new RegisterUserDtoValidator());
        }
    }
}
