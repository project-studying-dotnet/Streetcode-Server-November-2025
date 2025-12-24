using Ardalis.Specification;
using Streetcode.DAL.Entities.Partners;

namespace Streetcode.DAL.Specifications.Partners
{
    public class PartnersWithDetailsSpecification : Specification<Partner>
    {
        public PartnersWithDetailsSpecification()
        {
            Query
                .Include(p => p.PartnerSourceLinks)
                .Include(p => p.Streetcodes);
        }
    }
}
