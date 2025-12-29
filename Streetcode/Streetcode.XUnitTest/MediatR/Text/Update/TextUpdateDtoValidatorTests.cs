using FluentValidation.TestHelper;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.BLL.MediatR.Streetcode.Text.Update;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Text.Update
{
    public class TextUpdateDtoValidatorTests
    {
        private TextUpdateDto CreateValidDto()
        {
            return new TextUpdateDto
            {
                Title = "Valid title",
                TextContent = "Some valid content",
                AdditionalText = "Optional extra",
                VideoUrl = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
            };
        }

        [Fact]
        public void Ctor_ShouldNotThrow()
        {
            var ex = Record.Exception(() => new TextUpdateDtoValidator());
            Assert.Null(ex);
        }

        [Fact]
        public void ValidDto_ShouldNotHaveValidationErrors()
        {
            var validator = new TextUpdateDtoValidator();
            var dto = CreateValidDto();

            var result = validator.TestValidate(dto);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void AdditionalText_NullAllowed()
        {
            var validator = new TextUpdateDtoValidator();
            var dto = CreateValidDto();
            dto.AdditionalText = null;

            var result = validator.TestValidate(dto);

            result.ShouldNotHaveValidationErrorFor(x => x.AdditionalText);
        }

        [Fact]
        public void VideoUrl_NullAllowed()
        {
            var validator = new TextUpdateDtoValidator();
            var dto = CreateValidDto();
            dto.VideoUrl = null;

            var result = validator.TestValidate(dto);

            result.ShouldNotHaveValidationErrorFor(x => x.VideoUrl);
        }
    }
}