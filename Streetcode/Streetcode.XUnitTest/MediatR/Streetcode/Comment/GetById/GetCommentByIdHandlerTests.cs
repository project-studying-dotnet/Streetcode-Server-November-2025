namespace Streetcode.XUnitTest.MediatR.Comment.GetById
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode.Comments;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Comments.GetById;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Comments.Fixtures;
    using Streetcode.XUnitTest.MediatR.Comments.Helpers;
    using Xunit;

    public class GetCommentByIdHandlerTests 
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetCommentByIdHandler handler;

        public GetCommentByIdHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetCommentByIdHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenCommentExists_ShouldReturnCommentDto()
        {
            // Arrange
            var comment = CommentTestData.CreateComment();
            var commentDto = CommentTestData.CreateCommentDto();

            var commentsRepoMock = new Mock<ICommentsRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepositoryWrapper(commentsRepoMock);
            commentsRepoMock.SetupGetBySpec(comment);

            this.mapperMock
                .Setup(m => m.Map<CommentDto>(comment))
                .Returns(commentDto);

            var query = new GetCommentByIdQuery(comment.Id);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(commentDto);

            // Verify
            commentsRepoMock.VerifyGetBySpec(Times.Once());
            this.mapperMock.VerifyMapCalledOnce<CommentDto>();
        }

        [Fact]
        public async Task Handle_WhenCommentNotFound_ShouldReturnFailureResult()
        {
            // Arrange
            var commentId = 999;
            var commentsRepoMock = new Mock<ICommentsRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepositoryWrapper(commentsRepoMock);
            commentsRepoMock.SetupGetBySpec(null);
            var query = new GetCommentByIdQuery(commentId);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().NotBeEmpty();

            // Verify
            commentsRepoMock.VerifyGetBySpec(Times.Once());
            this.mapperMock.VerifyMapCalledNever<CommentDto>();
        }

        [Fact]
        public async Task Handle_WhenCommentNotFound_ShouldLogError()
        {
            // Arrange
            var commentId = 999;
            var commentsRepoMock = new Mock<ICommentsRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepositoryWrapper(commentsRepoMock);
            commentsRepoMock.SetupGetBySpec(null);

            this.loggerMock.SetupLogger();

            var query = new GetCommentByIdQuery(commentId);

            // Act
            await this.handler.Handle(query, default);

            // Assert
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenCommentExists_ShouldNotLogError()
        {
            // Arrange
            var comment = CommentTestData.CreateComment();
            var commentDto = CommentTestData.CreateCommentDto();

            var commentsRepoMock = new Mock<ICommentsRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepositoryWrapper(commentsRepoMock);
            commentsRepoMock.SetupGetBySpec(comment);
            this.mapperMock
                .Setup(m => m.Map<CommentDto>(comment))
                .Returns(commentDto);

            this.loggerMock.SetupLogger();

            var query = new GetCommentByIdQuery(comment.Id);

            // Act
            await this.handler.Handle(query, default);

            // Assert
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        [Fact]
        public async Task Handle_WhenCommentExistsWithReplies_ShouldReturnCommentDtoWithReplies()
        {
            // Arrange
            var comment = CommentTestData.CreateCommentWithReplies();
            var commentDto = CommentTestData.CreateCommentDtoWithReplies();

            var commentsRepoMock = new Mock<ICommentsRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepositoryWrapper(commentsRepoMock);
            commentsRepoMock.SetupGetBySpec(comment);

            this.mapperMock
                .Setup(m => m.Map<CommentDto>(comment))
                .Returns(commentDto);

            var query = new GetCommentByIdQuery(comment.Id);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(commentDto);

            result.Value.Replies.Should().HaveCountGreaterThan(0);

            // Verify
            commentsRepoMock.VerifyGetBySpecOnce();
            commentsRepoMock.VerifyGetBySpec(Times.Once());
            this.mapperMock.VerifyMapCalledOnce<CommentDto>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}