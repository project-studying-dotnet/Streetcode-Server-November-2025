using FluentValidation;

namespace Streetcode.BLL.MediatR.Toponyms.Merge
{
    public class MergeToponymsCommandValidator : AbstractValidator<MergeToponymsCommand>
    {
        public MergeToponymsCommandValidator()
        {
            RuleFor(x => x.MergeRequest)
                .NotNull()
                .WithMessage(ErrorMessages.MergeToponymDataRequired)
                .SetValidator(new MergeToponymsDtoValidator());
        }
    }
}
