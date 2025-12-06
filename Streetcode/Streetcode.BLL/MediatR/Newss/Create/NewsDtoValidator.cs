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
                .WithMessage("News title is required")
                .MaximumLength(ValidationConstants.News.TitleMaxLength)
                .WithMessage($"News title must not exceed {ValidationConstants.News.TitleMaxLength} characters");

            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("News text is required");

            RuleFor(x => x.URL)
                .NotEmpty()
                .WithMessage("News URL is required")
                .MaximumLength(ValidationConstants.News.UrlMaxLength)
                .WithMessage($"News URL must not exceed {ValidationConstants.News.UrlMaxLength} characters");

            RuleFor(x => x.ImageId)
                .GreaterThan(ValidationConstants.Common.MinId - 1)
                .When(x => x.ImageId.HasValue)
                .WithMessage("ImageId must be greater than 0");

            RuleFor(x => x.CreationDate)
                .NotEmpty()
                .WithMessage("CreationDate is required")
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("CreationDate cannot be in the future");
        }
    }
}
