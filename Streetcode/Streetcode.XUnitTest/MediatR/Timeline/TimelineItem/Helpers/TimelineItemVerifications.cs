namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Helpers
{
    using System.Linq.Expressions;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Timeline;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.DAL.Entities.Timeline;
 using global::Streetcode.DAL.Repositories.Interfaces.Timeline;

    /// <summary>
    /// Provides extension methods for verifying interactions with mocked repository,
    /// mapper, and logger components in TimelineItem-related unit tests.
    /// </summary>
    public static class TimelineItemVerifications
    {
        // -------------------------- Verify Repository -------------------------------

        /// <summary>
        /// Verifies that <c>GetAllAsync</c> was called exactly once on the mocked
        /// <see cref="ITimelineRepository"/>.
        /// </summary>
        /// <param name="timelineRepositoryMock">The mocked timeline repository.</param>
        public static void VerifyGetAllAsyncCalledOnce(this Mock<ITimelineRepository> timelineRepositoryMock)
        {
            timelineRepositoryMock.Verify(
                tr => tr.GetAllAsync(
                    It.IsAny<Expression<Func<TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<TimelineItem>, IIncludableQueryable<TimelineItem, object>>>()),
                Times.Once(),
                ErrorMessages.VerifyGetAllAsyncCalledOnce);
        }

        /// <summary>
        /// Verifies that <c>GetFirstOrDefaultAsync</c> was called exactly once on the mocked
        /// <see cref="ITimelineRepository"/>.
        /// </summary>
        /// <param name="timelineRepositoryMock">The mocked timeline repository.</param>
        public static void VerifyGetFirstOrDefaultCalledOnce(this Mock<ITimelineRepository> timelineRepositoryMock)
        {
            timelineRepositoryMock.Verify(
                tr => tr.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<TimelineItem>, IIncludableQueryable<TimelineItem, object>>>()),
                Times.Once(),
                ErrorMessages.VerifyGetFirstOrDefaultAsyncCalledOnce);
        }

        // -------------------------- Verify Logger -------------------------------

        /// <summary>
        /// Verifies that <c>LogError</c> was called exactly once on the mocked
        /// <see cref="ILoggerService"/>.
        /// </summary>
        /// <param name="loggerMock">The mocked logger service.</param>
        public static void VerifyLogErrorCalledOnce(this Mock<ILoggerService> loggerMock)
        {
            loggerMock.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once(),
                ErrorMessages.VerifyLogCalledOnceTimelineNull);
        }

        /// <summary>
        /// Verifies that <c>LogError</c> was never called on the mocked
        /// <see cref="ILoggerService"/>.
        /// </summary>
        /// <param name="loggerMock">The mocked logger service.</param>
        public static void VerifyLogErrorCalledNever(this Mock<ILoggerService> loggerMock)
        {
            loggerMock.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Never,
                ErrorMessages.VerifyLogDidntCalledWhenTimelineexists);
        }

        // -------------------------- Verify Mapper -------------------------------

        /// <summary>
        /// Verifies that the mapper's collection mapping method was called exactly once
        /// with the provided collection of <see cref="TimelineItem"/> entities.
        /// </summary>
        /// <param name="mapperMock">The mocked mapper.</param>
        /// <param name="entities">The collection of entities expected to be mapped.</param>
        public static void VerifyMapCalledOnce(this Mock<IMapper> mapperMock, IEnumerable<TimelineItem> entities)
        {
            mapperMock.Verify(
                m => m.Map<IEnumerable<TimelineItemDto>>(entities),
                Times.Once,
                ErrorMessages.VerifyMapperCalledOnce);
        }

        /// <summary>
        /// Verifies that the mapper's single-entity mapping method was called exactly once
        /// with the provided <see cref="TimelineItem"/> entity.
        /// </summary>
        /// <param name="mapperMock">The mocked mapper.</param>
        /// <param name="entity">The single entity expected to be mapped.</param>
        public static void VerifyMapCalledOnce(this Mock<IMapper> mapperMock, TimelineItem entity)
        {
            mapperMock.Verify(
                m => m.Map<TimelineItemDto>(entity),
                Times.Once,
                ErrorMessages.VerifyMapperCalledOnce);
        }

        /// <summary>
        /// Verifies that no mapping operation was performed on the mocked mapper.
        /// </summary>
        /// <typeparam name="TDestination">The destination type to verify.</typeparam>
        public static void VerifyMapCalledNever<TDestination>(this Mock<IMapper> mapperMock)
        {
            mapperMock.Verify(
                m => m.Map<TDestination>(It.IsAny<object>()),
                Times.Never,
                string.Format(ErrorMessages.MapToExatct, typeof(TDestination).Name));
        }
    }
}
