using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.DTO.Streetcode.Update;

namespace Streetcode.BLL.DTO.Streetcode.Create
{
    public class StreetcodeCreateDto : CreateUpdateStreetcodeDto
    {
        public IEnumerable<StreetcodeCoordinateDto>? Coordinates { get; set; } = new List<StreetcodeCoordinateDto>();
    }
}
