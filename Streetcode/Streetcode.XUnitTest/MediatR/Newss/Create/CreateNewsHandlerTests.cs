namespace Streetcode.XUnitTest.MediatR.Newss.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.Create;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="CreateNewsHandler"/>.
    /// Covers different scenarios for creating news: successful creation, mapper failure, save failure, and ImageId handling.
    /// </summary>
    public class CreateNewsHandlerTests
    {
        private const string ErrorMsgSave = "Failed to create a news";
        private const string ErrorMsgMapper = "Cannot convert null to news";

        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly CreateNewsHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNewsHandlerTests"/> class,
        /// setting up mocks and the handler instance.
        /// </summary>
        public CreateNewsHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repoMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new CreateNewsHandler(
                this.mapperMock.Object,
                this.repoMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns success when valid data is provided.
        /// </summary>
        /// <returns>A successful <see cref="Result{NewsDTO}"/>.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenDataValid()
        {
            // Arrange
            var dto = NewsTestData.CreateNewsDTO();
            var entity = NewsTestData.CreateNews();

            MockMapperHelper.SetupMapper(this.mapperMock, dto, entity);
            MockRepoHelper.SetupNewsCreate(this.repoMock, entity);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);
            MockMapperHelper.SetupMapper(this.mapperMock, entity, dto);

            var command = new CreateNewsCommand(dto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dto);

            // Verify
            MockMapperHelper.VerifyMap<NewsDTO, News>(this.mapperMock, Times.Once());
            MockRepoHelper.VerifyNewsCreateOnce(this.repoMock);
            MockMapperHelper.VerifyMap<News, NewsDTO>(this.mapperMock, Times.Once());
        }

        /// <summary>
        /// Tests that ImageId is set to null when the incoming DTO has ImageId = 0.
        /// </summary>
        /// <returns>A successful <see cref="Result{NewsDTO}"/> with ImageId set to null.</returns>
        [Fact]
        public async Task Handle_ShouldSetImageIdToNull_WhenImageIdIsZero()
        {
            // Arrange
            var dto = NewsTestData.CreateNewsDTO(imageId: 0);
            var entity = NewsTestData.CreateNews(imageId: 0);

            MockMapperHelper.SetupMapper(this.mapperMock, dto, entity);
            MockRepoHelper.SetupNewsCreate(this.repoMock, entity);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);
            MockMapperHelper.SetupMapper(this.mapperMock, entity, dto);

            var command = new CreateNewsCommand(dto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            entity.ImageId.Should().BeNull();

            // Verify
            MockMapperHelper.VerifyMap<NewsDTO, News>(this.mapperMock, Times.Once());
            MockRepoHelper.VerifyNewsCreateOnce(this.repoMock);
            MockMapperHelper.VerifyMap<News, NewsDTO>(this.mapperMock, Times.Once());
        }

        /// <summary>
        /// Tests that the handler returns failure when the mapper returns null.
        /// </summary>
        /// <returns>A failed <see cref="Result{NewsDTO}"/> with an appropriate error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenMapperReturnsNull()
        {
            // Arrange
            var dto = NewsTestData.CreateNewsDTO();

            MockMapperHelper.SetupMapper(this.mapperMock, dto, (News)null);

            var command = new CreateNewsCommand(dto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == ErrorMsgMapper);

            // Verify
            MockMapperHelper.VerifyMap<NewsDTO, News>(this.mapperMock, Times.Once());
            MockRepoHelper.VerifyNewsCreateNever(this.repoMock);
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, ErrorMsgMapper);
        }

        /// <summary>
        /// Tests that the handler returns failure when saving changes to the repository fails.
        /// </summary>
        /// <returns>A failed <see cref="Result{NewsDTO}"/> with an appropriate error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSaveFails()
        {
            // Arrange
            var dto = NewsTestData.CreateNewsDTO();
            var entity = NewsTestData.CreateNews();

            MockMapperHelper.SetupMapper(this.mapperMock, dto, entity);
            MockRepoHelper.SetupNewsCreate(this.repoMock, entity);
            MockRepoHelper.SetupSaveFail(this.repoMock);

            var command = new CreateNewsCommand(dto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == ErrorMsgSave);

            // Verify
            MockMapperHelper.VerifyMap<NewsDTO, News>(this.mapperMock, Times.Once());
            MockRepoHelper.VerifyNewsCreateOnce(this.repoMock);
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, ErrorMsgSave);
        }
    }
}