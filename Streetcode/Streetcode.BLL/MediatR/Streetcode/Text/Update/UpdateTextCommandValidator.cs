using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Update
{
    /// <summary>
    /// Validator for UpdateTextCommand.
    /// </summary>
    public class UpdateTextCommandValidator : AbstractValidator<UpdateTextCommand>
    {
        public UpdateTextCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.TextIdMustBeGreaterThanZero);

            RuleFor(x => x.Text)
                .NotNull()
                .WithMessage(ErrorMessages.TextDataRequired)
                .SetValidator(new TextUpdateDtoValidator());
        }
    }
}
