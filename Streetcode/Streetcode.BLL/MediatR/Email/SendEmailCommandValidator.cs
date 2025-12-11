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
                .WithMessage("Дані електронного листа не можуть бути порожніми")
                .SetValidator(new EmailDtoValidator());
        }
    }
}
