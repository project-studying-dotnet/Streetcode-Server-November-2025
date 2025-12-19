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
                .WithMessage(ErrorMessages.StreetcodeIdRequired);

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveIntegerProperty("Id"))
                .WithMessage(ErrorMessages.StreetcodeIdMustBeInteger)
                .When(x => JsonElementValidator.HaveRequiredProperty("Id")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HavePositiveIntegerProperty("Id"))
                .WithMessage(string.Format(
                    ErrorMessages.StreetcodeIdMustBeGreaterThanZero,
                    ValidationConstants.Common.MinPositiveValue))
                .When(x => JsonElementValidator.HaveProperty("Id")(x) && JsonElementValidator.HaveIntegerProperty("Id")(x));
        }
    }
}
