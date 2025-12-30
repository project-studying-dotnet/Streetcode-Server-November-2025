using Ardalis.Specification;
using Streetcode.DAL.Entities.Streetcode;

namespace Streetcode.DAL.Specifications.Comments
{
    public class CommentWithRepliesSpecification : Specification<Comment>
    {
        public CommentWithRepliesSpecification(int commentId)
        {
            Query
                .Where(c => c.Id == commentId && !c.IsDeleted)
                .Include(c => c.Replies.Where(r => !r.IsDeleted));
        }
    }
}