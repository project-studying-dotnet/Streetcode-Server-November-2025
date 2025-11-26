namespace Streetcode.XUnitTest.MediatR.Newss.GetByUrl
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.GetByUrl;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    /// <summary>
    /// Verifies behavior for finding news by URL, including success and failure scenarios.
    /// </summary>
    public class GetNewsByUrlHandlerTests
    {
        private const int NewsId = 1;
        private const string Url = "test-url";
        private const string BlobName = "news-image.jpg";
        private const string Base64Content = "BASE64_STRING";
        private const string NewsNotFoundByUrlErrorMessageTemplate = $"No news by entered Url - {Url}";

        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IBlobService> blobServiceMock;
        private readonly GetNewsByUrlHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNewsByUrlHandlerTests"/> class.
        /// Initializes mocks and the <see cref="GetNewsByUrlHandler"/> instance.
        /// </summary>
        public GetNewsByUrlHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repoMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.blobServiceMock = new Mock<IBlobService>();
            this.handler = new GetNewsByUrlHandler(
                this.mapperMock.Object,
                this.repoMock.Object,
                this.blobServiceMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that <see cref="GetNewsByUrlHandler.Handle(GetNewsByUrlQuery, CancellationToken)"/>
        /// returns a failure result when no news exists for the given URL.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNewsNotFound()
        {
            // Arrange
            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, null!);

            var query = new GetNewsByUrlQuery(Url);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == NewsNotFoundByUrlErrorMessageTemplate);

            // Verify
            MockMapperHelper.VerifyMap<News, NewsDTO>(this.mapperMock, Times.Once());
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, NewsNotFoundByUrlErrorMessageTemplate);
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
        }

        /// <summary>
        /// Tests that <see cref="GetNewsByUrlHandler.Handle(GetNewsByUrlQuery, CancellationToken)"/>
        /// returns a success result when news exists without an associated image.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNewsFoundWithoutImage()
        {
            // Arrange
            var news = NewsTestData.CreateNews(1, imageId: null);
            news.URL = Url;
            var newsDto = NewsTestData.CreateNewsDTO(1, imageId: null);
            newsDto.URL = Url;

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, news);
            MockMapperHelper.SetupMapper(this.mapperMock, news, newsDto);

            var query = new GetNewsByUrlQuery(Url);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(newsDto);
            result.Value.Image.Should().BeNull();

            // Verify
            MockMapperHelper.VerifyMap<News, NewsDTO>(this.mapperMock, Times.Once());
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
        }

        /// <summary>
        /// Tests that <see cref="GetNewsByUrlHandler.Handle(GetNewsByUrlQuery, CancellationToken)"/>
        /// returns a success result when news exists with an associated image.
        /// Ensures that <see cref="IBlobService.FindFileInStorageAsBase64(string)"/> is called
        /// to populate the <see cref="ImageDTO.Base64"/> property.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNewsFoundWithImage()
        {
            // Arrange
            var news = NewsTestData.CreateNews(NewsId);
            news.URL = Url;
            news.Image = new Image { BlobName = BlobName };

            var newsDto = NewsTestData.CreateNewsDTO(NewsId);
            newsDto.URL = Url;

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, news);
            MockMapperHelper.SetupMapper(this.mapperMock, news, newsDto);
            MockBlobServiceHelper.SetupBlobService(this.blobServiceMock, Base64Content);

            var query = new GetNewsByUrlQuery(Url);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(newsDto);
            result.Value.Image?.Base64.Should().Be(Base64Content);

            // Verify
            MockMapperHelper.VerifyMap<News, NewsDTO>(this.mapperMock, Times.Once());
            MockBlobServiceHelper.VerifyTimes(this.blobServiceMock, 1);
        }
    }
}