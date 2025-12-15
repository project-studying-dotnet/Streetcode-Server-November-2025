namespace Streetcode.XUnitTest.MediatR.Update
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode;
    using Streetcode.BLL.Interfaces.Cache;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.Update;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatR.Base;
    using Streetcode.XUnitTest.MediatR.Fixture;
    using Xunit;

    public class UpdateStreetcodeHandlerTests
    {
        private readonly UpdateStreetcodeHandler handler;

        private readonly StreetcodeHandlersTestsHelper streetcodeHandlersTestsHelper;

        private Mock<IRepositoryWrapper> repositoryMock = new Mock<IRepositoryWrapper>();

        private Mock<IMapper> mapperMock = new Mock<IMapper>();

        private Mock<ILoggerService> loggerMock = new Mock<ILoggerService>();
        private Mock<ICacheService> cacheServiceMock = new Mock<ICacheService>();

        public UpdateStreetcodeHandlerTests()
        {
            this.handler = new UpdateStreetcodeHandler(
                this.repositoryMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object,
                this.cacheServiceMock.Object);

            this.streetcodeHandlersTestsHelper =
                new StreetcodeHandlersTestsHelper(this.repositoryMock, this.mapperMock, this.loggerMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_When_ProperInput()
        {
            var request = this.streetcodeHandlersTestsHelper.PrepareValidRequest();

            this.streetcodeHandlersTestsHelper.SetupSuccessfulUpdate(request);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_When_ImgAndTagsAreNull()
        {
            var request = this.streetcodeHandlersTestsHelper.PrepareValidRequest(
                StreetcodeTestData.CreateNullValuesStreetcode());

            this.streetcodeHandlersTestsHelper.SetupSuccessfulUpdate(request);

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(7)]
        public async Task Handle_ShouldReturnSuccess_When_AudioIsProper(int? audioId)
        {
            var request = this.streetcodeHandlersTestsHelper.PrepareValidRequest(
                StreetcodeTestData.CreatePersonStreetcode(audioId: audioId));

            this.streetcodeHandlersTestsHelper.SetupSuccessfulUpdate(request);

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [MemberData(nameof(TagsImagesTestData))]
        public async Task Handle_ShouldReturnSuccess_When_ProperTagsImagesInput(
            int?[] tags, int?[] images)
        {
            var request = this.streetcodeHandlersTestsHelper.PrepareValidRequest(
                StreetcodeTestData.CreatePersonStreetcode(tagIds: tags, imgIds: images));

            this.streetcodeHandlersTestsHelper.SetupSuccessfulUpdate(request);

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_When_StreetcodeNotFound()
        {
            var request = this.streetcodeHandlersTestsHelper.PrepareValidRequest();

            this.streetcodeHandlersTestsHelper.SetupStreetcodeNotFound();

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal("Streetcode not found", result.Errors[0].Message);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_When_MappingFailed()
        {
            var request = this.streetcodeHandlersTestsHelper.PrepareValidRequest();

            this.mapperMock
                .Setup(m => m.Map(It.IsAny<UpdateStreetcodeDto>(), It.IsAny<StreetcodeContent>()))
                .Throws(new Exception());

            this.streetcodeHandlersTestsHelper.SetupStreetcodeExists();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal("StreetcodeType value can't be changed", result.Errors[0].Message);
        }

        [Theory]
        [InlineData(-1, "invalid audio Id")]
        [InlineData(14, "Audio doesn't exist")]
        public async Task Handle_ShouldReturnFail_When_WrongAudio(int audioId, string error)
        {
            var request = this.streetcodeHandlersTestsHelper.PrepareValidRequest(
                StreetcodeTestData.CreatePersonStreetcode(audioId: audioId));

            this.streetcodeHandlersTestsHelper.SetupSuccessfulUpdate(request);

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal(error, result.Errors[0].Message);
        }

        [Theory]
        [MemberData(nameof(TagsTestData))]
        public async Task Handle_ShouldReturnFail_When_TagsNotFound(int?[] tags, string error)
        {
            var request = this.streetcodeHandlersTestsHelper.PrepareValidRequest(
                StreetcodeTestData.CreatePersonStreetcode(tagIds: tags));

            this.streetcodeHandlersTestsHelper.SetupSuccessfulUpdate(request);

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal(error, result.Errors[0].Message);
        }

        [Theory]
        [MemberData(nameof(ImagesTestData))]
        public async Task Handle_ShouldReturnFail_When_ImagesNotFound(int?[] images, string error)
        {
            var request = this.streetcodeHandlersTestsHelper.PrepareValidRequest(
                StreetcodeTestData.CreatePersonStreetcode(imgIds: images));

            this.streetcodeHandlersTestsHelper.SetupSuccessfulUpdate(request);

            var result = await this.handler.Handle(request, CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal(error, result.Errors[0].Message);
        }

        public static IList<object[]> ImagesTestData() =>
            new List<object[]>
            {
            new object[] { new int?[] { 1, 5 }, "Image 1 not found; Image 5 not found" },
            new object[] { new int?[] { 1 }, "Image 1 not found" },
            };

        public static IList<object[]> TagsTestData() =>
            new List<object[]>
            {
            new object[] { new int?[] { 5, 10 }, "Tag 5 not found; Tag 10 not found" },
            new object[] { new int?[] { 5 }, "Tag 5 not found" },
            };

        public static IList<object[]> TagsImagesTestData() =>
            new List<object[]>
            {
            new object[] { Array.Empty<int?>(), new int?[] { 10, 15 } },
            new object[] { new int?[] { 15, 20 }, Array.Empty<int?>() },
            };
    }
}
