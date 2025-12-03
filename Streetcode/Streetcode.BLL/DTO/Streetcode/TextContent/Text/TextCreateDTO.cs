using System.ComponentModel.DataAnnotations;

namespace Streetcode.BLL.DTO.Streetcode.TextContent.Text
{
  public class TextCreateDTO
  {
    public string Title { get; set; }
    public string TextContent { get; set; }
    public string? AdditionalText { get; set; }
    [RegularExpression(
        @"^(https?://)?(www\.)?(youtube\.com/(watch\?v=|embed/|v/)|youtu\.be/)[\w\-]+",
        ErrorMessage = "Video must be from YouTube")]
    public string? VideoUrl { get; set; }
    public string? Authorship { get; set; }
    public int StreetcodeId { get; set; }
  }
}
