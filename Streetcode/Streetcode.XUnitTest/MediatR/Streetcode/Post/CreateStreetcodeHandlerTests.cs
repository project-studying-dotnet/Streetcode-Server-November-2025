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

            string json = StreetcodeData.CreatePersonStreetcode();

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
    }
}
