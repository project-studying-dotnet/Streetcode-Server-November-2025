using System.Text.RegularExpressions;
using FluentValidation;
using Streetcode.BLL.DTO.Timeline;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create
{
    public class CreateHistoricalContextDtoValidator : AbstractValidator<CreateHistoricalContextDto>
    {
        public CreateHistoricalContextDtoValidator()
        {
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
