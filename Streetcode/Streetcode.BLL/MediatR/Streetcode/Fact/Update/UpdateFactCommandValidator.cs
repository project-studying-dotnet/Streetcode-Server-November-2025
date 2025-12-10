using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Update
{
    /// <summary>
    /// Validator for UpdateFactCommand.
    /// </summary>
    public class UpdateFactCommandValidator : AbstractValidator<UpdateFactCommand>
    {
        public UpdateFactCommandValidator()
        {
            RuleFor(x => x.updateFact)
                .NotNull()
                .WithMessage("Дані факту не можуть бути порожніми")
                .SetValidator(new UpdateFactDtoValidator());
        }
    }
}
