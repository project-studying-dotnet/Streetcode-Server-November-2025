namespace Streetcode.XUnitTest.MediatR.Media.Audio.GetByStreetcodeId
{
    using System.Linq.Expressions;
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Media.Audio;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Audio.GetByStreetcodeId;
    using Streetcode.BLL.MediatR.ResultVariations;
    using Streetcode.DAL.Entities.Media;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetByStreetcodeIdHandlerTests
    {
        private readonly Mock<IBlobService> mockBlob;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly Mock<IRepositoryWrapper> mockRepo;
        private readonly Mock<IMapper> mockMapper;

        private readonly GetAudioByStreetcodeIdQueryHandler handler;

        public GetByStreetcodeIdHandlerTests()
        {
            this.mockBlob = new Mock<IBlobService>();
            this.mockLogger = new Mock<ILoggerService>();
            this.mockRepo = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();

            this.handler = new GetAudioByStreetcodeIdQueryHandler(
                this.mockRepo.Object,
                this.mockMapper.Object,
                this.mockBlob.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenAudioExists()
        {
            // Arrange.
            var (streetcodeContent, audioDto, targetStreetcodeId) = CreateValidStreetcodeAndDTO();
            this.SetupMocks(streetcodeContent, audioDto);

            // Act.
            var result = await this.handler.Handle(new GetAudioByStreetcodeIdQuery(targetStreetcodeId), default);

            // Assert.
            result.IsSuccess.Should().BeTrue();
            this.VerifyMocksCalledOnce();
        }

        [Fact]
        public async Task Handle_ShouldReturnFailed_WhenAudioDoesNotExists()
        {
            // Arrange.
            var (streetcodeContent, audioDto, targetStreetcodeId) = CreateNullStreetcodeAndDTO();
            this.SetupMocks(streetcodeContent, audioDto);

            // Act.
            var result = await this.handler.Handle(new GetAudioByStreetcodeIdQuery(targetStreetcodeId), default);

            // Assert.
            result.IsFailed.Should().BeTrue();
            this.VerifyLoggerCalledOnce(targetStreetcodeId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailed_WhenStreetcodeDoesNotExists()
        {
            // Arrange.
            var (streetcodeContent, audioDto, targetStreetcodeId) = CreateValidStreetcodeAndDTO();
            streetcodeContent = null;
            this.SetupMocks(streetcodeContent, audioDto);

            // Act.
            var result = await this.handler.Handle(new GetAudioByStreetcodeIdQuery(targetStreetcodeId), default);

            // Assert.
            result.IsFailed.Should().BeTrue();
            this.VerifyLoggerCalledOnce(targetStreetcodeId);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailed_WhenAudioPropertyInStreetCodeDoesNotExists()
        {
            // Arrange.
            var (streetcodeContent, audioDto, targetStreetcodeId) = CreateValidStreetcodeAndDTO();

            streetcodeContent.Audio = null;
            audioDto = null;

            this.SetupMocks(streetcodeContent, audioDto);

            // Act.
            var result = await this.handler.Handle(new GetAudioByStreetcodeIdQuery(targetStreetcodeId), default);

            // Assert.
            result.IsSuccess.Should().BeTrue();
            result.Should().BeOfType<NullResult<AudioDTO>>();
        }

        private static (StreetcodeContent StreetcodeContent, AudioDTO AudioDTO, int TargetStreetcodeId) CreateValidStreetcodeAndDTO()
        {
            const int targetStreetcodeId = 1;
            const int targetAudioId = 1;

            const string blobName = "validBlobName";
            const string base64 = "base64string";

            var entity = new Audio { Id = targetAudioId, BlobName = blobName, Base64 = base64 };
            var audioDTO = new AudioDTO { Id = targetAudioId, BlobName = blobName, Base64 = base64 };

            var streetcode = new StreetcodeContent { Id = targetStreetcodeId, Audio = entity };

            return (streetcode, audioDTO, targetStreetcodeId);
        }

        private static (StreetcodeContent? StreetcodeContent, AudioDTO? AudioDTO, int TargetStreetcodeId) CreateNullStreetcodeAndDTO()
        {
            const int targetStreetcodeId = 1;
            const int targetAudioId = 1;

            Audio? entity = null;
            AudioDTO? audioDTO = null;

            StreetcodeContent? streetcode = null;

            return (streetcode, audioDTO, targetStreetcodeId);
        }

        private void SetupMocks(StreetcodeContent? content, AudioDTO? audioDTO)
        {
            this.mockRepo.Setup(r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .Callback((Expression<Func<StreetcodeContent, bool>> p, Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>> include) =>
                {
                    var dummyQuery = new List<StreetcodeContent>().AsQueryable();

                    include?.Invoke(dummyQuery);
                })
                .ReturnsAsync(content);

            if (content?.Audio != null && audioDTO != null)
            {
                this.mockMapper.Setup(m => m.Map<AudioDTO>(content.Audio))
                    .Returns(audioDTO);

                this.mockBlob.Setup(b => b.FindFileInStorageAsBase64(audioDTO.BlobName))
                    .Returns(audioDTO.Base64);
            }
        }

        private void VerifyMocksCalledOnce()
        {
            this.mockRepo.Verify(
                r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()),
                Times.Once);

            this.mockMapper.Verify(
                m => m.Map<AudioDTO>(It.IsAny<Audio>()),
                Times.Once);

            this.mockBlob.Verify(
                b => b.FindFileInStorageAsBase64(It.IsAny<string>()),
                Times.Once);
        }

        private void VerifyLoggerCalledOnce(int targetStreetcodeId)
        {
            this.mockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    $"Cannot find an audio with the corresponding streetcode id: {targetStreetcodeId}"),
                Times.Once);
        }
    }
}
