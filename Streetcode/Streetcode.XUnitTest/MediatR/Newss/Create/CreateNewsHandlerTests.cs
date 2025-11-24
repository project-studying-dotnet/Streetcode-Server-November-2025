namespace Streetcode.XUnitTest.MediatR.Newss.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Streetcode.BLL.DTO.News;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.Create;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatR.Newss.Helpers;
    using Xunit;

    public class CreateNewsHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly CreateNewsHandler handler;

        public CreateNewsHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repoMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new CreateNewsHandler(
                this.mapperMock.Object,
                this.repoMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenDataValid()
        {
            var dto = NewsTestData.CreateNewsDTO();
            var entity = NewsTestData.CreateNews();

            MockMapperHelper.SetupMapper(this.mapperMock, dto, entity);
            MockRepoHelper.SetupNewsCreate(this.repoMock, entity);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);
            MockMapperHelper.SetupMapper(this.mapperMock, entity, dto);

            var command = new CreateNewsCommand(dto);

            var result = await this.handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dto);

            MockMapperHelper.VerifyMapOnce<NewsDTO, News>(this.mapperMock);
            MockRepoHelper.VerifyNewsCreateOnce(this.repoMock);
            MockMapperHelper.VerifyMapOnce<News, NewsDTO>(this.mapperMock);
        }

        [Fact]

        public async Task Handle_ShouldSetImageIdToNull_WhenImageIdIsZero()
        {
            var dto = NewsTestData.CreateNewsDTO(imageId: 0);
            var entity = NewsTestData.CreateNews(imageId: 0);

            MockMapperHelper.SetupMapper(this.mapperMock, dto, entity);
            MockRepoHelper.SetupNewsCreate(this.repoMock, entity);
            MockRepoHelper.SetupSaveSuccess(this.repoMock);
            MockMapperHelper.SetupMapper(this.mapperMock, entity, dto);

            var command = new CreateNewsCommand(dto);

            var result = await this.handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            entity.ImageId.Should().BeNull();

            MockMapperHelper.VerifyMapOnce<NewsDTO, News>(this.mapperMock);
            MockRepoHelper.VerifyNewsCreateOnce(this.repoMock);
            MockMapperHelper.VerifyMapOnce<News, NewsDTO>(this.mapperMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenMapperReturnsNull()
        {
            const string errorMsg = "Cannot convert null to news";

            var dto = NewsTestData.CreateNewsDTO();

            this.mapperMock.Setup(m => m.Map<News>(It.IsAny<NewsDTO>())).Returns((News)null);

            var command = new CreateNewsCommand(dto);
            var result = await this.handler.Handle(command, default);

            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == errorMsg);

            MockMapperHelper.VerifyMapOnce<NewsDTO, News>(this.mapperMock);
            MockRepoHelper.VerifyNewsCreateNever(this.repoMock);
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, errorMsg);
        }
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSaveFails()
        {
            const string errorMsg = "Failed to create a news";

            var dto = NewsTestData.CreateNewsDTO();
            var entity = NewsTestData.CreateNews();

            MockMapperHelper.SetupMapper(this.mapperMock, dto, entity);
            MockRepoHelper.SetupNewsCreate(this.repoMock, entity);
            MockRepoHelper.SetupSaveFail(this.repoMock);

            var command = new CreateNewsCommand(dto);
            var result = await this.handler.Handle(command, default);

            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.Message == errorMsg);

            MockMapperHelper.VerifyMapOnce<NewsDTO, News>(this.mapperMock);
            MockRepoHelper.VerifyNewsCreateOnce(this.repoMock);
            MockLoggerHelper.VerifyLogErrorOnceWithMessage(this.loggerMock, errorMsg);
        }
    }
}
