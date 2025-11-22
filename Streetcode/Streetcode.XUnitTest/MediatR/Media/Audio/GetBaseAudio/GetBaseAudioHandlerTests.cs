namespace Streetcode.XUnitTest.MediatR.Media.Audio.GetBaseAudio
{
    using System.Linq.Expressions;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Audio.GetBaseAudio;
    using Streetcode.DAL.Entities.Media;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetBaseAudioHandlerTests
    {
        private readonly Mock<IBlobService> mockBlob;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly Mock<IRepositoryWrapper> mockRepo;

        private readonly GetBaseAudioHandler handler;

        public GetBaseAudioHandlerTests()
        {
            this.mockBlob = new Mock<IBlobService>();
            this.mockLogger = new Mock<ILoggerService>();
            this.mockRepo = new Mock<IRepositoryWrapper>();

            this.handler = new GetBaseAudioHandler(
                this.mockBlob.Object,
                this.mockRepo.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenAudioExists()
        {
            // Arrange.
            var (entity, expectedStream, targetAudioId) = CreateValidAudioEntityAndStream();

            this.SetupMocks(entity, expectedStream);

            // Act.
            var result = await this.handler.Handle(new GetBaseAudioQuery(targetAudioId), default);

            // Assert.
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeSameAs(expectedStream);

            this.VerifyMocksCalledOnce(entity.BlobName);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResult_WhenAudioDoesNotExist()
        {
            // Arrange.
            var (entity, expectedStream, targetAudioId) = CreateNullAudioEntity();

            this.SetupMocks(entity, expectedStream);

            // Act.
            var result = await this.handler.Handle(new GetBaseAudioQuery(targetAudioId), default);

            // Assert.
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().Contain(e => e.Message == $"Cannot find an audio with corresponding id: {targetAudioId}");

            this.VerifyLoggerCalledOnce(targetAudioId);
        }

        private static (Audio Audio, MemoryStream MemoryStream, int TargetAudioId) CreateValidAudioEntityAndStream()
        {
            const int targetAudioId = 1;
            string blobName = "validBlobName";

            var entity = new Audio { Id = targetAudioId, BlobName = blobName };
            var stream = new MemoryStream(new byte[] { 1, 2, 3 });

            return (entity, stream, targetAudioId);
        }

        private static (Audio? Audio, MemoryStream? MemoryStream, int TargetAudioId) CreateNullAudioEntity()
        {
            const int targetAudioId = 1;

            Audio? entity = null;
            MemoryStream? stream = null;

            return (entity, stream, targetAudioId);
        }

        private void SetupMocks(Audio? entity, MemoryStream? stream)
        {
            this.mockRepo
                .Setup(r => r.AudioRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Audio, bool>>>(),
                    It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()))
                .ReturnsAsync(entity);

            if (entity != null)
            {
                this.mockBlob
                    .Setup(b => b.FindFileInStorageAsMemoryStream(entity.BlobName!))
                    .Returns(stream);
            }
        }

        private void VerifyMocksCalledOnce(string? blobName)
        {
            this.mockRepo.Verify(
            r => r.AudioRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Audio, bool>>>(),
                It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()),
            Times.Once);

            this.mockBlob.Verify(
                b => b.FindFileInStorageAsMemoryStream(blobName!),
                Times.Once);
        }

        private void VerifyLoggerCalledOnce(int targetAudioId)
        {
            this.mockLogger.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    $"Cannot find an audio with corresponding id: {targetAudioId}"),
                Times.Once);
        }
    }
}
