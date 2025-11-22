namespace Streetcode.XUnitTest.MediatR.Media.Audio.GetAll
{
    using System.Linq.Expressions;
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Media.Audio;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Audio.GetAll;
    using Streetcode.DAL.Entities.Media;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetAllAudiosHandlerTests
    {
        private readonly Mock<IBlobService> mockBlob;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly Mock<IRepositoryWrapper> mockRepo;
        private readonly Mock<IMapper> mockMapper;

        private readonly GetAllAudiosHandler handler;

        public GetAllAudiosHandlerTests()
        {
            this.mockBlob = new Mock<IBlobService>();
            this.mockLogger = new Mock<ILoggerService>();
            this.mockRepo = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();

            this.handler = new GetAllAudiosHandler(
                this.mockRepo.Object,
                this.mockMapper.Object,
                this.mockBlob.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenAudiosFound()
        {
            // Arrange.
            var (audioEntities, audioDtos) = CreateValidAudioEntitiesAndDTOs();
            this.SetupMock(audioEntities, audioDtos);

            // Act.
            var result = await this.handler.Handle(new GetAllAudiosQuery(), default);

            // Assert.
            result.IsSuccess.Should().BeTrue();
            Assert.Equal(audioDtos, result.Value);
            this.VerifyMocksCalledOnce();
        }

        [Fact]
        public async Task Handle_ShouldReturnFailesResult_WhenAudiosNotFound()
        {
            // Arrange.
            var (audioEntities, audioDtos) = CreateNullAudioEntitiesAndDTOs();
            const string errorMsg = "Cannot find any audios";
            this.SetupMock(audioEntities, audioDtos);

            // Act.
            var result = await this.handler.Handle(new GetAllAudiosQuery(), default);

            // Assert.
            result.IsFailed.Should().BeTrue();
            this.VerifyLoggerErrorCalledOnce(errorMsg);
        }

        private static (IEnumerable<Audio> AudioEntities, IEnumerable<AudioDTO> AudioDtos) CreateValidAudioEntitiesAndDTOs()
        {
            var audioEntities = new List<Audio>
            {
                new Audio { Id = 1, BlobName = "audio1.mp3" },
                new Audio { Id = 2, BlobName = "audio2.mp3" },
            };
            var audioDtos = new List<AudioDTO>
            {
                new AudioDTO { Id = 1, Base64 = "base64string1" },
                new AudioDTO { Id = 2, Base64 = "base64string2" },
            };
            return (audioEntities, audioDtos);
        }

        private static (IEnumerable<Audio> AudioEntities, IEnumerable<AudioDTO> AudioDtos) CreateNullAudioEntitiesAndDTOs()
        {
            List<Audio>? audioEntities = null;
            List<AudioDTO>? audioDtos = null;

            return (audioEntities, audioDtos);
        }

        private void SetupMock(IEnumerable<Audio> audioEntities, IEnumerable<AudioDTO> audioDtos)
        {
            this.mockRepo.Setup(r => r.AudioRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Audio, bool>>>(),
                    It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()))
                .ReturnsAsync(audioEntities);

            this.mockMapper
                .Setup(m => m.Map<IEnumerable<AudioDTO>>(It.IsAny<IEnumerable<Audio>>()))
                .Returns(audioDtos);

            this.mockBlob
                .Setup(b => b.FindFileInStorageAsBase64(It.IsAny<string>()));
        }

        private void VerifyMocksCalledOnce()
        {
            this.mockRepo.Verify(
                r => r.AudioRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Audio, bool>>>(),
                    It.IsAny<Func<IQueryable<Audio>, IIncludableQueryable<Audio, object>>>()),
                Times.Once);

            this.mockMapper.Verify(
                m => m.Map<IEnumerable<AudioDTO>>(It.IsAny<IEnumerable<Audio>>()),
                Times.Once);

            this.mockBlob.Verify(
                b => b.FindFileInStorageAsBase64(It.IsAny<string>()),
                Times.AtLeastOnce);
        }

        private void VerifyLoggerErrorCalledOnce(string logMsg)
        {
            this.mockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    logMsg),
                Times.Once);
        }
    }
}
