using FluentValidation;

namespace Streetcode.BLL.MediatR.Email
{
    /// <summary>
    /// Validator for SendEmailCommand.
    /// </summary>
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Email cannot be null")
                .SetValidator(new EmailDtoValidator());
        }
    }
}
