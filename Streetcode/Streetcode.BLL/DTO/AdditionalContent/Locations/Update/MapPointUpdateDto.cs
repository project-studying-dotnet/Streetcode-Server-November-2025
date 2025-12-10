using Streetcode.BLL.DTO.AdditionalContent.Coordinates;
using Streetcode.BLL.Enums;

namespace Streetcode.BLL.DTO.AdditionalContent.Locations.Update
{
    public class MapPointUpdateDto
    {
        public int Id { get; set; }
        public int PlateNumber { get; set; }
        public string Address { get; set; } = null!;
        public int StreetcodeId { get; set; }
        public StreetcodeCoordinateUpdateDto StreetcodeCoordinate { get; set; } = null!;
        public ModelState ModelState { get; set; }
    }
}
