namespace Streetcode.XUnitTest.MediatR.Comment.Delete
{
    using Ardalis.Specification;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Comments.Delete;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Comments.Fixtures;
    using Streetcode.XUnitTest.MediatR.Comments.Helpers;
    using Xunit;

    public class DeleteCommentHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly DeleteCommentHandler handler;

        public DeleteCommentHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new DeleteCommentHandler(
                this.repositoryWrapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCommentDoesNotExist()
        {
            // Arrange
            var commentId = 999;
            var command = new DeleteCommentCommand(commentId);
            var commentsRepoMock = this.SetupRepositoryMock();
            commentsRepoMock.SetupGetFirstOrDefaultAsync((Comment?)null);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().NotBeEmpty();
            commentsRepoMock.VerifyGetFirstOrDefaultCalledOnce();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledNever();
        }

        [Fact]
        public async Task Handle_ShouldHardDeleteComment_WhenCommentHasNoChildren()
        {
            // Arrange
            var comment = CommentTestData.CreateComment();
            comment.ParentCommentId = null;
            var command = new DeleteCommentCommand(comment.Id);

            var commentsRepoMock = this.SetupBasicDeleteScenario(comment, hasChildren: false);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            commentsRepoMock.Verify(x => x.Delete(comment), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldSoftDeleteComment_WhenCommentHasChildren()
        {
            // Arrange
            var comment = CommentTestData.CreateComment();
            comment.IsDeleted = false;
            var command = new DeleteCommentCommand(comment.Id);

            var commentsRepoMock = this.SetupBasicDeleteScenario(comment, hasChildren: true);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            comment.IsDeleted.Should().BeTrue();
            comment.DeletedAt.Should().NotBeNull();
            commentsRepoMock.VerifyUpdateCalledOnce(comment);
        }

        [Fact]
        public async Task Handle_ShouldCleanupParentChain_WhenParentIsDeletedAndHasNoChildren()
        {
            // Arrange
            var parentComment = CommentTestData.CreateComment();
            parentComment.Id = 1;
            parentComment.IsDeleted = true;

            var childComment = CommentTestData.CreateComment();
            childComment.Id = 2;
            childComment.ParentCommentId = parentComment.Id;

            var command = new DeleteCommentCommand(childComment.Id);

            var commentsRepoMock = this.SetupParentCleanupScenario(childComment, parentComment);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            commentsRepoMock.Verify(x => x.Delete(It.IsAny<Comment>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_ShouldNotCleanupParent_WhenParentIsNotDeleted()
        {
            // Arrange
            var parentComment = CommentTestData.CreateComment();
            parentComment.Id = 1;
            parentComment.IsDeleted = false;

            var childComment = CommentTestData.CreateComment();
            childComment.Id = 2;
            childComment.ParentCommentId = parentComment.Id;

            var command = new DeleteCommentCommand(childComment.Id);

            var commentsRepoMock = this.SetupParentCleanupScenario(childComment, parentComment);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            commentsRepoMock.Verify(x => x.Delete(It.IsAny<Comment>()), Times.Once);
        }

        // Helper methods
        private Mock<ICommentsRepository> SetupRepositoryMock()
        {
            var commentsRepoMock = new Mock<ICommentsRepository>();
            this.repositoryWrapperMock.SetupRepositoryWrapper(commentsRepoMock);
            this.repositoryWrapperMock.SetupRepository(
                r => r.CommentsRepository,
                commentsRepoMock);
            return commentsRepoMock;
        }

        private Mock<ICommentsRepository> SetupBasicDeleteScenario(Comment comment, bool hasChildren)
        {
            var commentsRepoMock = this.SetupRepositoryMock();

            commentsRepoMock.SetupGetFirstOrDefaultAsync(comment);
            commentsRepoMock.Setup(x => x.AnyAsync(
                It.IsAny<ISpecification<Comment>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(hasChildren);

            if (hasChildren)
            {
                commentsRepoMock.SetupUpdate();
            }
            else
            {
                commentsRepoMock.SetupDelete<ICommentsRepository, Comment>();
            }

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            return commentsRepoMock;
        }

        private Mock<ICommentsRepository> SetupParentCleanupScenario(Comment child, Comment parent)
        {
            var commentsRepoMock = this.SetupRepositoryMock();

            commentsRepoMock.SetupSequence(x => x.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<Func<Comment, bool>>>(),
                    It.IsAny<Func<System.Linq.IQueryable<Comment>,
                        Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Comment, object>>>()))
                .ReturnsAsync(child)
                .ReturnsAsync(parent);

            commentsRepoMock.Setup(x => x.AnyAsync(
                    It.IsAny<ISpecification<Comment>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            commentsRepoMock.SetupDelete<ICommentsRepository, Comment>();
            this.repositoryWrapperMock.SetupSaveChangesAsync();

            return commentsRepoMock;
        }
    }
}