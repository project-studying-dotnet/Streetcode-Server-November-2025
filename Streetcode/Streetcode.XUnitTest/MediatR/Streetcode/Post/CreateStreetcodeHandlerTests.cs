namespace Streetcode.XUnitTest.MediatR.Post
{
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
    using Streetcode.BLL.Util;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
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

        private void SetupMapperForStreetcodeContent(CreateStreetcodeDto streetcodeDto, StreetcodeContent streetcode)
        {
            this._mapperMock
                .Setup(mapper => mapper.Map<StreetcodeContent>(It.IsAny<CreateStreetcodeDto>()))
                .Returns(streetcode);

            this._mapperMock
                .Setup(mapper => mapper.Map<CreateStreetcodeDto>(streetcode))
                .Returns(streetcodeDto);

        }

        private void SetupCreateStreetcodeAsync(StreetcodeContent streetcode)
        {
            this._repositoryMock
                .Setup(repo => repo.StreetcodeRepository.CreateAsync(It.IsAny<StreetcodeContent>()))
                .ReturnsAsync(streetcode);
        }

        [Fact]
        public async Task Handler_ReturnsSuccess_When_Proper_Input()
        {
            StreetcodeCreateHelper streetcodeCreateHelper = new StreetcodeCreateHelper(this._loggerMock.Object);

            var json = @"
            {
              ""Index"": 1,
              ""Title"": ""Test Title"",
              ""StreetcodeType"": ""Person"",
              ""FirstName"": ""John"",
              ""LastName"": ""Doe"",
              ""TransliterationUrl"": ""test-john-doe"",
              ""Date"": ""2024-12-03"",
              ""Tags"": [

              ],
              ""Images"": [
              ]
            }";

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

            this.SetupMapperForStreetcodeContent(createStreetcodeDto, streetcodeEntity);
            this.SetupCreateStreetcodeAsync(streetcodeEntity);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
            result.Value.Should().BeEquivalentTo(JsonSerializer.SerializeToElement(createStreetcodeDto));
        }
    }
}
