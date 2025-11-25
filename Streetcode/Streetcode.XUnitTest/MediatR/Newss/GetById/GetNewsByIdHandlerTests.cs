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

    /// <summary>
    /// Unit tests for <see cref="GetNewsByIdHandler"/>.
    /// </summary>
    public class GetNewsByIdHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IBlobService> blobServiceMock;
        private readonly GetNewsByIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetNewsByIdHandlerTests"/> class.
        /// </summary>
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

        /// <summary>
        /// Tests that <see cref="GetNewsByIdHandler.Handle(GetNewsByIdQuery, CancellationToken)"/>
        /// returns a failure result when no news is found for the specified Id.
        /// </summary>
        /// <returns>A failed <see cref="Result{NewsDTO}"/> with an error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNewsNotFound()
        {
            // Arrange
            const int NEWS_ID = 1;
            const string expectedErrorMessage = "No news by entered Id - 1";

            MockRepoHelper.SetupGetNewsById(this.repoMock, null);

            var query = new GetNewsByIdQuery(NEWS_ID);

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

        /// <summary>
        /// Tests that <see cref="GetNewsByIdHandler.Handle(GetNewsByIdQuery, CancellationToken)"/> 
        /// returns a success result when news is found but has no associated image.
        /// </summary>
        /// <returns>A successful <see cref="Result{NewsDTO}"/> with <c>null</c> image.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNewsFoundWithoutImage()
        {
            // Arrange
            const int NEWS_ID = 2;

            var news = NewsTestData.CreateNews(NEWS_ID);
            var newsDto = NewsTestData.CreateNewsDTO(NEWS_ID, imageId: null);

            MockRepoHelper.SetupGetNewsById(this.repoMock, news);
            MockMapperHelper.SetupMapper<News, NewsDTO>(this.mapperMock, news, newsDto);

            var query = new GetNewsByIdQuery(NEWS_ID);

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

        /// <summary>
        /// Tests that <see cref="GetNewsByIdHandler.Handle(GetNewsByIdQuery, CancellationToken)"/> 
        /// returns a success result when news is found and has an associated image.
        /// Ensures that the Base64 content is populated via <see cref="IBlobService"/>.
        /// </summary>
        /// <returns>A successful <see cref="Result{NewsDTO}"/> with populated <see cref="ImageDTO.Base64"/>.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenNewsFoundWithImage()
        {
            const int NEWS_ID = 3;
            const string Base64Content = "BASE64_STRING";

            var news = NewsTestData.CreateNews(NEWS_ID);
            var newsDto = NewsTestData.CreateNewsDTO(NEWS_ID);

            MockRepoHelper.SetupGetNewsById(this.repoMock, news);
            MockMapperHelper.SetupMapper(this.mapperMock, news, newsDto);
            MockBlobServiceHelper.SetupBlobService(this.blobServiceMock, Base64Content);

            var query = new GetNewsByIdQuery(NEWS_ID);

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
