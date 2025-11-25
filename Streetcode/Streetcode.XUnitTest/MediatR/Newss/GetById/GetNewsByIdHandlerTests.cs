namespace Streetcode.XUnitTest.MediatR.Newss.GetById
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.GetById;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    public class GetNewsByIdHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IBlobService> blobServiceMock;
        private readonly GetNewsByIdHandler handler;

        public GetNewsByIdHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repoMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.blobServiceMock = new Mock<IBlobService>();
            this.handler = new GetNewsByIdHandler(
                this.mapperMock.Object,
                this.repoMock.Object,
                this.blobServiceMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNewsNotFound()
        {
            // Arrange
            const int id = 1;
            const string expectedErrorMessage = "No news by entered Id - 1";

            MockRepoHelper.SetupGetNewsById(this.repoMock, null);

            var query = new GetNewsByIdQuery(id);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == expectedErrorMessage);

            // Verify
            MockMapperHelper.VerifyMapOnce<News, NewsDTO>(this.mapperMock);
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, expectedErrorMessage);
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNewsFoundWithoutImage()
        {
            // Arrange
            const int id = 2;

            var news = NewsTestData.CreateNews(id);
            var newsDto = NewsTestData.CreateNewsDTO(id, imageId: null);

            MockRepoHelper.SetupGetNewsById(this.repoMock, news);
            MockMapperHelper.SetupMapper<News, NewsDTO>(this.mapperMock, news, newsDto);

            var query = new GetNewsByIdQuery(id);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(newsDto);
            result.Value.Image.Should().BeNull();

            // Verify
            MockMapperHelper.VerifyMapOnce<News, NewsDTO>(this.mapperMock);
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNewsFoundWithImage()
        {
            const int newsId = 3;
            const string Base64Content = "BASE64_STRING";

            var news = NewsTestData.CreateNews(newsId);
            var newsDto = NewsTestData.CreateNewsDTO(newsId);

            MockRepoHelper.SetupGetNewsById(this.repoMock, news);
            MockMapperHelper.SetupMapper(this.mapperMock, news, newsDto);
            MockBlobServiceHelper.SetupBlobService(this.blobServiceMock, Base64Content);

            var query = new GetNewsByIdQuery(newsId);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(newsDto);
            result.Value.Image?.Base64.Should().Be(Base64Content);

            // Verify
            MockMapperHelper.VerifyMapOnce<News, NewsDTO>(this.mapperMock);
            MockBlobServiceHelper.VerifyTimes(this.blobServiceMock, 1);
        }
    }
}
