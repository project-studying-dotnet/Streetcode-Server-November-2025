using FluentValidation;
using Streetcode.BLL.DTO.Timeline;
using System.Text.RegularExpressions;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Update
{
    public class UpdateHistoricalContextDtoValidator : AbstractValidator<UpdateHistoricalContextDto>
    {
        public UpdateHistoricalContextDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

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
