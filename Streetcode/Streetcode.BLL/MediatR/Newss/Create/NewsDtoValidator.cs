using System;
using FluentValidation;
using Streetcode.BLL.DTO.News;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Newss.Create
{
    /// <summary>
    /// Validator for NewsDto.
    /// </summary>
    public class NewsDtoValidator : AbstractValidator<NewsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsDtoValidator"/> class.
        /// </summary>
        public NewsDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Назва новини є обов'язковою")
                .MaximumLength(ValidationConstants.News.TitleMaxLength)
                .WithMessage($"Назва новини не може перевищувати {ValidationConstants.News.TitleMaxLength} символів");

            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Текст новини є обов'язковим");

            RuleFor(x => x.URL)
                .NotEmpty()
                .WithMessage("URL новини є обов'язковим")
                .MaximumLength(ValidationConstants.News.UrlMaxLength)
                .WithMessage($"URL новини не може перевищувати {ValidationConstants.News.UrlMaxLength} символів");

            RuleFor(x => x.ImageId)
                .MustBeValidId("ImageId має бути більше 0")
                .When(x => x.ImageId.HasValue);

            RuleFor(x => x.CreationDate)
                .NotEmpty()
                .WithMessage("Дата створення є обов'язковою")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Дата створення не може бути в майбутньому");
        }
    }
}
