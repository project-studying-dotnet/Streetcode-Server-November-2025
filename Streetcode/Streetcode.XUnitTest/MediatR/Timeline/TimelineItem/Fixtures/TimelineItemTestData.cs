namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures
{
 using global::Streetcode.BLL.DTO.Timeline;
 using global::Streetcode.DAL.Entities.Timeline;
 using global::Streetcode.DAL.Enums;

    /// <summary>
    /// Provides factory methods for creating test instances of <see cref="TimelineItem"/>,
    /// <see cref="TimelineItemDto"/>, <see cref="CreateTimelineItemDto"/>, and <see cref="UpdateTimelineItemDto"/>
    /// objects for use in unit tests.
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
                StreetcodeId = streetcodeId,
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
        /// Creates a single <see cref="TimelineItemDto"/> instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the timeline item DTO.</param>
        /// <returns>A fully initialized <see cref="TimelineItemDto"/> object for testing.</returns>
        public static TimelineItemDto CreateTimelineItemDTO(int id = 1)
        {
            return new TimelineItemDto
            {
                Id = id,
                Date = new DateTime(1920, 1, 15),
                DateViewPattern = DateViewPattern.DateMonthYear,
                Title = "Founding of the Organization",
                Description = "The organization was officially founded and began its operations.",
                HistoricalContexts = new List<HistoricalContextDto>(),
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="TimelineItemDto"/> objects with sequential IDs
        /// and automatically increasing years.
        /// </summary>
        /// <param name="count">The number of DTO items to create.</param>
        /// <returns>A list of <see cref="TimelineItemDto"/> instances.</returns>
        public static List<TimelineItemDto> CreateTimelineItemDTOs(int count = 5)
        {
            var items = new List<TimelineItemDto>(count);

            for (int i = 0; i < count; ++i)
            {
                items.Add(new TimelineItemDto()
                {
                    Id = i + 1,
                    Date = new DateTime(1920 + (i * 10), 1, 15),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = $"Event {i + 1}",
                    Description = $"Description for event {i + 1}.",
                    HistoricalContexts = new List<HistoricalContextDto>(),
                });
            }

            return items;
        }

        /// <summary>
        /// Creates a <see cref="CreateTimelineItemDto"/> with valid test data.
        /// </summary>
        /// <param name="streetcodeId">The streetcode ID for the timeline item.</param>
        /// <param name="historicalContextIds">Optional list of historical context IDs.</param>
        /// <returns>A valid <see cref="CreateTimelineItemDto"/> for testing create operations.</returns>
        public static CreateTimelineItemDto CreateTimelineItemCreateDto(
            int streetcodeId = 101,
            List<int>? historicalContextIds = null)
        {
            return new CreateTimelineItemDto
            {
                Title = "Test Event",
                Description = "Test event description for timeline.",
                Date = new DateTime(1950, 6, 15),
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = historicalContextIds ?? new List<int>()
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateTimelineItemDto"/> with maximum allowed character limits.
        /// </summary>
        /// <returns>A <see cref="CreateTimelineItemDto"/> at character boundaries.</returns>
        public static CreateTimelineItemDto CreateTimelineItemCreateDtoAtMaxLength()
        {
            return new CreateTimelineItemDto
            {
                Title = new string('A', 28),
                Description = new string('B', 400),
                Date = new DateTime(1950, 6, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = 101,
                HistoricalContextIds = new List<int>()
            };
        }

        /// <summary>
        /// Creates a <see cref="UpdateTimelineItemDto"/> with valid test data.
        /// </summary>
        /// <param name="id">The ID of the timeline item to update.</param>
        /// <param name="streetcodeId">The streetcode ID.</param>
        /// <param name="historicalContextIds">Optional list of historical context IDs.</param>
        /// <returns>A valid <see cref="UpdateTimelineItemDto"/> for testing update operations.</returns>
        public static UpdateTimelineItemDto CreateTimelineItemUpdateDto(
            int id = 1,
            int streetcodeId = 101,
            List<int>? historicalContextIds = null)
        {
            return new UpdateTimelineItemDto
            {
                Id = id,
                Title = "Updated Event",
                Description = "Updated description for timeline event.",
                Date = new DateTime(1955, 8, 20),
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = historicalContextIds ?? new List<int>()
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateTimelineItemDto"/> with invalid data (exceeds character limits).
        /// </summary>
        /// <returns>An invalid <see cref="CreateTimelineItemDto"/> for testing validation.</returns>
        public static CreateTimelineItemDto CreateInvalidTimelineItemDto()
        {
            return new CreateTimelineItemDto
            {
                Title = new string('A', 29),
                Description = new string('B', 401),
                Date = DateTime.MinValue,
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = 0,
                HistoricalContextIds = new List<int> { -1 }
            };
        }

        /// <summary>
        /// Creates a TimelineItem with HistoricalContext relationships.
        /// </summary>
        /// <param name="id">The timeline item ID.</param>
        /// <param name="contextIds">List of historical context IDs to associate.</param>
        /// <returns>A <see cref="TimelineItem"/> with HistoricalContextTimeline relationships.</returns>
        public static TimelineItem CreateTimelineItemWithContexts(int id = 1, params int[] contextIds)
        {
            var timelineItem = CreateTimelineItem(id);
            timelineItem.HistoricalContextTimelines = contextIds.Select(contextId =>
                new HistoricalContextTimeline
                {
                    TimelineId = id,
                    HistoricalContextId = contextId,
                    Timeline = timelineItem,
                    HistoricalContext = new HistoricalContext { Id = contextId, Title = $"Context {contextId}" }
                }).ToList();
            return timelineItem;
        }

        /// <summary>
        /// Creates test data for all DateViewPattern enum values.
        /// </summary>
        /// <returns>A list of <see cref="CreateTimelineItemDto"/> with different DateViewPattern values.</returns>
        public static List<CreateTimelineItemDto> CreateTimelineItemsWithAllDatePatterns()
        {
            var patterns = Enum.GetValues(typeof(DateViewPattern)).Cast<DateViewPattern>();
            return patterns.Select((pattern, index) => new CreateTimelineItemDto
            {
                Title = $"Event with {pattern}",
                Description = $"Testing {pattern} pattern",
                Date = new DateTime(2000 + index, 1, 1),
                DateViewPattern = pattern,
                StreetcodeId = 101,
                HistoricalContextIds = new List<int>()
            }).ToList();
        }
    }
}
