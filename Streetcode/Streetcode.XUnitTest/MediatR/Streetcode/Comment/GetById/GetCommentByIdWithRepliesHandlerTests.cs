namespace Streetcode.XUnitTest.MediatR.StreetCode.Comments.GetById
{
    using System.Linq.Expressions;
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode.Comments;
    using Streetcode.BLL.MediatR.Streetcode.Comments.GetById;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetCommentByIdWithRepliesHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepo;
        private readonly Mock<IMapper> mockMapper;
        private readonly GetCommentByIdWithRepliesHandler handler;

        public GetCommentByIdWithRepliesHandlerTests()
        {
            this.mockRepo = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();
            this.handler = new GetCommentByIdWithRepliesHandler(this.mockRepo.Object, this.mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ExistingId_ReturnsSuccessAndDto()
        {
            int commentId = 1;

            var comment = new Comment
            {
                Id = commentId,
                Content = "Main Comment",
                Replies = new List<Comment>
                {
                    new Comment { Id = 2, Content = "Reply", StreetcodeId = 1 },
                },
                StreetcodeId = 1,
            };

            var commentDto = new CommentWithRepliesDto { Id = commentId, Content = "Main Comment" };

            this.mockRepo.Setup(r => r.CommentsRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>>()))
                .ReturnsAsync(comment);

            this.mockMapper.Setup(m => m.Map<CommentWithRepliesDto>(comment))
                .Returns(commentDto);

            var query = new GetCommentByIdWithRepliesQuery(commentId);

            var result = await this.handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Content.Should().Be("Main Comment");
        }

        [Fact]
        public async Task Handle_NonExistingId_ReturnsFail()
        {
            int commentId = 999;

            this.mockRepo.Setup(r => r.CommentsRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>>()))
                .ReturnsAsync((Comment?)null);

            var query = new GetCommentByIdWithRepliesQuery(commentId);

            var result = await this.handler.Handle(query, CancellationToken.None);

            result.IsFailed.Should().BeTrue();
        }
    }
}