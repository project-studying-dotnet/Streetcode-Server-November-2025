namespace Streetcode.XUnitTest.MediatR.Toponyms.Fixtures
{
    using System.Collections.Generic;
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Entities.Toponyms;

    /// <summary>
    /// Provides factory methods for creating test instances of <see cref="Toponym"/>
    /// and related DTO objects for use in unit tests.
    /// </summary>
    public static class ToponymTestData
    {
        /// <summary>
        /// Creates a single <see cref="Toponym"/> entity instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the toponym.</param>
        /// <param name="oblast">The oblast name. If null, defaults to "Kyiv Oblast".</param>
        /// <param name="streetName">The street name. If null, defaults to "Test Street {id}".</param>
        /// <param name="streetType">The street type. If null, defaults to "Street".</param>
        /// <returns>A fully initialized <see cref="Toponym"/> object for testing.</returns>
        public static Toponym CreateToponym(
            int id = 1,
            string? oblast = null,
            string? streetName = null,
            string? streetType = null)
        {
            return new Toponym
            {
                Id = id,
                Oblast = oblast ?? "Kyiv Oblast",
                StreetName = streetName ?? $"Test Street {id}",
                StreetType = streetType ?? "Street",
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="Toponym"/> entities with predefined test data.
        /// </summary>
        /// <returns>A list of <see cref="Toponym"/> objects with standard test data.</returns>
        public static List<Toponym> CreateToponyms()
        {
            return new List<Toponym>
            {
                new Toponym
                {
                    Id = 1,
                    Oblast = "Kyiv Oblast",
                    StreetName = "Main Street",
                    StreetType = "Street",
                },
                new Toponym
                {
                    Id = 2,
                    Oblast = "Lviv Oblast",
                    StreetName = "Second Avenue",
                    StreetType = "Avenue",
                },
                new Toponym
                {
                    Id = 3,
                    Oblast = "Kharkiv Oblast",
                    StreetName = "Main Boulevard",
                    StreetType = "Boulevard",
                },
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="Toponym"/> entities associated with a specific streetcode.
        /// </summary>
        /// <param name="streetcodeId">The streetcode ID to associate with the toponyms.</param>
        /// <returns>A list of <see cref="Toponym"/> objects with streetcode associations.</returns>
        public static List<Toponym> CreateToponymsWithStreetcodes(int streetcodeId)
        {
            return new List<Toponym>
            {
                new Toponym
                {
                    Id = 1,
                    Oblast = "Kyiv Oblast",
                    StreetName = "Main Street",
                    StreetType = "Street",
                    Streetcodes = new List<StreetcodeContent> { new StreetcodeContent { Id = streetcodeId } },
                },
                new Toponym
                {
                    Id = 2,
                    Oblast = "Lviv Oblast",
                    StreetName = "Second Avenue",
                    StreetType = "Avenue",
                    Streetcodes = new List<StreetcodeContent> { new StreetcodeContent { Id = streetcodeId } },
                },
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="Toponym"/> entities with duplicate street names for testing distinct operations.
        /// </summary>
        /// <param name="streetcodeId">The streetcode ID to associate with the toponyms.</param>
        /// <returns>A list of <see cref="Toponym"/> objects with duplicate street names.</returns>
        public static List<Toponym> CreateToponymsWithDuplicateStreetNames(int streetcodeId)
        {
            return new List<Toponym>
            {
                new Toponym
                {
                    Id = 1,
                    Oblast = "Kyiv Oblast",
                    StreetName = "Main Street",
                    StreetType = "Street",
                    Streetcodes = new List<StreetcodeContent> { new StreetcodeContent { Id = streetcodeId } },
                },
                new Toponym
                {
                    Id = 2,
                    Oblast = "Lviv Oblast",
                    StreetName = "Main Street",
                    StreetType = "Street",
                    Streetcodes = new List<StreetcodeContent> { new StreetcodeContent { Id = streetcodeId } },
                },
                new Toponym
                {
                    Id = 3,
                    Oblast = "Kharkiv Oblast",
                    StreetName = "Second Avenue",
                    StreetType = "Avenue",
                    Streetcodes = new List<StreetcodeContent> { new StreetcodeContent { Id = streetcodeId } },
                },
            };
        }

        /// <summary>
        /// Creates a single <see cref="ToponymDto"/> instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the toponym DTO.</param>
        /// <param name="oblast">The oblast name. If null, defaults to "Kyiv Oblast".</param>
        /// <param name="streetName">The street name. If null, defaults to "Test Street {id}".</param>
        /// <param name="streetType">The street type. If null, defaults to "Street".</param>
        /// <returns>A fully initialized <see cref="ToponymDto"/> object for testing.</returns>
        public static ToponymDto CreateToponymDto(
            int id = 1,
            string? oblast = null,
            string? streetName = null,
            string? streetType = null)
        {
            return new ToponymDto
            {
                Id = id,
                Oblast = oblast ?? "Kyiv Oblast",
                StreetName = streetName ?? $"Test Street {id}",
                StreetType = streetType ?? "Street",
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="ToponymDto"/> objects with predefined test data.
        /// </summary>
        /// <returns>A list of <see cref="ToponymDto"/> instances with standard test data.</returns>
        public static List<ToponymDto> CreateToponymDtos()
        {
            return new List<ToponymDto>
            {
                new ToponymDto
                {
                    Id = 1,
                    Oblast = "Kyiv Oblast",
                    StreetName = "Main Street",
                    StreetType = "Street",
                },
                new ToponymDto
                {
                    Id = 2,
                    Oblast = "Lviv Oblast",
                    StreetName = "Second Avenue",
                    StreetType = "Avenue",
                },
                new ToponymDto
                {
                    Id = 3,
                    Oblast = "Kharkiv Oblast",
                    StreetName = "Main Boulevard",
                    StreetType = "Boulevard",
                },
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="ToponymDto"/> objects with duplicate street names for testing distinct operations.
        /// </summary>
        /// <returns>A list of <see cref="ToponymDto"/> instances with duplicate street names.</returns>
        public static List<ToponymDto> CreateToponymDtosWithDuplicates()
        {
            return new List<ToponymDto>
            {
                new ToponymDto
                {
                    Id = 1,
                    Oblast = "Kyiv Oblast",
                    StreetName = "Main Street",
                    StreetType = "Street",
                },
                new ToponymDto
                {
                    Id = 2,
                    Oblast = "Lviv Oblast",
                    StreetName = "Main Street",
                    StreetType = "Street",
                },
                new ToponymDto
                {
                    Id = 3,
                    Oblast = "Kharkiv Oblast",
                    StreetName = "Second Avenue",
                    StreetType = "Avenue",
                },
            };
        }
    }
}

