using FluentValidation;
using Streetcode.BLL.DTO.Toponyms;

namespace Streetcode.BLL.MediatR.Toponyms.Create
{
    /// <summary>
    /// Validator for StreetcodeToponymDto.
    /// </summary>
    public class StreetcodeToponymDtoValidator : AbstractValidator<StreetcodeToponymDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StreetcodeToponymDtoValidator"/> class.
        /// </summary>
        public StreetcodeToponymDtoValidator()
        {
            RuleFor(x => x.StreetcodeId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage(ErrorMessages.StreetcodeIdMustBeGreaterThanZero);

            RuleFor(x => x.ToponymId)
                .GreaterThan(0)
                .WithMessage(ErrorMessages.ToponymIdMustBeGreaterThanZero);
        }
    }
}
