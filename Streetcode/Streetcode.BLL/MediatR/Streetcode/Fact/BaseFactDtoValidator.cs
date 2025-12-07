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
                .WithMessage("Заголовок факту є обов'язковим")
                .MaximumLength(ValidationConstants.Fact.TitleMaxLength)
                .WithMessage($"Заголовок факту не може перевищувати {ValidationConstants.Fact.TitleMaxLength} символів");

            RuleFor(x => GetFactContent(x))
                .NotEmpty()
                .WithMessage("Зміст факту є обов'язковим")
                .MaximumLength(ValidationConstants.Fact.ContentMaxLength)
                .WithMessage($"Зміст факту не може перевищувати {ValidationConstants.Fact.ContentMaxLength} символів");

            RuleFor(x => GetImageId(x))
                .GreaterThan(0)
                .WithMessage("ID зображення має бути більше 0");
        }

        protected abstract string GetTitle(T dto);
        protected abstract string GetFactContent(T dto);
        protected abstract int GetImageId(T dto);
    }
}
