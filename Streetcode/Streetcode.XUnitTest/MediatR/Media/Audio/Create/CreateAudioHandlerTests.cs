namespace Streetcode.XUnitTest.MediatR.Media.Audio.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Media.Audio;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Audio.Create;
    using Streetcode.DAL.Entities.Media;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class CreateAudioHandlerTests
    {
        private readonly Mock<IBlobService> mockBlob = new ();
        private readonly Mock<ILoggerService> mockLogger = new ();
        private readonly Mock<IRepositoryWrapper> mockRepo = new ();
        private readonly Mock<IMapper> mockMapper = new ();

        private readonly CreateAudioHandler handler;

        public CreateAudioHandlerTests()
        {
            this.handler = new CreateAudioHandler(
                this.mockBlob.Object,
                this.mockRepo.Object,
                this.mockMapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_Should_RetursSuccessResult_WhenAudioCreated()
        {
            // Arrange.
            var audioFileBaseCreateDTO = new AudioFileBaseCreateDTO
            {
                Title = "Test audio title",
                Description = "Test desciption",
                MimeType = "audio/mpeg",
                BaseFormat = "base64string",
                Extension = "mp3",
            };

            var expectedAudioDTO = new AudioDTO
            {
                Id = 1,
                Base64 = "base64string",
                MimeType = "audio/mpeg",
                BlobName = "sha256.mp3",
            };

            var hashBlobStorageName = "sha256";

            var expectedBlobName = $"{hashBlobStorageName}.{audioFileBaseCreateDTO.Extension}";

            var createAudioCommand = new CreateAudioCommand(audioFileBaseCreateDTO);

            this.mockBlob
                .Setup(b => b.SaveFileInStorage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(hashBlobStorageName);

            this.mockMapper
                 .Setup(m => m.Map<Audio>(It.IsAny<AudioFileBaseCreateDTO>()))
                 .Returns(new Audio
                 {
                     Title = audioFileBaseCreateDTO.Title,
                     MimeType = audioFileBaseCreateDTO.MimeType,
                     BlobName = expectedBlobName,
                     Base64 = audioFileBaseCreateDTO.Description,
                 });

            this.mockRepo
                .Setup(r => r.AudioRepository.CreateAsync(It.IsAny<Audio>()))
                .ReturnsAsync((Audio a) =>
                {
                    a.Id = 1;
                    return a;
                });

            this.mockMapper
                .Setup(m => m.Map<AudioDTO>(It.IsAny<Audio>()))
                .Returns(expectedAudioDTO);

            this.mockRepo
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act.
            var result = await this.handler.Handle(createAudioCommand, default);

            // Assert.
            result.IsSuccess.Should().BeTrue();

            result.Value.Should().BeEquivalentTo(expectedAudioDTO);

            this.mockRepo.Verify(r => r.AudioRepository.CreateAsync(It.Is<Audio>(a => a.BlobName == expectedBlobName)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenSaveChangesFailed()
        {
            // Arrange.
            var audioFileBaseCreateDTO = new AudioFileBaseCreateDTO
            {
                Title = "Test audio title",
                Description = "Test desciption",
                MimeType = "audio/mpeg",
                BaseFormat = "base64string",
                Extension = "mp3",
            };

            var expectedAudioDTO = new AudioDTO
            {
                Id = 1,
                Base64 = "base64string",
                MimeType = "audio/mpeg",
                BlobName = "sha256.mp3",
            };

            var hashBlobStorageName = "sha256";

            var expectedBlobName = $"{hashBlobStorageName}.{audioFileBaseCreateDTO.Extension}";

            var expectedErrorMsg = $"Failed to create an audio";

            var createAudioCommand = new CreateAudioCommand(audioFileBaseCreateDTO);

            this.mockBlob
                .Setup(b => b.SaveFileInStorage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(hashBlobStorageName);

            this.mockMapper
                 .Setup(m => m.Map<Audio>(It.IsAny<AudioFileBaseCreateDTO>()))
                 .Returns(new Audio
                 {
                     Title = audioFileBaseCreateDTO.Title,
                     MimeType = audioFileBaseCreateDTO.MimeType,
                     BlobName = expectedBlobName,
                     Base64 = audioFileBaseCreateDTO.Description,
                 });

            this.mockRepo
                .Setup(r => r.AudioRepository.CreateAsync(It.IsAny<Audio>()))
                .ReturnsAsync((Audio a) =>
                {
                    a.Id = 1;
                    return a;
                });

            this.mockMapper
                .Setup(m => m.Map<AudioDTO>(It.IsAny<Audio>()))
                .Returns(expectedAudioDTO);

            this.mockRepo
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);

            // Act.
            var result = await this.handler.Handle(createAudioCommand, default);

            // Assert.
            result.IsFailed.Should().BeTrue();

            result.Errors.Should().Contain(e => e.Message == expectedErrorMsg);

            this.mockLogger.Verify(l => l.LogError(It.IsAny<object>(), expectedErrorMsg), Times.Once);

            this.mockRepo.Verify(r => r.AudioRepository.CreateAsync(It.IsAny<Audio>()), Times.Once);
        }
    }
}
