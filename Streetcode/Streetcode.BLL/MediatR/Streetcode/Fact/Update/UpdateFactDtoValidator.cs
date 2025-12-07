using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Update
{
    /// <summary>
    /// Validator for UpdateFactDto.
    /// </summary>
    public class UpdateFactDtoValidator : BaseFactDtoValidator<UpdateFactDto>
    {
        public UpdateFactDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("ID факту має бути більше 0");

            ConfigureSharedRules();
        }

        protected override string GetTitle(UpdateFactDto dto) => dto.Title;
        protected override string GetFactContent(UpdateFactDto dto) => dto.FactContent;
        protected override int GetImageId(UpdateFactDto dto) => dto.ImageId;
    }
}
