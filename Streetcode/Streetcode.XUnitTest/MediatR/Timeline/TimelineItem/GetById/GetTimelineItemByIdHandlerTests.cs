namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.GetById
{
    using AutoMapper;
    using Moq;
    using Streetcode.BLL.DTO.Timeline;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetById;
    using Streetcode.DAL.Entities.Timeline;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Timeline;
    using Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures;
    using Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Helpers;
    using Xunit;

    public class GetTimelineItemByIdHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTimelineItemByIdHandler handler;

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

        [Fact]
        public async Task Handle_WhenTimelineItemIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            const int id = 1;
            var timelineRepositoryMock = new Mock<ITimelineRepository>();

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

        [Fact]
        public async Task Handle_WhenTimelineItemExists_ShouldReturnMappedTimelineItem()
        {
            // Arrange
            const int id = 1;
            var entity = TimelineItemTestData.CreateTimelineItem(id);
            var dto = TimelineItemTestData.CreateTimelineItemDTO(id);
            var timelineRepositoryMock = new Mock<ITimelineRepository>();

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