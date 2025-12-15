using FluentValidation;
using Streetcode.BLL.DTO.News;

namespace Streetcode.BLL.MediatR.Newss.Create
{
    /// <summary>
    /// Validator for CreateNewsCommand.
    /// </summary>
    public class CreateNewsCommandValidator : AbstractValidator<CreateNewsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewsCommandValidator"/> class.
        /// </summary>
        public CreateNewsCommandValidator()
        {
            RuleFor(x => x.newNews)
                .NotNull()
                .WithMessage("Дані новини є обов'язковими")
                .SetValidator(new NewsDtoValidator());
        }
    }
}
