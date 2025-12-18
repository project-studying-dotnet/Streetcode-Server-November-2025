namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Fixtures
{
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.DAL.Entities.Toponyms;

    /// <summary>
    /// Provides factory methods for creating test instances of <see cref="StreetcodeToponym"/>
    /// and related DTO objects for use in unit tests.
    /// </summary>
    public static class StreetcodeToponymTestData
    {
        /// <summary>
        /// Creates a single <see cref="StreetcodeToponym"/> entity instance with predefined values.
        /// </summary>
        /// <param name="streetcodeId">The streetcode ID.</param>
        /// <param name="toponymId">The toponym ID.</param>
        /// <returns>A fully initialized <see cref="StreetcodeToponym"/> object for testing.</returns>
        public static StreetcodeToponym CreateStreetcodeToponym(
            int streetcodeId = 1,
            int toponymId = 1)
        {
            return new StreetcodeToponym
            {
                StreetcodeId = streetcodeId,
                ToponymId = toponymId,
                Streetcode = null,
                Toponym = null,
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="StreetcodeToponym"/> entities with sequential IDs.
        /// </summary>
        /// <param name="count">The number of items to generate.</param>
        /// <param name="streetcodeId">The streetcode ID for all items.</param>
        /// <returns>A list of <see cref="StreetcodeToponym"/> objects.</returns>
        public static List<StreetcodeToponym> CreateStreetcodeToponyms(
            int count = 3,
            int streetcodeId = 1)
        {
            var items = new List<StreetcodeToponym>(count);

            for (int i = 1; i <= count; ++i)
            {
                items.Add(new StreetcodeToponym
                {
                    StreetcodeId = streetcodeId,
                    ToponymId = i,
                    Streetcode = null,
                    Toponym = null,
                });
            }

            return items;
        }

        /// <summary>
        /// Creates a single <see cref="StreetcodeToponymDto"/> instance with predefined values.
        /// </summary>
        /// <param name="streetcodeId">The streetcode ID.</param>
        /// <param name="toponymId">The toponym ID.</param>
        /// <returns>A fully initialized <see cref="StreetcodeToponymDto"/> object for testing.</returns>
        public static StreetcodeToponymDto CreateStreetcodeToponymDto(
            int streetcodeId = 1,
            int toponymId = 1)
        {
            return new StreetcodeToponymDto
            {
                StreetcodeId = streetcodeId,
                ToponymId = toponymId,
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="StreetcodeToponymDto"/> objects with sequential IDs.
        /// </summary>
        /// <param name="count">The number of items to generate.</param>
        /// <param name="streetcodeId">The streetcode ID for all items.</param>
        /// <returns>A list of <see cref="StreetcodeToponymDto"/> objects.</returns>
        public static List<StreetcodeToponymDto> CreateStreetcodeToponymDtos(
            int count = 3,
            int streetcodeId = 1)
        {
            var items = new List<StreetcodeToponymDto>(count);

            for (int i = 1; i <= count; ++i)
            {
                items.Add(new StreetcodeToponymDto
                {
                    StreetcodeId = streetcodeId,
                    ToponymId = i,
                });
            }

            return items;
        }

        /// <summary>
        /// Creates a <see cref="MergeToponymsDto"/> instance for testing merge operations.
        /// </summary>
        /// <param name="targetToponymId">The target toponym ID.</param>
        /// <param name="sourceToponymIds">The source toponym IDs to merge.</param>
        /// <returns>A fully initialized <see cref="MergeToponymsDto"/> object for testing.</returns>
        public static MergeToponymsDto CreateMergeToponymsDto(
            int targetToponymId = 1,
            List<int>? sourceToponymIds = null)
        {
            return new MergeToponymsDto
            {
                TargetToponymId = targetToponymId,
                SourceToponymIds = sourceToponymIds ?? new List<int> { 2, 3 },
            };
        }
    }
}