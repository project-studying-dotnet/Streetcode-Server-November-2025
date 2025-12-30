namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Delete
{
    using global::Streetcode.BLL.Interfaces.Logging;
    using global::Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete;
    using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using global::Streetcode.DAL.Repositories.Interfaces.Timeline;
    using global::Streetcode.XUnitTest.Helpers;
    using global::Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Xunit;

    public class DeleteTimelineItemHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ITimelineRepository> timelineRepositoryMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly DeleteTimelineItemHandler handler;

        public DeleteTimelineItemHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.timelineRepositoryMock = new Mock<ITimelineRepository>();
            this.loggerMock = new Mock<ILoggerService>();

            this.repositoryWrapperMock
                .Setup(r => r.TimelineRepository)
                .Returns(this.timelineRepositoryMock.Object);

            this.handler = new DeleteTimelineItemHandler(this.repositoryWrapperMock.Object, this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldReturnSuccessResult()
        {
            // Arrange
            int timelineItemId = 1;
            var timelineItem = TimelineItemTestData.CreateTimelineItem(timelineItemId, 1);
            var command = new DeleteTimelineItemCommand(timelineItemId);

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(timelineItem);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_WithNonExistentId_ShouldReturnFailure()
        {
            // Arrange
            int nonExistentId = 999;
            var command = new DeleteTimelineItemCommand(nonExistentId);

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(null);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("Cannot find a timeline item", result.Errors[0].Message);
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldCallDeleteOnRepository()
        {
            // Arrange
            int timelineItemId = 1;
            var timelineItem = TimelineItemTestData.CreateTimelineItem(timelineItemId, 1);
            var command = new DeleteTimelineItemCommand(timelineItemId);

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(timelineItem);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.timelineRepositoryMock.Verify(
                r => r.Delete(It.Is<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(t => t.Id == timelineItemId)),
                Times.Once);
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldCallSaveChangesOnce()
        {
            // Arrange
            int timelineItemId = 1;
            var timelineItem = TimelineItemTestData.CreateTimelineItem(timelineItemId, 1);
            var command = new DeleteTimelineItemCommand(timelineItemId);

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(timelineItem);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.repositoryWrapperMock.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task Handle_WithNonExistentId_ShouldNotCallDelete()
        {
            // Arrange
            int nonExistentId = 999;
            var command = new DeleteTimelineItemCommand(nonExistentId);

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(null);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.timelineRepositoryMock.Verify(
                r => r.Delete(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_WithNonExistentId_ShouldNotCallSaveChanges()
        {
            // Arrange
            int nonExistentId = 999;
            var command = new DeleteTimelineItemCommand(nonExistentId);

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(null);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.repositoryWrapperMock.Verify(
                r => r.SaveChangesAsync(),
                Times.Never);
        }

        [Fact]
        public async Task Handle_WithNonExistentId_ShouldLogError()
        {
            // Arrange
            int nonExistentId = 999;
            var command = new DeleteTimelineItemCommand(nonExistentId);

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(null);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.loggerMock.Verify(
                l => l.LogError(command, It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ShouldReturnFailure()
        {
            // Arrange
            int timelineItemId = 1;
            var command = new DeleteTimelineItemCommand(timelineItemId);
            var exceptionMessage = "Database connection failed";

            this.timelineRepositoryMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem, object>>>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("Failed to delete a timeline item", result.Errors[0].Message);
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ShouldLogError()
        {
            // Arrange
            int timelineItemId = 1;
            var command = new DeleteTimelineItemCommand(timelineItemId);
            var exceptionMessage = "Database connection failed";

            this.timelineRepositoryMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem, object>>>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.loggerMock.Verify(
                l => l.LogError(command, exceptionMessage),
                Times.Once);
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldRetrieveItemBeforeDeletion()
        {
            // Arrange
            int timelineItemId = 5;
            var timelineItem = TimelineItemTestData.CreateTimelineItem(timelineItemId, 1);
            var command = new DeleteTimelineItemCommand(timelineItemId);

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(timelineItem);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.timelineRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem, object>>>()),
                Times.Once);
        }
    }
}
