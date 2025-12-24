using FluentValidation;
using Streetcode.Auth.Application.Dtos.Users;

namespace Streetcode.Auth.Application.MediatR.Register
{
    public class RegisterDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterDtoValidator()
        {
            // TODO
            // Needs to be implemented later
            // If rewriting to Microservices, core logic for validation must be moved
            // to BuildingBlocks, then we can use it here

            //// Name - required, field-specific length
            //RuleFor(x => x.Name)
            //    .NotEmpty()
            //    .WithMessage(ErrorMessages.UserNameRequired)
            //    .MaximumLength(ValidationConstants.User.NameMaxLength)
            //    .WithMessage(string.Format(
            //        ErrorMessages.UserNameTooLong,
            //        ValidationConstants.User.NameMaxLength));

            //// Surname - allow optional, if present, check field-specific length
            //RuleFor(x => x.Surname)
            //    .MaximumLength(ValidationConstants.User.SurnameMaxLength)
            //    .WithMessage(string.Format(
            //        ErrorMessages.UserSurnameTooLong,
            //        ValidationConstants.User.SurnameMaxLength));

            //// UserName - required, field-specific length, and allowed format
            //RuleFor(x => x.UserName)
            //    .NotEmpty()
            //    .WithMessage(ErrorMessages.UserLoginRequired)
            //    .MaximumLength(ValidationConstants.User.UserNameMaxLength)
            //    .WithMessage(string.Format(
            //        ErrorMessages.UserLoginTooLong,
            //        ValidationConstants.User.UserNameMaxLength))
            //    .Matches(ValidationConstants.RegexPatterns.UserName)
            //    .WithMessage(ErrorMessages.UserLoginInvalidFormat);

            //// Email - required, robust format (standard regex), and max length
            //RuleFor(x => x.Email)
            //    .NotEmpty()
            //    .WithMessage(ErrorMessages.UserEmailRequired)
            //    .EmailAddress()
            //    .WithMessage(ErrorMessages.UserEmailInvalidFormat)
            //    .MaximumLength(ValidationConstants.User.EmailMaxLength)
            //    .WithMessage(string.Format(
            //        ErrorMessages.UserEmailTooLong,
            //        ValidationConstants.User.EmailMaxLength));

            //// Password - required, min/max, Identity-like composition
            //RuleFor(x => x.Password)
            //    .NotEmpty()
            //    .WithMessage(ErrorMessages.UserPasswordRequired)
            //    .MinimumLength(ValidationConstants.User.PasswordMinLength)
            //    .WithMessage(string.Format(
            //        ErrorMessages.UserPasswordTooShort,
            //        ValidationConstants.User.PasswordMinLength))
            //    .MaximumLength(ValidationConstants.User.PasswordMaxLength)
            //    .WithMessage(string.Format(
            //        ErrorMessages.UserPasswordTooLong,
            //        ValidationConstants.User.PasswordMaxLength))
            //    .Matches(ValidationConstants.RegexPatterns.Password)
            //    .WithMessage(ErrorMessages.UserPasswordInvalidFormat);

            //// Role - must be defined enum
            //RuleFor(x => x.Role)
            //    .IsInEnum()
            //    .WithMessage(ErrorMessages.UserRoleInvalid);

            //// PhoneNumber - optional, but must match international format if given
            //RuleFor(x => x.PhoneNumber)
            //    .Matches(ValidationConstants.RegexPatterns.PhoneNumber)
            //    .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            //    .WithMessage(ErrorMessages.UserPhoneNumberInvalidFormat);
        }
    }
}
