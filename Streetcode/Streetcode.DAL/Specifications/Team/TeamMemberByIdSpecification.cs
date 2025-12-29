using Ardalis.Specification;
using Streetcode.DAL.Entities.Team;

namespace Streetcode.DAL.Specifications.Team
{
    public class TeamMemberByIdSpecification : Specification<TeamMember>
    {
        public TeamMemberByIdSpecification(int memberId)
        {
            Query
                .Where(tm => tm.Id == memberId)
                .Include(tm => tm.TeamMemberLinks)
                .Include(tm => tm.Positions);
        }
    }
}