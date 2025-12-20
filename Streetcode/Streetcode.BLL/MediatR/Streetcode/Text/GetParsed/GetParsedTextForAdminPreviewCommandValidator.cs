using FluentValidation;

namespace Streetcode.BLL.MediatR.Streetcode.Text.GetParsed
{
    /// <summary>
    /// Validator for GetParsedTextForAdminPreviewCommand.
    /// </summary>
    public class GetParsedTextForAdminPreviewCommandValidator : AbstractValidator<GetParsedTextForAdminPreviewCommand>
    {
        public GetParsedTextForAdminPreviewCommandValidator()
        {
            RuleFor(x => x.textToParse)
                .NotEmpty()
                .WithMessage("Text to parse cannot be empty")
                .MaximumLength(10000)
                .WithMessage("Text to parse cannot exceed 10000 characters");
        }
    }
}
