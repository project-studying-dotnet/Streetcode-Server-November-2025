using FluentValidation;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
    /// <summary>
    /// Validator for CreateTagQuery.
    /// </summary>
    public class CreateTagQueryValidator : AbstractValidator<CreateTagQuery>
    {
        public CreateTagQueryValidator()
        {
            RuleFor(x => x.tag)
                .NotNull()
                .WithMessage(ErrorMessages.TagDataCantBeEmpty)
                .SetValidator(new CreateTagDtoValidator());
        }
    }
}
