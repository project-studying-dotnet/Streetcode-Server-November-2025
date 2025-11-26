using System.Collections.Generic;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.DTO.Streetcode;
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

        public static List<Partner> CreatePartnerEntities(int count)
        {
            var partners = new List<Partner>();
            for (int i = 1; i <= count; i++)
            {
                partners.Add(CreatePartnerEntity(i));
            }

            return partners;
        }

        public static List<PartnerDTO> CreatePartnerDTOs(int count)
        {
            var dtos = new List<PartnerDTO>();
            for (int i = 1; i <= count; i++)
            {
                dtos.Add(CreatePartnerDTO(i));
            }

            return dtos;
        }

        public static List<PartnerShortDTO> CreatePartnerShortDTOs(int count)
        {
            var dtos = new List<PartnerShortDTO>();
            for (int i = 1; i <= count; i++)
            {
                dtos.Add(CreatePartnerShortDTO(i));
            }

            return dtos;
        }

        public static StreetcodeShortDTO CreateStreetcodeShortDTO(int id = 1)
            => new ()
            {
                Id = id,
            };

        public static PartnerSourceLink CreatePartnerSourceLink(int id, int partnerId)
            => new ()
            {
                Id = id,
                PartnerId = partnerId,
            };

        public static StreetcodePartner CreateStreetcodePartner(int partnerId, int streetcodeId)
            => new ()
            {
                PartnerId = partnerId,
                StreetcodeId = streetcodeId,
            };
    }
}
