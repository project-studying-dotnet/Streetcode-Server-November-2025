using FluentValidation;
using Streetcode.BLL.DTO.News;
using Streetcode.BLL.Util.Validators;

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
                .WithMessage(ErrorMessages.NewsDataRequired)
                .SetValidator(new Create.NewsDtoValidator());

            RuleFor(x => x.news.Id)
                .MustBeValidId(ErrorMessages.NewsIdMustBeGreaterThanZero)
                .When(x => x.news != null);
        }
    }
}
