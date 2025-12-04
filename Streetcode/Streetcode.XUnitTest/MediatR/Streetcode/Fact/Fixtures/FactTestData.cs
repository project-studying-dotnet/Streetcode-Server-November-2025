namespace Streetcode.XUnitTest.MediatR.Fact.Fixtures
{
    using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
    using Streetcode.DAL.Entities.Streetcode.TextContent;

    /// <summary>
    /// Provides factory methods for creating test instances of <see cref="DAL.Entities.Streetcode.TextContent.Fact"/>
    /// and related DTO objects for use in unit tests.
    /// </summary>
    public static class FactTestData
    {
        /// <summary>
        /// Creates a single <see cref="Fact"/> entity instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the fact.</param>
        /// <param name="streetcodeId">The streetcode ID associated with the fact.</param>
        /// <param name="imageId">The image ID associated with the fact.</param>
        /// <returns>A fully initialized <see cref="Fact"/> object for testing.</returns>
        public static Fact CreateFact(int id = 1, int streetcodeId = 101, int imageId = 1)
        {
            return new Fact
            {
                Id = id,
                Title = "Historical Fact",
                ImageId = imageId,
                StreetcodeId = streetcodeId,
                FactContent = "This is a detailed description of an important historical fact.",
                Image = null,
                Streetcode = null,
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="DAL.Entities.Streetcode.TextContent.Fact"/> entities with sequential IDs.
        /// </summary>
        /// <param name="count">The number of fact items to generate.</param>
        /// <param name="streetcodeId">The streetcode ID for all items.</param>
        /// <param name="imageId">The starting image ID for items.</param>
        /// <returns>A list of <see cref="DAL.Entities.Streetcode.TextContent.Fact"/> objects.</returns>
        public static List<Fact> CreateFacts(int count = 5, int streetcodeId = 101, int imageId = 1)
        {
            var items = new List<Fact>(count);

            for (int i = 0; i < count; ++i)
            {
                items.Add(new Fact
                {
                    Id = i + 1,
                    Title = $"Historical Fact {i + 1}",
                    ImageId = imageId + i,
                    StreetcodeId = streetcodeId,
                    FactContent = $"This is a detailed description of historical fact number {i + 1}.",
                    Image = null,
                    Streetcode = null,
                });
            }

            return items;
        }

        /// <summary>
        /// Creates a single <see cref="FactDto"/> instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the fact DTO.</param>
        /// <param name="imageId">The image ID associated with the fact.</param>
        /// <returns>A fully initialized <see cref="FactDto"/> object for testing.</returns>
        public static FactDto CreateFactDto(int id = 1, int imageId = 1)
        {
            return new FactDto
            {
                Id = id,
                Title = "Historical Fact",
                ImageId = imageId,
                FactContent = "This is a detailed description of an important historical fact.",
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="FactDto"/> objects with sequential IDs.
        /// </summary>
        /// <param name="count">The number of DTO items to create.</param>
        /// <param name="imageId">The starting image ID for items.</param>
        /// <returns>A list of <see cref="FactDto"/> instances.</returns>
        public static List<FactDto> CreateFactDtos(int count = 5, int imageId = 1)
        {
            var items = new List<FactDto>(count);

            for (int i = 0; i < count; ++i)
            {
                items.Add(new FactDto
                {
                    Id = i + 1,
                    Title = $"Historical Fact {i + 1}",
                    ImageId = imageId + i,
                    FactContent = $"This is a detailed description of historical fact number {i + 1}.",
                });
            }

            return items;
        }

        /// <summary>
        /// Creates a single <see cref="CreateFactDTO"/> instance with predefined values.
        /// </summary>
        /// <param name="streetcodeId">The streetcode ID for the new fact.</param>
        /// <param name="imageId">The image ID for the new fact.</param>
        /// <returns>A fully initialized <see cref="CreateFactDTO"/> object for testing.</returns>
        public static CreateFactDTO CreateCreateFactDto(int streetcodeId = 101, int imageId = 1)
        {
            return new CreateFactDTO
            {
                Title = "Historical Fact",
                ImageId = imageId,
                StreetcodeId = streetcodeId,
                FactContent = "This is a detailed description of an important historical fact.",
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="CreateFactDTO"/> objects with sequential values.
        /// </summary>
        /// <param name="count">The number of DTO items to create.</param>
        /// <param name="streetcodeId">The streetcode ID for all items.</param>
        /// <param name="imageId">The starting image ID for items.</param>
        /// <returns>A list of <see cref="CreateFactDTO"/> instances.</returns>
        public static List<CreateFactDTO> CreateCreateFactDtos(int count = 5, int streetcodeId = 101, int imageId = 1)
        {
            var items = new List<CreateFactDTO>(count);

            for (int i = 0; i < count; ++i)
            {
                items.Add(new CreateFactDTO
                {
                    Title = $"Historical Fact {i + 1}",
                    ImageId = imageId + i,
                    StreetcodeId = streetcodeId,
                    FactContent = $"This is a detailed description of historical fact number {i + 1}.",
                });
            }

            return items;
        }

        /// <summary>
        /// Creates a single <see cref="UpdateFactDto"/> instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the fact to update.</param>
        /// <param name="imageId">The image ID for the fact.</param>
        /// <returns>A fully initialized <see cref="UpdateFactDto"/> object for testing.</returns>
        public static UpdateFactDto CreateUpdateFactDto(int id = 1, int imageId = 1)
        {
            return new UpdateFactDto
            {
                Id = id,
                Title = "Historical Fact",
                ImageId = imageId,
                FactContent = "This is a detailed description of an important historical fact.",
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="UpdateFactDto"/> objects with sequential IDs.
        /// </summary>
        /// <param name="count">The number of DTO items to create.</param>
        /// <param name="imageId">The starting image ID for items.</param>
        /// <returns>A list of <see cref="UpdateFactDto"/> instances.</returns>
        public static List<UpdateFactDto> CreateUpdateFactDtos(int count = 5, int imageId = 1)
        {
            var items = new List<UpdateFactDto>(count);

            for (int i = 0; i < count; ++i)
            {
                items.Add(new UpdateFactDto
                {
                    Id = i + 1,
                    Title = $"Historical Fact {i + 1}",
                    ImageId = imageId + i,
                    FactContent = $"This is a detailed description of historical fact number {i + 1}.",
                });
            }

            return items;
        }
    }
}