using FluentValidation;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Partners.Update
{
    /// <summary>
    /// Validator for UpdatePartnerQuery.
    /// </summary>
    public class UpdatePartnerQueryValidator : AbstractValidator<UpdatePartnerQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePartnerQueryValidator"/> class.
        /// </summary>
        public UpdatePartnerQueryValidator()
        {
            RuleFor(x => x.Partner)
                .NotNull()
                .WithMessage("Partner data is required")
                .SetValidator(new Create.CreatePartnerDtoValidator());

            RuleFor(x => x.Partner.Id)
                .GreaterThan(ValidationConstants.Common.MinId - 1)
                .When(x => x.Partner != null)
                .WithMessage("Partner Id must be greater than 0");
        }
    }
}
