namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Subtitle.GetById
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL.DTO.AdditionalContent.Subtitles;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.AdditionalContent.GetById;
 using global::Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetById;
 using global::Streetcode.DAL.Entities.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetSubtitleByIdHandler"/>.
    /// Covers scenarios for retrieving a specific subtitle by its unique identifier,
    /// including successful retrieval and cases where the subtitle is not found.
    /// </summary>
    public class GetSubtitleByIdHandlerTests
    {
        private const int Id = 1;
        private const string ErrorMsg = "Cannot find a subtitle with corresponding id: {0}";

        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetSubtitleByIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSubtitleByIdHandlerTests"/> class,
        /// setting up the mock dependencies and the handler instance.
        /// </summary>
        public GetSubtitleByIdHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetSubtitleByIdHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns success and the correct subtitle DTO when the subtitle exists in the database.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> containing the subtitle DTO.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenSubtitleExists()
        {
            // Arrange
            var subtitle = TestDataHelper.CreateSubtitle(Id);
            var subtitleDto = TestDataHelper.CreateSubtitleDto(Id);
            var subtitleRepositoryMock = new Mock<ISubtitleRepository>(MockBehavior.Strict);

            this.repoWrapperMock.SetupRepository(
                r => r.SubtitleRepository,
                subtitleRepositoryMock);
            subtitleRepositoryMock.SetupGetFirstOrDefaultAsync(subtitle);
            this.mapperMock.SetupMapper(subtitle, subtitleDto);

            var query = new GetSubtitleByIdQuery(Id);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(subtitleDto);

            // Verify
            this.mapperMock.VerifyMapCalledOnce<SubtitleDto>();
            subtitleRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<ISubtitleRepository, Subtitle>();
        }

        /// <summary>
        /// Tests that the handler returns failure and logs an error when the specified subtitle ID does not exist.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the "Subtitle not found" error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenSubtitleDoesNotExist()
        {
            // Arrange
            var subtitleRepositoryMock = new Mock<ISubtitleRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepository(
                r => r.SubtitleRepository,
                subtitleRepositoryMock);

            subtitleRepositoryMock.SetupGetFirstOrDefaultAsync((Subtitle?)null);
            this.loggerMock.SetupLogger();

            var query = new GetSubtitleByIdQuery(Id);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(string.Format(ErrorMsg, Id));

            // Verify
            subtitleRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<ISubtitleRepository, Subtitle>();
            this.mapperMock.VerifyMapCalledNever<SubtitleDto>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}