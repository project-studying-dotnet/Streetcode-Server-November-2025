using FluentValidation;
using Streetcode.BLL.DTO.Payment;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Payment
{
    /// <summary>
    /// Validator for PaymentDto.
    /// </summary>
    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.PaymentAmountMustBeGreaterThanZero)
                .LessThanOrEqualTo(ValidationConstants.Payment.MaxAmount)
                .WithMessage(string.Format(
                    ErrorMessages.PaymentAmountExceeded,
                    ValidationConstants.Payment.MaxAmount));

            RuleFor(x => x.RedirectUrl)
                .MaximumLength(ValidationConstants.Payment.RedirectUrlMaxLength)
                .When(x => !string.IsNullOrEmpty(x.RedirectUrl))
                .WithMessage(string.Format(
                    ErrorMessages.PaymentRedirectUrlTooLong,
                    ValidationConstants.Payment.RedirectUrlMaxLength))
                .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: false))
                .When(x => !string.IsNullOrEmpty(x.RedirectUrl))
                .WithMessage(ErrorMessages.PaymentRedirectUrlInvalid);
        }
    }
}
