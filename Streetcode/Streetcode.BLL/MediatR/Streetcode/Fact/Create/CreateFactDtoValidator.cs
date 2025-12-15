using FluentValidation;
using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
using Streetcode.BLL.Util.Validators;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create
{
    /// <summary>
    /// Validator for CreateFactDto.
    /// </summary>
    public class CreateFactDtoValidator : BaseFactDtoValidator<CreateFactDto>
    {
        public CreateFactDtoValidator()
        {
            ConfigureSharedRules();

            RuleFor(x => x.StreetcodeId)
                .GreaterThan(0)
                .WithMessage("ID стріткоду має бути більше 0");
        }

        protected override string GetTitle(CreateFactDto dto) => dto.Title;
        protected override string GetFactContent(CreateFactDto dto) => dto.FactContent;
        protected override int GetImageId(CreateFactDto dto) => dto.ImageId;
    }
}
