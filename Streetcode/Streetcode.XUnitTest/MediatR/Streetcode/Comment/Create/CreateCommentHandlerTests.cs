namespace Streetcode.XUnitTest.MediatR.Comments.Create
{
    using AutoMapper;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Streetcode.Comments;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Comments.Create;
 using global::Streetcode.BLL.MediatR.Streetcode.Comments.Create;
 using global::Streetcode.DAL.Entities.Streetcode;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Streetcode;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.Comments.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Comments.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="CreateCommentHandler"/>.
    /// Covers success and failure scenarios for comment creation,
    /// including validation of streetcode existence, mapping failures, and successful comment creation.
    /// </summary>
    public class CreateCommentHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly CreateCommentHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommentHandlerTests"/> class.
        /// Initializes mocks and the <see cref="CreateCommentHandler"/> instance.
        /// </summary>
        public CreateCommentHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new CreateCommentHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler successfully creates a new comment when all validations pass.
        /// Ensures that the streetcode exists, mappings are successful, and the comment is persisted to the repository.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenAllValidationsPass_ShouldReturnCommentDto()
        {
            // Arrange
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var commentsRepositoryMock = new Mock<ICommentsRepository>(MockBehavior.Strict);
            var createCommentDto = CommentTestData.CreateCreateCommentDto();
            var command = new CreateCommentCommand(createCommentDto);
            var streetcode = new StreetcodeContent { Id = createCommentDto.StreetcodeId };
            var newComment = CommentTestData.CreateComment(streetcodeId: createCommentDto.StreetcodeId);
            var commentDto = CommentTestData.CreateCommentDto();

            this.repositoryWrapperMock.SetupRepositoryWrapper(
                streetcodeRepositoryMock,
                commentsRepositoryMock);
            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);
            this.mapperMock.SetupMapper(createCommentDto, newComment);
            commentsRepositoryMock.SetupCreateAsync(newComment);
            this.repositoryWrapperMock.SetupSaveChangesAsync();
            this.mapperMock.SetupMapper(newComment, commentDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(commentDto, result.Value);

            // Verify
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            this.mapperMock.VerifyMapCalledOnce<Comment>();
            commentsRepositoryMock.VerifyCreateAsyncCalledOnce<ICommentsRepository, Comment>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce<CommentDto>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        /// <summary>
        /// Tests that the handler returns a failure result when the associated streetcode does not exist.
        /// Ensures that the appropriate error message is returned and the error is logged.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenStreetcodeDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var createCommentDto = CommentTestData.CreateCreateCommentDto();
            var command = new CreateCommentCommand(createCommentDto);

            this.repositoryWrapperMock.SetupRepositoryWrapper(streetcodeRepositoryMock);
            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(entity: null);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(string.Format(ErrorMessages.StreetcodeNotFoundById, createCommentDto.StreetcodeId), result.Errors.FirstOrDefault()?.Message);

            // Verify
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Tests that the handler returns a failure result when mapping from <see cref="CreateCommentDto"/> to <see cref="Comment"/> fails.
        /// Ensures that the appropriate error message is returned and the error is logged.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenCreateCommentDtoMappingFails_ShouldReturnFailureResult()
        {
            // Arrange
            string errorMsg = ErrorMessages.CreateCommentMappingFailed;
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var createCommentDto = CommentTestData.CreateCreateCommentDto();
            var command = new CreateCommentCommand(createCommentDto);
            var streetcode = new StreetcodeContent { Id = createCommentDto.StreetcodeId };

            this.repositoryWrapperMock.SetupRepositoryWrapper(streetcodeRepositoryMock);
            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);
            this.mapperMock.SetupMapper<CreateCommentDto, Comment>(createCommentDto, null!);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(errorMsg, result.Errors.FirstOrDefault()?.Message);

            // Verify
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            this.mapperMock.VerifyMapCalledOnce<Comment>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Tests that the handler returns a success result with null value when mapping from <see cref="Comment"/> to <see cref="CommentDto"/> fails.
        /// Ensures that the comment is created and persisted successfully, but the final DTO mapping returns null.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenCommentDtoMappingFails_ShouldReturnSuccessWithNullValue()
        {
            // Arrange
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var commentsRepositoryMock = new Mock<ICommentsRepository>(MockBehavior.Strict);
            var createCommentDto = CommentTestData.CreateCreateCommentDto();
            var command = new CreateCommentCommand(createCommentDto);
            var streetcode = new StreetcodeContent { Id = createCommentDto.StreetcodeId };
            var newComment = CommentTestData.CreateComment(streetcodeId: createCommentDto.StreetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(
                streetcodeRepositoryMock,
                commentsRepositoryMock);
            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);
            this.mapperMock.SetupMapper(createCommentDto, newComment);
            commentsRepositoryMock.SetupCreateAsync(newComment);
            this.repositoryWrapperMock.SetupSaveChangesAsync();
            this.mapperMock.SetupMapper<Comment, CommentDto>(newComment, null!);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Null(result.Value);

            // Verify
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            this.mapperMock.VerifyMapCalledOnce<Comment>();
            commentsRepositoryMock.VerifyCreateAsyncCalledOnce<ICommentsRepository, Comment>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce<CommentDto>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}