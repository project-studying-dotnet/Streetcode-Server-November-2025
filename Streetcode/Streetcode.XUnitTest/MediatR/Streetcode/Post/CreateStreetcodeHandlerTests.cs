namespace Streetcode.XUnitTest.MediatR.Post
{
    using System.Linq.Expressions;
    using System.Text.Json;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Media.Images;
    using Streetcode.BLL.DTO.Streetcode;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
    using Streetcode.BLL.Util;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Entities.Media;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Fixture;
    using Xunit;

    public class CreateStreetcodeHandlerTests : CreateStreetcodeHandlerTestsBase
    {
        private CreateStreetcodeHandler handler;

        public CreateStreetcodeHandlerTests()
        {
            this.handler = new CreateStreetcodeHandler(
                this._repositoryMock.Object,
                this._mapperMock.Object,
                this._loggerMock.Object,
                this._mediatorMock.Object);
        }

        [Fact]
        public async Task Handler_ReturnsSuccess_When_Proper_Input()
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this._loggerMock.Object);

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

            this.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.SetupCreateStreetcodeAsync(streetcodeEntity);
            this.SetupImageRepoMocks();
            this.SetupAudioRepoMocks();
            this.SetupTagsRepositoryMocks();
            this._repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);

            Assert.Equal(createStreetcodeDto.Index, result.Value.GetProperty("Index").GetInt32());
        }

        [Fact]
        public async Task Handler_ReturnsError_When_Index_Exists()
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this._loggerMock.Object);

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

            this.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.SetupCreateStreetcodeAsync(streetcodeEntity);
            this._repositoryMock.SetupSaveChangesAsync();

            var streetcodeRepoMock = new Mock<IStreetcodeRepository>();

            this._repositoryMock
                .Setup(r => r.StreetcodeRepository)
                .Returns(streetcodeRepoMock.Object);

            streetcodeRepoMock.SetupGetAllAsync(new List<StreetcodeContent>()
            {
                new StreetcodeContent { Index = 1 },
            });

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);

            Assert.Equal($"Streetcode with Index {1} already exists", result.Errors[0].Message);
        }

        [Fact]
        public async Task Handler_ReturnsSuccess_When_AudioNull()
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this._loggerMock.Object);

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

            this.SetupImageRepoMocks();
            this.SetupAudioRepoMocks();
            this.SetupTagsRepositoryMocks();
            this.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.SetupCreateStreetcodeAsync(streetcodeEntity);
            this._repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [MemberData(nameof(NullOrEmptyArrayData))]
        public async Task Handler_ReturnsSuccess_When_ImageAreNullOrEmpty(int?[] images)
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this._loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode(tagIds: images);
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

            this.SetupImageRepoMocks();
            this.SetupAudioRepoMocks();
            this.SetupTagsRepositoryMocks();
            this.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.SetupCreateStreetcodeAsync(streetcodeEntity);
            this._repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [MemberData(nameof(NullOrEmptyArrayData))]
        public async Task Handler_ReturnsSuccess_When_TagsAreNullOrEmpty(int?[] tags)
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this._loggerMock.Object);

            string json = StreetcodeTestData.CreatePersonStreetcode(imgIds: tags);
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

            this.SetupImageRepoMocks();
            this.SetupAudioRepoMocks();
            this.SetupTagsRepositoryMocks();
            this.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.SetupCreateStreetcodeAsync(streetcodeEntity);
            this._repositoryMock.SetupSaveChangesAsync();

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
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this._loggerMock.Object);

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

            this.SetupImageRepoMocks();
            this.SetupAudioRepoMocks();
            this.SetupTagsRepositoryMocks();
            this.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.SetupCreateStreetcodeAsync(streetcodeEntity);
            this._repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal("Audio not found", result.Errors[0].Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public async Task Handler_ReturnsFail_When_AudioNotFound(int testedImageId)
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this._loggerMock.Object);

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

            this.SetupImageRepoMocks();
            this.SetupAudioRepoMocks();
            this.SetupTagsRepositoryMocks();
            this.SetupMappers(createStreetcodeDto, streetcodeEntity);
            this.SetupCreateStreetcodeAsync(streetcodeEntity);
            this._repositoryMock.SetupSaveChangesAsync();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal("Audio not found", result.Errors[0].Message);
        }

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
