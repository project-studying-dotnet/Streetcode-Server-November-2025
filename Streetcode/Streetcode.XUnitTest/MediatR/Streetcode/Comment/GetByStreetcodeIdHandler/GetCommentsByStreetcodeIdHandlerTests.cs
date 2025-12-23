namespace Streetcode.XUnitTest.MediatR.Comment.GetByStreetcodeIdHandler
{
    using AutoMapper;
    using DAL.Entities.Streetcode;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode.Comments;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Comments.GetByStreetcodeId;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Comments.Fixtures;
    using Streetcode.XUnitTest.MediatR.Comments.Helpers;
    using Xunit;

    public class GetCommentsByStreetcodeIdHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetCommentsByStreetcodeIdHandler handler;

        public GetCommentsByStreetcodeIdHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetCommentsByStreetcodeIdHandler(repositoryWrapperMock.Object, mapperMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenNoCommentsFound_ReturnEmptyResult()
        {
            // Arrange
            var commentsRepositoryMock = new Mock<ICommentsRepository>(MockBehavior.Strict);
            var streetcodeId = 101;
            var query = new GetCommentsByStreetcodeIdQuery(streetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(commentsRepositoryMock);
            commentsRepositoryMock.SetupGetAllAsync(Enumerable.Empty<Comment>());
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Empty(result.Value);

            // Verify
            commentsRepositoryMock.VerifyGetAllAsyncCalledOnce<ICommentsRepository, Comment>();
            this.loggerMock.VerifyLogDebugCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenCommentsExist_ShouldReturnSuccessResult()
        {
            // Arrange
            var commentsRepositoryMock = new Mock<ICommentsRepository>(MockBehavior.Strict);
            var streetcodeId = 1;
            var comments = CommentTestData.CreateCommentsHierarchy();
            var commentsDtos = CommentTestData.CreateCommentsDtosHierarchy();
            var query = new GetCommentsByStreetcodeIdQuery(streetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(commentsRepositoryMock);
            commentsRepositoryMock.SetupGetAllAsync(comments);
            this.mapperMock.SetupMapper(comments, commentsDtos);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Empty(result.Value);

            // Verify
            commentsRepositoryMock.VerifyGetAllAsyncCalledOnce<ICommentsRepository, Comment>();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<CommentDto>>();
            this.loggerMock.VerifyLogDebugCalledNever();
        }
    }
}