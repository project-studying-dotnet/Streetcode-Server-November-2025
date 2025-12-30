namespace Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.GetAll
{
    using AutoMapper;
    using Moq;
    using global::Streetcode.BLL;
    using global::Streetcode.BLL.DTO.Timeline;
    using global::Streetcode.BLL.Interfaces.Logging;
    using global::Streetcode.BLL.MediatR.Timeline.HistoricalContext.GetAll;
    using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using global::Streetcode.DAL.Repositories.Interfaces.Timeline;
    using global::Streetcode.XUnitTest.Helpers;
    using global::Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.Fixtures;
    using Xunit;

    /// <summary>
    /// Contains unit tests for the <see cref="GetAllHistoricalContextHandler"/>.
    /// </summary>
    public class GetAllHistoricalContextHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<IHistoricalContextRepository> historicalContextRepositoryMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetAllHistoricalContextHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllHistoricalContextHandlerTests"/> class.
        /// Sets up the required mocked dependencies and creates an instance of the handler to test.
        /// </summary>
        public GetAllHistoricalContextHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.historicalContextRepositoryMock = new Mock<IHistoricalContextRepository>();
            this.loggerMock = new Mock<ILoggerService>();

            this.repositoryWrapperMock
                .Setup(r => r.HistoricalContextRepository)
                .Returns(this.historicalContextRepositoryMock.Object);

            this.handler = new GetAllHistoricalContextHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        ///     Tests the <see cref="GetAllHistoricalContextHandler"/> behavior when the repository
        ///     returns <c>null</c> instead of a collection of historical contexts.
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
        public async Task Handle_WhenHistoricalContextsIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            this.loggerMock
                .Setup(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()));

            var query = new GetAllHistoricalContextQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(ErrorMessages.HistoricalContextNotFound, result.Errors.FirstOrDefault()?.Message);

            // Verify
            this.historicalContextRepositoryMock.Verify(
                r => r.GetAllAsync(null, null),
                Times.Once);

            this.loggerMock.Verify(
                l => l.LogError(It.IsAny<object>(), It.IsAny<string>()),
                Times.Once);

            this.mapperMock.Verify(
                m => m.Map<IEnumerable<HistoricalContextDto>>(It.IsAny<IEnumerable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>>()),
                Times.Never);
        }

        /// <summary>
        ///     Tests that the <see cref="GetAllHistoricalContextHandler"/> correctly returns
        ///     a successful <see cref="Result{T}"/> containing mapped
        ///     <see cref="HistoricalContextDto"/> objects when contexts exist in the repository.
        /// </summary>
        /// <remarks>
        ///     This test verifies that:
        ///     <list type="bullet">
        ///         <item><description>The handler returns a successful <see cref="FluentResults.Result{T}"/>.</description></item>
        ///         <item><description>The returned result contains the correct number of mapped <see cref="HistoricalContextDto"/> objects.</description></item>
        ///         <item><description><c>GetAllAsync</c> is called exactly once on the repository.</description></item>
        ///         <item><description>The mapper's <c>Map</c> method is called exactly once with the retrieved contexts.</description></item>
        ///         <item><description>No errors are logged.</description></item>
        ///     </list>
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WhenHistoricalContextsExist_ShouldReturnMappedContexts()
        {
            // Arrange
            const int entitiesCount = 5;
            var entities = HistoricalContextTestData.CreateHistoricalContexts(entitiesCount);
            var dtos = entities.Select(e => HistoricalContextTestData.CreateHistoricalContextDto(e.Id, e.Title)).ToList();

            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(entities);

            this.mapperMock
                .Setup(m => m.Map<IEnumerable<HistoricalContextDto>>(entities))
                .Returns(dtos);

            var query = new GetAllHistoricalContextQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Value);
            Assert.NotEmpty(result.Value);
            Assert.Equal(entitiesCount, result.Value.Count());

            // Verify
            this.historicalContextRepositoryMock.Verify(
                r => r.GetAllAsync(null, null),
                Times.Once);

            this.mapperMock.Verify(
                m => m.Map<IEnumerable<HistoricalContextDto>>(entities),
                Times.Once);

            this.loggerMock.Verify(
                l => l.LogError(It.IsAny<object>(), It.IsAny<string>()),
                Times.Never);
        }

        /// <summary>
        ///     Tests that the <see cref="GetAllHistoricalContextHandler"/> returns a successful result
        ///     with an empty collection when no contexts exist in the repository.
        /// </summary>
        /// <remarks>
        ///     This test verifies that:
        ///     <list type="bullet">
        ///         <item><description>The handler returns a successful result.</description></item>
        ///         <item><description>The returned collection is empty.</description></item>
        ///         <item><description><c>GetAllAsync</c> is called exactly once.</description></item>
        ///         <item><description>The mapper is called to map an empty collection.</description></item>
        ///         <item><description>No errors are logged.</description></item>
        ///     </list>
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WhenNoContextsExist_ShouldReturnEmptyCollection()
        {
            // Arrange
            var emptyList = new List<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>();
            var emptyDtos = new List<HistoricalContextDto>();

            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(emptyList);

            this.mapperMock
                .Setup(m => m.Map<IEnumerable<HistoricalContextDto>>(emptyList))
                .Returns(emptyDtos);

            var query = new GetAllHistoricalContextQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Value);
            Assert.Empty(result.Value);

            // Verify
            this.historicalContextRepositoryMock.Verify(
                r => r.GetAllAsync(null, null),
                Times.Once);

            this.mapperMock.Verify(
                m => m.Map<IEnumerable<HistoricalContextDto>>(emptyList),
                Times.Once);

            this.loggerMock.Verify(
                l => l.LogError(It.IsAny<object>(), It.IsAny<string>()),
                Times.Never);
        }

        /// <summary>
        ///     Tests that the <see cref="GetAllHistoricalContextHandler"/> correctly maps
        ///     a single context when only one exists.
        /// </summary>
        /// <remarks>
        ///     This test verifies that:
        ///     <list type="bullet">
        ///         <item><description>The handler returns a successful result.</description></item>
        ///         <item><description>The returned collection contains exactly one item.</description></item>
        ///         <item><description>The mapped DTO has the correct properties.</description></item>
        ///     </list>
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WithSingleContext_ShouldReturnSingleMappedContext()
        {
            // Arrange
            var entity = HistoricalContextTestData.CreateHistoricalContext(1, "Єдиний контекст");
            var entities = new List<global::Streetcode.DAL.Entities.Timeline.HistoricalContext> { entity };
            var dto = HistoricalContextTestData.CreateHistoricalContextDto(1, "Єдиний контекст");
            var dtos = new List<HistoricalContextDto> { dto };

            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(entities);

            this.mapperMock
                .Setup(m => m.Map<IEnumerable<HistoricalContextDto>>(entities))
                .Returns(dtos);

            var query = new GetAllHistoricalContextQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Single(result.Value);
            Assert.Equal("Єдиний контекст", result.Value.First().Title);
            Assert.Equal(1, result.Value.First().Id);

            // Verify
            this.historicalContextRepositoryMock.Verify(
                r => r.GetAllAsync(null, null),
                Times.Once);

            this.mapperMock.Verify(
                m => m.Map<IEnumerable<HistoricalContextDto>>(entities),
                Times.Once);
        }

        /// <summary>
        ///     Tests that the <see cref="GetAllHistoricalContextHandler"/> correctly handles
        ///     multiple contexts and returns them all.
        /// </summary>
        /// <remarks>
        ///     This test verifies that:
        ///     <list type="bullet">
        ///         <item><description>The handler returns a successful result.</description></item>
        ///         <item><description>All contexts are returned in the collection.</description></item>
        ///         <item><description>The count matches the expected number of contexts.</description></item>
        ///     </list>
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WithMultipleContexts_ShouldReturnAllContexts()
        {
            // Arrange
            const int contextCount = 10;
            var entities = HistoricalContextTestData.CreateHistoricalContexts(contextCount);
            var dtos = entities.Select(e => HistoricalContextTestData.CreateHistoricalContextDto(e.Id, e.Title)).ToList();

            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(entities);

            this.mapperMock
                .Setup(m => m.Map<IEnumerable<HistoricalContextDto>>(entities))
                .Returns(dtos);

            var query = new GetAllHistoricalContextQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(contextCount, result.Value.Count());

            // Verify
            this.historicalContextRepositoryMock.Verify(
                r => r.GetAllAsync(null, null),
                Times.Once);

            this.mapperMock.Verify(
                m => m.Map<IEnumerable<HistoricalContextDto>>(entities),
                Times.Once);
        }

        /// <summary>
        ///     Tests that the <see cref="GetAllHistoricalContextHandler"/> logs an error
        ///     when the repository returns null.
        /// </summary>
        /// <remarks>
        ///     This test specifically verifies the error logging behavior.
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WhenRepositoryReturnsNull_ShouldLogError()
        {
            // Arrange
            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            this.loggerMock
                .Setup(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()));

            var query = new GetAllHistoricalContextQuery();

            // Act
            await this.handler.Handle(query, CancellationToken.None);

            // Assert
            this.loggerMock.Verify(
                l => l.LogError(query, ErrorMessages.HistoricalContextNotFound),
                Times.Once);
        }

        /// <summary>
        ///     Tests that the <see cref="GetAllHistoricalContextHandler"/> does not invoke
        ///     the mapper when the repository returns null.
        /// </summary>
        /// <remarks>
        ///     This test verifies that the handler short-circuits and doesn't attempt mapping
        ///     when there's no data to map.
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WhenRepositoryReturnsNull_ShouldNotInvokeMapper()
        {
            // Arrange
            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            this.loggerMock
                .Setup(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()));

            var query = new GetAllHistoricalContextQuery();

            // Act
            await this.handler.Handle(query, CancellationToken.None);

            // Assert
            this.mapperMock.Verify(
                m => m.Map<IEnumerable<HistoricalContextDto>>(It.IsAny<IEnumerable<global::Streetcode.DAL.Entities.Timeline.HistoricalContext>>()),
                Times.Never);
        }

        /// <summary>
        ///     Tests that the <see cref="GetAllHistoricalContextHandler"/> returns the correct
        ///     error message when contexts are not found.
        /// </summary>
        /// <remarks>
        ///     This test verifies the error message content.
        /// </remarks>
        /// <returns>
        ///     A task representing the asynchronous test execution.
        /// </returns>
        [Fact]
        public async Task Handle_WhenContextsNotFound_ShouldReturnCorrectErrorMessage()
        {
            // Arrange
            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, global::Streetcode.DAL.Entities.Timeline.HistoricalContext>(null);

            this.loggerMock
                .Setup(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()));

            var query = new GetAllHistoricalContextQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Contains(ErrorMessages.HistoricalContextNotFound, result.Errors.Select(e => e.Message));
        }
    }
}
