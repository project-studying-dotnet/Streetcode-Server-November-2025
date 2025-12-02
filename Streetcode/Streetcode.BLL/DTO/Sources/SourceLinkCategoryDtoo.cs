using Streetcode.BLL.DTO.Media.Images;
namespace Streetcode.BLL.DTO.Sources;

public class SourceLinkCategoryDtoo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ImageId { get; set; }
    public ImageDtoo? Image { get; set; }
}