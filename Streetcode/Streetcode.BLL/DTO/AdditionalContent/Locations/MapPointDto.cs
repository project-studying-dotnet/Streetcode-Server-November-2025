using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;

namespace Streetcode.BLL.DTO.AdditionalContent.Locations
{
    public class MapPointDto
    {
        public int PlateNumber { get; set; }
        public StreetcodeCoordinateDto StreetcodeCoordinate { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
