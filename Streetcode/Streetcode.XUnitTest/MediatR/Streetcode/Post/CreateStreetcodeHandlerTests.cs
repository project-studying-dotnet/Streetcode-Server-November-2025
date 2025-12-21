namespace Streetcode.XUnitTest.MediatR.Post
{
    using System.Linq.Expressions;
    using System.Text.Json;
    using AutoMapper;
    using global::MediatR;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.Media.Images;
    using Streetcode.BLL.DTO.Streetcode;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
    using Streetcode.BLL.Util;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Entities.Media;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Base;
    using Streetcode.XUnitTest.MediatR.Fixture;
    using Xunit;

    public class CreateStreetcodeHandlerTests
    {
        private readonly StreetcodeHandlersTestsHelper streetcodeHandlersTestsHelper;

        private readonly CreateStreetcodeHandler handler;

        private Mock<IRepositoryWrapper> repositoryMock = new Mock<IRepositoryWrapper>();

        private Mock<IMapper> mapperMock = new Mock<IMapper>();

        private Mock<ILoggerService> loggerMock = new Mock<ILoggerService>();

        public CreateStreetcodeHandlerTests()
        {
            this.handler = new CreateStreetcodeHandler(
                this.repositoryMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);

            this.streetcodeHandlersTestsHelper =
                new StreetcodeHandlersTestsHelper(this.repositoryMock, this.mapperMock, this.loggerMock);
        }

        [Fact]
        public async Task Handler_ReturnsSuccess_When_Proper_Input()
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this.loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode();

            using var doc = JsonDocument.Parse(json);
            JsonElement createStreetcodeDtoRaw = doc.RootElement.Clone();

            string streetcodeType = createStreetcodeDtoRaw.GetProperty("StreetcodeType").GetString();

            CreateStreetcodeCommand request = new CreateStreetcodeCommand(createStreetcodeDtoRaw);

            CreateStreetcodeDto createStreetcodeDto =
                streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

            var streetcodeEntity = new StreetcodeContent
            {
                Id = 1,
                Index = createStreetcodeDto.Index,
                Title = createStreetcodeDto.Title,
                TransliterationUrl = createStreetcodeDto.TransliterationUrl,
            };

            this.streetcodeHandlersTestsHelper.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.streetcodeHandlersTestsHelper.SetupCreateStreetcodeAsync(streetcodeEntity);
            this.streetcodeHandlersTestsHelper.SetupImageRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupAudioRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupTagsRepositoryMocks();
            this.repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);

            Assert.Equal(createStreetcodeDto.Index, result.Value.GetProperty("Index").GetInt32());
        }

        [Fact]
        public async Task Handler_ReturnsError_When_Index_Exists()
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this.loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode();
            using var doc = JsonDocument.Parse(json);
            JsonElement createStreetcodeDtoRaw = doc.RootElement.Clone();

            string streetcodeType = createStreetcodeDtoRaw.GetProperty("StreetcodeType").GetString();

            CreateStreetcodeCommand request = new CreateStreetcodeCommand(createStreetcodeDtoRaw);

            CreateStreetcodeDto createStreetcodeDto =
                streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

            var streetcodeEntity = new StreetcodeContent
            {
                Id = 1,
                Index = createStreetcodeDto.Index,
                Title = createStreetcodeDto.Title,
                TransliterationUrl = createStreetcodeDto.TransliterationUrl,
            };

            this.streetcodeHandlersTestsHelper.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.streetcodeHandlersTestsHelper.SetupCreateStreetcodeAsync(streetcodeEntity);

            var streetcodeRepoMock = new Mock<IStreetcodeRepository>();

            this.repositoryMock
                .Setup(r => r.StreetcodeRepository)
                .Returns(streetcodeRepoMock.Object);

            streetcodeRepoMock.SetupGetAllAsync(new List<StreetcodeContent>()
            {
                new StreetcodeContent { Index = 1 },
            });

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);

            Assert.Equal(string.Format(ErrorMessages.StreetcodeWithIndexAlreadyExists, 1), result.Errors[0].Message);
        }

        [Fact]
        public async Task HandlerReturnsExceptionMessageWhenExceprionTrown()
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this.loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode();
            using var doc = JsonDocument.Parse(json);
            JsonElement createStreetcodeDtoRaw = doc.RootElement.Clone();

            string streetcodeType = createStreetcodeDtoRaw.GetProperty("StreetcodeType").GetString();

            CreateStreetcodeCommand request = new CreateStreetcodeCommand(createStreetcodeDtoRaw);

            CreateStreetcodeDto createStreetcodeDto =
                streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

            var streetcodeEntity = new StreetcodeContent
            {
                Id = 1,
                Index = createStreetcodeDto.Index,
                Title = createStreetcodeDto.Title,
                TransliterationUrl = createStreetcodeDto.TransliterationUrl,
            };

            this.streetcodeHandlersTestsHelper.SetupMappers(createStreetcodeDto, streetcodeEntity);

            this.repositoryMock.Setup(r => r.StreetcodeRepository.CreateAsync(It.IsAny<StreetcodeContent>()))
                .ThrowsAsync(new InvalidOperationException("Test exception"));

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);

            Assert.Equal(ErrorMessages.StreetcodeCreationFailed, result.Errors[0].Message);
        }

        [Fact]
        public async Task Handler_ReturnsSuccess_When_AudioNull()
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this.loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode(audioId: null);
            using var doc = JsonDocument.Parse(json);
            JsonElement createStreetcodeDtoRaw = doc.RootElement.Clone();

            string streetcodeType = createStreetcodeDtoRaw.GetProperty("StreetcodeType").GetString();

            CreateStreetcodeCommand request = new CreateStreetcodeCommand(createStreetcodeDtoRaw);

            CreateStreetcodeDto createStreetcodeDto =
                streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

            var streetcodeEntity = new StreetcodeContent
            {
                Id = 1,
                Index = createStreetcodeDto.Index,
                Title = createStreetcodeDto.Title,
                TransliterationUrl = createStreetcodeDto.TransliterationUrl,
            };

            this.streetcodeHandlersTestsHelper.SetupImageRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupAudioRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupTagsRepositoryMocks();
            this.streetcodeHandlersTestsHelper.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.streetcodeHandlersTestsHelper.SetupCreateStreetcodeAsync(streetcodeEntity);
            this.repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [MemberData(nameof(NullOrEmptyArrayData))]
        public async Task Handler_ReturnsSuccess_When_ImageAreNullOrEmpty(int?[] images)
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this.loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode(imgIds: images);
            using var doc = JsonDocument.Parse(json);
            JsonElement createStreetcodeDtoRaw = doc.RootElement.Clone();

            string streetcodeType = createStreetcodeDtoRaw.GetProperty("StreetcodeType").GetString();

            CreateStreetcodeCommand request = new CreateStreetcodeCommand(createStreetcodeDtoRaw);

            CreateStreetcodeDto createStreetcodeDto =
                streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

            var streetcodeEntity = new StreetcodeContent
            {
                Id = 1,
                Index = createStreetcodeDto.Index,
                Title = createStreetcodeDto.Title,
                TransliterationUrl = createStreetcodeDto.TransliterationUrl,
            };

            this.streetcodeHandlersTestsHelper.SetupImageRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupAudioRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupTagsRepositoryMocks();
            this.streetcodeHandlersTestsHelper.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.streetcodeHandlersTestsHelper.SetupCreateStreetcodeAsync(streetcodeEntity);
            this.repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [MemberData(nameof(NullOrEmptyArrayData))]
        public async Task Handler_ReturnsSuccess_When_TagsAreNullOrEmpty(int?[] tags)
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this.loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode(tagIds: tags);
            using var doc = JsonDocument.Parse(json);
            JsonElement createStreetcodeDtoRaw = doc.RootElement.Clone();

            string streetcodeType = createStreetcodeDtoRaw.GetProperty("StreetcodeType").GetString();

            CreateStreetcodeCommand request = new CreateStreetcodeCommand(createStreetcodeDtoRaw);

            CreateStreetcodeDto createStreetcodeDto =
                streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

            var streetcodeEntity = new StreetcodeContent
            {
                Id = 1,
                Index = createStreetcodeDto.Index,
                Title = createStreetcodeDto.Title,
                TransliterationUrl = createStreetcodeDto.TransliterationUrl,
            };

            this.streetcodeHandlersTestsHelper.SetupImageRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupAudioRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupTagsRepositoryMocks();
            this.streetcodeHandlersTestsHelper.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.streetcodeHandlersTestsHelper.SetupCreateStreetcodeAsync(streetcodeEntity);
            this.repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public async Task Handler_ReturnsFail_When_AudioNotFound(int testedAudioId)
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this.loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode(audioId: testedAudioId);
            using var doc = JsonDocument.Parse(json);
            JsonElement createStreetcodeDtoRaw = doc.RootElement.Clone();

            string streetcodeType = createStreetcodeDtoRaw.GetProperty("StreetcodeType").GetString();

            CreateStreetcodeCommand request = new CreateStreetcodeCommand(createStreetcodeDtoRaw);

            CreateStreetcodeDto createStreetcodeDto =
                streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

            var streetcodeEntity = new StreetcodeContent
            {
                Id = 1,
                Index = createStreetcodeDto.Index,
                Title = createStreetcodeDto.Title,
                TransliterationUrl = createStreetcodeDto.TransliterationUrl,
            };

            this.streetcodeHandlersTestsHelper.SetupImageRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupAudioRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupTagsRepositoryMocks();
            this.streetcodeHandlersTestsHelper.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.streetcodeHandlersTestsHelper.SetupCreateStreetcodeAsync(streetcodeEntity);
            this.repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal("Audio not found", result.Errors[0].Message);
        }

        [Theory]
        [MemberData(nameof(ImagesTestData))]
        public async Task Handler_ReturnsFail_When_ImagesNotFound(int?[] testedImageId, string errorMessage)
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this.loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode(imgIds: testedImageId);
            using var doc = JsonDocument.Parse(json);
            JsonElement createStreetcodeDtoRaw = doc.RootElement.Clone();

            string streetcodeType = createStreetcodeDtoRaw.GetProperty("StreetcodeType").GetString();

            CreateStreetcodeCommand request = new CreateStreetcodeCommand(createStreetcodeDtoRaw);

            CreateStreetcodeDto createStreetcodeDto =
                streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

            var streetcodeEntity = new StreetcodeContent
            {
                Id = 1,
                Index = createStreetcodeDto.Index,
                Title = createStreetcodeDto.Title,
                TransliterationUrl = createStreetcodeDto.TransliterationUrl,
            };

            this.streetcodeHandlersTestsHelper.SetupImageRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupAudioRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupTagsRepositoryMocks();
            this.streetcodeHandlersTestsHelper.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.streetcodeHandlersTestsHelper.SetupCreateStreetcodeAsync(streetcodeEntity);
            this.repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal(errorMessage, result.Errors[0].Message);
        }

        [Theory]
        [MemberData(nameof(TagsTestData))]
        public async Task Handler_ReturnsFail_When_TagsNotFound(int?[] testedTagId, string errorMessage)
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this.loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode(tagIds: testedTagId);
            using var doc = JsonDocument.Parse(json);
            JsonElement createStreetcodeDtoRaw = doc.RootElement.Clone();

            string streetcodeType = createStreetcodeDtoRaw.GetProperty("StreetcodeType").GetString();

            CreateStreetcodeCommand request = new CreateStreetcodeCommand(createStreetcodeDtoRaw);

            CreateStreetcodeDto createStreetcodeDto =
                streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

            var streetcodeEntity = new StreetcodeContent
            {
                Id = 1,
                Index = createStreetcodeDto.Index,
                Title = createStreetcodeDto.Title,
                TransliterationUrl = createStreetcodeDto.TransliterationUrl,
            };

            this.streetcodeHandlersTestsHelper.SetupImageRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupAudioRepoMocks();
            this.streetcodeHandlersTestsHelper.SetupTagsRepositoryMocks();
            this.streetcodeHandlersTestsHelper.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.streetcodeHandlersTestsHelper.SetupCreateStreetcodeAsync(streetcodeEntity);
            this.repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal(errorMessage, result.Errors[0].Message);
        }

        public static IList<object[]> ImagesTestData() =>
            new List<object[]>
            {
            new object[] { new int?[] { 1, 5 }, $"{string.Format(ErrorMessages.StreetcodeImageNotFoundById, 1)}; {string.Format(ErrorMessages.StreetcodeImageNotFoundById, 5)}" },
            new object[] { new int?[] { 1 }, string.Format(ErrorMessages.StreetcodeImageNotFoundById, 1) },
            };

        public static IList<object[]> TagsTestData() =>
            new List<object[]>
            {
            new object[] { new int?[] { 5, 10 }, $"{string.Format(ErrorMessages.StreetcodeTagNotFoundById, 5)}; {string.Format(ErrorMessages.StreetcodeTagNotFoundById, 10)}" },
            new object[] { new int?[] { 5 }, string.Format(ErrorMessages.StreetcodeTagNotFoundById, 5) },
            };

        public static IList<object[]> NullOrEmptyArrayData()
        {
            return new List<object[]>
            {
                new object[] { null },
                new object[] { Array.Empty<int?>() },
            };
        }
    }
}
