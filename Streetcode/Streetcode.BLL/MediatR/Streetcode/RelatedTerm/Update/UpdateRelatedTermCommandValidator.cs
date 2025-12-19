using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Update
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
                .WithMessage(ErrorMessages.RelatedTermTermIdMustBeGreaterThanZero);

            RuleFor(x => x.RelatedTerm)
                .NotNull()
                .WithMessage(ErrorMessages.RelatedTermWordRequired)
                .SetValidator(new UpdateRelatedTermDtoValidator());
        }
    }
}
