namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures
{
    using Streetcode.BLL.DTO.Timeline;
    using Streetcode.DAL.Entities.Timeline;
    using Streetcode.DAL.Enums;

    public static class TimelineItemTestData
    {
        public static TimelineItem CreateTimelineItem(int id = 1)
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

        public static List<TimelineItem> CreateTimelineItems(int count = 5)
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
                    StreetcodeId = 100 + i,
                    Streetcode = null,
                    HistoricalContextTimelines = new List<HistoricalContextTimeline>(),
                });
            }

            return items;
        }

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