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
                .WithMessage(ErrorMessages.NewsTitleRequired)
                .MaximumLength(ValidationConstants.News.TitleMaxLength)
                .WithMessage(string.Format(
                    ErrorMessages.NewsTitleTooLong,
                    ValidationConstants.News.TitleMaxLength));

            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage(ErrorMessages.NewsTextRequired);

            RuleFor(x => x.URL)
                .NotEmpty()
                .WithMessage(ErrorMessages.NewsUrlRequired)
                .MaximumLength(ValidationConstants.News.UrlMaxLength)
                .WithMessage(string.Format(
                    ErrorMessages.NewsUrlTooLong,
                    ValidationConstants.News.UrlMaxLength));

            RuleFor(x => x.ImageId)
                .MustBeValidId(ErrorMessages.ImageIdMustBeGreaterThanZero)
                .When(x => x.ImageId.HasValue);

            RuleFor(x => x.CreationDate)
                .NotEmpty()
                .WithMessage(ErrorMessages.NewsCreationDateRequired)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage(ErrorMessages.NewsCreationDateInFuture);
        }
    }
}
