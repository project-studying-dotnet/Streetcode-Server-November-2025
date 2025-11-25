using System.Collections.Generic;
using Streetcode.BLL.DTO.Partners;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Streetcode;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public static class PartnerTestHelpers
    {
        public static Partner CreatePartnerEntity(int id = 1)
            => new ()
            {
                Id = id,
                Title = $"Test Partner {id}",
                IsKeyPartner = id % 2 != 0,
                IsVisibleEverywhere = id % 2 != 0,
                LogoId = id,
                TargetUrl = $"https://partner{id}.com",
                Description = $"Description {id}",
                PartnerSourceLinks = new List<PartnerSourceLink>(),
                Streetcodes = new List<StreetcodeContent>(),
            };

        public static PartnerDTO CreatePartnerDTO(int id = 1)
            => new ()
            {
                Id = id,
                Title = $"Test Partner {id}",
                IsKeyPartner = id % 2 != 0,
                IsVisibleEverywhere = id % 2 != 0,
                LogoId = id,
                Description = $"Description {id}",
            };

        public static PartnerShortDTO CreatePartnerShortDTO(int id = 1)
            => new ()
            {
                Id = id,
                Title = $"Test Partner {id}",
            };
    }
}
