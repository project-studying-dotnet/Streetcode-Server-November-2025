using System.Text.Json;
using FluentValidation;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;

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
                .Must(HaveRequiredProperty("Id"))
                .WithMessage("Id is required");

            RuleFor(x => x)
                .Must(HaveIntegerProperty("Id"))
                .WithMessage("Id must be an integer")
                .When(x => HaveRequiredProperty("Id")(x));

            RuleFor(x => x)
                .Must(HavePositiveIntegerProperty("Id"))
                .WithMessage("Id must be greater than 0")
                .When(x => HaveRequiredProperty("Id")(x) && HaveIntegerProperty("Id")(x));
        }

        private static System.Func<JsonElement, bool> HaveRequiredProperty(string propertyName)
        {
            return json => json.TryGetProperty(propertyName, out var property) && property.ValueKind != JsonValueKind.Null;
        }

        private static Func<JsonElement, bool> HaveIntegerProperty(string propertyName)
        {
            return json =>
            {
                if (json.TryGetProperty(propertyName, out var property))
                {
                    return property.ValueKind == JsonValueKind.Number && property.TryGetInt32(out _);
                }

                return false;
            };
        }

        private static Func<JsonElement, bool> HavePositiveIntegerProperty(string propertyName)
        {
            return json =>
            {
                if (json.TryGetProperty(propertyName, out var property) && property.TryGetInt32(out var value))
                {
                    return value > 0;
                }

                return false;
            };
        }
    }
}
