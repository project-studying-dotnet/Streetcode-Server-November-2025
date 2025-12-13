using FluentValidation;
using Streetcode.BLL.DTO.Users;
using Streetcode.BLL.Util.Validators;
namespace Streetcode.BLL.MediatR.Users.Register
{
    /// <summary>
    /// Validator for RegisterUserDto.
    /// </summary>
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserDtoValidator"/> class.
        /// </summary>
        public RegisterUserDtoValidator()
        {
            // Name - required, field-specific length
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ім'я користувача є обов'язковим")
                .MaximumLength(ValidationConstants.User.NameMaxLength)
                .WithMessage($"Ім'я не може перевищувати {ValidationConstants.User.NameMaxLength} символів");

            // Surname - allow optional, if present, check field-specific length
            RuleFor(x => x.Surname)
                .MaximumLength(ValidationConstants.User.SurnameMaxLength)
                .WithMessage($"Прізвище не може перевищувати {ValidationConstants.User.SurnameMaxLength} символів");

            // UserName - required, field-specific length, and allowed format
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Логін є обов'язковий")
                .MaximumLength(ValidationConstants.User.UserNameMaxLength)
                .WithMessage($"Логін не може перевищувати {ValidationConstants.User.UserNameMaxLength} символів")
                .Matches(ValidationConstants.RegexPatterns.UserName)
                .WithMessage("Логін може містити тільки латинські літери, цифри, крапку, дефіс і підкреслення");

            // Email - required, robust format (standard regex), and max length
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email є обов'язковий")
                .EmailAddress()
                .WithMessage("Email має не вірний формат")
                .MaximumLength(ValidationConstants.User.EmailMaxLength)
                .WithMessage($"Email не може перевищувати {ValidationConstants.User.EmailMaxLength} символів");

            // Password - required, min/max, Identity-like composition
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Пароль є обов'язковий")
                .MinimumLength(ValidationConstants.User.PasswordMinLength)
                .WithMessage($"Пароль має містити як мінімум {ValidationConstants.User.PasswordMinLength} символів")
                .MaximumLength(ValidationConstants.User.PasswordMaxLength)
                .WithMessage($"Пароль не може перевищувати {ValidationConstants.User.PasswordMaxLength} символів")
                .Matches(ValidationConstants.RegexPatterns.Password)
                .WithMessage("Пароль повинен містити щонайменше одну малу, одну велику літеру і одну цифру");

            // Role - must be defined enum
            RuleFor(x => x.Role)
                .IsInEnum()
                .WithMessage("Невірна роль користувача");

            // PhoneNumber - optional, but must match international format if given
            RuleFor(x => x.PhoneNumber)
                .Matches(ValidationConstants.RegexPatterns.PhoneNumber)
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage("Номер телефону повинен бути у міжнародному форматі (наприклад, +кодкраїниXXXXXXXXX)");
        }
    }
}
