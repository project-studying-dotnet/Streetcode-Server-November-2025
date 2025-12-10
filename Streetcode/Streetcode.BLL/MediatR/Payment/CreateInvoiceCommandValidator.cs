using FluentValidation;

namespace Streetcode.BLL.MediatR.Payment
{
    /// <summary>
    /// Validator for CreateInvoiceCommand.
    /// </summary>
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            RuleFor(x => x.Payment)
                .NotNull()
                .WithMessage("Дані платежу не можуть бути порожніми")
                .SetValidator(new PaymentDtoValidator());
        }
    }
}
