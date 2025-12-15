namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Subtitle.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.AdditionalContent.Subtitles;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetAll;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    public class GetAllSubtitlesHandlerTests
    {
        private const string ErrorMsg = "Cannot find any subtitles";

        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetAllSubtitlesHandler handler;

        public GetAllSubtitlesHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetAllSubtitlesHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenSubtitlesIsNull()
        {
            // Arrange
            var subtitleRepositoryMock = new Mock<ISubtitleRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepository(
                r => r.SubtitleRepository,
                subtitleRepositoryMock);

            this.loggerMock.SetupLogger();
            subtitleRepositoryMock.SetupGetAllAsync((List<Subtitle>?)null);

            var query = new GetAllSubtitlesQuery();

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(ErrorMsg);

            // Verify
            subtitleRepositoryMock.VerifyGetAllAsyncCalledOnce<ISubtitleRepository, Subtitle>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSubtitlesExist()
        {
            // Arrange
            var data = TestDataHelper.CreateSubtitles();
            var dto = TestDataHelper.CreateSubtitlesDtos();

            var subtitleRepositoryMock = new Mock<ISubtitleRepository>(MockBehavior.Strict);
            subtitleRepositoryMock.SetupGetAllAsync(data);

            this.repoWrapperMock.SetupRepository(
                r => r.SubtitleRepository,
                subtitleRepositoryMock);
            this.mapperMock.SetupMapper<IEnumerable<Subtitle>, IEnumerable<SubtitleDto>>(data, dto);
            this.loggerMock.SetupLogger();

            var query = new GetAllSubtitlesQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dto);

            // Verify
            subtitleRepositoryMock.VerifyGetAllAsyncCalledOnce<ISubtitleRepository, Subtitle>();
            this.loggerMock.VerifyLogErrorCalledNever();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<SubtitleDto>>();
        }
    }
}