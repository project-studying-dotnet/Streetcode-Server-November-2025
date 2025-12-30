namespace Streetcode.XUnitTest.MediatR.Comment.GetByStreetcodeIdHandler
{
    using AutoMapper;
    using DAL.Entities.Streetcode;
    using Moq;
 using global::Streetcode.BLL.DTO.Streetcode.Comments;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Streetcode.Comments.GetByStreetcodeId;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Streetcode;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.Comments.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Comments.Helpers;
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
            var streetcodeId = 101;
            var comments = CommentTestData.CreateCommentsHierarchy();
            var rootComments = comments.Where(c => c.ParentCommentId == null).ToList();
            var commentsDtos = CommentTestData.CreateCommentsDtosHierarchy();
            var query = new GetCommentsByStreetcodeIdQuery(streetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(commentsRepositoryMock);
            commentsRepositoryMock.SetupGetAllAsync(rootComments);
            this.mapperMock
                .Setup(m => m.Map<IEnumerable<CommentDto>>(
                    It.IsAny<IEnumerable<Comment>>()))
                .Returns(commentsDtos);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.NotEmpty(result.Value);
            Assert.Equal(commentsDtos.Count, result.Value.Count());

            // Verify
            commentsRepositoryMock.VerifyGetAllAsyncCalledOnce<ICommentsRepository, Comment>();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<CommentDto>>();
            this.loggerMock.VerifyLogDebugCalledNever();
        }
    }
}