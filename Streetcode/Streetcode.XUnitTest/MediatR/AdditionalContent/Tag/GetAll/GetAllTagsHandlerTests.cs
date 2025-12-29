namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL.DTO.AdditionalContent;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.AdditionalContent.Tag.GetAll;
 using global::Streetcode.DAL.Entities.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetAllTagsHandler"/>.
    /// Covers scenarios for retrieving all tags, including successful retrieval,
    /// handling null results from the repository, and empty collections.
    /// </summary>
    public class GetAllTagsHandlerTests
    {
        private const string ErrorMsg = "Cannot find any tags";
        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetAllTagsHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllTagsHandlerTests"/> class,
        /// setting up the mock dependencies and the handler instance.
        /// </summary>
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

        /// <summary>
        /// Tests that the handler returns a successful result with the list of tag DTOs when tags exist in the repository.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> with an enumerable of tag DTOs.</returns>
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

        /// <summary>
        /// Tests that the handler returns a failure result and logs an error when the repository returns null for tags.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the "Cannot find any tags" error message.</returns>
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

        /// <summary>
        /// Tests that the handler returns a successful result with an empty collection when no tags are found in the repository.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> with an empty enumerable of tag DTOs.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTagsAreEmpty()
        {
            // Arrange
            var emptyTagEntities = new List<Tag>();
            var emptyTagDtos = new List<TagDto>();
            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);

            tagRepoMock.SetupGetAllAsync(emptyTagEntities);
            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);
            this.mapperMock.SetupMapper<IEnumerable<Tag>, IEnumerable<TagDto>>(emptyTagEntities, emptyTagDtos);

            var query = new GetAllTagsQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();
            result.Value.Should().NotBeNull();

            // Verify
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<TagDto>>();
            tagRepoMock.VerifyGetAllAsyncCalledOnce<ITagRepository, Tag>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        /// <summary>
        /// Tests that the handler returns a successful result with a null value when the mapper fails to map the collection.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> where the value is null.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenMapperReturnsNull()
        {
            // Arrange
            var tagEntities = TestDataHelper.CreateTags();
            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);

            tagRepoMock.SetupGetAllAsync(tagEntities);
            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);
            this.mapperMock.Setup(m => m.Map<IEnumerable<TagDto>>(It.IsAny<IEnumerable<Tag>>()))
                .Returns((IEnumerable<TagDto>)null!);

            var query = new GetAllTagsQuery();

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeNull();

            // Verify
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<TagDto>>();
            tagRepoMock.VerifyGetAllAsyncCalledOnce<ITagRepository, Tag>();
        }
    }
}