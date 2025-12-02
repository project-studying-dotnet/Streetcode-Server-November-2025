namespace Streetcode.BLL.DTO.Streetcode;

public class GetAllStreetcodesResponseDtoo
{
    public int Pages { get; set; }
    public IEnumerable<StreetcodeDto> Streetcodes { get; set; }
}
