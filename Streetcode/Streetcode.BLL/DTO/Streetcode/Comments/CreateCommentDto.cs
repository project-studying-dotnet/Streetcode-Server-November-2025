namespace Streetcode.BLL.DTO.Streetcode.Comments;

public class CreateCommentDto
{
    public string Content { get; set; }
    public string AuthorName { get; set; }
    public int StreetcodeId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int? ParentCommentId { get; set; }
}