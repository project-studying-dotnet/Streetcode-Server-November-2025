namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.GetAll
{
    using System.Linq.Expressions;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Timeline;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetAll;
    using Streetcode.DAL.Entities.Timeline;
    using Streetcode.DAL.Enums;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Timeline;
    using Xunit;

    /// <summary>
    /// Contains unit tests for the <see cref="GetAllTimelineItemsHandler"/>.
    /// </summary>
    public class GetAllTimelineItemsHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetAllTimelineItemsHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllTimelineItemsHandlerTests"/> class.
        /// Sets up the required mocked dependencies and creates an instance of the handler to test.
        /// </summary>
        public GetAllTimelineItemsHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetAllTimelineItemsHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        ///     Tests the <see cref="GetAllTimelineItemsHandler"/> behavior when the repository
        ///     returns <c>null</c> instead of a collection of timeline items.
        /// </summary>
        /// <remarks>
        ///     This test verifies that:
        ///     <list type="bullet">
        ///         <item><description>The handler returns a failure <see cref="FluentResults.Result"/>.</description></item>
        ///         <item><description>An appropriate error message is included in the result.</description></item>
        ///         <item><description><c>GetAllAsync</c> is called exactly once.</description></item>
        ///         <item><description><c>LogError</c> is invoked when <c>null</c> is returned from the repository.</description></item>
        ///         <item><description>The mapper is not invoked at all.</description></item>
        ///     </list>
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WhenTimelineItemsIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            var timelineRepositoryMock = new Mock<ITimelineRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock
                .Setup(rw => rw.TimelineRepository)
                .Returns(timelineRepositoryMock.Object);

            timelineRepositoryMock
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<DAL.Entities.Timeline.TimelineItem>, IIncludableQueryable<DAL.Entities.Timeline.TimelineItem, object>>>()))
                .ReturnsAsync((IEnumerable<DAL.Entities.Timeline.TimelineItem>)null!);

            this.loggerMock
                .Setup(l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()));

            var query = new GetAllTimelineItemsQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal("Cannot find any timelineItem", result.Errors.FirstOrDefault()?.Message);

            // Verify
            timelineRepositoryMock.Verify(
                tr => tr.GetAllAsync(
                    It.IsAny<Expression<Func<DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<DAL.Entities.Timeline.TimelineItem>, IIncludableQueryable<DAL.Entities.Timeline.TimelineItem, object>>>()),
                Times.Once(),
                "GetAllAsync method should be called exactly once");

            this.loggerMock.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.AtLeastOnce(),
                "LogError method should be called exactly once when timelineItems is null");

            this.mapperMock.Verify(
                m => m.Map<IEnumerable<TimelineItemDTO>>(
                    It.IsAny<object>()),
                Times.Never,
                "Map method should not have been called at all");
        }

        /// <summary>
        ///     Tests that the <see cref="GetAllTimelineItemsHandler"/> correctly returns
        ///     a successful <see cref="Result{T}"/> containing mapped
        ///     <see cref="TimelineItemDTO"/> objects when timeline items exist in the repository.
        /// </summary>
        /// <remarks>
        ///     This test verifies that:
        ///     <list type="bullet">
        ///         <item><description>The handler returns a successful <see cref="FluentResults.Result{T}"/>.</description></item>
        ///         <item><description>The returned result contains the correct number of mapped <see cref="TimelineItemDTO"/> objects.</description></item>
        ///         <item><description><c>GetAllAsync</c> is called exactly once on the repository.</description></item>
        ///         <item><description>The mapper's <c>Map</c> method is called exactly once with the retrieved timeline items.</description></item>
        ///         <item><description>No errors are logged.</description></item>
        ///     </list>
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WhenTimelineItemsExist_ShouldReturnMappedTimelineItems()
        {
            // Arrange
            var timelineItems = new List<TimelineItem>
            {
                new ()
                {
                    Id = 1,
                    Date = new DateTime(1920, 1, 15),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = "Founding of the Organization",
                    Description = "The organization was officially founded and began its operations.",
                    StreetcodeId = 101,
                    Streetcode = null,
                    HistoricalContextTimelines = new List<HistoricalContextTimeline>(),
                },
                new ()
                {
                    Id = 2,
                    Date = new DateTime(1945, 5, 9),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = "Important Historical Event",
                    Description = "A significant event that influenced further developments.",
                    StreetcodeId = 102,
                    Streetcode = null,
                    HistoricalContextTimelines = new List<HistoricalContextTimeline>(),
                },
                new ()
                {
                    Id = 3,
                    Date = new DateTime(2001, 9, 1),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = "New Millennium Milestone",
                    Description = "A milestone that marked major technological advancements.",
                    StreetcodeId = 103,
                    Streetcode = null,
                    HistoricalContextTimelines = new List<HistoricalContextTimeline>(),
                },
                new ()
                {
                    Id = 4,
                    Date = new DateTime(2020, 3, 12),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = "Modern Era Breakthrough",
                    Description = "A breakthrough in modern history that reshaped the industry.",
                    StreetcodeId = 104,
                    Streetcode = null,
                    HistoricalContextTimelines = new List<HistoricalContextTimeline>(),
                },
            };
            var expectedTimelineItemsDTOs = new List<TimelineItemDTO>
            {
                new ()
                {
                    Id = 1,
                    Date = new DateTime(1920, 1, 15),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = "Founding of the Organization",
                    Description = "The organization was officially founded and began its operations.",
                    HistoricalContexts = new List<HistoricalContextDTO>(),
                },
                new ()
                {
                    Id = 2,
                    Date = new DateTime(1945, 5, 9),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = "Important Historical Event",
                    Description = "A significant event that influenced further developments.",
                    HistoricalContexts = new List<HistoricalContextDTO>(),
                },
                new ()
                {
                    Id = 3,
                    Date = new DateTime(2001, 9, 1),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = "New Millennium Milestone",
                    Description = "A milestone that marked major technological advancements.",
                    HistoricalContexts = new List<HistoricalContextDTO>(),
                },
                new ()
                {
                    Id = 4,
                    Date = new DateTime(2020, 3, 12),
                    DateViewPattern = DateViewPattern.DateMonthYear,
                    Title = "Modern Era Breakthrough",
                    Description = "A breakthrough in modern history that reshaped the industry.",
                    HistoricalContexts = new List<HistoricalContextDTO>(),
                },
            };

            var timelineRepositoryMock = new Mock<ITimelineRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock
                .Setup(rw => rw.TimelineRepository)
                .Returns(timelineRepositoryMock.Object);

            timelineRepositoryMock
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<TimelineItem>, IIncludableQueryable<TimelineItem, object>>>()))
                .ReturnsAsync(timelineItems);

            this.mapperMock
                .Setup(m => m.Map<IEnumerable<TimelineItemDTO>>(timelineItems))
                .Returns(expectedTimelineItemsDTOs);

            var query = new GetAllTimelineItemsQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(timelineItems.Count, result.Value.Count());

            // Verify
            timelineRepositoryMock.Verify(
                tr => tr.GetAllAsync(
                    It.IsAny<Expression<Func<TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<TimelineItem>, IIncludableQueryable<TimelineItem, object>>>()),
                Times.Once,
                "GetAllAsync method should be called exactly once");

            this.mapperMock.Verify(
                m => m.Map<IEnumerable<TimelineItemDTO>>(timelineItems),
                Times.Once,
                "Map method should be called exactly once with the retrieved timeline items");

            this.loggerMock.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Never,
                "LogError method should not be called when timelineItems exists");
        }
    }
}