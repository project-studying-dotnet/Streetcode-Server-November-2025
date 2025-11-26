namespace Streetcode.XUnitTest.MediatR.Newss.SortedByDateTime
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.SortedByDateTime;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="SortedByDateTimeQueryHandler"/>.
    /// Tests retrieval of news sorted by date with image handling.
    /// </summary>
    public class SortedByDateTimeHandlerTests
    {
        private const string NoNewsInDatabaseErrorMessage = "There are no news in the database";
        private const string Base64ImageContent = "base64ImageData";

        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IBlobService> blobServiceMock;
        private readonly SortedByDateTimeHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedByDateTimeHandlerTests"/> class.
        /// Creates an instance of <see cref="SortedByDateTimeHandler"/> using the mocked dependencies.
        /// </summary>
        public SortedByDateTimeHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repoMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.blobServiceMock = new Mock<IBlobService>();
            this.handler = new SortedByDateTimeHandler(
                this.repoMock.Object,
                this.mapperMock.Object,
                this.blobServiceMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests the scenario when the repository returns null news collection.
        /// Ensures that the handler returns a failed <see cref="Result{T}"/> with the correct error message,
        /// does not call the mapper, and logs the error.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNewsCollectionIsNull()
        {
            // Arange
            MockRepoHelper.SetupGetAllNews(this.repoMock, null);

            var query = new SortedByDateTimeQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message.Contains(NoNewsInDatabaseErrorMessage));

            // Verify
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, NoNewsInDatabaseErrorMessage);
            MockMapperHelper.VerifyMapCollection<News, NewsDTO>(this.mapperMock, Times.Never());
        }

        /// <summary>
        /// Tests the scenario when the repository returns a non-empty news collection without images.
        /// Ensures that the handler returns a successful <see cref="Result{T}"/> with news sorted by <see cref="NewsDTO.CreationDate"/> descending,
        /// does not call the blob service for image Base64, and calls the mapper exactly once.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnNewsSortedByDateDescending_WhenNewsExistWithoutImages()
        {
            // Arrange
            var newsList = new List<News>
            {
                NewsTestData.CreateNewsWithDate(1, new DateTime(2024, 1, 15)),
                NewsTestData.CreateNewsWithDate(2, new DateTime(2024, 1, 25)),
                NewsTestData.CreateNewsWithDate(3, new DateTime(2024, 1, 10)),
            };

            var newsDTOList = new List<NewsDTO>
            {
                NewsTestData.CreateNewsDTOWithDate(1, new DateTime(2024, 1, 15)),
                NewsTestData.CreateNewsDTOWithDate(2, new DateTime(2024, 1, 25)),
                NewsTestData.CreateNewsDTOWithDate(3, new DateTime(2024, 1, 10)),
            };

            MockRepoHelper.SetupGetAllNews(this.repoMock, newsList);
            MockMapperHelper.SetupMapCollection(this.mapperMock, newsList, newsDTOList);

            var query = new SortedByDateTimeQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(3);
            result.Value.Should().BeInDescendingOrder(n => n.CreationDate);
            result.Value.Select(n => n.Id).Should().Equal(2, 1, 3);

            // Verify
            MockBlobServiceHelper.VerifyNever(this.blobServiceMock);
            MockMapperHelper.VerifyMapCollection<News, NewsDTO>(this.mapperMock, Times.Once());
        }

        /// <summary>
        /// Tests the scenario when the repository returns a non-empty news collection with images.
        /// Ensures that the handler returns a successful <see cref="Result{T}"/> with news sorted by <see cref="NewsDTO.CreationDate"/> descending,
        /// populates the <see cref="ImageDTO.Base64"/> field for each news, and calls the mapper and blob service the correct number of times.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnNewsSortedByDateDescendingWithBase64Images_WhenNewsExistWithImages()
        {
            // Arrange
            var newsList = new List<News>
            {
                NewsTestData.CreateNewsWithDate(1, new DateTime(2024, 2, 10), withImage: true),
                NewsTestData.CreateNewsWithDate(2, new DateTime(2024, 2, 20), withImage: true),
            };

            var newsDTOList = new List<NewsDTO>
            {
                NewsTestData.CreateNewsDTOWithDate(1, new DateTime(2024, 2, 10), withImage: true),
                NewsTestData.CreateNewsDTOWithDate(2, new DateTime(2024, 2, 20), withImage: true),
            };

            MockRepoHelper.SetupGetAllNews(this.repoMock, newsList);
            MockMapperHelper.SetupMapCollection(this.mapperMock, newsList, newsDTOList);
            MockBlobServiceHelper.SetupBlobService(this.blobServiceMock, Base64ImageContent);

            var query = new SortedByDateTimeQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(2);
            result.Value.Should().BeInDescendingOrder(n => n.CreationDate);
            result.Value.Select(n => n.Id).Should().Equal(2, 1);
            result.Value.Should().OnlyContain(n => n.Image != null && n.Image.Base64 == Base64ImageContent);

            // Verify
            MockBlobServiceHelper.VerifyTimes(this.blobServiceMock, 2);
            MockMapperHelper.VerifyMapCollection<News, NewsDTO>(this.mapperMock, Times.Once());
        }
    }
}