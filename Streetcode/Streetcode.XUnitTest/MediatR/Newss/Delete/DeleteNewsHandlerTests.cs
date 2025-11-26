namespace Streetcode.XUnitTest.MediatR.Newss.Delete
{
    using FluentAssertions;
    using global::MediatR;
    using Moq;
    using Repositories.Interfaces;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.Delete;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Newss;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="DeleteNewsHandler"/>.
    /// Covers success and failure scenarios of deleting news,
    /// including handling of images, SaveChangesAsync behavior, and logging.
    /// </summary>
    public class DeleteNewsHandlerTests
    {
        private const int NewsId = 1;
        private const string NewsNotFoundErrorMessage = "No news found by entered Id - 1";
        private const string SaveErrorMessage = "Failed to delete news";

        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly DeleteNewsHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteNewsHandlerTests"/> class.
        /// Initializes mocks and the <see cref="DeleteNewsHandler"/> instance.
        /// </summary>
        public DeleteNewsHandlerTests()
        {
            this.repoMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new DeleteNewsHandler(this.repoMock.Object, this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns a failed result when the news item is not found in the repository.
        /// Ensures proper error logging and that Delete is never called.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNewsNotFound()
        {
            // Arrange
            MockRepoHelper.SetupGetNewsById(this.repoMock, null);

            var command = new DeleteNewsCommand(NewsId);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors[0].Message.Should().Contain(NewsNotFoundErrorMessage);

            // Verify
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, NewsNotFoundErrorMessage);
            MockRepoHelper.VerifyDelete<News>(this.repoMock, Times.Never());
        }

        /// <summary>
        /// Tests that a news item without an image is successfully deleted.
        /// Ensures that the news repository Delete method is called once,
        /// no image deletion occurs, and SaveChangesAsync is called.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnOk_WhenNewsExistsWithoutImageAndDeletedSuccessfully()
        {
            // Arrange
            var news = NewsTestData.CreateNews(NewsId, withImage: false);

            MockRepoHelper.SetupGetNewsById(this.repoMock, news);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);

            var command = new DeleteNewsCommand(NewsId);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Unit.Value);

            // Verify
            MockRepoHelper.VerifyDelete<News>(this.repoMock, Times.Once());
            MockRepoHelper.VerifyDelete<Image>(this.repoMock, Times.Never());
        }

        /// <summary>
        /// Tests that a news item with an image is successfully deleted.
        /// Ensures that both the news and associated image are deleted from the repository,
        /// and that SaveChangesAsync is called once.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnOk_WhenNewsExistsWithImageAndDeletedSuccessfully()
        {
            // Arrange
            var news = NewsTestData.CreateNews(NewsId, withImage: true);

            var newsRepoMock = new Mock<INewsRepository>();
            var imageRepoMock = new Mock<IImageRepository>();

            this.repoMock.Setup(r => r.NewsRepository).Returns(newsRepoMock.Object);
            this.repoMock.Setup(r => r.ImageRepository).Returns(imageRepoMock.Object);

            MockRepoHelper.SetupGetNewsById(this.repoMock, news);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);
            this.repoMock.Setup(r => r.ImageRepository).Returns(imageRepoMock.Object);

            var command = new DeleteNewsCommand(NewsId);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Unit.Value);

            // Verify
            MockRepoHelper.VerifyDelete<Image>(this.repoMock, Times.Once());
            MockRepoHelper.VerifyDelete<News>(this.repoMock, Times.Once());
        }

        /// <summary>
        /// Tests that the handler returns a failed result when SaveChangesAsync fails after attempting deletion.
        /// Ensures that proper error logging occurs.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSaveChangesFails()
        {
            // Arrange
            var news = NewsTestData.CreateNews(NewsId, imageId: null);

            MockRepoHelper.SetupGetNewsById(this.repoMock, news);
            MockRepoHelper.SetupSaveFail(this.repoMock);

            var command = new DeleteNewsCommand(NewsId);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors[0].Message.Should().Be(SaveErrorMessage);

            // Verify
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, SaveErrorMessage);
        }
    }
}