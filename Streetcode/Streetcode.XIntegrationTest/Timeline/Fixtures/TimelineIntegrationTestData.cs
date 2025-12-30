namespace Streetcode.XIntegrationTest.Timeline.Fixtures
{
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Entities.Timeline;
    using Streetcode.DAL.Enums;

    /// <summary>
    /// Provides factory methods for creating test data for Timeline integration tests.
    /// </summary>
    public static class TimelineIntegrationTestData
    {
        /// <summary>
        /// Creates a collection of TimelineItems with associated HistoricalContexts for integration testing.
        /// </summary>
        /// <param name="streetcodeId">The streetcode ID to associate items with.</param>
        /// <returns>A tuple containing timeline items and historical contexts.</returns>
        public static (List<TimelineItem> TimelineItems, List<HistoricalContext> Contexts) CreateTimelineTestData(int streetcodeId = 1)
        {
            var contexts = new List<HistoricalContext>
            {
                new HistoricalContext
                {
                    Id = 1,
                    Title = "Середньовіччя",
                },
                new HistoricalContext
                {
                    Id = 2,
                    Title = "Козацька доба",
                },
                new HistoricalContext
                {
                    Id = 3,
                    Title = "Новий час",
                },
            };

            var timelineItems = new List<TimelineItem>
            {
                new TimelineItem
                {
                    Id = 1,
                    Title = "Заснування міста",
                    Description = "Перша письмова згадка про місто",
                    Date = new DateTime(1256, 6, 15),
                    DateViewPattern = DateViewPattern.Year,
                    StreetcodeId = streetcodeId,
                    HistoricalContextTimelines = new List<HistoricalContextTimeline>
                    {
                        new HistoricalContextTimeline { HistoricalContextId = 1 },
                    },
                },
                new TimelineItem
                {
                    Id = 2,
                    Title = "Магдебурзьке право",
                    Description = "Місто отримало Магдебурзьке право",
                    Date = new DateTime(1356, 3, 1),
                    DateViewPattern = DateViewPattern.MonthYear,
                    StreetcodeId = streetcodeId,
                    HistoricalContextTimelines = new List<HistoricalContextTimeline>
                    {
                        new HistoricalContextTimeline { HistoricalContextId = 1 },
                    },
                },
                new TimelineItem
                {
                    Id = 3,
                    Title = "Визвольна війна",
                    Description = "Участь у визвольній війні",
                    Date = new DateTime(1648, 1, 1),
                    DateViewPattern = DateViewPattern.Year,
                    StreetcodeId = streetcodeId,
                    HistoricalContextTimelines = new List<HistoricalContextTimeline>
                    {
                        new HistoricalContextTimeline { HistoricalContextId = 2 },
                    },
                },
            };

            return (timelineItems, contexts);
        }

        /// <summary>
        /// Creates a test streetcode entity for integration tests.
        /// </summary>
        /// <param name="id">The streetcode ID.</param>
        /// <returns>A StreetcodeContent entity.</returns>
        public static StreetcodeContent CreateTestStreetcode(int id = 1)
        {
            return new StreetcodeContent
            {
                Id = id,
                Index = id,
                Title = $"Test Streetcode {id}",
                DateString = "2024",
                Alias = $"test-streetcode-{id}",
                TransliterationUrl = $"test-streetcode-{id}",
                Status = StreetcodeStatus.Published,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }

        /// <summary>
        /// Creates multiple test streetcodes.
        /// </summary>
        /// <param name="count">Number of streetcodes to create.</param>
        /// <returns>A list of StreetcodeContent entities.</returns>
        public static List<StreetcodeContent> CreateTestStreetcodes(int count = 3)
        {
            var streetcodes = new List<StreetcodeContent>();
            for (int i = 1; i <= count; i++)
            {
                streetcodes.Add(CreateTestStreetcode(i));
            }

            return streetcodes;
        }

        /// <summary>
        /// Creates a simple timeline item for testing.
        /// </summary>
        /// <param name="id">The timeline item ID.</param>
        /// <param name="streetcodeId">The streetcode ID.</param>
        /// <param name="title">The title.</param>
        /// <returns>A TimelineItem entity.</returns>
        public static TimelineItem CreateSimpleTimelineItem(
            int id,
            int streetcodeId,
            string title = "Test Event")
        {
            return new TimelineItem
            {
                Id = id,
                Title = title,
                Description = $"Description for {title}",
                Date = new DateTime(2000 + id, 1, 1),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
            };
        }

        /// <summary>
        /// Creates a simple historical context for testing.
        /// </summary>
        /// <param name="id">The context ID.</param>
        /// <param name="title">The title.</param>
        /// <returns>A HistoricalContext entity.</returns>
        public static HistoricalContext CreateSimpleHistoricalContext(
            int id,
            string title = "Test Context")
        {
            return new HistoricalContext
            {
                Id = id,
                Title = title,
            };
        }
    }
}
