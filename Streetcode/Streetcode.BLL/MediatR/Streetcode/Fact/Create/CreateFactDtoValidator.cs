using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create
{
    /// <summary>
    /// Validator for CreateFactDto.
    /// </summary>
    public class CreateFactDtoValidator : AbstractValidator<CreateFactDto>
    {
        public CreateFactDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Заголовок факту є обов'язковим")
                .MaximumLength(ValidationConstants.Fact.TitleMaxLength)
                .WithMessage($"Заголовок факту не може перевищувати {ValidationConstants.Fact.TitleMaxLength} символів");

            RuleFor(x => x.FactContent)
                .NotEmpty()
                .WithMessage("Зміст факту є обов'язковим")
                .MaximumLength(ValidationConstants.Fact.ContentMaxLength)
                .WithMessage($"Зміст факту не може перевищувати {ValidationConstants.Fact.ContentMaxLength} символів");

            RuleFor(x => x.ImageId)
                .GreaterThan(0)
                .WithMessage("ID зображення має бути більше 0");

            RuleFor(x => x.StreetcodeId)
                .GreaterThan(0)
                .WithMessage("ID стріткоду має бути більше 0");
        }
    }
}
