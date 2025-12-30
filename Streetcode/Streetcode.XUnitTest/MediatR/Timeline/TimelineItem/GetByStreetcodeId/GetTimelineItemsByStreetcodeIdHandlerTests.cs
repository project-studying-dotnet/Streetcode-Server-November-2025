namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.GetByStreetcodeId
{
    using AutoMapper;
    using FluentResults;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Timeline;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Timeline.TimelineItem.GetByStreetcodeId;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Timeline;
 using global::Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Helpers;
    using Xunit;

    /// <summary>
    /// Contains unit tests for the <see cref="GetTimelineItemsByStreetcodeIdHandler"/>.
    /// </summary>
    public class GetTimelineItemsByStreetcodeIdHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTimelineItemsByStreetcodeIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTimelineItemsByStreetcodeIdHandlerTests"/> class.
        /// Sets up the required mocked dependencies and creates an instance of the handler to test.
        /// </summary>
        public GetTimelineItemsByStreetcodeIdHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetTimelineItemsByStreetcodeIdHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        ///     Tests the <see cref="GetTimelineItemsByStreetcodeIdHandler"/> behavior when the repository
        ///     returns <c>null</c> instead of timeline items.
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
            const int streetcodeId = 101;
            var timelineRepositoryMock = new Mock<ITimelineRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock.SetupRepositoryWrapper(timelineRepositoryMock);
            timelineRepositoryMock.SetupGetAllAsync(entities: null);
            this.loggerMock.SetupLogger();

            var query = new GetTimelineItemsByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(string.Format(ErrorMessages.TimelineItemNotFoundByStreetcodeId, streetcodeId), result.Errors.FirstOrDefault()?.Message);

            // Verify
            timelineRepositoryMock.VerifyGetAllAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledOnce();
            this.mapperMock.VerifyMapCalledNever<TimelineItemDto>();
        }

        /// <summary>
        ///     Tests that the <see cref="GetTimelineItemsByStreetcodeIdHandler"/> correctly returns
        ///     a successful <see cref="Result"/> containing mapped
        ///     <see cref="TimelineItemDto"/> objects when timeline items by specified streetcodeId exist in the repository.
        /// </summary>
        /// <remarks>
        ///     This test verifies that:
        ///     <list type="bullet">
        ///         <item><description>The handler returns a successful <see cref="FluentResults.Result{T}"/>.</description></item>
        ///         <item><description>The returned result contains the correct number of mapped <see cref="TimelineItemDto"/> objects.</description></item>
        ///         <item><description><c>GetAllAsync</c> is called exactly once on the repository.</description></item>
        ///         <item><description>The mapper's <c>Map</c> method is called exactly once with the retrieved timeline items.</description></item>
        ///         <item><description>No errors are logged.</description></item>
        ///     </list>
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WhenTimelineItemsExists_ShouldReturnMappedTimelineItems()
        {
            // Arrange
            const int entitiesCount = 10;
            const int streetcodeId = 101;
            var entities = TimelineItemTestData.CreateTimelineItems(entitiesCount, streetcodeId);
            var dtos = TimelineItemTestData.CreateTimelineItemDTOs(entitiesCount);

            var timelineRepositoryMock = new Mock<ITimelineRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock.SetupRepositoryWrapper(timelineRepositoryMock);
            timelineRepositoryMock.SetupGetAllAsync(entities);
            this.mapperMock.SetupMapper(entities, dtos);

            var query = new GetTimelineItemsByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Value);
            Assert.NotEmpty(result.Value);
            Assert.Equal(entities.Count, result.Value.Count());

            // Verify
            timelineRepositoryMock.VerifyGetAllAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce(entities);
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}
