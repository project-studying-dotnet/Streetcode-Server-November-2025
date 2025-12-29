using FluentValidation;

namespace Streetcode.BLL.MediatR.RelatedTerm.Update
{
    /// <summary>
    /// Validator for UpdateRelatedTermCommand.
    /// </summary>
    public class UpdateRelatedTermCommandValidator : AbstractValidator<UpdateRelatedTermCommand>
    {
        public UpdateRelatedTermCommandValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.RelatedTermIdMustBeGreaterThanZero);

            RuleFor(x => x.RelatedTerm)
                .NotNull()
                .WithMessage(ErrorMessages.RelatedTermWordRequired)
                .SetValidator(new UpdateRelatedTermDtoValidator());
        }
    }
}
