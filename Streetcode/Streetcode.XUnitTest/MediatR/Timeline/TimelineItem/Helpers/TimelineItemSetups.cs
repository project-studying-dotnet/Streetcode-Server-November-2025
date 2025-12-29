namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Helpers
{
    using System.Linq.Expressions;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
 using global::Streetcode.BLL.DTO.Timeline;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.DAL.Entities.Timeline;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Timeline;

    /// <summary>
    /// Provides extension methods for configuring mocked repository, mapper, and logger behavior
    /// when testing TimelineItem-related handlers.
    /// </summary>
    public static class TimelineItemSetups
    {
        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="ITimelineRepository"/> instance when accessing the TimelineRepository property.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="timelineRepositoryMock">The mocked timeline repository to be returned.</param>
        public static void SetupRepositoryWrapper(this Mock<IRepositoryWrapper> repositoryWrapperMock, Mock<ITimelineRepository> timelineRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.TimelineRepository)
                .Returns(timelineRepositoryMock.Object);
        }

        /// <summary>
        /// Configures the mocked <see cref="ITimelineRepository"/> to return the specified collection
        /// of <see cref="TimelineItem"/> entities when calling <c>GetAllAsync</c>.
        /// </summary>
        /// <param name="timelineRepositoryMock">The mocked timeline repository.</param>
        /// <param name="entities">The collection of entities to return, or null.</param>
        public static void SetupGetAllAsync(this Mock<ITimelineRepository> timelineRepositoryMock, IEnumerable<TimelineItem>? entities)
        {
            timelineRepositoryMock
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<TimelineItem>, IIncludableQueryable<TimelineItem, object>>>()))
                .ReturnsAsync(entities!);
        }

        /// <summary>
        /// Configures the mocked <see cref="ITimelineRepository"/> to return the specified
        /// <see cref="TimelineItem"/> when calling <c>GetFirstOrDefaultAsync</c>.
        /// </summary>
        /// <param name="timelineRepositoryMock">The mocked timeline repository.</param>
        /// <param name="entity">The entity to return, or null.</param>
        public static void SetupGetFirstOrDefaultAsync(this Mock<ITimelineRepository> timelineRepositoryMock, TimelineItem? entity)
        {
            timelineRepositoryMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<TimelineItem>, IIncludableQueryable<TimelineItem, object>>>()))
                .ReturnsAsync(entity!);
        }

        /// <summary>
        /// Configures the mocked <see cref="ILoggerService"/> to accept any LogError call.
        /// This prevents logger usage from affecting test execution.
        /// </summary>
        /// <param name="loggerMock">The mocked logger service.</param>
        public static void SetupLogger(this Mock<ILoggerService> loggerMock)
        {
            loggerMock
                .Setup(l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()));
        }

        /// <summary>
        /// Configures the mocked <see cref="IMapper"/> to map a collection of
        /// <see cref="TimelineItem"/> entities to the corresponding collection of
        /// <see cref="TimelineItemDto"/> objects.
        /// </summary>
        /// <param name="mapperMock">The mocked mapper.</param>
        /// <param name="entities">The source TimelineItem entities.</param>
        /// <param name="dtos">The DTO collection to return.</param>
        public static void SetupMapper(this Mock<IMapper> mapperMock, IEnumerable<TimelineItem> entities, IEnumerable<TimelineItemDto> dtos)
        {
            mapperMock
                .Setup(m => m.Map<IEnumerable<TimelineItemDto>>(entities))
                .Returns(dtos);
        }

        /// <summary>
        /// Configures the mocked <see cref="IMapper"/> to map a single <see cref="TimelineItem"/>
        /// instance to a <see cref="TimelineItemDto"/>.
        /// </summary>
        /// <param name="mapperMock">The mocked mapper.</param>
        /// <param name="entity">The source TimelineItem entity.</param>
        /// <param name="dto">The DTO to return.</param>
        public static void SetupMapper(this Mock<IMapper> mapperMock, TimelineItem entity, TimelineItemDto dto)
        {
            mapperMock
                .Setup(m => m.Map<TimelineItemDto>(entity))
                .Returns(dto);
        }
    }
}
