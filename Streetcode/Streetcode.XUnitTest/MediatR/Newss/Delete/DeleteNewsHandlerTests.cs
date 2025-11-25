namespace Streetcode.XUnitTest.MediatR.Newss.Delete
{
    using FluentAssertions;
    using global::MediatR;
    using Moq;
    using Repositories.Interfaces;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.Delete;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Newss;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    public class DeleteNewsHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly DeleteNewsHandler handler;

        public DeleteNewsHandlerTests()
        {
            this.repoMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new DeleteNewsHandler(this.repoMock.Object, this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenNewsNotFound()
        {
            // Arrange
            const int NEWS_ID = 1;
            const string ERROR_MSG = $"No news found by entered Id - 1";

            MockRepoHelper.SetupGetNewsById(this.repoMock, null);

            var command = new DeleteNewsCommand(NEWS_ID);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors[0].Message.Should().Contain(ERROR_MSG);

            // Verify
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, ERROR_MSG);
            this.repoMock.Verify(r => r.NewsRepository.Delete(It.IsAny<News>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenNewsExistsWithoutImageAndDeletedSuccessfully()
        {
            // Arrange
            const int NEWS_ID = 1;
            var news = NewsTestData.CreateNews(NEWS_ID, withImage: false);

            MockRepoHelper.SetupGetNewsById(this.repoMock, news);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);

            var command = new DeleteNewsCommand(NEWS_ID);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Unit.Value);

            // Verify
            this.repoMock.Verify(r => r.NewsRepository.Delete(news), Times.Once);
            this.repoMock.Verify(r => r.ImageRepository.Delete(It.IsAny<Image>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenNewsExistsWithImageAndDeletedSuccessfully()
        {
            // Arrange
            const int NEWS_ID = 1;

            var news = NewsTestData.CreateNews(NEWS_ID, withImage: true);

            var newsRepoMock = new Mock<INewsRepository>();
            var imageRepoMock = new Mock<IImageRepository>();

            this.repoMock.Setup(r => r.NewsRepository).Returns(newsRepoMock.Object);
            this.repoMock.Setup(r => r.ImageRepository).Returns(imageRepoMock.Object);

            MockRepoHelper.SetupGetNewsById(this.repoMock, news);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);
            repoMock.Setup(r => r.ImageRepository).Returns(imageRepoMock.Object);

            var command = new DeleteNewsCommand(NEWS_ID);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(Unit.Value);

            // Verify
            this.repoMock.Verify(r => r.ImageRepository.Delete(news.Image), Times.Once);
            this.repoMock.Verify(r => r.NewsRepository.Delete(news), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSaveChangesFails()
        {
            // Arrange
            const string ERROR_MSG = "Failed to delete news";
            const int NEWS_ID = 1;
            var news = NewsTestData.CreateNews(NEWS_ID, imageId: null);

            MockRepoHelper.SetupGetNewsById(this.repoMock, news);
            MockRepoHelper.SetupSaveFail(this.repoMock);

            var command = new DeleteNewsCommand(NEWS_ID);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors[0].Message.Should().Be(ERROR_MSG);

            // Verify
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, ERROR_MSG);
        }
    }
}
