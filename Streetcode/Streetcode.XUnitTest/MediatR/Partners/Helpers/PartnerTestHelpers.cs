using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Streetcode;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    /// <summary>
    /// Helper class for creating test data for Partner-related tests.
    /// </summary>
    public static class PartnerTestHelpers
    {
        /// <summary>
        /// Creates a Partner entity with test data.
        /// </summary>
        /// <param name="id">The partner ID.</param>
        /// <returns>A Partner entity.</returns>
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

        /// <summary>
        /// Creates a PartnerDTO with test data.
        /// </summary>
        /// <param name="id">The partner ID.</param>
        /// <returns>A PartnerDTO.</returns>
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

        /// <summary>
        /// Creates a PartnerShortDTO with test data.
        /// </summary>
        /// <param name="id">The partner ID.</param>
        /// <returns>A PartnerShortDTO.</returns>
        public static PartnerShortDTO CreatePartnerShortDTO(int id = 1)
            => new ()
            {
                Id = id,
                Title = $"Test Partner {id}",
            };

        /// <summary>
        /// Creates a list of Partner entities with test data.
        /// </summary>
        /// <param name="count">The number of partners to create.</param>
        /// <returns>A list of Partner entities.</returns>
        public static List<Partner> CreatePartnerEntities(int count)
        {
            var partners = new List<Partner>();
            for (int i = 1; i <= count; i++)
            {
                partners.Add(CreatePartnerEntity(i));
            }

            return partners;
        }

        /// <summary>
        /// Creates a list of PartnerDTOs with test data.
        /// </summary>
        /// <param name="count">The number of DTOs to create.</param>
        /// <returns>A list of PartnerDTOs.</returns>
        public static List<PartnerDTO> CreatePartnerDTOs(int count)
        {
            var dtos = new List<PartnerDTO>();
            for (int i = 1; i <= count; i++)
            {
                dtos.Add(CreatePartnerDTO(i));
            }

            return dtos;
        }

        /// <summary>
        /// Creates a list of PartnerShortDTOs with test data.
        /// </summary>
        /// <param name="count">The number of DTOs to create.</param>
        /// <returns>A list of PartnerShortDTOs.</returns>
        public static List<PartnerShortDTO> CreatePartnerShortDTOs(int count)
        {
            var dtos = new List<PartnerShortDTO>();
            for (int i = 1; i <= count; i++)
            {
                dtos.Add(CreatePartnerShortDTO(i));
            }

            return dtos;
        }

        /// <summary>
        /// Creates a StreetcodeShortDTO with test data.
        /// </summary>
        /// <param name="id">The streetcode ID.</param>
        /// <returns>A StreetcodeShortDTO.</returns>
        public static StreetcodeShortDTO CreateStreetcodeShortDTO(int id = 1)
            => new ()
            {
                Id = id,
            };

        /// <summary>
        /// Creates a PartnerSourceLink with test data.
        /// </summary>
        /// <param name="id">The link ID.</param>
        /// <param name="partnerId">The partner ID.</param>
        /// <returns>A PartnerSourceLink.</returns>
        public static PartnerSourceLink CreatePartnerSourceLink(int id, int partnerId)
            => new ()
            {
                Id = id,
                PartnerId = partnerId,
            };

        /// <summary>
        /// Creates a StreetcodePartner relationship with test data.
        /// </summary>
        /// <param name="partnerId">The partner ID.</param>
        /// <param name="streetcodeId">The streetcode ID.</param>
        /// <returns>A StreetcodePartner.</returns>
        public static StreetcodePartner CreateStreetcodePartner(int partnerId, int streetcodeId)
            => new ()
            {
                PartnerId = partnerId,
                StreetcodeId = streetcodeId,
            };
    }
}
