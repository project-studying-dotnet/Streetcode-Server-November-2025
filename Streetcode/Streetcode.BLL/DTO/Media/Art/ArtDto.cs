using Streetcode.BLL.DTO.Media.Images;

namespace Streetcode.BLL.DTO.Media.Art;

public class ArtDto
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Title { get; set; }
    public int ImageId { get; set; }
    public ImageDtoo? Image { get; set; }
}
