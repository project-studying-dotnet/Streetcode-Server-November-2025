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
                .WithMessage("Id is required");

            RuleFor(x => x)
                .Must(JsonElementValidator.HaveIntegerProperty("Id"))
                .WithMessage("Id must be an integer")
                .When(x => JsonElementValidator.HaveRequiredProperty("Id")(x));

            RuleFor(x => x)
                .Must(JsonElementValidator.HavePositiveIntegerProperty("Id"))
                .WithMessage($"Id must be greater than {ValidationConstants.Common.MinId - 1}")
                .When(x => JsonElementValidator.HaveRequiredProperty("Id")(x) && JsonElementValidator.HaveIntegerProperty("Id")(x));
        }
    }
}
