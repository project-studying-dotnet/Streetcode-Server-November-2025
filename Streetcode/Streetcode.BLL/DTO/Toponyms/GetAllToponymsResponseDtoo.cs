namespace Streetcode.BLL.DTO.Toponyms;

public class GetAllToponymsResponseDtoo
{
    public int Pages { get; set; }
    public IEnumerable<ToponymDtoo> Toponyms { get; set; }
}