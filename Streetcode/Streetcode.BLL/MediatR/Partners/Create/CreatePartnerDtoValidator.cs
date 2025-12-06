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
                .WithMessage("Назва партнера є обов'язковою")
                .MaximumLength(ValidationConstants.Partner.TitleMaxLength)
                .WithMessage($"Назва партнера не може перевищувати {ValidationConstants.Partner.TitleMaxLength} символів");

            RuleFor(x => x.TargetUrl)
                .Must(url => UrlValidator.IsValidAbsoluteUrl(url, isRequired: false))
                .WithMessage("TargetUrl має бути дійсною абсолютною URL-адресою");

            RuleFor(x => x.LogoId)
                .GreaterThan(ValidationConstants.Common.MinId - 1)
                .WithMessage("LogoId має бути більше 0");

            RuleFor(x => x.Streetcodes)
                .NotNull()
                .WithMessage("Список стріткодів є обов'язковим");

            RuleFor(x => x.Description)
                .MaximumLength(ValidationConstants.Partner.DescriptionMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage($"Опис не може перевищувати {ValidationConstants.Partner.DescriptionMaxLength} символів");

            RuleFor(x => x.UrlTitle)
                .MaximumLength(ValidationConstants.Partner.UrlTitleMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.UrlTitle))
                .WithMessage($"UrlTitle не може перевищувати {ValidationConstants.Partner.UrlTitleMaxLength} символів");
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
                .WithMessage("Партнер з такою назвою вже існує")
                .When(x => !string.IsNullOrWhiteSpace(x.Title));
        }
    }
}
