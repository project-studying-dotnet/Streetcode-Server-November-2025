namespace Streetcode.BLL.DTO.Streetcode.Comments;

public class CreateCommentDto
{
    public string Content { get; set; }
    public string AuthorName { get; set; } = "Гість";
    public int StreetcodeId { get; set; }
}