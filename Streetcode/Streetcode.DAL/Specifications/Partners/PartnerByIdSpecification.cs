using Ardalis.Specification;
using Streetcode.DAL.Entities.Partners;

namespace Streetcode.DAL.Specifications.Partners
{
    public class PartnerByIdSpecification : Specification<Partner>
    {
        public PartnerByIdSpecification(int partnerId)
        {
            Query
                .Where(p => p.Id == partnerId)
                .Include(p => p.PartnerSourceLinks);
        }
    }
}
