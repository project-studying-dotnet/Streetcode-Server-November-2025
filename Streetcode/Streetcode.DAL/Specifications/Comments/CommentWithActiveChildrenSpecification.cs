using Ardalis.Specification;
using Streetcode.DAL.Entities.Streetcode;

namespace Streetcode.DAL.Specifications.Comments
{
    public class CommentWithActiveChildrenSpecification : Specification<Comment>
    {
        public CommentWithActiveChildrenSpecification(int parentCommentId)
        {
            Query
                .Where(c => c.ParentCommentId == parentCommentId && !c.IsDeleted);
        }
    }
}
