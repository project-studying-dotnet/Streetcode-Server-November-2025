namespace Streetcode.XUnitTest.MediatR.Payment
{
    using FluentValidation.TestHelper;
    using Streetcode.BLL.DTO.Payment;
    using Streetcode.BLL.MediatR.Payment;
    using Streetcode.BLL.Util.Validators;
    using Xunit;

    public class PaymentDtoValidatorTests
    {
        private readonly PaymentDtoValidator _validator;

        public PaymentDtoValidatorTests()
        {
            _validator = new PaymentDtoValidator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Should_Have_Error_When_Amount_Is_Too_Low(long amount)
        {
            var dto = new PaymentDto { Amount = amount };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Amount)
                  .WithErrorMessage("Сума платежу має бути більше 0");
        }

        [Fact]
        public void Should_Have_Error_When_Amount_Is_Too_High()
        {
            var dto = new PaymentDto { Amount = ValidationConstants.Payment.MaxAmount + 1 };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Amount)
                  .WithErrorMessage($"Сума платежу не може перевищувати {ValidationConstants.Payment.MaxAmount:N0}");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Amount_Is_Valid()
        {
            var dto = new PaymentDto { Amount = 100 };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Amount);
        }

        [Fact]
        public void Should_Have_Error_When_RedirectUrl_Is_Too_Long()
        {
            var dto = new PaymentDto
            {
                RedirectUrl = new string('a', ValidationConstants.Payment.RedirectUrlMaxLength + 1)
            };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.RedirectUrl)
                  .WithErrorMessage($"URL перенаправлення не може перевищувати {ValidationConstants.Payment.RedirectUrlMaxLength} символів");
        }

        [Fact]
        public void Should_Have_Error_When_RedirectUrl_Is_Invalid_Format()
        {
            var dto = new PaymentDto { RedirectUrl = "not-a-valid-url" };
            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.RedirectUrl)
                  .WithErrorMessage("URL перенаправлення має бути правильним");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Not_Have_Error_When_RedirectUrl_Is_Empty(string url)
        {
            var dto = new PaymentDto { RedirectUrl = url };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.RedirectUrl);
        }

        [Fact]
        public void Should_Not_Have_Error_When_RedirectUrl_Is_Valid()
        {
            var dto = new PaymentDto { RedirectUrl = "https://monobank.ua" };
            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.RedirectUrl);
        }
    }
}