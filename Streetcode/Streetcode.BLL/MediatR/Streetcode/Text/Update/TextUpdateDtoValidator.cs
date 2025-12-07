using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Update
{
    /// <summary>
    /// Validator for TextUpdateDto.
    /// </summary>
    public class TextUpdateDtoValidator : BaseTextDtoValidator<TextUpdateDto>
    {
        public TextUpdateDtoValidator()
        {
            ConfigureSharedRules();
        }

        protected override string GetTitle(TextUpdateDto dto) => dto.Title;
        protected override string GetTextContent(TextUpdateDto dto) => dto.TextContent;
        protected override string? GetAdditionalText(TextUpdateDto dto) => dto.AdditionalText;
        protected override string? GetVideoUrl(TextUpdateDto dto) => dto.VideoUrl;
    }
}
