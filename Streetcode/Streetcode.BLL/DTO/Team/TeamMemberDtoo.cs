using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.DTO.Streetcode;

namespace Streetcode.BLL.DTO.Team
{
    public class TeamMemberDtoo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
        public int ImageId { get; set; }
        public List<TeamMemberLinkDtoo> TeamMemberLinks { get; set; } = new List<TeamMemberLinkDtoo>();
        public List<PositionDtoo> Positions { get; set; } = new List<PositionDtoo>();
    }
}
