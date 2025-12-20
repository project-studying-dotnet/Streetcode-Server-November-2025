using FluentValidation;
using Streetcode.BLL.DTO.AdditionalContent.Tag;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
    /// <summary>
    /// Validator for CreateTagDto.
    /// </summary>
    public class CreateTagDtoValidator : AbstractValidator<CreateTagDto>
    {
        public CreateTagDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(ErrorMessages.TagNameIsRequired)
                .MaximumLength(ValidationConstants.Tag.TitleMaxLength)
                .WithMessage(string.Format(ErrorMessages.TagNameCantExceed, ValidationConstants.Tag.TitleMaxLength))
                .Matches(@"^[а-яА-ЯіІїЇєЄґҐa-zA-Z0-9\s\-]+$")
                .WithMessage(ErrorMessages.TagFormatError);
        }
    }
}
