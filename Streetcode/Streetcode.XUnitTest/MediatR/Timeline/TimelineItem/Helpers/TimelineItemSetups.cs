using Moq;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Interfaces.Timeline;

namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Helpers
{
    public static class TimelineItemSetups
    {
        public static void SetupRepositoryWrapper(this Mock<IRepositoryWrapper> repositoryWrapperMock, Mock<ITimelineRepository> timelineRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.TimelineRepository)
                .Returns(timelineRepositoryMock.Object);
        }
    }
}
