using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;

namespace Streetcode.BLL.DTO.AdditionalContent.Locations
{
    public class MapPointResponseDto
    {
        public int Id { get; set; }
        public int PlateNumber { get; set; }
        public StreetcodeCoordinateDto StreetcodeCoordinate { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
