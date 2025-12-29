using FluentValidation;
using Microsoft.Extensions.Caching.Memory;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.Util.Validators;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Partners.Create
{
    /// <summary>
    /// Validator for CreatePartnerDto.
    /// </summary>
    public class CreatePartnerDtoValidator : AbstractValidator<CreatePartnerDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePartnerDtoValidator"/> class.
        /// </summary>
        public CreatePartnerDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(ErrorMessages.PartnerTitleRequired)
                .MaximumLength(ValidationConstants.Partner.TitleMaxLength)
                .WithMessage(string.Format(
                    ErrorMessages.PartnerTitleTooLong,
                    ValidationConstants.Partner.TitleMaxLength));

            RuleFor(x => x.TargetUrl)
                .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: false))
                .WithMessage(ErrorMessages.PartnerTargetUrlInvalid);

            RuleFor(x => x.LogoId)
                .MustBeValidId(ErrorMessages.PartnerLogoIdMustBeGreaterThanZero);

            RuleFor(x => x.Streetcodes)
                .NotNull()
                .WithMessage(ErrorMessages.PartnerStreetcodesRequired);

            RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.Partner.DescriptionMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage(string.Format(
                    ErrorMessages.PartnerDescriptionTooLong,
                    ValidationConstants.Partner.DescriptionMaxLength));

            RuleFor(x => x.UrlTitle)
                .MaximumLength(ValidationConstants.Partner.UrlTitleMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.UrlTitle))
                .WithMessage(string.Format(
                    ErrorMessages.PartnerUrlTitleTooLong,
                    ValidationConstants.Partner.UrlTitleMaxLength));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePartnerDtoValidator"/> class with repository wrapper for async validation.
        /// </summary>
        /// <param name="repositoryWrapper">The repository wrapper for database access.</param>
        /// <param name="cache">Optional memory cache for performance optimization.</param>
        public CreatePartnerDtoValidator(IRepositoryWrapper repositoryWrapper, IMemoryCache? cache = null)
            : this()
        {
            var uniqueTitleValidator = new UniquePartnerTitleValidator(repositoryWrapper, cache);

            RuleFor(x => x.Title)
                .MustAsync(async (title, cancellation) => await uniqueTitleValidator.IsTitleUniqueAsync(title, cancellation))
                .WithMessage(ErrorMessages.PartnerTitleAlreadyExists)
                .When(x => !string.IsNullOrWhiteSpace(x.Title));
        }
    }
}
