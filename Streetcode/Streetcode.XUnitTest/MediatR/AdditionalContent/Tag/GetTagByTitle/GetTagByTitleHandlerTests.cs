namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.GetTagByTitle
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.AdditionalContent;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetTagByTitle;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    public class GetTagByTitleHandlerTests
    {
        private const string TestTag = "TestTag";
        private const string ErrorMsg = $"Cannot find any tag by the title: {TestTag}";
        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTagByTitleHandler handler;

        public GetTagByTitleHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();

            this.handler = new GetTagByTitleHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTagExists()
        {
            // Arrange
            var tagEntity = TestDataHelper.CreateTag(title: TestTag);
            var tagDto = TestDataHelper.CreateTagDto(title: TestTag);

            var tagRepo = new Mock<ITagRepository>();
            tagRepo.SetupGetFirstOrDefaultAsync(tagEntity);

            this.mapperMock.SetupMapper(tagEntity, tagDto);
            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepo);

            var query = new GetTagByTitleQuery(TestTag);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(tagDto);

            // Verify
            tagRepo.VerifyGetFirstOrDefaultCalledOnce<ITagRepository, Tag>();
            this.mapperMock.VerifyMapCalledOnce<TagDto>();
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenTagDoesNotExist()
        {
            // Arrange
            var tagRepo = new Mock<ITagRepository>();

            tagRepo.SetupGetFirstOrDefaultAsync((Tag?)null);
            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepo);
            this.loggerMock.SetupLogger();
            var query = new GetTagByTitleQuery(TestTag);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Contain(ErrorMsg);

            // Verify
            tagRepo.VerifyGetFirstOrDefaultCalledOnce<ITagRepository, Tag>();
            this.loggerMock.VerifyLogErrorCalledOnce();
            this.mapperMock.VerifyMapCalledNever<TagDto>();
        }
    }
}