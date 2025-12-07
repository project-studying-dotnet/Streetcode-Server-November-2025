using FluentValidation;
using Streetcode.BLL.Util.Validators;

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
                .MustBeValidId("ID партнера має бути більше 0");
        }
    }
}
