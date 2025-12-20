namespace Streetcode.XUnitTest.MediatR.Newss.GetNewsAndLinksByUrl
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.GetNewsAndLinksByUrl;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetNewsAndLinksByUrlHandler"/>.
    /// Tests successful and unsuccessful scenarios of retrieving news by URL,
    /// including previous, next, and random news logic.
    /// </summary>
    public class GetNewsAndLinksByUrlHandlerTests
    {
        private const int NewsId = 1;
        private const string Url = "test-url";
        private const string NewsImageBlobName = "news.jpg";
        private const string Base64Content = "BASE64_STRING";

        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IBlobService> blobServiceMock;
        private readonly GetNewsAndLinksByUrlHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNewsAndLinksByUrlHandlerTests"/> class.
        /// Initializes mocks and <see cref="GetNewsAndLinksByUrlHandler"/> instance.
        /// </summary>
        public GetNewsAndLinksByUrlHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repoMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.blobServiceMock = new Mock<IBlobService>();
            this.handler = new GetNewsAndLinksByUrlHandler(
                this.mapperMock.Object,
                this.repoMock.Object,
                this.blobServiceMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests the scenario when the news is not found by the given URL.
        /// Expects failure result with error message and logger call.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNewsNotFound()
        {
            // Arrange
            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, null);

            // Act
            var result = await this.handler.Handle(new GetNewsAndLinksByUrlQuery(Url), default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == string.Format(ErrorMessages.NewsNotFoundByUrl, Url));

            // Verify
            MockMapperHelper.VerifyMap<News, NewsDto>(this.mapperMock, Times.Once());
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, string.Format(ErrorMessages.NewsNotFoundByUrl, Url));
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
        }

        /// <summary>
        /// Tests successful retrieval of news with image.
        /// Expects Base64 content loaded from <see cref="IBlobService"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnNewsImageBase64_WhenNewsHasImage()
        {
            // Arrange
            var news = NewsTestData.CreateNews(NewsId);
            news.URL = Url;
            news.Image = new Image { BlobName = NewsImageBlobName };

            var newsDto = NewsTestData.CreateNewsDTO(NewsId);
            newsDto.URL = Url;

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, news);
            MockMapperHelper.SetupMapper(this.mapperMock, news, newsDto);
            MockBlobServiceHelper.SetupBlobService(this.blobServiceMock, Base64Content);
            MockRepoHelper.SetupGetAllNews(this.repoMock, new List<News> { news });

            // Act
            var result = await this.handler.Handle(new GetNewsAndLinksByUrlQuery(Url), default);

            // Assert
            result.Value.News.Image?.Base64.Should().Be(Base64Content);

            // Verify
            MockMapperHelper.VerifyMap<News, NewsDto>(this.mapperMock, Times.Once());
            MockBlobServiceHelper.VerifyTimes(this.blobServiceMock, 1);
        }

        /// <summary>
        /// Tests retrieval of the first news in the list.
        /// Ensures previous link is null and next link is present.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnCorrectNextNewsUrl_WhenNewsIsFirst()
        {
            // Arrange
            var allNews = NewsTestData.CreateNewsList(5);
            var newsEntity = allNews[0];
            var newsDTO = NewsTestData.CreateNewsDTO(newsEntity.Id);
            newsDTO.URL = newsEntity.URL;

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, newsEntity);
            MockMapperHelper.SetupMapper(this.mapperMock, newsEntity, newsDTO);
            MockRepoHelper.SetupGetAllNews(this.repoMock, allNews);

            // Act
            var result = await this.handler.Handle(new GetNewsAndLinksByUrlQuery(newsEntity.URL), default);

            // Assert
            result.Value.NextNewsUrl.Should().Be(allNews[1].URL);
        }

        /// <summary>
        /// Tests retrieval of the last news in the list.
        /// Ensures next link is null and previous link is present.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnCorrectPrevNewsUrl_WhenNewsIsLast()
        {
            // Arrange
            var allNews = NewsTestData.CreateNewsList(5);
            var newsEntity = allNews[4];
            var newsDTO = NewsTestData.CreateNewsDTO(newsEntity.Id);
            newsDTO.URL = newsEntity.URL;

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, newsEntity);
            MockMapperHelper.SetupMapper(this.mapperMock, newsEntity, newsDTO);
            MockRepoHelper.SetupGetAllNews(this.repoMock, allNews);

            // Act
            var result = await this.handler.Handle(new GetNewsAndLinksByUrlQuery(newsEntity.URL), default);

            // Assert
            result.Value.PrevNewsUrl.Should().Be(allNews[3].URL);
        }

        /// <summary>
        /// Tests the logic of computing the random news when there are more than three news items.
        /// Ensures the expected random news is returned.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnCorrectRandomNews_WhenMoreThanThreeNews()
        {
            // Arrange
            var allNews = NewsTestData.CreateNewsList(5);
            var newsEntity = allNews[1];
            var newsDTO = NewsTestData.CreateNewsDTO(newsEntity.Id);
            newsDTO.URL = newsEntity.URL;

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, newsEntity);
            MockMapperHelper.SetupMapper(this.mapperMock, newsEntity, newsDTO);
            MockRepoHelper.SetupGetAllNews(this.repoMock, allNews);

            // Act
            var result = await this.handler.Handle(new GetNewsAndLinksByUrlQuery(newsEntity.URL), default);

            // Assert
            result.Value.RandomNews?.RandomNewsUrl.Should().Be(allNews[4].URL);
            result.Value.RandomNews?.Title.Should().Be(allNews[4].Title);
        }
    }
}