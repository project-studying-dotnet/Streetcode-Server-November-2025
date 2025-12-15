namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.AdditionalContent;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetAll;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    public class GetAllTagsHandlerTests
    {
        private const string ErrorMsg = "Cannot find any tags";
        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetAllTagsHandler handler;

        public GetAllTagsHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetAllTagsHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTagsExist()
        {
            // Arrange
            var tagEntities = TestDataHelper.CreateTags();
            var tagDtos = TestDataHelper.CreateTagDtos();

            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);
            tagRepoMock.SetupGetAllAsync(tagEntities);

            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);

            this.mapperMock.SetupMapper<IEnumerable<Tag>, IEnumerable<TagDto>>(tagEntities, tagDtos);

            var query = new GetAllTagsQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(tagDtos);

            // Verify
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<TagDto>>();
            tagRepoMock.VerifyGetAllAsyncCalledOnce<ITagRepository, Tag>();
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenTagsAreNull()
        {
            // Arrange
            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);
            tagRepoMock.SetupGetAllAsync((IEnumerable<Tag>?)null);

            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);
            this.loggerMock.SetupLogger();

            var query = new GetAllTagsQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(ErrorMsg);

            // Verify
            this.mapperMock.VerifyMapCalledNever<IEnumerable<TagDto>>();
            tagRepoMock.VerifyGetAllAsyncCalledOnce<ITagRepository, Tag>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}