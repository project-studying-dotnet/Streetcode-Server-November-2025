namespace Streetcode.XUnitTest.MediatR.Newss.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.GetAll;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetAllNewsHandler"/>.
    /// Verifies correct behavior when retrieving all news items from the repository,
    /// including scenarios with and without images.
    /// </summary>
    public class GetAllNewsHandlerTests
    {
        private const string Base64Content = "base64content";
        private const string NoNewsInDatabaseErrorMessage = "There are no news in the database";
        private const int NewsCount = 3;

        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IBlobService> blobServiceMock;
        private readonly GetAllNewsHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllNewsHandlerTests"/> class.
        /// Initializes mocks and the <see cref="GetAllNewsHandler"/> instance.
        /// </summary>
        public GetAllNewsHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repoMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.blobServiceMock = new Mock<IBlobService>();
            this.handler = new GetAllNewsHandler(
                this.repoMock.Object,
                this.mapperMock.Object,
                this.blobServiceMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that handler returns failure when no news exist in the repository.
        /// </summary>
        /// <returns>A task representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNoNewsFound()
        {
            // Arrange
            MockRepoHelper.SetupGetAllNews(this.repoMock, null);

            var query = new GetAllNewsQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == NoNewsInDatabaseErrorMessage);

            // Verify
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, NoNewsInDatabaseErrorMessage);
            MockMapperHelper.VerifyMapCollection<News, NewsDTO>(this.mapperMock, Times.Never());
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
        }

        /// <summary>
        /// Tests that handler returns all news with images when news exist.
        /// </summary>
        /// <returns>A task representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnAllNewsWithImages_WhenNewsExist()
        {
            // Arrange
            var newList = NewsTestData.CreateNewsList(NewsCount, withImages: true);
            var newsDTOList = NewsTestData.CreateNewsDTOList(NewsCount, withImages: true);

            MockRepoHelper.SetupGetAllNews(this.repoMock, newList);
            MockMapperHelper.SetupMapCollection(this.mapperMock, newList, newsDTOList);
            MockBlobServiceHelper.SetupBlobService(this.blobServiceMock, Base64Content);

            var query = new GetAllNewsQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(NewsCount);
            result.Value.All(n => n.Image?.Base64 == Base64Content).Should().BeTrue();

            // Verify
            MockMapperHelper.VerifyMapCollection<News, NewsDTO>(this.mapperMock, Times.Once());
            MockBlobServiceHelper.VerifyTimes(this.blobServiceMock, NewsCount);
        }

        /// <summary>
        /// Tests that handler returns all news without images when news exist but images are not present.
        /// </summary>
        /// <returns>A task representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnAllNewsWithoutImages_WhenNewsExist()
        {
            // Arrange
            var newList = NewsTestData.CreateNewsList(NewsCount, false);
            var newsDTOList = NewsTestData.CreateNewsDTOList(NewsCount, false);

            MockRepoHelper.SetupGetAllNews(this.repoMock, newList);
            MockMapperHelper.SetupMapCollection(this.mapperMock, newList, newsDTOList);

            var query = new GetAllNewsQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.All(n => n.Image == null).Should().BeTrue();

            // Verify
            MockMapperHelper.VerifyMapCollection<News, NewsDTO>(this.mapperMock, Times.Once());
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
        }
    }
}