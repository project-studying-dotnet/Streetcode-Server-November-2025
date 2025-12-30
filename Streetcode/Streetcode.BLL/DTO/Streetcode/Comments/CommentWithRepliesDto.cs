using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.BLL.DTO.Streetcode.Comments
{
	public class CommentWithRepliesDto
	{
		public int Id { get; set; }
		public string Content { get; set; } = string.Empty;
		public string AuthorName { get; set; } = string.Empty;
		public int StreetcodeId { get; set; }
		public DateTime CreatedAt { get; set; }
		public List<CommentWithRepliesDto> Replies { get; set; } = new();
	}
}
