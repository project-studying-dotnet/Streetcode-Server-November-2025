using FluentValidation.TestHelper;
using Xunit;
using global::Streetcode.BLL.MediatR.Payment;
using global::Streetcode.BLL.DTO.Payment;
using global::Streetcode.BLL;

namespace Streetcode.XUnitTest.MediatR.Payment
{
    public class CreateInvoiceCommandValidatorTests
    {
        private readonly CreateInvoiceCommandValidator _validator;

        public CreateInvoiceCommandValidatorTests()
        {
            _validator = new CreateInvoiceCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Payment_Is_Null()
        {
            var command = new CreateInvoiceCommand(null);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Payment)
                  .WithErrorMessage(ErrorMessages.PaymentDataRequired);
        }

        [Fact]
        public void Should_Have_Error_When_Child_Validator_Fails()
        {
            var invalidDto = new PaymentDto { Amount = 0 };
            var command = new CreateInvoiceCommand(invalidDto);

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Payment.Amount);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Payment_Is_Valid()
        {
            var validDto = new PaymentDto
            {
                Amount = 100,
                RedirectUrl = "https://google.com"
            };
            var command = new CreateInvoiceCommand(validDto);

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}