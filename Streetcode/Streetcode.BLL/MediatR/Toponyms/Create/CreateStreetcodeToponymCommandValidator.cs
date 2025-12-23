using FluentValidation;

namespace Streetcode.BLL.MediatR.Toponyms.Create
{
    public class CreateStreetcodeToponymCommandValidator : AbstractValidator<CreateStreetcodeToponymCommand>
    {
        public CreateStreetcodeToponymCommandValidator()
        {
            RuleFor(x => x.StreetcodeToponym)
                .NotNull()
                .WithMessage(ErrorMessages.CreateToponymDataRequired)
                .SetValidator(new StreetcodeToponymDtoValidator());
        }
    }
}
