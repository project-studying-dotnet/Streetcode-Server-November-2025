namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures
{
    using Streetcode.BLL.DTO.Timeline;
    using Streetcode.DAL.Entities.Timeline;
    using Streetcode.DAL.Enums;

    /// <summary>
    /// Provides factory methods for creating test instances of <see cref="TimelineItem"/>
    /// and <see cref="TimelineItemDTO"/> objects for use in unit tests.
    /// </summary>
    public static class TimelineItemTestData
    {
        /// <summary>
        /// Creates a single <see cref="TimelineItem"/> entity instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the timeline item.</param>
        /// <param name="streetcodeId">The streetcode ID associated with the item.</param>
        /// <returns>A fully initialized <see cref="TimelineItem"/> object for testing.</returns>
        public static TimelineItem CreateTimelineItem(int id = 1, int streetcodeId = 101)
        {
            return new TimelineItem
            {
                Id = id,
                Date = new DateTime(1920, 1, 15),
                DateViewPattern = DateViewPattern.DateMonthYear,
                Title = "Founding of the Organization",
                Description = "The organization was officially founded and began its operations.",
                StreetcodeId = 101,
                Streetcode = null,
                HistoricalContextTimelines = new List<HistoricalContextTimeline>(),
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="TimelineItem"/> entities with sequential IDs
        /// and automatically increasing years.
        /// </summary>
        /// <param name="count">The number of timeline items to generate.</param>
        /// <param name="streetcodeId">The streetcode ID for all items.</param>
        /// <returns>A list of <see cref="TimelineItem"/> objects.</returns>
        public static List<TimelineItem> CreateTimelineItems(int count = 5, int streetcodeId = 101)
        {
            var items = new List<TimelineItem>(count);

            for (int i = 0; i < count; ++i)
            {
                items.Add(new TimelineItem()
                {
                    Id = i + 1,
                    Date = new DateTime(1920 + (i * 10), 1, 15),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = $"Event {i + 1}",
                    Description = $"Description for event {i + 1}.",
                    StreetcodeId = streetcodeId,
                    Streetcode = null,
                    HistoricalContextTimelines = new List<HistoricalContextTimeline>(),
                });
            }

            return items;
        }

        /// <summary>
        /// Creates a single <see cref="TimelineItemDTO"/> instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the timeline item DTO.</param>
        /// <returns>A fully initialized <see cref="TimelineItemDTO"/> object for testing.</returns>
        public static TimelineItemDTO CreateTimelineItemDTO(int id = 1)
        {
            return new TimelineItemDTO
            {
                Id = id,
                Date = new DateTime(1920, 1, 15),
                DateViewPattern = DateViewPattern.DateMonthYear,
                Title = "Founding of the Organization",
                Description = "The organization was officially founded and began its operations.",
                HistoricalContexts = new List<HistoricalContextDTO>(),
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="TimelineItemDTO"/> objects with sequential IDs
        /// and automatically increasing years.
        /// </summary>
        /// <param name="count">The number of DTO items to create.</param>
        /// <returns>A list of <see cref="TimelineItemDTO"/> instances.</returns>
        public static List<TimelineItemDTO> CreateTimelineItemDTOs(int count = 5)
        {
            var items = new List<TimelineItemDTO>(count);

            for (int i = 0; i < count; ++i)
            {
                items.Add(new TimelineItemDTO()
                {
                    Id = i + 1,
                    Date = new DateTime(1920 + (i * 10), 1, 15),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = $"Event {i + 1}",
                    Description = $"Description for event {i + 1}.",
                    HistoricalContexts = new List<HistoricalContextDTO>(),
                });
            }

            return items;
        }
    }
}