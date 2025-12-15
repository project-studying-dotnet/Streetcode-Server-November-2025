namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Subtitle.GetByStreetCodeId
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.AdditionalContent.Subtitles;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetByStreetcodeId;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    public class GetSubtitlesByStreetcodeIdHandlerTests
    {
        private const int StreetcodeId = 1;
        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetSubtitlesByStreetcodeIdHandler handler;

        public GetSubtitlesByStreetcodeIdHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetSubtitlesByStreetcodeIdHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSubtitleExists()
        {
            // Arrange
            var subtitle = TestDataHelper.CreateSubtitle(streetCodeId: StreetcodeId);
            var subtitleDto = TestDataHelper.CreateSubtitleDto(streetCodeId: StreetcodeId);

            var subtitleRepoMock = new Mock<ISubtitleRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepository(
                r => r.SubtitleRepository,
                subtitleRepoMock);
            subtitleRepoMock.SetupGetFirstOrDefaultAsync(subtitle);
            this.mapperMock.SetupMapper(subtitle, subtitleDto);

            var query = new GetSubtitlesByStreetcodeIdQuery(StreetcodeId);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.Value.Should().BeEquivalentTo(subtitleDto);

            // Verify
            this.mapperMock.VerifyMapCalledOnce<SubtitleDto>();
            subtitleRepoMock.VerifyGetFirstOrDefaultCalledOnce<ISubtitleRepository, Subtitle>();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenSubtitleDoesNotExist()
        {
            // Arrange
            var subtitle = TestDataHelper.CreateSubtitle(streetCodeId: StreetcodeId);
            var subtitleRepoMock = new Mock<ISubtitleRepository>(MockBehavior.Strict);

            this.repoWrapperMock.SetupRepository(
                r => r.SubtitleRepository,
                subtitleRepoMock);
            subtitleRepoMock.SetupGetFirstOrDefaultAsync((Subtitle?)null);
            this.mapperMock.SetupMapper(subtitle, (SubtitleDto?)null);

            var query = new GetSubtitlesByStreetcodeIdQuery(StreetcodeId);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.Value.Should().BeNull();

            // Verify
            this.mapperMock.VerifyMapCalledOnce<SubtitleDto?>();
            subtitleRepoMock.VerifyGetFirstOrDefaultCalledOnce<ISubtitleRepository, Subtitle>();
        }
    }
}