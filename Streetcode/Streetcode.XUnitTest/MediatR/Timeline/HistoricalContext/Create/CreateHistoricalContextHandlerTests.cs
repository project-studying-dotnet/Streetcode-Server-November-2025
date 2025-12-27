namespace Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.Create
{
    using AutoMapper;
    using global::Streetcode.BLL.DTO.Timeline;
    using global::Streetcode.BLL.Interfaces.Logging;
    using global::Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create;
    using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using global::Streetcode.DAL.Repositories.Interfaces.Timeline;
    using global::Streetcode.XUnitTest.Helpers;
    using global::Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.Fixtures;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Xunit;

    public class CreateHistoricalContextHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<IHistoricalContextRepository> historicalContextRepositoryMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly CreateHistoricalContextHandler handler;

        public CreateHistoricalContextHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.historicalContextRepositoryMock = new Mock<IHistoricalContextRepository>();
            this.loggerMock = new Mock<ILoggerService>();

            this.repositoryWrapperMock
                .Setup(r => r.HistoricalContextRepository)
                .Returns(this.historicalContextRepositoryMock.Object);

            this.handler = new CreateHistoricalContextHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidData_ShouldReturnSuccessResult()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("New Context");
            var command = new CreateHistoricalContextCommand(createDto);

            var entity = HistoricalContextTestData.CreateHistoricalContext(1, createDto.Title);
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(1, createDto.Title);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(createDto))
                .Returns(entity);

            this.historicalContextRepositoryMock
                .SetupCreateAsync(entity);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(entity))
                .Returns(resultDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(createDto.Title, result.Value.Title);
        }

        [Fact]
        public async Task Handle_WithDuplicateTitle_ShouldReturnFailure()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("Existing Title");
            var command = new CreateHistoricalContextCommand(createDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(1, createDto.Title);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(existingContext);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("already exists", result.Errors[0].Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Handle_WithDuplicateTitle_ShouldNotCreateContext()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("Existing Title");
            var command = new CreateHistoricalContextCommand(createDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(1, createDto.Title);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(existingContext);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.historicalContextRepositoryMock.Verify(
                r => r.CreateAsync(It.IsAny<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_WithDuplicateTitle_ShouldLogError()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("Existing Title");
            var command = new CreateHistoricalContextCommand(createDto);

            var existingContext = HistoricalContextTestData.CreateHistoricalContext(1, createDto.Title);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(existingContext);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.loggerMock.Verify(
                l => l.LogError(command, It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_OnSuccessfulCreation_ShouldCallSaveChanges()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("New Context");
            var command = new CreateHistoricalContextCommand(createDto);

            var entity = HistoricalContextTestData.CreateHistoricalContext(1, createDto.Title);
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(1, createDto.Title);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(createDto))
                .Returns(entity);

            this.historicalContextRepositoryMock
                .SetupCreateAsync(entity);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(entity))
                .Returns(resultDto);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.repositoryWrapperMock.Verify(
                r => r.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldUseMapperForDtoToEntityConversion()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("Test Context");
            var command = new CreateHistoricalContextCommand(createDto);

            var entity = HistoricalContextTestData.CreateHistoricalContext(1, createDto.Title);
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(1, createDto.Title);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(createDto))
                .Returns(entity);

            this.historicalContextRepositoryMock
                .SetupCreateAsync(entity);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(entity))
                .Returns(resultDto);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.mapperMock.Verify(
                m => m.Map<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(createDto),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldUseMapperForEntityToDtoConversion()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("Test Context");
            var command = new CreateHistoricalContextCommand(createDto);

            var entity = HistoricalContextTestData.CreateHistoricalContext(1, createDto.Title);
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(1, createDto.Title);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(createDto))
                .Returns(entity);

            this.historicalContextRepositoryMock
                .SetupCreateAsync(entity);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(entity))
                .Returns(resultDto);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.mapperMock.Verify(
                m => m.Map<HistoricalContextDto>(entity),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCallCreateAsyncOnRepository()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("Test Context");
            var command = new CreateHistoricalContextCommand(createDto);

            var entity = HistoricalContextTestData.CreateHistoricalContext(1, createDto.Title);
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(1, createDto.Title);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(createDto))
                .Returns(entity);

            this.historicalContextRepositoryMock
                .SetupCreateAsync(entity);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(entity))
                .Returns(resultDto);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.historicalContextRepositoryMock.Verify(
                r => r.CreateAsync(It.Is<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(c => c.Title == createDto.Title)),
                Times.Once);
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ShouldReturnFailure()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("Test Context");
            var command = new CreateHistoricalContextCommand(createDto);
            var exceptionMessage = "Database error";

            this.historicalContextRepositoryMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("creation failed", result.Errors[0].Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Handle_WhenExceptionOccurs_ShouldLogError()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("Test Context");
            var command = new CreateHistoricalContextCommand(createDto);
            var exceptionMessage = "Database error";

            this.historicalContextRepositoryMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.loggerMock.Verify(
                l => l.LogError(command, exceptionMessage),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldCheckForDuplicateTitleBeforeCreation()
        {
            // Arrange
            var createDto = HistoricalContextTestData.CreateHistoricalContextCreateDto("Test Context");
            var command = new CreateHistoricalContextCommand(createDto);

            var entity = HistoricalContextTestData.CreateHistoricalContext(1, createDto.Title);
            var resultDto = HistoricalContextTestData.CreateHistoricalContextDto(1, createDto.Title);

            this.historicalContextRepositoryMock
                .SetupGetFirstOrDefaultAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(createDto))
                .Returns(entity);

            this.historicalContextRepositoryMock
                .SetupCreateAsync(entity);

            this.repositoryWrapperMock
                .SetupSaveChangesAsync();

            this.mapperMock
                .Setup(m => m.Map<HistoricalContextDto>(entity))
                .Returns(resultDto);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.historicalContextRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<System.Func<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext, object>>>()),
                Times.Once);
        }
    }
}
