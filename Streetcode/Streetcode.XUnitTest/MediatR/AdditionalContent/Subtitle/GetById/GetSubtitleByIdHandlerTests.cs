namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Subtitle.GetById
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.AdditionalContent.Subtitles;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.AdditionalContent.GetById;
    using Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetById;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    public class GetSubtitleByIdHandlerTests
    {
        private const int Id = 1;
        private const string ErrorMsg = "Cannot find a subtitle with corresponding id: {0}";

        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetSubtitleByIdHandler handler;

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