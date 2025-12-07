using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Create
{
    /// <summary>
    /// Validator for TextCreateDto.
    /// </summary>
    public class TextCreateDtoValidator : BaseTextDtoValidator<TextCreateDto>
    {
        public TextCreateDtoValidator()
        {
            ConfigureSharedRules();

            RuleFor(x => x.StreetcodeId)
                .GreaterThan(0)
                .WithMessage("ID стріткоду має бути більше 0");
        }

        protected override string GetTitle(TextCreateDto dto) => dto.Title;
        protected override string GetTextContent(TextCreateDto dto) => dto.TextContent;
        protected override string? GetAdditionalText(TextCreateDto dto) => dto.AdditionalText;
        protected override string? GetVideoUrl(TextCreateDto dto) => dto.VideoUrl;
    }
}
