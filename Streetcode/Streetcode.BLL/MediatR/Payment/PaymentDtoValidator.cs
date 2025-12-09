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
                .WithMessage("Сума платежу має бути більше 0")
                .LessThanOrEqualTo(ValidationConstants.Payment.MaxAmount)
                .WithMessage($"Сума платежу не може перевищувати {ValidationConstants.Payment.MaxAmount:N0}");

            RuleFor(x => x.RedirectUrl)
                .MaximumLength(ValidationConstants.Payment.RedirectUrlMaxLength)
                .When(x => !string.IsNullOrEmpty(x.RedirectUrl))
                .WithMessage($"URL перенаправлення не може перевищувати {ValidationConstants.Payment.RedirectUrlMaxLength} символів")
                .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: false))
                .When(x => !string.IsNullOrEmpty(x.RedirectUrl))
                .WithMessage("URL перенаправлення має бути правильним");
        }
    }
}
