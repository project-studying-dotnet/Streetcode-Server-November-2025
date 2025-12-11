using FluentValidation;
using Streetcode.BLL.DTO.Email;
using Streetcode.BLL.Util.Validators;

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
                .MaximumLength(ValidationConstants.Email.FromMaxLength)
                .When(x => !string.IsNullOrEmpty(x.From))
                .WithMessage($"Поле 'Від кого' не може перевищувати {ValidationConstants.Email.FromMaxLength} символів");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Зміст листа є обов'язковим")
                .MaximumLength(ValidationConstants.Email.ContentMaxLength)
                .WithMessage($"Зміст листа не може перевищувати {ValidationConstants.Email.ContentMaxLength} символів");
        }
    }
}
