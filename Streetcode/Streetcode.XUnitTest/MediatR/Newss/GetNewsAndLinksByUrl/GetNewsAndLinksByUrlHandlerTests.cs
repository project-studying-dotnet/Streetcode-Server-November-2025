
namespace Streetcode.XUnitTest.MediatR.Newss.GetNewsAndLinksByUrl
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.GetNewsAndLinksByUrl;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    public class GetNewsAndLinksByUrlHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IBlobService> blobServiceMock;
        private readonly GetNewsAndLinksByUrlHandler handler;

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

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNewsNotFound()
        {
            // Arrange
            const string url = "non-existing-url";
            const string expectedError = $"No news by entered Url - {url}";

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, null);

            // Act
            var result = await this.handler.Handle(new GetNewsAndLinksByUrlQuery(url), default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == expectedError);

            MockMapperHelper.VerifyMapOnce<News, NewsDTO>(this.mapperMock);
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, expectedError);
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNewsFoundWithoutImage()
        {
            // Arrange
            const string url = "news-no-image";
            const int newsId = 1;
            var news = NewsTestData.CreateNews(newsId, imageId: null);
            news.URL = url;
            var newsDto = NewsTestData.CreateNewsDTO(newsId, imageId: null);
            newsDto.URL = url;

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, news);
            MockMapperHelper.SetupMapper(this.mapperMock, news, newsDto);
            MockRepoHelper.SetupGetAllNews(this.repoMock, new List<News> { news });

            var query = new GetNewsAndLinksByUrlQuery(url);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.News.Should().BeEquivalentTo(newsDto);
            result.Value.News.Image.Should().BeNull();
            result.Value.PrevNewsUrl.Should().BeNull();
            result.Value.NextNewsUrl.Should().BeNull();
            result.Value.RandomNews.Should().NotBeNull();

            // Verify
            MockMapperHelper.VerifyMapOnce<News, NewsDTO>(this.mapperMock);
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNewsFoundWithImage()
        {
            // Arrange
            const string Base64Content = "BASE64_STRING";
            const string url = "news-with-image";
            const int newsId = 1;

            var news = NewsTestData.CreateNews(newsId);
            news.URL = url;
            news.Image = new Image { BlobName = "news.jpg" };

            var newsDto = NewsTestData.CreateNewsDTO(newsId);
            newsDto.URL = url;

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, news);
            MockMapperHelper.SetupMapper(this.mapperMock, news, newsDto);
            MockBlobServiceHelper.SetupBlobService(this.blobServiceMock, Base64Content);
            MockRepoHelper.SetupGetAllNews(this.repoMock, new List<News> { news });

            var query = new GetNewsAndLinksByUrlQuery(url);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.News.Should().BeEquivalentTo(newsDto);
            result.Value.News.Image?.Base64.Should().Be(Base64Content);

            // Verify
            MockMapperHelper.VerifyMapOnce<News, NewsDTO>(this.mapperMock);
            MockBlobServiceHelper.VerifyTimes(this.blobServiceMock, 1);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenFirstNews_HasOnlyNextLink()
        {
            // Arrange
            var allNews = NewsTestData.CreateNewsList(5);
            var newsEntity = allNews[0];
            var newsDTO = NewsTestData.CreateNewsDTO(newsEntity.Id);
            newsDTO.URL = newsEntity.URL;

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, newsEntity);
            MockMapperHelper.SetupMapper(this.mapperMock, newsEntity, newsDTO);
            MockRepoHelper.SetupGetAllNews(this.repoMock, allNews);

            var query = new GetNewsAndLinksByUrlQuery(newsEntity.URL);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.PrevNewsUrl.Should().BeNull();
            result.Value.NextNewsUrl.Should().Be(allNews[1].URL);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenLastNews_HasOnlyPrevLink()
        {
            var allNews = NewsTestData.CreateNewsList(5);
            var newsEntity = allNews[4];
            var newsDTO = NewsTestData.CreateNewsDTO(newsEntity.Id);
            newsDTO.URL = newsEntity.URL;

            MockRepoHelper.SetupGetNewsByUrl(this.repoMock, newsEntity);
            MockMapperHelper.SetupMapper(this.mapperMock, newsEntity, newsDTO);
            MockRepoHelper.SetupGetAllNews(this.repoMock, allNews);

            var query = new GetNewsAndLinksByUrlQuery(newsEntity.URL);
            var result = await this.handler.Handle(query, default);

            result.IsSuccess.Should().BeTrue();
            result.Value.PrevNewsUrl.Should().Be(allNews[3].URL);
            result.Value.NextNewsUrl.Should().BeNull();
        }

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

            var query = new GetNewsAndLinksByUrlQuery(newsEntity.URL);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.RandomNews?.RandomNewsUrl.Should().Be(allNews[4].URL);
            result.Value.RandomNews?.Title.Should().Be(allNews[4].Title);
        }
    }
}
