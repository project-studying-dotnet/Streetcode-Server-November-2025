namespace Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.Update
{
    using AutoMapper;
    using global::Streetcode.BLL.DTO.Timeline;
    using global::Streetcode.BLL.Interfaces.Logging;
    using global::Streetcode.BLL.MediatR.Timeline.HistoricalContext.Update;
    using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using global::Streetcode.DAL.Repositories.Interfaces.Timeline;
    using global::Streetcode.XUnitTest.Helpers;
    using global::Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.Fixtures;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Xunit;

    public class UpdateHistoricalContextHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<IHistoricalContextRepository> historicalContextRepositoryMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly UpdateHistoricalContextHandler handler;

        public UpdateHistoricalContextHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.historicalContextRepositoryMock = new Mock<IHistoricalContextRepository>();
            this.loggerMock = new Mock<ILoggerService>();

            this.repositoryWrapperMock
                .Setup(r => r.HistoricalContextRepository)
                .Returns(this.historicalContextRepositoryMock.Object);

            this.handler = new UpdateHistoricalContextHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidData_ShouldReturnSuccessResult()
        {
            // Arrange
            int contextId = 1;
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Old Title");
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(contextId, updateDto.Title);

            this.historicalContextRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ReturnsAsync(existingContext)
                .ReturnsAsync((global::Streetcode.DAL.Entities.Timeline.HistoricalContext?)null);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingContext))
                .Callback(() => existingContext.Title = updateDto.Title);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(existingContext))
                .Returns(resultDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(updateDto.Title, result.Value.Title);
        }

        [Fact]
        public async Task Handle_WithNonExistentId_ShouldReturnFailure()
        {
            // Arrange
            int nonExistentId = 999;
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = nonExistentId,
                Title = "Test Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("not found", result.Errors[0].Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Handle_WithDuplicateTitle_ShouldReturnFailure()
        {
            // Arrange
            int contextId = 1;
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Duplicate Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Old Title");
            var duplicateContext = HistoricalContextTestData.CreateHistoricalContext(2, updateDto.Title);

            this.historicalContextRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ReturnsAsync(existingContext)
                .ReturnsAsync(duplicateContext);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("already exists", result.Errors[0].Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Handle_WithSameTitle_ShouldNotCheckForDuplicates()
        {
            // Arrange
            int contextId = 1;
            string sameTitle = "Same Title";
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = sameTitle
            };
            var command = new UpdateHistoricalContextCommand(updateDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(contextId, sameTitle);
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(contextId, sameTitle);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(existingContext);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingContext));

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(existingContext))
                .Returns(resultDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            this.historicalContextRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()),
                Times.Once);  // Only one call to find by ID, no duplicate check
        }

        [Fact]
        public async Task Handle_OnSuccessfulUpdate_ShouldCallSaveChanges()
        {
            // Arrange
            int contextId = 1;
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Old Title");
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(contextId, updateDto.Title);

            this.historicalContextRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ReturnsAsync(existingContext)
                .ReturnsAsync((global::Streetcode.DAL.Entities.Timeline.HistoricalContext?)null);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingContext));

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(existingContext))
                .Returns(resultDto);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.repositoryWrapperMock.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallUpdateOnRepository()
        {
            // Arrange
            int contextId = 1;
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Old Title");
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(contextId, updateDto.Title);

            this.historicalContextRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ReturnsAsync(existingContext)
                .ReturnsAsync((global::Streetcode.DAL.Entities.Timeline.HistoricalContext?)null);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingContext));

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(existingContext))
                .Returns(resultDto);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.historicalContextRepositoryMock.Verify(
                r => r.Update(It.Is<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(c => c.Id == contextId)),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldUseMapperForDtoToEntityMapping()
        {
            // Arrange
            int contextId = 1;
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Old Title");
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(contextId, updateDto.Title);

            this.historicalContextRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ReturnsAsync(existingContext)
                .ReturnsAsync((global::Streetcode.DAL.Entities.Timeline.HistoricalContext?)null);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingContext));

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(existingContext))
                .Returns(resultDto);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.mapperMock.Verify(
                m => m.Map(updateDto, existingContext),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldUseMapperForEntityToDtoConversion()
        {
            // Arrange
            int contextId = 1;
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Old Title");
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(contextId, updateDto.Title);

            this.historicalContextRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ReturnsAsync(existingContext)
                .ReturnsAsync((global::Streetcode.DAL.Entities.Timeline.HistoricalContext?)null);

            this.mapperMock
                .Setup(m => m.Map(updateDto, existingContext));

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(existingContext))
                .Returns(resultDto);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.mapperMock.Verify(
                m => m.Map<HistoricalContextDto>(existingContext),
                Times.Once);
        }

        [Fact]
        public async Task Handle_WithNonExistentId_ShouldLogError()
        {
            // Arrange
            int nonExistentId = 999;
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = nonExistentId,
                Title = "Test Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);

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
        public async Task Handle_WithDuplicateTitle_ShouldLogError()
        {
            // Arrange
            int contextId = 1;
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Duplicate Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(contextId, "Old Title");
            var duplicateContext = HistoricalContextTestData.CreateHistoricalContext(2, updateDto.Title);

            this.historicalContextRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ReturnsAsync(existingContext)
                .ReturnsAsync(duplicateContext);

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
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = 1,
                Title = "Test Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);
            var exceptionMessage = "Database error";

            this.historicalContextRepositoryMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("update failed", result.Errors[0].Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ShouldLogError()
        {
            // Arrange
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = 1,
                Title = "Test Title"
            };
            var command = new UpdateHistoricalContextCommand(updateDto);
            var exceptionMessage = "Database error";

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
    }
}
