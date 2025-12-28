namespace Streetcode.XUnitTest.MediatR.Media.Audio.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.Media.Audio;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Audio.Create;
    using Streetcode.DAL.Entities.Media;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class CreateAudioHandlerTests
    {
        private readonly Mock<IBlobService> mockBlob;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly Mock<IRepositoryWrapper> mockRepo;
        private readonly Mock<IMapper> mockMapper;

        private readonly CreateAudioHandler handler;

        public CreateAudioHandlerTests()
        {
            this.mockBlob = new Mock<IBlobService>();
            this.mockLogger = new Mock<ILoggerService>();
            this.mockRepo = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();

            this.handler = new CreateAudioHandler(
                this.mockBlob.Object,
                this.mockRepo.Object,
                this.mockMapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenAudioCreated()
        {
            // Arrange.
            var audioFileBaseCreateDTO = new AudioFileBaseCreateDto
            {
                Title = "Test audio title",
                Description = "Test description",
                MimeType = "audio/mpeg",
                BaseFormat = "base64string",
                Extension = "mp3",
            };

            var expectedAudioDTO = new AudioDto
            {
                Id = 1,
                Base64 = "base64string",
                MimeType = "audio/mpeg",
                BlobName = "sha256.mp3",
            };

            const string hashBlobStorageName = "sha256";

            var createAudioCommand = new CreateAudioCommand(audioFileBaseCreateDTO);

            this.mockBlob
                .Setup(b => b.SaveFileInStorageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(hashBlobStorageName);

            this.mockMapper
                 .Setup(m => m.Map<Audio>(It.IsAny<AudioFileBaseCreateDto>()))
                 .Returns(new Audio
                 {
                     Title = audioFileBaseCreateDTO.Title,
                     MimeType = audioFileBaseCreateDTO.MimeType,
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
                .Setup(m => m.Map<AudioDto>(It.IsAny<Audio>()))
                .Returns(expectedAudioDTO);

            this.mockRepo
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act.
            var result = await this.handler.Handle(createAudioCommand, default);

            // Assert.
            result.IsSuccess.Should().BeTrue();

            result.Value.Should().BeEquivalentTo(expectedAudioDTO);

            this.mockRepo.Verify(r => r.AudioRepository.CreateAsync(It.Is<Audio>(a => a.BlobName == hashBlobStorageName)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResult_WhenSaveChangesFailed()
        {
            // Arrange.
            var audioFileBaseCreateDTO = new AudioFileBaseCreateDto
            {
                Title = "Test audio title",
                Description = "Test description",
                MimeType = "audio/mpeg",
                BaseFormat = "base64string",
                Extension = "mp3",
            };

            var expectedAudioDTO = new AudioDto
            {
                Id = 1,
                Base64 = "base64string",
                MimeType = "audio/mpeg",
                BlobName = "sha256.mp3",
            };

            const string hashBlobStorageName = "sha256";

            var createAudioCommand = new CreateAudioCommand(audioFileBaseCreateDTO);

            this.mockBlob
                .Setup(b => b.SaveFileInStorageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(hashBlobStorageName);

            this.mockMapper
                 .Setup(m => m.Map<Audio>(It.IsAny<AudioFileBaseCreateDto>()))
                 .Returns(new Audio
                 {
                     Title = audioFileBaseCreateDTO.Title,
                     MimeType = audioFileBaseCreateDTO.MimeType,
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
                .Setup(m => m.Map<AudioDto>(It.IsAny<Audio>()))
                .Returns(expectedAudioDTO);

            this.mockRepo
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);

            // Act.
            var result = await this.handler.Handle(createAudioCommand, default);

            // Assert.
            result.IsFailed.Should().BeTrue();

            result.Errors.Should().Contain(e => e.Message == ErrorMessages.AudioCreationFailed);

            this.mockLogger.Verify(l => l.LogError(It.IsAny<object>(), ErrorMessages.AudioCreationFailed), Times.Once);

            this.mockRepo.Verify(r => r.AudioRepository.CreateAsync(It.IsAny<Audio>()), Times.Once);
        }
    }
}
