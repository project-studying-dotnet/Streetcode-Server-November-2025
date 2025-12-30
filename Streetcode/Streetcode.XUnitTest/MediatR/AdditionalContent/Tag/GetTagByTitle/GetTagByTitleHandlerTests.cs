namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.GetTagByTitle
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL.DTO.AdditionalContent;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;
 using global::Streetcode.BLL.MediatR.AdditionalContent.Tag.GetTagByTitle;
 using global::Streetcode.DAL.Entities.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetTagByTitleHandler"/>.
    /// Covers scenarios for retrieving a tag by its title, including successful retrieval
    /// and cases where the tag is not found in the database.
    /// </summary>
    public class GetTagByTitleHandlerTests
    {
        private const string TestTag = "TestTag";
        private const string ErrorMsg = $"Cannot find any tag by the title: {TestTag}";
        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTagByTitleHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTagByTitleHandlerTests"/> class,
        /// setting up the mock dependencies and the handler instance.
        /// </summary>
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

        /// <summary>
        /// Tests that the handler returns a successful result with the tag DTO when a tag
        /// with the specified title exists in the database.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> containing the found tag DTO.</returns>
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

        /// <summary>
        /// Tests that the handler returns a failure result and logs an error when a tag
        /// with the specified title does not exist.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the "Tag not found" error message.</returns>
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