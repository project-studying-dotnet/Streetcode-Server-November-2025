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
                .WithMessage("ID тексту має бути більше 0");

            RuleFor(x => x.Text)
                .NotNull()
                .WithMessage("Дані тексту не можуть бути порожніми")
                .SetValidator(new TextUpdateDtoValidator());
        }
    }
}
