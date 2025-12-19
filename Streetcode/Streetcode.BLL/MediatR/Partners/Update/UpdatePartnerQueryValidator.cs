using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Streetcode.BLL.Util.Validators;
using Streetcode.DAL.Repositories.Interfaces.Base;

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
                .WithMessage(ErrorMessages.PartnerDataRequired)
                .SetValidator(new Create.CreatePartnerDtoValidator());

            RuleFor(x => x.Partner.Id)
                .MustBeValidId(ErrorMessages.PartnerIdMustBeGreaterThanZero)
                .When(x => x.Partner != null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePartnerQueryValidator"/> class with repository wrapper for async validation.
        /// </summary>
        /// <param name="repositoryWrapper">The repository wrapper for database access.</param>
        /// <param name="cache">Optional memory cache for performance optimization.</param>
        public UpdatePartnerQueryValidator(IRepositoryWrapper repositoryWrapper, IMemoryCache? cache = null)
            : this()
        {
            var uniqueTitleValidator = new UniquePartnerTitleValidator(repositoryWrapper, cache);

            RuleFor(x => x.Partner)
                .MustAsync(async (partner, cancellation) =>
                {
                    if (partner == null || string.IsNullOrWhiteSpace(partner.Title))
                    {
                        return true;
                    }

                    return await uniqueTitleValidator.IsTitleUniqueAsync(partner.Title, partner.Id, cancellation);
                })
                .WithMessage(ErrorMessages.PartnerTitleAlreadyExists)
                .When(x => x.Partner != null);
        }
    }
}
