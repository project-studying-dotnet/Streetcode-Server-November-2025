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

    /// <summary>
    /// Unit tests for <see cref="GetSubtitlesByStreetcodeIdHandler"/>.
    /// Covers scenarios for retrieving subtitles associated with a specific streetcode,
    /// including successful retrieval and cases where the subtitle does not exist.
    /// </summary>
    public class GetSubtitlesByStreetcodeIdHandlerTests
    {
        private const int StreetcodeId = 1;
        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetSubtitlesByStreetcodeIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSubtitlesByStreetcodeIdHandlerTests"/> class,
        /// setting up the mock dependencies and the handler instance.
        /// </summary>
        public GetSubtitlesByStreetcodeIdHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetSubtitlesByStreetcodeIdHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns a successful result with the subtitle DTO when a subtitle
        /// exists for the given Streetcode ID.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> containing the subtitle DTO.</returns>
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

        /// <summary>
        /// Tests that the handler returns a null value within the result when no subtitle is found
        /// for the specified Streetcode ID.
        /// </summary>
        /// <returns>A <see cref="Task"/> with a result where the Value is null.</returns>
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