using FluentValidation;

namespace Streetcode.BLL.MediatR.Toponyms.Delete
{
    public class DeleteStreetcodeToponymCommandValidator : AbstractValidator<DeleteStreetcodeToponymCommand>
    {
        public DeleteStreetcodeToponymCommandValidator()
        {
            RuleFor(x => x.StreetcodeId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.StreetcodeIdMustBeGreaterThanZero);

            RuleFor(x => x.ToponymId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.ToponymIdMustBeGreaterThanZero);
        }
    }
}
