namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.GetByStreetcodeId
{
    using AutoMapper;
    using Moq;
    using Org.BouncyCastle.Asn1.Ocsp;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetAll;
    using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetByStreetcodeId;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Timeline;
    using Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures;
    using Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Helpers;
    using Xunit;

    public class GetTimelineItemsByStreetcodeIdHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTimelineItemsByStreetcodeIdHandler handler;

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
            Assert.Equal($"Cannot find any timeline item by the streetcode id: {streetcodeId}", result.Errors.FirstOrDefault()?.Message);

            // Verify
            timelineRepositoryMock.VerifyGetAllAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledOnce();
            this.mapperMock.VerifyMapCalledNever();
        }

        [Fact]
        public async Task Handle_WhenTimelineItemsExists_ShouldReturnMappedTimelineItems()
        {
            // Arrange
            const int streetcodeId = 101;
            var entities = TimelineItemTestData.CreateTimelineItems(count: 10, streetcodeId);
            var dtos = TimelineItemTestData.CreateTimelineItemDTOs(count: 10);

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
            Assert.NotEmpty(result.Value);
            Assert.Equal(entities.Count, result.Value.Count());

            // Verify
            timelineRepositoryMock.VerifyGetAllAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce(entities);
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}
