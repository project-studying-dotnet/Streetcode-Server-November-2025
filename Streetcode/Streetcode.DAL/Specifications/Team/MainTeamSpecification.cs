using Ardalis.Specification;
using Streetcode.DAL.Entities.Team;

namespace Streetcode.DAL.Specifications.Team
{
    public class MainTeamSpecification : Specification<TeamMember>
    {
        public MainTeamSpecification()
        {
            Query
                .Where(tm => tm.IsMain)
                .Include(tm => tm.Positions)
                .Include(tm => tm.TeamMemberLinks);
        }
    }
}
