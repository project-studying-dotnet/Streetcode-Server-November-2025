using Ardalis.Specification;
using Streetcode.DAL.Entities.Partners;

namespace Streetcode.DAL.Specifications.Partners
{
    public class PartnersByStreetcodeIdSpecification : Specification<Partner>
    {
        public PartnersByStreetcodeIdSpecification(int streetcodeId)
        {
            Query
                .Where(p => p.Streetcodes.Any(sc => sc.Id == streetcodeId) || p.IsVisibleEverywhere)
                .Include(p => p.PartnerSourceLinks);
        }
    }
}
