using Streetcode.BLL.MediatR.AdditionalContent.Coordinate;

namespace Streetcode.BLL.MediatR.Streetcode.Term.Create
{
    public class CreateTermDtoValidator : BaseTermDtoValidator
    {
        public CreateTermDtoValidator()
        {
            ConfigureSharedRules();
        }
    }
}
