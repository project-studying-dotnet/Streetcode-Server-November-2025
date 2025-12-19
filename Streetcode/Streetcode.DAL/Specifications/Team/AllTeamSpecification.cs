using Ardalis.Specification;
using Streetcode.DAL.Entities.Team;

namespace Streetcode.DAL.Specifications.Team
{
    public class AllTeamSpecification : Specification<TeamMember>
    {
        public AllTeamSpecification()
        {
            Query
                .Include(tm => tm.Positions)
                .Include(tm => tm.TeamMemberLinks);
        }
    }
}
