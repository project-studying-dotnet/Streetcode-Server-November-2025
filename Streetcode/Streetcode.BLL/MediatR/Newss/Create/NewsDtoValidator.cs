using System;
using FluentValidation;
using Streetcode.BLL.DTO.News;

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
                .MaximumLength(150)
                .WithMessage("News title must not exceed 150 characters");

            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("News text is required");

            RuleFor(x => x.URL)
                .NotEmpty()
                .WithMessage("News URL is required")
                .MaximumLength(100)
                .WithMessage("News URL must not exceed 100 characters");

            RuleFor(x => x.ImageId)
                .GreaterThan(0)
                .When(x => x.ImageId.HasValue)
                .WithMessage("ImageId must be greater than 0");

            RuleFor(x => x.CreationDate)
                .NotEmpty()
                .WithMessage("CreationDate is required")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("CreationDate cannot be in the future");
        }
    }
}
