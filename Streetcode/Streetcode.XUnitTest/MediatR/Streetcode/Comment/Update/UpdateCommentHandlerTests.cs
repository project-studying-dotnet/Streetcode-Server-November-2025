namespace Streetcode.XUnitTest.MediatR.Comment.Update
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode.Comments;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Comments.Update;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Comments.Fixtures;
    using Streetcode.XUnitTest.MediatR.Comments.Helpers;
    using Xunit;

    public class UpdateCommentHandlerTests
    {
        private const string UpdatedContent = "Updated content";
        private const string UpdatedAuthorName = "Updated Author";

        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly UpdateCommentHandler handler;

        public UpdateCommentHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new UpdateCommentHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateComment_WhenCommentExists()
        {
            // Arrange
            var existingComment = CommentTestData.CreateComment();
            var updateDto = new UpdateCommentDto
            {
                Id = existingComment.Id,
                Content = UpdatedContent,
                AuthorName = UpdatedAuthorName,
            };
            var command = new UpdateCommentCommand(updateDto);

            var commentsRepoMock = new Mock<ICommentsRepository>();
            this.repositoryWrapperMock.SetupRepositoryWrapper(commentsRepoMock);

            commentsRepoMock.SetupGetFirstOrDefaultAsync(existingComment);

            commentsRepoMock.SetupUpdate();
            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.mapperMock.SetupMapper(existingComment, new CommentDto
            {
                Id = existingComment.Id,
                Content = updateDto.Content,
                AuthorName = updateDto.AuthorName,
                StreetcodeId = existingComment.StreetcodeId,
                CreatedAt = existingComment.CreatedAt,
            });

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value!.Content.Should().Be(UpdatedContent);
            result.Value.AuthorName.Should().Be(UpdatedAuthorName);

            // Verify
            commentsRepoMock.VerifyGetFirstOrDefaultCalledOnce();
            commentsRepoMock.VerifyUpdateCalledOnce(existingComment);
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce<CommentDto>();
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCommentDoesNotExist()
        {
            // Arrange
            var updateDto = new UpdateCommentDto
            {
                Id = 999,
                Content = UpdatedContent,
                AuthorName = UpdatedAuthorName,
            };
            var command = new UpdateCommentCommand(updateDto);

            var commentsRepoMock = new Mock<ICommentsRepository>();
            this.repositoryWrapperMock.SetupRepositoryWrapper(commentsRepoMock);

            commentsRepoMock.SetupGetFirstOrDefaultAsync((Comment?)null);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().NotBeEmpty();

            // Verify
            commentsRepoMock.VerifyGetFirstOrDefaultCalledOnce();
            commentsRepoMock.VerifyUpdateCalledNever();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledNever();
            this.mapperMock.VerifyMapCalledNever<CommentDto>();
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenSaveChangesFails()
        {
            // Arrange
            var existingComment = CommentTestData.CreateComment();
            var updateDto = new UpdateCommentDto
            {
                Id = existingComment.Id,
                Content = UpdatedContent,
                AuthorName = UpdatedAuthorName,
            };
            var command = new UpdateCommentCommand(updateDto);

            var commentsRepoMock = new Mock<ICommentsRepository>();
            this.repositoryWrapperMock.SetupRepositoryWrapper(commentsRepoMock);

            commentsRepoMock.SetupGetFirstOrDefaultAsync(existingComment);
            commentsRepoMock.SetupUpdate();
            this.repositoryWrapperMock.SetupNotSaveChangesAsync();

            this.mapperMock.SetupMapper(existingComment, new CommentDto
            {
                Id = existingComment.Id,
                Content = updateDto.Content,
                AuthorName = updateDto.AuthorName,
                StreetcodeId = existingComment.StreetcodeId,
                CreatedAt = existingComment.CreatedAt,
            });

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().NotBeEmpty();

            // Verify
            commentsRepoMock.VerifyGetFirstOrDefaultCalledOnce();
            commentsRepoMock.VerifyUpdateCalledOnce(existingComment);
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledNever<CommentDto>();

            this.mapperMock.Verify(
                x => x.Map(updateDto, existingComment),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldSetUpdatedAtToCurrentUtcTime_WhenUpdatingComment()
        {
            // Arrange
            var existingComment = CommentTestData.CreateComment();
            var updateDto = new UpdateCommentDto
            {
                Id = existingComment.Id,
                Content = UpdatedContent,
                AuthorName = UpdatedAuthorName,
            };
            var command = new UpdateCommentCommand(updateDto);
            var beforeUpdate = DateTime.UtcNow;

            var commentsRepoMock = new Mock<ICommentsRepository>();
            this.repositoryWrapperMock.SetupRepositoryWrapper(commentsRepoMock);

            commentsRepoMock.SetupGetFirstOrDefaultAsync(existingComment);
            commentsRepoMock.SetupUpdate();
            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.mapperMock.Setup(m => m.Map(updateDto, existingComment));
            this.mapperMock.SetupMapper(existingComment, new CommentDto());

            // Act
            await this.handler.Handle(command, CancellationToken.None);
            var afterUpdate = DateTime.UtcNow;

            // Assert
            existingComment.UpdatedAt.Should().BeOnOrAfter(beforeUpdate);
            existingComment.UpdatedAt.Should().BeOnOrBefore(afterUpdate);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenCommentDoesNotExist()
        {
            // Arrange
            var updateDto = new UpdateCommentDto { Id = 999 };
            var command = new UpdateCommentCommand(updateDto);

            var commentsRepoMock = new Mock<ICommentsRepository>();
            this.repositoryWrapperMock.SetupRepositoryWrapper(commentsRepoMock);
            commentsRepoMock.SetupGetFirstOrDefaultAsync((Comment?)null);

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.loggerMock.Verify(
                x => x.LogError(
                    command,
                    It.Is<string>(s => s.Contains("999"))),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenSaveChangesFails()
        {
            // Arrange
            var existingComment = CommentTestData.CreateComment();
            var updateDto = new UpdateCommentDto
            {
                Id = existingComment.Id,
                Content = UpdatedContent,
            };
            var command = new UpdateCommentCommand(updateDto);

            var commentsRepoMock = new Mock<ICommentsRepository>();
            this.repositoryWrapperMock.SetupRepositoryWrapper(commentsRepoMock);

            commentsRepoMock.SetupGetFirstOrDefaultAsync(existingComment);
            this.repositoryWrapperMock.SetupNotSaveChangesAsync();

            // Act
            await this.handler.Handle(command, CancellationToken.None);

            // Assert
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}