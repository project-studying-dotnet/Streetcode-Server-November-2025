using FluentValidation;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Fact
{
    /// <summary>
    /// Base validator containing shared validation rules for Fact DTOs.
    /// </summary>
    public abstract class BaseFactDtoValidator<T> : AbstractValidator<T>
    {
        protected void ConfigureSharedRules()
        {
            RuleFor(x => GetTitle(x))
                .NotEmpty()
                .WithMessage(ErrorMessages.FactTitleRequired)
                .MaximumLength(ValidationConstants.Fact.TitleMaxLength)
                .WithMessage(string.Format(
                    ErrorMessages.FactTitleTooLong,
                    ValidationConstants.Fact.TitleMaxLength));

            RuleFor(x => GetFactContent(x))
                .NotEmpty()
                .WithMessage(ErrorMessages.FactContentRequired)
                .MaximumLength(ValidationConstants.Fact.ContentMaxLength)
                .WithMessage(string.Format(
                    ErrorMessages.FactContentTooLong,
                    ValidationConstants.Fact.ContentMaxLength));

            RuleFor(x => GetImageId(x))
                .GreaterThan(0)
                .WithMessage(ErrorMessages.FactImageIdMustBeGreaterThanZero);
        }

        protected abstract string GetTitle(T dto);
        protected abstract string GetFactContent(T dto);
        protected abstract int GetImageId(T dto);
    }
}
