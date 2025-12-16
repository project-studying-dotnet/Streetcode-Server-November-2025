using FluentValidation;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
    /// <summary>
    /// Validator for CreateTagQuery.
    /// </summary>
    public class CreateTagQueryValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagQueryValidator()
        {
            RuleFor(x => x.tag)
                .NotNull()
                .WithMessage("Дані тегу не можуть бути порожніми")
                .SetValidator(new CreateTagDtoValidator());
        }
    }
}
