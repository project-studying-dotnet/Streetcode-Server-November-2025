namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.GetAll
{
    using AutoMapper;
    using Moq;
    using Streetcode.BLL.DTO.Timeline;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetAll;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Timeline;
    using Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures;
    using Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Helpers;
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

            this.repositoryWrapperMock.SetupRepositoryWrapper(timelineRepositoryMock);
            timelineRepositoryMock.SetupTimelineRepository(entities: null);
            this.loggerMock.SetupLogger();

            var query = new GetAllTimelineItemsQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal("Cannot find any timelineItem", result.Errors.FirstOrDefault()?.Message);

            // Verify
            timelineRepositoryMock.VerifyGetAllAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledOnce();
            this.mapperMock.VerifyMapCalledNever();
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
            var entities = TimelineItemTestData.CreateTimelineItems(count: 10);
            var dtos = TimelineItemTestData.CreateTimelineItemDTOs(count: 10);

            var timelineRepositoryMock = new Mock<ITimelineRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock.SetupRepositoryWrapper(timelineRepositoryMock);
            timelineRepositoryMock.SetupTimelineRepository(entities);
            this.mapperMock.SetupMapper(entities, dtos);

            var query = new GetAllTimelineItemsQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(entities.Count, result.Value.Count());

            // Verify
            timelineRepositoryMock.VerifyGetAllAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce(entities);
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}