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
                .WithMessage("ID пов'язаного терміну має бути більше 0");

            RuleFor(x => x.RelatedTerm)
                .NotNull()
                .WithMessage("Дані пов'язаного терміну не можуть бути порожніми")
                .SetValidator(new UpdateRelatedTermDtoValidator());
        }
    }
}
