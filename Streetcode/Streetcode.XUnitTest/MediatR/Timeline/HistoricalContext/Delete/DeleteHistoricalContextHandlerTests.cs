namespace Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.Delete
{
    using global::Streetcode.BLL.Interfaces.Logging;
    using global::Streetcode.BLL.MediatR.Timeline.HistoricalContext.Delete;
    using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using global::Streetcode.DAL.Repositories.Interfaces.Timeline;
    using global::Streetcode.XUnitTest.Helpers;
    using global::Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.Fixtures;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Xunit;

    public class DeleteHistoricalContextHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<IHistoricalContextRepository> historicalContextRepositoryMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly DeleteHistoricalContextHandler handler;

        public DeleteHistoricalContextHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.historicalContextRepositoryMock = new Mock<IHistoricalContextRepository>();
            this.loggerMock = new Mock<ILoggerService>();

            this.repositoryWrapperMock
                .Setup(r => r.HistoricalContextRepository)
                .Returns(this.historicalContextRepositoryMock.Object);

            this.handler = new DeleteHistoricalContextHandler(this.repositoryWrapperMock.Object, this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldReturnSuccessResult()
        {
            // Arrange
            int contextId = 1;
            var historicalContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Test Context");
            var command = new DeleteHistoricalContextCommand(contextId);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(historicalContext);

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
            var command = new DeleteHistoricalContextCommand(nonExistentId);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("not found", result.Errors[0].Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldCallDeleteOnRepository()
        {
            // Arrange
            int contextId = 1;
            var historicalContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Test Context");
            var command = new DeleteHistoricalContextCommand(contextId);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(historicalContext);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.historicalContextRepositoryMock.Verify(
                r => r.Delete(It.Is<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(c => c.Id == contextId)),
                Times.Once);
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldCallSaveChangesOnce()
        {
            // Arrange
            int contextId = 1;
            var historicalContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Test Context");
            var command = new DeleteHistoricalContextCommand(contextId);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(historicalContext);

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
            var command = new DeleteHistoricalContextCommand(nonExistentId);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.historicalContextRepositoryMock.Verify(
                r => r.Delete(It.IsAny<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_WithNonExistentId_ShouldNotCallSaveChanges()
        {
            // Arrange
            int nonExistentId = 999;
            var command = new DeleteHistoricalContextCommand(nonExistentId);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

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
            var command = new DeleteHistoricalContextCommand(nonExistentId);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

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
            int contextId = 1;
            var command = new DeleteHistoricalContextCommand(contextId);
            var exceptionMessage = "Database connection failed";

            this.historicalContextRepositoryMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("deletion failed", result.Errors[0].Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ShouldLogError()
        {
            // Arrange
            int contextId = 1;
            var command = new DeleteHistoricalContextCommand(contextId);
            var exceptionMessage = "Database connection failed";

            this.historicalContextRepositoryMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.loggerMock.Verify(
                l => l.LogError(command, exceptionMessage),
                Times.Once);
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldRetrieveContextBeforeDeletion()
        {
            // Arrange
            int contextId = 5;
            var historicalContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Test Context");
            var command = new DeleteHistoricalContextCommand(contextId);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(historicalContext);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.historicalContextRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()),
                Times.Once);
        }
    }
}
