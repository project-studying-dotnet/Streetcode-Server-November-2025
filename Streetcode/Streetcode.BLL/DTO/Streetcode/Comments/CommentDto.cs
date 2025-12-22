namespace Streetcode.BLL.DTO.Streetcode.Comments;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string AuthorName { get; set; } = "Гість";
    public int StreetcodeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? EditedAt { get; set; }
}