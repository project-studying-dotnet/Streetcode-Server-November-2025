namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.AdditionalContent;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.Create;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="CreateTagHandler"/>.
    /// Covers scenarios including successful tag creation, save exceptions, repository create exceptions, and logging of errors.
    /// </summary>
    public class CreateTagHandlerTests
    {
        private const string ExceptionText = "DB error";
        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly CreateTagHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTagHandlerTests"/> class.
        /// </summary>
        public CreateTagHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new CreateTagHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler successfully creates a tag and returns the corresponding DTO.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> with the created tag DTO.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTagCreated()
        {
            // Arange
            var tagEntity = TestDataHelper.CreateTag();
            var tagDto = TestDataHelper.CreateTagDto();
            var createTagDto = TestDataHelper.CreateCreateTagDto();

            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);
            tagRepoMock.SetupCreateAsync(tagEntity);

            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);
            this.repoWrapperMock.SetupSaveChangesAsync();
            this.mapperMock.SetupMapper(tagEntity, tagDto);

            var query = new CreateTagCommand(createTagDto);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(tagDto);

            // Verify
            this.mapperMock.VerifyMapCalledOnce<TagDto>();
            tagRepoMock.VerifyCreateAsyncCalledOnce<ITagRepository, Tag>();
            this.repoWrapperMock.VerifySaveChangesAsyncCalledOnce();
        }

        /// <summary>
        /// Tests that the handler returns failure and logs an error when the SaveChanges operation throws an exception.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> containing the exception message.</returns>
        [Fact]
        public async Task Handle_ShouldThrowException_WhenSaveChangesFails()
        {
            // Arrange
            var tagEntity = TestDataHelper.CreateTag();
            var createTagDto = TestDataHelper.CreateCreateTagDto();

            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);
            tagRepoMock.SetupCreateAsync(tagEntity);
            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);

            this.repoWrapperMock.Setup(r => r.SaveChangesAsync()).ThrowsAsync(new Exception(ExceptionText));

            var query = new CreateTagCommand(createTagDto);

            // Act
            Func<Task> act = async () => await this.handler.Handle(query, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<Exception>(act);
        }

        /// <summary>
        /// Tests that the handler returns failure when the repository's CreateAsync method throws an exception.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> containing the exception message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenCreateAsyncThrowsException()
        {
            // Arrange
            var createTagDto = TestDataHelper.CreateCreateTagDto();
            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);

            tagRepoMock.Setup(r => r.CreateAsync(It.IsAny<Tag>()))
                .ThrowsAsync(new Exception(ExceptionText));

            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);
            this.loggerMock.SetupLogger();

            var query = new CreateTagCommand(createTagDto);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Contain(ExceptionText);

            // Verify
            tagRepoMock.VerifyCreateAsyncCalledOnce<ITagRepository, Tag>();
            this.repoWrapperMock.VerifySaveChangesAsyncCalledNever();
            this.mapperMock.VerifyMapCalledNever<TagDto>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Tests that the handler logs the correct error message when an exception occurs during SaveChanges.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> and verifies the logging of the exception message.</returns>
        [Fact]
        public async Task Handle_ShouldLogCorrectError_WhenExceptionOccurs()
        {
            // Arrange
            var expectedError = "Database connection timeout";
            var tagEntity = TestDataHelper.CreateTag();
            var createTagDto = TestDataHelper.CreateCreateTagDto();
            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);

            tagRepoMock.SetupCreateAsync(tagEntity);
            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);
            this.repoWrapperMock.Setup(r => r.SaveChangesAsync())
                .ThrowsAsync(new Exception(expectedError));

            var query = new CreateTagCommand(createTagDto);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();

            // Verify
            this.loggerMock.Verify(
                l => l.LogError(
                    It.Is<CreateTagCommand>(q => q == query),
                    It.Is<string>(s => s.Contains(expectedError))),
                Times.Once);
        }
    }
}
