using System.Text.Json;
using FluentValidation;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Update
{
    /// <summary>
    /// Validator for Update Streetcode DTO JSON structure.
    /// </summary>
    public class UpdateStreetcodeDtoValidator : AbstractValidator<JsonElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStreetcodeDtoValidator"/> class.
        /// </summary>
        public UpdateStreetcodeDtoValidator()
        {
            Include(new CreateStreetcodeDtoValidator());

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveRequiredProperty("Id"))
                .WithMessage("Id є обов'язковим");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveIntegerProperty("Id"))
                .WithMessage("Id має бути цілим числом")
                .When(x => JsonElementValidator.HaveRequiredProperty("Id")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HavePositiveIntegerProperty("Id"))
                .WithMessage($"Id має бути більше {ValidationConstants.Common.MinPositiveValue}")
                .When(x => JsonElementValidator.HaveProperty("Id")(x) && JsonElementValidator.HaveIntegerProperty("Id")(x));
        }
    }
}
