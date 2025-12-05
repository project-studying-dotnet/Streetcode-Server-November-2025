using FluentValidation;

namespace Streetcode.BLL.MediatR.Partners.Delete
{
    /// <summary>
    /// Validator for DeletePartnerQuery.
    /// </summary>
    public class DeletePartnerQueryValidator : AbstractValidator<DeletePartnerQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePartnerQueryValidator"/> class.
        /// </summary>
        public DeletePartnerQueryValidator()
        {
            RuleFor(x => x.id)
                .GreaterThan(0)
                .WithMessage("Partner Id must be greater than 0");
        }
    }
}
