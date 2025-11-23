namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Helpers
{
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.DAL.Entities.Timeline;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Timeline;

    public static class TimelineItemSetups
    {
        public static void SetupRepositoryWrapper(this Mock<IRepositoryWrapper> repositoryWrapperMock, Mock<ITimelineRepository> timelineRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.TimelineRepository)
                .Returns(timelineRepositoryMock.Object);
        }

        public static void SetupTimelineRepository(this Mock<ITimelineRepository> timelineRepositoryMock, List<TimelineItem>? timelineItems)
        {
            timelineRepositoryMock
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<TimelineItem>, IIncludableQueryable<DAL.Entities.Timeline.TimelineItem, object>>>()))
                .ReturnsAsync(timelineItems!);
        }

        public static void SetupLogger(this Mock<ILoggerService> loggerMock)
        {
            loggerMock
                .Setup(l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()));
        }
    }
}
