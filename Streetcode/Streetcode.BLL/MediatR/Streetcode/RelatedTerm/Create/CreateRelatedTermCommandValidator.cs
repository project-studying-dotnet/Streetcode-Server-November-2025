using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create
{
    /// <summary>
    /// Validator for CreateRelatedTermCommand.
    /// </summary>
    public class CreateRelatedTermCommandValidator : AbstractValidator<CreateRelatedTermCommand>
    {
        public CreateRelatedTermCommandValidator()
        {
            RuleFor(x => x.RelatedTerm)
                .NotNull()
                .WithMessage(ErrorMessages.RelatedTermDataRequired)
                .SetValidator(new RelatedTermDtoValidator());
        }
    }
}
