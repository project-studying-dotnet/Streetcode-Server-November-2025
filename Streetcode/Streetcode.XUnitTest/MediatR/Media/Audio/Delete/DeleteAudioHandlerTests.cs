namespace Streetcode.XUnitTest.MediatR.Media.Audio.Delete
{
    using System.Linq.Expressions;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Audio.Delete;
    using Streetcode.DAL.Entities.Media;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class DeleteAudioHandlerTests
    {
        private readonly Mock<IBlobService> mockBlob = new ();
        private readonly Mock<ILoggerService> mockLogger = new ();
        private readonly Mock<IRepositoryWrapper> mockRepo = new ();
        private readonly DeleteAudioHandler mockHandler;

        public DeleteAudioHandlerTests()
        {
            this.mockHandler = new DeleteAudioHandler(
                this.mockRepo.Object,
                this.mockBlob.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenAudioExistsDeleted()
        {
            // Arrange.
            var (audio, targetAudioId) = CreateValidAudioEntity();
            const int saveChangesResult = 1;

            const string logMsg = $"DeleteAudioCommand handled successfully";
            this.SetupMocks(audio, saveChangesResult);

            // Act.
            var result = await this.mockHandler.Handle(new DeleteAudioCommand(targetAudioId), default);

            // Assert.
            result.IsSuccess.Should().BeTrue();
            this.VerifyMockCalledOnce();
            this.VerifyLoggerInformationCalledOnce(logMsg);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResult_WhenAudioDoesNotExistsDeleted()
        {
            // Arrange.
            var (audio, targetAudioId) = CreateNullAudioEntity();
            const int saveChangesResult = 1;
            this.SetupMocks(audio, saveChangesResult);

            // Act.
            var result = await this.mockHandler.Handle(new DeleteAudioCommand(targetAudioId), default);

            // Assert.
            result.IsFailed.Should().BeTrue();
            this.VerifyLoggerErrorCalledOnce(targetAudioId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResult_WhenSaveChangesFails()
        {
            // Arrange.
            var (audio, targetAudioId) = CreateValidAudioEntity();
            const int saveChangesResult = 0;
            const string logMsg = $"Failed to delete an audio";

            this.SetupMocks(audio, saveChangesResult);

            // Act.
            var result = await this.mockHandler.Handle(new DeleteAudioCommand(targetAudioId), default);

            // Assert.
            result.IsFailed.Should().BeTrue();
            this.VerifyLoggerErrorCalledOnce(logMsg);
        }

        private static (Audio Audio, int TargetAudioId) CreateValidAudioEntity()
        {
            const int targetAudioId = 1;
            const string blobName = "validBlobName";
            const string base64 = "base64string";

            var entity = new Audio { Id = targetAudioId, BlobName = blobName, Base64 = base64 };

            return (entity, targetAudioId);
        }

        private static (Audio? Audio, int TargetAudioId) CreateNullAudioEntity()
        {
            const int targetAudioId = 1;

            Audio? entity = null;

            return (entity, targetAudioId);
        }

        private void SetupMocks(Audio? audio, int saveChangesResult)
        {
            this.mockRepo.Setup(r => r.AudioRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Audio, bool>>>(),
                    It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()))
                .ReturnsAsync(audio);

            if (audio != null)
            {
                this.mockRepo.Setup(r => r.AudioRepository.Delete(audio));
                this.mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(saveChangesResult);
                this.mockBlob.Setup(b => b.DeleteFileInStorage(audio.BlobName!));
            }
        }

        private void VerifyMockCalledOnce()
        {
            this.mockRepo.Verify(
                r => r.AudioRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Audio, bool>>>(),
                    It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()),
                Times.Once);

            this.mockRepo.Verify(r => r.AudioRepository.Delete(It.IsAny<Audio>()), Times.Once);

            this.mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);

            this.mockBlob.Verify(b => b.DeleteFileInStorage(It.IsAny<string>()), Times.Once);
        }

        private void VerifyLoggerErrorCalledOnce(int targetAudioId)
        {
            this.mockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    $"Cannot find an audio with corresponding categoryId: {targetAudioId}"),
                Times.Once);
        }

        private void VerifyLoggerErrorCalledOnce(string logMsg)
        {
            this.mockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    logMsg),
                Times.Once);
        }

        private void VerifyLoggerInformationCalledOnce(string logMsg)
        {
            this.mockLogger.Verify(
                logger => logger.LogInformation(
                    logMsg),
                Times.Once);
        }
    }
}
