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
            const string ERROR_MSG = "There are no news in the database";

            MockRepoHelper.SetupGetAllNews(this.repoMock, null);

            var query = new GetAllNewsQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == ERROR_MSG);

            // Verify
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, ERROR_MSG);
            MockMapperHelper.VerifyMapNever<News, NewsDTO>(this.mapperMock);
            this.blobServiceMock.Verify(b => b.FindFileInStorageAsBase64(It.IsAny<string>()), Times.Never);
        }

        /// <summary>
        /// Tests that handler returns all news with images when news exist.
        /// </summary>
        /// <returns>A task representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnAllNewsWithImages_WhenNewsExist()
        {
            // Arrange
            var newList = NewsTestData.CreateNewsList(3);
            var newsDTOList = NewsTestData.CreateNewsDTOList(3);

            MockRepoHelper.SetupGetAllNews(this.repoMock, newList);
            MockMapperHelper.SetupMapNewsList(this.mapperMock, newList, newsDTOList);
            MockBlobServiceHelper.SetupBlobService(this.blobServiceMock, "base64content");

            var query = new GetAllNewsQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(3);
            result.Value.All(n => n.Image?.Base64 == "base64content").Should().BeTrue();

            // Verify
            this.mapperMock.Verify(m => m.Map<IEnumerable<NewsDTO>>(It.IsAny<IEnumerable<News>>()), Times.Once);
            this.blobServiceMock.Verify(b => b.FindFileInStorageAsBase64(It.IsAny<string>()), Times.Exactly(3));
        }

        /// <summary>
        /// Tests that handler returns all news without images when news exist but images are not present.
        /// </summary>
        /// <returns>A task representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnAllNewsWithoutImages_WhenNewsExist()
        {
            // Arrange
            var newList = NewsTestData.CreateNewsList(3, false);
            var newsDTOList = NewsTestData.CreateNewsDTOList(3, false);

            MockRepoHelper.SetupGetAllNews(this.repoMock, newList);
            MockMapperHelper.SetupMapNewsList(this.mapperMock, newList, newsDTOList);

            var query = new GetAllNewsQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.All(n => n.Image == null).Should().BeTrue();

            // Verify
            this.mapperMock.Verify(m => m.Map<IEnumerable<NewsDTO>>(It.IsAny<IEnumerable<News>>()), Times.Once);
            this.blobServiceMock.Verify(b => b.FindFileInStorageAsBase64(It.IsAny<string>()), Times.Never);
        }
    }
}
