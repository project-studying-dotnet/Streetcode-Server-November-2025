using FluentValidation;
using Streetcode.BLL.DTO.Email;

namespace Streetcode.BLL.MediatR.Email
{
    /// <summary>
    /// Validator for EmailDto.
    /// </summary>
    public class EmailDtoValidator : AbstractValidator<EmailDto>
    {
        public EmailDtoValidator()
        {
            RuleFor(x => x.From)
                .NotEmpty()
                .WithMessage("Email sender cannot be empty")
                .MaximumLength(80)
                .WithMessage("Email sender cannot exceed 80 characters")
                .EmailAddress()
                .WithMessage("Invalid email address format");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Email content cannot be empty")
                .MinimumLength(1)
                .WithMessage("Email content must be at least 1 character")
                .MaximumLength(500)
                .WithMessage("Email content cannot exceed 500 characters");
        }
    }
}
