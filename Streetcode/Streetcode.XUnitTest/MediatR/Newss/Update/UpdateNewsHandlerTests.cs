namespace Streetcode.XUnitTest.MediatR.Newss.Update
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Media.Images;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.Update;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="UpdateNewsHandler"/>.
    /// Covers failure and success scenarios for updating news,
    /// including image handling, old image deletion, and SaveChanges behavior.
    /// </summary>
    public class UpdateNewsHandlerTests
    {
        private const string MappingNullErrorMessage = "Cannot convert null to news";
        private const string SaveFailErrorMessage = "Failed to update news";
        private const int NewsId = 1;
        private const string Base64Data = "BASE64_DATA";
        private const string BlobNameFile = "file.jpg";
        private const string OldBlobName = "old.jpg";

        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IBlobService> blobServiceMock;
        private readonly UpdateNewsHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateNewsHandlerTests"/> class.
        /// Initializes mocks and <see cref="UpdateNewsHandler"/> instance.
        /// </summary>
        public UpdateNewsHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repoMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.blobServiceMock = new Mock<IBlobService>();
            this.handler = new UpdateNewsHandler(
                this.repoMock.Object,
                this.mapperMock.Object,
                this.blobServiceMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests the scenario when mapping from <see cref="NewsDto"/> to <see cref="News"/> returns null.
        /// Ensures that the handler returns a failed result with an appropriate error message.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenMappingReturnsNull()
        {
            // Arrange
            var newsDto = NewsTestData.CreateNewsDTO(NewsId);

            MockMapperHelper.SetupMapper<NewsDto, News>(this.mapperMock, newsDto, null);

            var command = new UpdateNewsCommand(newsDto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message.Contains(MappingNullErrorMessage));

            // Verify
            MockMapperHelper.VerifyMap<NewsDto, News>(this.mapperMock, Times.Once());
            MockLoggerHelper.VerifyLogErrorOnce(this.loggerMock);
        }

        /// <summary>
        /// Tests the scenario when <see cref="IRepositoryWrapper.SaveChangesAsync"/> fails.
        /// Ensures that the handler returns a failed result with the message "Failed to update news".
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSaveChangesFails()
        {
            // Arrange
            var dto = NewsTestData.CreateNewsDTO(NewsId);
            var entity = NewsTestData.CreateNews(NewsId);

            MockMapperHelper.SetupMapper<NewsDto, News>(this.mapperMock, dto, entity);
            MockMapperHelper.SetupMapper<News, NewsDto>(this.mapperMock, entity, dto);

            MockRepoHelper.SetupUpdate(this.repoMock);

            MockRepoHelper.SetupSaveFail(this.repoMock);

            var command = new UpdateNewsCommand(dto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message.Contains(SaveFailErrorMessage));

            // Verify
            MockRepoHelper.VerifyNewsUpdateOnce(this.repoMock);
            MockRepoHelper.VerifySaveChangesOnce(this.repoMock);
            MockLoggerHelper.VerifyLogErrorOnce(this.loggerMock);
        }

        /// <summary>
        /// Tests successful update when the news has an image.
        /// Ensures that Base64 is loaded from <see cref="IBlobService"/>,
        /// the news is updated in the repository, and SaveChanges is called.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNewsHasImageAndLoadsBase64()
        {
            // Arrange
            var dto = NewsTestData.CreateNewsDTO(NewsId);
            var entity = NewsTestData.CreateNews(NewsId);

            entity.Image = new Image { BlobName = BlobNameFile };
            dto.Image = new ImageDtoo { BlobName = BlobNameFile };

            MockMapperHelper.SetupMapper<NewsDto, News>(this.mapperMock, dto, entity);
            MockMapperHelper.SetupMapper<News, NewsDto>(this.mapperMock, entity, dto);

            MockBlobServiceHelper.SetupBlobService(this.blobServiceMock, Base64Data);

            MockRepoHelper.SetupUpdate(this.repoMock);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);

            var command = new UpdateNewsCommand(dto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dto);

            result.Value.Image.Should().NotBeNull();
            result.Value.Image!.Base64.Should().Be(Base64Data);

            // Verify
            MockBlobServiceHelper.VerifyTimes(this.blobServiceMock, 1);
            MockRepoHelper.VerifySaveChangesOnce(this.repoMock);
            MockRepoHelper.VerifyNewsUpdateOnce(this.repoMock, entity.Id);
        }

        /// <summary>
        /// Tests successful update when the news has no image but an old image exists.
        /// Ensures that the old image is deleted, the news is updated, and SaveChanges is called.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldDeleteOldImage_WhenNewsHasNoImageButOldImageExists()
        {
            // Arrange
            var dto = NewsTestData.CreateNewsDTO(NewsId);
            var entity = NewsTestData.CreateNews(NewsId);
            entity.Image = null;
            dto.ImageId = 1;

            var oldImage = new Image { Id = dto.ImageId.Value, BlobName = OldBlobName };

            MockMapperHelper.SetupMapper<NewsDto, News>(this.mapperMock, dto, entity);
            MockMapperHelper.SetupMapper<News, NewsDto>(this.mapperMock, entity, dto);

            MockRepoHelper.SetupGetImageById(this.repoMock, oldImage);

            MockRepoHelper.SetupUpdate(this.repoMock);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);

            var command = new UpdateNewsCommand(dto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dto);

            // Verify
            MockRepoHelper.VerifyNewsUpdateOnce(this.repoMock, entity.Id);
            MockRepoHelper.VerifyDelete<Image>(this.repoMock, Times.Once());
            MockRepoHelper.VerifySaveChangesOnce(this.repoMock);
        }

        /// <summary>
        /// Tests successful update when the news has no image and no old image exists.
        /// Ensures that no image deletion occurs, the news is updated, and SaveChanges is called.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNewsHasNoImageAndNoOldImageExists()
        {
            // Arrange
            var dto = NewsTestData.CreateNewsDTO(NewsId);
            var entity = NewsTestData.CreateNews(NewsId);

            entity.Image = null;
            dto.ImageId = null;

            MockMapperHelper.SetupMapper<NewsDto, News>(this.mapperMock, dto, entity);
            MockMapperHelper.SetupMapper<News, NewsDto>(this.mapperMock, entity, dto);
            MockRepoHelper.SetupGetImageById(this.repoMock, null);
            MockRepoHelper.SetupUpdate(this.repoMock);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);

            var command = new UpdateNewsCommand(dto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dto);

            // Verify
            MockRepoHelper.VerifyDelete<Image>(this.repoMock, Times.Never());
            MockRepoHelper.VerifyNewsUpdateOnce(this.repoMock, entity.Id);
            MockRepoHelper.VerifySaveChangesOnce(this.repoMock);
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
        }
    }
}