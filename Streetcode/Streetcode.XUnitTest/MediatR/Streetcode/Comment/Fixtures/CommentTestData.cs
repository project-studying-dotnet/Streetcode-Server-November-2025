namespace Streetcode.XUnitTest.MediatR.Comments.Fixtures
{
    using Streetcode.BLL.DTO.Streetcode.Comments;
    using Streetcode.DAL.Entities.Streetcode;

    /// <summary>
    /// Provides factory methods for creating test instances of <see cref="DAL.Entities.Streetcode.Comment"/>
    /// and related DTO objects for use in unit tests.
    /// </summary>
    public static class CommentTestData
    {
        /// <summary>
        /// Creates a single <see cref="Comment"/> entity instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the comment.</param>
        /// <param name="streetcodeId">The streetcode ID associated with the comment.</param>
        /// <param name="authorName">The author name of the comment.</param>
        /// <returns>A fully initialized <see cref="Comment"/> object for testing.</returns>
        public static Comment CreateComment(int id = 1, int streetcodeId = 101, string authorName = "John Doe")
        {
            return new Comment
            {
                Id = id,
                StreetcodeId = streetcodeId,
                AuthorName = authorName,
                Content = "This is a test comment.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                ParentCommentId = null,
                Streetcode = null,
                ParentComment = null,
                Replies = new List<Comment>(),
            };
        }

        /// <summary>
        /// Creates a single <see cref="CommentDto"/> instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the comment DTO.</param>
        /// <param name="streetcodeId">The streetcode ID associated with the comment.</param>
        /// <param name="authorName">The author name of the comment.</param>
        /// <returns>A fully initialized <see cref="CommentDto"/> object for testing.</returns>
        public static CommentDto CreateCommentDto(int id = 1, int streetcodeId = 101, string authorName = "John Doe")
        {
            return new CommentDto
            {
                Id = id,
                StreetcodeId = streetcodeId,
                AuthorName = authorName,
                Content = "This is a test comment.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                ParentCommentId = null,
            };
        }

        /// <summary>
        /// Creates a single <see cref="CreateCommentDto"/> instance with predefined values.
        /// </summary>
        /// <param name="streetcodeId">The streetcode ID for the new comment.</param>
        /// <param name="authorName">The author name for the new comment.</param>
        /// <returns>A fully initialized <see cref="CreateCommentDto"/> object for testing.</returns>
        public static CreateCommentDto CreateCreateCommentDto(int streetcodeId = 101, string authorName = "John Doe")
        {
            return new CreateCommentDto
            {
                StreetcodeId = streetcodeId,
                AuthorName = authorName,
                Content = "This is a test comment.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                ParentCommentId = null,
            };
        }
    }
}