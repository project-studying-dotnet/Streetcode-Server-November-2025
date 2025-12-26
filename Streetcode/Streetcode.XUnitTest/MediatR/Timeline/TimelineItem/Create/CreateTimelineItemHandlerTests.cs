namespace Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Create
{
    using System.Linq.Expressions;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using global::Streetcode.BLL.DTO.Timeline;
    using global::Streetcode.BLL.Interfaces.Logging;
    using global::Streetcode.BLL.MediatR.Timeline.TimelineItem.Create;
    using global::Streetcode.DAL.Entities.Streetcode;
    using global::Streetcode.DAL.Entities.Timeline;
    using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using global::Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using global::Streetcode.DAL.Repositories.Interfaces.Timeline;
    using global::Streetcode.XUnitTest.Helpers;
    using global::Streetcode.XUnitTest.MediatR.Timeline.TimelineItem.Fixtures;
    using Xunit;

    /// <summary>
    /// Contains unit tests for the <see cref="CreateTimelineItemHandler"/>.
    /// Tests focus on successful creation scenarios with valid data, HistoricalContext associations,
    /// and proper repository interactions.
    /// </summary>
    public class CreateTimelineItemHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<ITimelineRepository> timelineRepositoryMock;
        private readonly Mock<IStreetcodeRepository> streetcodeRepositoryMock;
        private readonly Mock<IHistoricalContextRepository> historicalContextRepositoryMock;
        private readonly CreateTimelineItemHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTimelineItemHandlerTests"/> class.
        /// Sets up the required mocked dependencies and creates an instance of the handler to test.
        /// </summary>
        public CreateTimelineItemHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.timelineRepositoryMock = new Mock<ITimelineRepository>();
            this.streetcodeRepositoryMock = new Mock<IStreetcodeRepository>();
            this.historicalContextRepositoryMock = new Mock<IHistoricalContextRepository>();

            this.repositoryWrapperMock
                .Setup(rw => rw.TimelineRepository)
                .Returns(this.timelineRepositoryMock.Object);

            this.repositoryWrapperMock
                .Setup(rw => rw.StreetcodeRepository)
                .Returns(this.streetcodeRepositoryMock.Object);

            this.repositoryWrapperMock
                .Setup(rw => rw.HistoricalContextRepository)
                .Returns(this.historicalContextRepositoryMock.Object);

            this.handler = new CreateTimelineItemHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> successfully creates a timeline item
        /// with valid data and no historical contexts.
        /// </summary>
        /// <remarks>
        /// This test verifies that:
        /// <list type="bullet">
        ///     <item><description>The handler returns a successful result.</description></item>
        ///     <item><description>The Streetcode existence check is performed.</description></item>
        ///     <item><description>The mapper correctly maps the DTO to entity.</description></item>
        ///     <item><description>CreateAsync is called on the repository.</description></item>
        ///     <item><description>SaveChangesAsync is called.</description></item>
        ///     <item><description>The created item is retrieved with includes.</description></item>
        ///     <item><description>The result is mapped back to DTO.</description></item>
        /// </list>
        /// </remarks>
        [Fact]
        public async Task Handle_WithValidDataAndNoContexts_ShouldCreateTimelineItemSuccessfully()
        {
            // Arrange
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto();
            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };
            var newEntity = TimelineItemTestData.CreateTimelineItem(id: 0, streetcodeId: createDto.StreetcodeId);
            var createdEntity = TimelineItemTestData.CreateTimelineItem(id: 1, streetcodeId: createDto.StreetcodeId);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.streetcodeRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto))
                .Returns(newEntity);

            this.timelineRepositoryMock
                .SetupCreateAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(createdEntity))
                .Returns(resultDto);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(resultDto.Id, result.Value.Id);

            // Verify
            this.streetcodeRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()),
                Times.Once);

            this.timelineRepositoryMock.Verify(
                r => r.CreateAsync(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()),
                Times.Once);

            this.repositoryWrapperMock.Verify(rw => rw.SaveChangesAsync(), Times.Once);

            this.mapperMock.Verify(
                m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto),
                Times.Once);

            this.mapperMock.Verify(
                m => m.Map<TimelineItemDto>(createdEntity),
                Times.Once);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> successfully creates a timeline item
        /// with valid HistoricalContext associations.
        /// </summary>
        /// <remarks>
        /// This test verifies that:
        /// <list type="bullet">
        ///     <item><description>The handler validates that all HistoricalContext IDs exist.</description></item>
        ///     <item><description>HistoricalContextTimelines relationships are created correctly.</description></item>
        ///     <item><description>The handler returns a successful result with the associated contexts.</description></item>
        /// </list>
        /// </remarks>
        [Fact]
        public async Task Handle_WithValidHistoricalContexts_ShouldCreateTimelineItemWithAssociations()
        {
            // Arrange
            var historicalContextIds = new List<int> { 1, 2 };
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto(
                historicalContextIds: historicalContextIds);

            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };
            
            var existingContexts = new List<HistoricalContext>
            {
                new HistoricalContext { Id = 1, Title = "Context 1" },
                new HistoricalContext { Id = 2, Title = "Context 2" },
            };

            var newEntity = TimelineItemTestData.CreateTimelineItem(id: 0, streetcodeId: createDto.StreetcodeId);
            var createdEntity = TimelineItemTestData.CreateTimelineItemWithContexts(id: 1, 1, 2);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.streetcodeRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, HistoricalContext>(existingContexts);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto))
                .Returns(newEntity);

            this.timelineRepositoryMock
                .SetupCreateAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(createdEntity))
                .Returns(resultDto);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

            // Verify HistoricalContext validation was performed
            this.historicalContextRepositoryMock.Verify(
                r => r.GetAllAsync(
                    It.IsAny<Expression<Func<HistoricalContext, bool>>>(),
                    It.IsAny<Func<IQueryable<HistoricalContext>, IIncludableQueryable<HistoricalContext, object>>>()),
                Times.Once);

            // Verify creation occurred
            this.timelineRepositoryMock.Verify(
                r => r.CreateAsync(It.Is<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(
                    ti => ti.HistoricalContextTimelines.Count == 2)),
                Times.Once);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> properly calls SaveChangesAsync
        /// to persist the timeline item to the database.
        /// </summary>
        [Fact]
        public async Task Handle_OnSuccessfulCreation_ShouldCallSaveChangesAsync()
        {
            // Arrange
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto();
            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };
            var newEntity = TimelineItemTestData.CreateTimelineItem(id: 0);
            var createdEntity = TimelineItemTestData.CreateTimelineItem(id: 1);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.streetcodeRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto))
                .Returns(newEntity);

            this.timelineRepositoryMock
                .SetupCreateAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(createdEntity))
                .Returns(resultDto);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            // Verify SaveChangesAsync was called exactly once
            this.repositoryWrapperMock.Verify(rw => rw.SaveChangesAsync(), Times.Once);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> retrieves the created item with
        /// includes for HistoricalContextTimelines after creation.
        /// </summary>
        [Fact]
        public async Task Handle_AfterCreation_ShouldRetrieveItemWithIncludes()
        {
            // Arrange
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto();
            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };
            var newEntity = TimelineItemTestData.CreateTimelineItem(id: 0);
            var createdEntity = TimelineItemTestData.CreateTimelineItem(id: 1);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.streetcodeRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto))
                .Returns(newEntity);

            this.timelineRepositoryMock
                .SetupCreateAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(createdEntity))
                .Returns(resultDto);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            // Verify GetFirstOrDefaultAsync was called twice:
            // Once for CreateAsync return, once for retrieving with includes
            this.timelineRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<global::Streetcode.DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem>,
                        IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem, object>>>()),
                Times.Once);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> correctly maps between DTOs and entities
        /// using the provided IMapper.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldUseMapperForDtoToEntityAndEntityToDtoConversions()
        {
            // Arrange
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto();
            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };
            var newEntity = TimelineItemTestData.CreateTimelineItem(id: 0);
            var createdEntity = TimelineItemTestData.CreateTimelineItem(id: 1);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.streetcodeRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto))
                .Returns(newEntity);

            this.timelineRepositoryMock
                .SetupCreateAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(createdEntity))
                .Returns(resultDto);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            // Verify mapper was used for DTO to Entity mapping
            this.mapperMock.Verify(
                m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto),
                Times.Once);

            // Verify mapper was used for Entity to DTO mapping
            this.mapperMock.Verify(
                m => m.Map<TimelineItemDto>(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()),
                Times.Once);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> creates proper HistoricalContextTimeline
        /// junction entities when HistoricalContext IDs are provided.
        /// </summary>
        [Fact]
        public async Task Handle_WithHistoricalContextIds_ShouldCreateProperJunctionEntities()
        {
            // Arrange
            var historicalContextIds = new List<int> { 10, 20, 30 };
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto(
                historicalContextIds: historicalContextIds);

            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };
            
            var existingContexts = new List<HistoricalContext>
            {
                new HistoricalContext { Id = 10, Title = "Context 10" },
                new HistoricalContext { Id = 20, Title = "Context 20" },
                new HistoricalContext { Id = 30, Title = "Context 30" },
            };

            var newEntity = TimelineItemTestData.CreateTimelineItem(id: 0);
            var createdEntity = TimelineItemTestData.CreateTimelineItemWithContexts(id: 1, 10, 20, 30);
            var resultDto = TimelineItemTestData.CreateTimelineItemDTO(id: 1);

            this.streetcodeRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, HistoricalContext>(existingContexts);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto))
                .Returns(newEntity);

            this.timelineRepositoryMock
                .SetupCreateAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(createdEntity))
                .Returns(resultDto);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            // Verify that CreateAsync was called with a TimelineItem that has 3 junction entities
            this.timelineRepositoryMock.Verify(
                r => r.CreateAsync(It.Is<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(
                    ti => ti.HistoricalContextTimelines.Count == 3 &&
                          ti.HistoricalContextTimelines.Any(hct => hct.HistoricalContextId == 10) &&
                          ti.HistoricalContextTimelines.Any(hct => hct.HistoricalContextId == 20) &&
                          ti.HistoricalContextTimelines.Any(hct => hct.HistoricalContextId == 30))),
                Times.Once);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> returns a failure when the specified
        /// Streetcode does not exist in the database.
        /// </summary>
        [Fact]
        public async Task Handle_WithNonExistentStreetcode_ShouldReturnFailure()
        {
            // Arrange
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto();

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(null);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("Streetcode", result.Errors[0].Message);

            // Verify that CreateAsync was never called
            this.timelineRepositoryMock.Verify(
                r => r.CreateAsync(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()),
                Times.Never);

            // Verify that SaveChangesAsync was never called
            this.repositoryWrapperMock.Verify(rw => rw.SaveChangesAsync(), Times.Never);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> returns a failure when one or more
        /// specified HistoricalContext IDs do not exist in the database.
        /// </summary>
        [Fact]
        public async Task Handle_WithNonExistentHistoricalContextIds_ShouldReturnFailure()
        {
            // Arrange
            var invalidContextIds = new List<int> { 999, 1000 };
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto(
                historicalContextIds: invalidContextIds);

            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };
            var emptyContexts = new List<HistoricalContext>();

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, HistoricalContext>(emptyContexts);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("HistoricalContext", result.Errors[0].Message);

            // Verify that CreateAsync was never called
            this.timelineRepositoryMock.Verify(
                r => r.CreateAsync(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()),
                Times.Never);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> returns a failure when some (but not all)
        /// HistoricalContext IDs exist, indicating partial validation failure.
        /// </summary>
        [Fact]
        public async Task Handle_WithPartiallyValidHistoricalContextIds_ShouldReturnFailure()
        {
            // Arrange
            var requestedIds = new List<int> { 1, 2, 999 };
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto(
                historicalContextIds: requestedIds);

            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };

            var existingContexts = new List<HistoricalContext>
            {
                new HistoricalContext { Id = 1, Title = "Context 1" },
                new HistoricalContext { Id = 2, Title = "Context 2" },
            };

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.historicalContextRepositoryMock
                .SetupGetAllAsync<IHistoricalContextRepository, HistoricalContext>(existingContexts);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("HistoricalContext", result.Errors[0].Message);

            // Verify that CreateAsync was never called
            this.timelineRepositoryMock.Verify(
                r => r.CreateAsync(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()),
                Times.Never);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> returns a failure when SaveChangesAsync
        /// fails to persist the timeline item to the database.
        /// </summary>
        [Fact]
        public async Task Handle_WhenSaveChangesFails_ShouldReturnFailure()
        {
            // Arrange
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto();
            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };
            var newEntity = TimelineItemTestData.CreateTimelineItem(id: 0);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto))
                .Returns(newEntity);

            this.timelineRepositoryMock
                .SetupCreateAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(newEntity);

            this.repositoryWrapperMock.SetupSaveChangesAsync(0);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("save", result.Errors[0].Message, StringComparison.OrdinalIgnoreCase);

            // Verify that CreateAsync was called
            this.timelineRepositoryMock.Verify(
                r => r.CreateAsync(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()),
                Times.Once);

            // Verify that SaveChangesAsync was called
            this.repositoryWrapperMock.Verify(rw => rw.SaveChangesAsync(), Times.Once);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> returns a failure when the created
        /// timeline item cannot be retrieved after saving.
        /// </summary>
        [Fact]
        public async Task Handle_WhenCreatedItemNotFound_ShouldReturnFailure()
        {
            // Arrange
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto();
            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };
            var newEntity = TimelineItemTestData.CreateTimelineItem(id: 1);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto))
                .Returns(newEntity);

            this.timelineRepositoryMock
                .SetupCreateAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(newEntity);

            this.repositoryWrapperMock.SetupSaveChangesAsync(1);

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(null);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("find", result.Errors[0].Message, StringComparison.OrdinalIgnoreCase);

            // Verify all steps were attempted
            this.timelineRepositoryMock.Verify(
                r => r.CreateAsync(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()),
                Times.Once);

            this.repositoryWrapperMock.Verify(rw => rw.SaveChangesAsync(), Times.Once);

            this.timelineRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<global::Streetcode.DAL.Entities.Timeline.TimelineItem, bool>>>(),
                    It.IsAny<Func<IQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem>,
                        IIncludableQueryable<global::Streetcode.DAL.Entities.Timeline.TimelineItem, object>>>()),
                Times.Once);
        }

        /// <summary>
        /// Tests that <see cref="CreateTimelineItemHandler"/> returns a failure when the mapper
        /// fails to map the created entity back to a DTO.
        /// </summary>
        [Fact]
        public async Task Handle_WhenMapperReturnsNull_ShouldReturnFailure()
        {
            // Arrange
            var createDto = TimelineItemTestData.CreateTimelineItemCreateDto();
            var streetcode = new StreetcodeContent { Id = createDto.StreetcodeId };
            var newEntity = TimelineItemTestData.CreateTimelineItem(id: 0);
            var createdEntity = TimelineItemTestData.CreateTimelineItem(id: 1);

            this.streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(streetcode);

            this.mapperMock
                .Setup(m => m.Map<global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createDto))
                .Returns(newEntity);

            this.timelineRepositoryMock
                .SetupCreateAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.repositoryWrapperMock.SetupSaveChangesAsync(1);

            this.timelineRepositoryMock
                .SetupGetFirstOrDefaultAsync<ITimelineRepository, global::Streetcode.DAL.Entities.Timeline.TimelineItem>(createdEntity);

            this.mapperMock
                .Setup(m => m.Map<TimelineItemDto>(createdEntity))
                .Returns((TimelineItemDto)null);

            var command = new CreateTimelineItemCommand(createDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("map", result.Errors[0].Message, StringComparison.OrdinalIgnoreCase);

            // Verify mapping was attempted
            this.mapperMock.Verify(
                m => m.Map<TimelineItemDto>(It.IsAny<global::Streetcode.DAL.Entities.Timeline.TimelineItem>()),
                Times.Once);
        }
    }
}
