namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.GetById
{
    using AutoMapper;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetById;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Timeline;
    using Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures;
    using Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Helpers;
    using Xunit;

    /// <summary>
    /// Contains unit tests for the <see cref="GetTimelineItemByIdHandler"/>.
    /// </summary>
    public class GetTimelineItemByIdHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTimelineItemByIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTimelineItemByIdHandlerTests"/> class.
        /// Sets up the required mocked dependencies and creates an instance of the handler to test.
        /// </summary>
        public GetTimelineItemByIdHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetTimelineItemByIdHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        ///     Tests the <see cref="GetTimelineItemByIdHandler"/> behavior when the repository
        ///     returns <c>null</c> instead of a timeline item.
        /// </summary>
        /// <remarks>
        ///     This test verifies that:
        ///     <list type="bullet">
        ///         <item><description>The handler returns a failure <see cref="FluentResults.Result"/>.</description></item>
        ///         <item><description>An appropriate error message is included in the result.</description></item>
        ///         <item><description><c>GetFirstOrDefaultAsync</c> is called exactly once.</description></item>
        ///         <item><description><c>LogError</c> is invoked when <c>null</c> is returned from the repository.</description></item>
        ///         <item><description>The mapper is not invoked at all.</description></item>
        ///     </list>
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WhenTimelineItemIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            const int id = 1;
            var timelineRepositoryMock = new Mock<ITimelineRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock.SetupRepositoryWrapper(timelineRepositoryMock);
            timelineRepositoryMock.SetupGetFirstOrDefaultAsync(entity: null);
            this.loggerMock.SetupLogger();

            var query = new GetTimelineItemByIdQuery(Id: id);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal($"Cannot find a timeline item with corresponding id: {id}", result.Errors.FirstOrDefault()?.Message);

            // Verify
            timelineRepositoryMock.VerifyGetFirstOrDefaultCalledOnce();
            this.loggerMock.VerifyLogErrorCalledOnce();
            this.mapperMock.VerifyMapCalledNever();
        }

        /// <summary>
        ///     Tests that the <see cref="GetTimelineItemByIdHandler"/> correctly returns
        ///     a successful <see cref="Result{T}"/> containing mapped
        ///     <see cref="TimelineItemDTO"/> object when timeline item exist in the repository.
        /// </summary>
        /// <remarks>
        ///     This test verifies that:
        ///     <list type="bullet">
        ///         <item><description>The handler returns a successful <see cref="FluentResults.Result{T}"/>.</description></item>
        ///         <item><description>The returned DTO contains the same <c>Id</c> value as the entity retrieved from the repository.</description></item>
        ///         <item><description><c>GetFirstOrDefaultAsync</c> is called exactly once on the repository.</description></item>
        ///         <item><description>The mapper's <c>Map</c> method is called exactly once with the retrieved timeline items.</description></item>
        ///         <item><description>No errors are logged.</description></item>
        ///     </list>
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WhenTimelineItemExists_ShouldReturnMappedTimelineItem()
        {
            // Arrange
            const int id = 1;
            var entity = TimelineItemTestData.CreateTimelineItem(id);
            var dto = TimelineItemTestData.CreateTimelineItemDTO(id);
            var timelineRepositoryMock = new Mock<ITimelineRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock.SetupRepositoryWrapper(timelineRepositoryMock);
            timelineRepositoryMock.SetupGetFirstOrDefaultAsync(entity);
            this.mapperMock.SetupMapper(entity, dto);

            var query = new GetTimelineItemByIdQuery(Id: id);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Value);
            Assert.Equal(entity.Id, result.Value.Id);

            // Verify
            timelineRepositoryMock.VerifyGetFirstOrDefaultCalledOnce();
            this.mapperMock.VerifyMapCalledOnce(entity);
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}