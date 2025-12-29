using System.Text.Json;
using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Create
{
    /// <summary>
    /// Validator for CreateStreetcodeCommand.
    /// </summary>
    public class CreateStreetcodeCommandValidator : AbstractValidator<CreateStreetcodeCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStreetcodeCommandValidator"/> class.
        /// </summary>
        public CreateStreetcodeCommandValidator()
        {
            RuleFor(x => x.rawJsonCreateDTO)
                .NotEmpty()
                .WithMessage(ErrorMessages.StreetcodeDataRequired)
                .Must(BeValidJson)
                .WithMessage(ErrorMessages.WrongJSONStructure)
                .DependentRules(() =>
                {
                    RuleFor(x => x.rawJsonCreateDTO)
                        .SetValidator(new CreateStreetcodeDtoValidator());
                });
        }

        private static bool BeValidJson(JsonElement json)
        {
            return json.ValueKind == JsonValueKind.Object;
        }
    }
}
