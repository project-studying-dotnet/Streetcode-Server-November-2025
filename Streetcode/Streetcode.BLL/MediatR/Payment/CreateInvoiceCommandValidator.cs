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
                .WithMessage(ErrorMessages.PaymentDataRequired)
                .SetValidator(new PaymentDtoValidator());
        }
    }
}
