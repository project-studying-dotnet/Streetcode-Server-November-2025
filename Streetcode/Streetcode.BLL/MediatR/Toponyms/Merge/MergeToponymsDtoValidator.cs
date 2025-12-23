using FluentValidation;
using Streetcode.BLL.DTO.Toponyms;

namespace Streetcode.BLL.MediatR.Toponyms.Merge
{
    /// <summary>
    /// Validator for MergeToponymsDto.
    /// </summary>
    public class MergeToponymsDtoValidator : AbstractValidator<MergeToponymsDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MergeToponymsDtoValidator"/> class.
        /// </summary>
        public MergeToponymsDtoValidator()
        {
            RuleFor(x => x.TargetToponymId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage(ErrorMessages.ToponymIdMustBeGreaterThanZero);

            RuleFor(x => x.SourceToponymIds)
                .NotNull()
                .NotEmpty()
                .WithMessage(ErrorMessages.SourceToponymIdsRequired);

            RuleForEach(x => x.SourceToponymIds)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.ToponymIdMustBeGreaterThanZero);

            RuleFor(x => x)
                .Must(x => x.SourceToponymIds == null || !x.SourceToponymIds.Contains(x.TargetToponymId))
                .WithMessage(ErrorMessages.TargetToponymCannotBeInSourceList);
        }
    }
}
