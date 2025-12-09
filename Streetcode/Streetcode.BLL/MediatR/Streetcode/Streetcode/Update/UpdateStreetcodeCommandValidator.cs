using System.Text.Json;
using FluentValidation;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Update
{
    /// <summary>
    /// Validator for UpdateStreetcodeCommand.
    /// </summary>
    public class UpdateStreetcodeCommandValidator : AbstractValidator<UpdateStreetcodeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStreetcodeCommandValidator"/> class.
        /// </summary>
        public UpdateStreetcodeCommandValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0)
                .WithMessage("Id має бути більше 0");

            RuleFor(x => x.rawJsonUpdateDTO)
                .NotEmpty()
                .WithMessage("Дані стріткоду є обов'язковими")
                .Must(BeValidJson)
                .WithMessage("Невірна структура JSON")
                .DependentRules(() =>
                {
                    RuleFor(x => x.rawJsonUpdateDTO)
                        .SetValidator(new UpdateStreetcodeDtoValidator());
                });
        }

        private static bool BeValidJson(JsonElement json)
        {
            return json.ValueKind == JsonValueKind.Object;
        }
    }
}
