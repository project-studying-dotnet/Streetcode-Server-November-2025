namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Helpers
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Timeline;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.DAL.Entities.Timeline;
    using Streetcode.DAL.Repositories.Interfaces.Timeline;
    using System.Linq.Expressions;

    public static class TimelineItemVerifications
    {
        // -------------------------- Verify Repository -------------------------------
        public static void VerifyGetAllAsyncCalledOnce(this Mock<ITimelineRepository> timelineRepositoryMock)
        {
            timelineRepositoryMock.Verify(
                tr => tr.GetAllAsync(
                    It.IsAny<Expression<Func<DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<DAL.Entities.Timeline.TimelineItem>, IIncludableQueryable<DAL.Entities.Timeline.TimelineItem, object>>>()),
                Times.Once(),
                "GetAllAsync method should be called exactly once");
        }

        // -------------------------- Verify Logger -------------------------------
        public static void VerifyLogErrorCalledOnce(this Mock<ILoggerService> loggerMock)
        {
            loggerMock.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once(),
                "LogError method should be called exactly once when timelineItems is null");
        }

        public static void VerifyLogErrorCalledNever(this Mock<ILoggerService> loggerMock)
        {
            loggerMock.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Never,
                "LogError method should not be called when timelineItems exists");
        }

        // -------------------------- Verify Mapper -------------------------------
        public static void VerifyMapCalledOnce(this Mock<IMapper> mapperMock, IEnumerable<TimelineItem> entites)
        {
            mapperMock.Verify(
                m => m.Map<IEnumerable<TimelineItemDTO>>(entites),
                Times.Once,
                "Map method should be called exactly once with the retrieved timeline items");
        }

        public static void VerifyMapCalledNever(this Mock<IMapper> mapperMock)
        {
            mapperMock.Verify(
                m => m.Map<IEnumerable<TimelineItemDTO>>(
                    It.IsAny<object>()),
                Times.Never,
                "Map method should not be called at all");
        }
    }
}
