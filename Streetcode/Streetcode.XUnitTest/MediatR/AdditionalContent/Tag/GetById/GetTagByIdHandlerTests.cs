namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.GetById
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.AdditionalContent;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetById;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetTagByIdHandler"/>.
    /// Covers scenarios for retrieving a specific tag by its unique identifier,
    /// including successful retrieval and cases where the tag is not found.
    /// </summary>
    public class GetTagByIdHandlerTests
    {
        private const int Id = 1;
        private const string ErrorMsg = "Cannot find a Tag with corresponding id: {0}";

        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTagByIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTagByIdHandlerTests"/> class,
        /// setting up the mock dependencies and the handler instance.
        /// </summary>
        public GetTagByIdHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();

            this.handler = new GetTagByIdHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns success and the correct tag DTO when the tag exists in the database.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> containing the tag DTO.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTagExists()
        {
            // Arrange
            var entity = TestDataHelper.CreateTag();
            var dto = TestDataHelper.CreateTagDto();

            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);
            tagRepoMock.SetupGetFirstOrDefaultAsync(entity);

            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);
            this.mapperMock.SetupMapper(entity, dto);

            var query = new GetTagByIdQuery(Id);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dto);

            // Verify
            tagRepoMock.VerifyGetFirstOrDefaultCalledOnce<ITagRepository, Tag>();
            this.mapperMock.VerifyMapCalledOnce<TagDto>();
        }

        /// <summary>
        /// Tests that the handler returns failure and logs an error when the specified tag ID does not exist.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the "Tag not found" error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenTagIsNull()
        {
            // Arrange
            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);
            tagRepoMock.SetupGetFirstOrDefaultAsync((Tag?)null);

            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);
            this.loggerMock.SetupLogger();

            var query = new GetTagByIdQuery(Id);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(string.Format(ErrorMsg, Id));

            // Verify
            this.loggerMock.VerifyLogErrorCalledOnce();
            this.mapperMock.VerifyMapCalledNever<TagDto>();
            tagRepoMock.VerifyGetFirstOrDefaultCalledOnce<ITagRepository, Tag>();
        }

        /// <summary>
        /// Tests that the handler returns success but with a null value if the tag exists but mapping to DTO fails (returns null).
        /// </summary>
        /// <returns>A successful <see cref="Task"/> where the value is null due to mapping failure.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenMapperReturnsNull()
        {
            // Arrange
            var entity = TestDataHelper.CreateTag();
            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);
            tagRepoMock.SetupGetFirstOrDefaultAsync(entity);

            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);

            this.mapperMock.Setup(m => m.Map<TagDto>(It.IsAny<Tag>()))
                .Returns((TagDto)null!);

            var query = new GetTagByIdQuery(Id);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeNull();

            // Verify
            tagRepoMock.VerifyGetFirstOrDefaultCalledOnce<ITagRepository, Tag>();
            this.mapperMock.VerifyMapCalledOnce<TagDto>();
        }
    }
}