using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Delete
{
    /// <summary>
    /// Validator for DeleteTextCommand.
    /// </summary>
    public class DeleteTextCommandValidator : AbstractValidator<DeleteTextCommand>
    {
        public DeleteTextCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("ID тексту має бути більше 0");
        }
    }
}
