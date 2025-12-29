using Streetcode.BLL.Enums;

namespace Streetcode.BLL.DTO.Toponyms
{
    public class StreetcodeToponymCreateUpdateDto
    {
        public int StreetcodeId { get; set; }
        public int ToponymId { get; set; }
        public ModelState ModelState { get; set; } = ModelState.Updated;
    }
}
