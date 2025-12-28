using System.Text.RegularExpressions;
using FluentValidation;
using Streetcode.BLL.DTO.Timeline;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Update
{
    public class UpdateHistoricalContextDtoValidator : AbstractValidator<UpdateHistoricalContextDto>
    {
        public UpdateHistoricalContextDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(ErrorMessages.HistoricalContextIdMustBeGreaterThanZero);

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ErrorMessages.HistoricalContextTitleRequired)
                .MaximumLength(50).WithMessage(string.Format(ErrorMessages.HistoricalContextTitleTooLong, 50))
                .Must(BeAlphabeticWithSpaces).WithMessage(ErrorMessages.HistoricalContextTitleInvalidFormat);
        }

        private bool BeAlphabeticWithSpaces(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return true;
            }

            return Regex.IsMatch(title, @"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ\s]+$");
        }
    }
}
