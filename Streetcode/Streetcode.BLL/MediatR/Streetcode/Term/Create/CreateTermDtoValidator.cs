using Streetcode.BLL.MediatR.AdditionalContent.Coordinate;

namespace Streetcode.BLL.MediatR.Term.Create
{
    public class CreateTermDtoValidator : BaseTermDtoValidator
    {
        public CreateTermDtoValidator()
        {
            ConfigureSharedRules();
        }
    }
}
