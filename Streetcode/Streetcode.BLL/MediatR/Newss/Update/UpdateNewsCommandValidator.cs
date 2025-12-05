using FluentValidation;
using Streetcode.BLL.DTO.News;

namespace Streetcode.BLL.MediatR.Newss.Update
{
    /// <summary>
    /// Validator for UpdateNewsCommand.
    /// </summary>
    public class UpdateNewsCommandValidator : AbstractValidator<UpdateNewsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNewsCommandValidator"/> class.
        /// </summary>
        public UpdateNewsCommandValidator()
        {
            RuleFor(x => x.news)
                .NotNull()
                .WithMessage("News data is required")
                .SetValidator(new Create.NewsDtoValidator());

            RuleFor(x => x.news.Id)
                .GreaterThan(0)
                .When(x => x.news != null)
                .WithMessage("News Id must be greater than 0");
        }
    }
}
