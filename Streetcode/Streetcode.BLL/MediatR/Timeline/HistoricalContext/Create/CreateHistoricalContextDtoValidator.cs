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
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Title cannot exceed 50 characters")
                .Must(BeAlphabeticWithSpaces).WithMessage("Title can only contain letters and spaces");
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
