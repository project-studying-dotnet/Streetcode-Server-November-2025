namespace Streetcode.XUnitTest.MediatR.Media.Audio.GetById
{
    using System.Linq.Expressions;
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Media.Audio;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Audio.GetById;
    using Streetcode.DAL.Entities.Media;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetAudioByIdHandlerTests
    {
        private readonly Mock<IBlobService> mockBlob;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly Mock<IRepositoryWrapper> mockRepo;
        private readonly Mock<IMapper> mockMapper;

        private readonly GetAudioByIdHandler handler;

        public GetAudioByIdHandlerTests()
        {
            this.mockBlob = new Mock<IBlobService>();
            this.mockLogger = new Mock<ILoggerService>();
            this.mockRepo = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();

            this.handler = new GetAudioByIdHandler(
                this.mockRepo.Object,
                this.mockMapper.Object,
                this.mockBlob.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenAudioExists()
        {
            // Arrange.
            var (entity, dto, targetAudioId) = CreateValidAudioEntityAndDTO();
            this.SetupMocks(entity, dto);

            // Act.
            var result = await this.handler.Handle(new GetAudioByIdQuery(targetAudioId), default);

            // Assert.
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dto);
            result.Value.Base64.Should().Be(dto.Base64);

            this.VerifyMocksCalledOnce(entity.BlobName);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResult_WhenAudioDoesNotExist()
        {
            // Arrange.
            var (entity, dto, targetAudioId) = CreateNullAudioEntity();
            this.SetupMocks(entity, dto);

            // Act.
            var result = await this.handler.Handle(new GetAudioByIdQuery(targetAudioId), default);

            // Assert.
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().Contain(e => e.Message == $"Cannot find an audio with corresponding id: {targetAudioId}");
            this.VerifyLoggerCalledOnce(targetAudioId);
        }

        private static (Audio Audio, AudioDTO AudioDTO, int TargetAudioId) CreateValidAudioEntityAndDTO()
        {
            const int targetAudioId = 1;
            const string blobName = "validBlobName";
            const string base64 = "base64string";

            var entity = new Audio { Id = targetAudioId, BlobName = blobName, Base64 = base64 };
            var audioDTO = new AudioDTO { Id = targetAudioId, BlobName = blobName, Base64 = base64 };

            return (entity, audioDTO, targetAudioId);
        }

        private static (Audio? Audio, AudioDTO? AudioDTO, int TargetAudioId) CreateNullAudioEntity()
        {
            const int targetAudioId = 1;

            Audio? entity = null;
            AudioDTO? mappedDTO = null;

            return (entity, mappedDTO, targetAudioId);
        }

        private void SetupMocks(Audio? entity, AudioDTO? audioDTO)
        {
            this.mockRepo
                .Setup(r => r.AudioRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Audio, bool>>>(),
                    It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()))
                .ReturnsAsync(entity);

            if (audioDTO != null)
            {
                this.mockMapper
                    .Setup(m => m.Map<AudioDTO>(It.IsAny<Audio>()))
                    .Returns(audioDTO);

                this.mockBlob
                    .Setup(b => b.FindFileInStorageAsBase64(It.IsAny<string>()))
                    .Returns(audioDTO.Base64);
            }
        }

        private void VerifyMocksCalledOnce(string? blobName)
        {
            this.mockRepo.Verify(
            r => r.AudioRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Audio, bool>>>(),
                It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()),
            Times.Once);

            this.mockMapper.Verify(
                m => m.Map<AudioDTO>(It.IsAny<Audio>()),
                Times.Once);

            this.mockBlob.Verify(
                b => b.FindFileInStorageAsBase64(blobName!),
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
