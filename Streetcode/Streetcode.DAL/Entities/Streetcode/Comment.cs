using System.ComponentModel.DataAnnotations.Schema;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.DAL.Entities.Streetcode
{
    [Table("comments", Schema = "streetcode")]

    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public int StreetcodeId { get; set; }
        public StreetcodeContent Streetcode { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
