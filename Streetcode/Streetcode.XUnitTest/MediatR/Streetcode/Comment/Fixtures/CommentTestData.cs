namespace Streetcode.XUnitTest.MediatR.Comments.Fixtures
{
    using BLL.DTO.Streetcode.Comments;
    using DAL.Entities.Streetcode;

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
        /// Creates a predefined hierarchy of <see cref="Comment"/> entities for testing purposes.
        /// </summary>
        /// <param name="streetcodeId">
        /// The identifier of the streetcode to which the comments belong.
        /// </param>
        /// <returns>
        /// A list of <see cref="Comment"/> entities representing a comment hierarchy
        /// suitable for unit and integration tests.
        /// </returns>
        public static List<Comment> CreateCommentsHierarchy(int streetcodeId = 101)
        {
            return new ()
            {
                // ===== Root comments =====
                new Comment
                {
                    AuthorName = "Дмитро",
                    Content = "Сильна і дуже болюча історія. Світла памʼять Роману.",
                },
                new Comment
                {
                    AuthorName = "Наталія",
                    Content = "Такі люди формують нову Україну. Дуже важливо про них писати.",
                },

                // ===== Level 1 replies =====
                new Comment
                {
                    AuthorName = "Ірина",
                    Content = "Погоджуюсь. Його боротьба за Протасів Яр — приклад для багатьох.",
                    ParentCommentId = 10,
                },
                new Comment
                {
                    AuthorName = "Олександр",
                    Content = "Особливо вражає, що він був таким молодим.",
                    ParentCommentId = 10,
                },

                // ===== Level 2 replies =====
                new Comment
                {
                    AuthorName = "Катерина",
                    Content = "Вік не має значення, коли є принципи та відповідальність.",
                    ParentCommentId = 13,
                },
                new Comment
                {
                    AuthorName = "Богдан",
                    Content = "Роман показав, що громадянська позиція — це не просто слова.",
                    ParentCommentId = 12,
                },

                // ===== Level 3 replies =====
                new Comment
                {
                    AuthorName = "Тарас",
                    Content = "Саме таких людей боїться корумпована система.",
                    ParentCommentId = 15,
                },

                // ===== Level 4 replies =====
                new Comment
                {
                    AuthorName = "Леся",
                    Content = "І водночас саме завдяки таким людям вона рано чи пізно падає.",
                    ParentCommentId = 16,
                },
            };
        }

        /// <summary>
        /// Creates a predefined hierarchy of <see cref="CommentDto"/> objects for testing purposes.
        /// </summary>
        /// <param name="streetcodeId">
        /// The identifier of the streetcode associated with the comment DTOs.
        /// </param>
        /// <returns>
        /// A list of <see cref="CommentDto"/> instances representing a multi-level
        /// comment hierarchy suitable for handler unit tests.
        /// </returns>
        public static List<CommentDto> CreateCommentsDtosHierarchy(int streetcodeId = 101)
        {
            var now = DateTime.UtcNow;

            return new List<CommentDto>
            {
                // ===== Root comments =====
                new CommentDto
                {
                    Id = 10,
                    StreetcodeId = streetcodeId,
                    AuthorName = "Дмитро",
                    Content = "Сильна і дуже болюча історія. Світла памʼять Роману.",
                    CreatedAt = now,
                },
                new CommentDto
                {
                    Id = 11,
                    StreetcodeId = streetcodeId,
                    AuthorName = "Наталія",
                    Content = "Такі люди формують нову Україну. Дуже важливо про них писати.",
                    CreatedAt = now,
                },

                // ===== Level 1 replies =====
                new CommentDto
                {
                    Id = 12,
                    StreetcodeId = streetcodeId,
                    AuthorName = "Ірина",
                    Content = "Погоджуюсь. Його боротьба за Протасів Яр — приклад для багатьох.",
                    ParentCommentId = 10,
                    CreatedAt = now,
                },
                new CommentDto
                {
                    Id = 13,
                    StreetcodeId = streetcodeId,
                    AuthorName = "Олександр",
                    Content = "Особливо вражає, що він був таким молодим.",
                    ParentCommentId = 10,
                    CreatedAt = now,
                },

                // ===== Level 2 replies =====
                new CommentDto
                {
                    Id = 14,
                    StreetcodeId = streetcodeId,
                    AuthorName = "Катерина",
                    Content = "Вік не має значення, коли є принципи та відповідальність.",
                    ParentCommentId = 13,
                    CreatedAt = now,
                },
                new CommentDto
                {
                    Id = 15,
                    StreetcodeId = streetcodeId,
                    AuthorName = "Богдан",
                    Content = "Роман показав, що громадянська позиція — це не просто слова.",
                    ParentCommentId = 12,
                    CreatedAt = now,
                },

                // ===== Level 3 replies =====
                new CommentDto
                {
                    Id = 16,
                    StreetcodeId = streetcodeId,
                    AuthorName = "Тарас",
                    Content = "Саме таких людей боїться корумпована система.",
                    ParentCommentId = 15,
                    CreatedAt = now,
                },

                // ===== Level 4 replies =====
                new CommentDto
                {
                    Id = 17,
                    StreetcodeId = streetcodeId,
                    AuthorName = "Леся",
                    Content = "І водночас саме завдяки таким людям вона рано чи пізно падає.",
                    ParentCommentId = 16,
                    CreatedAt = now,
                },
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

        /// <summary>
        /// Creates a <see cref="CommentDto"/> with multiple replies for testing purposes.
        /// </summary>
        /// <param name="id">The ID of the parent comment DTO.</param>
        /// <param name="streetcodeId">The streetcode ID associated with the comment.</param>
        /// <param name="authorName">The author name of the parent comment.</param>
        /// <returns>A <see cref="CommentDto"/> object with nested replies.</returns>
        public static CommentDto CreateCommentDtoWithReplies(int id = 1, int streetcodeId = 101, string authorName = "John Doe")
        {
            var now = DateTime.UtcNow;

            return new CommentDto
            {
                Id = id,
                StreetcodeId = streetcodeId,
                AuthorName = authorName,
                Content = "This is a parent comment.",
                CreatedAt = now,
                UpdatedAt = null,
                ParentCommentId = null,
                Replies = new List<CommentDto>
                {
                    new CommentDto
                    {
                        Id = id + 1,
                        StreetcodeId = streetcodeId,
                        AuthorName = "Jane Smith",
                        Content = "This is the first reply.",
                        CreatedAt = now,
                        UpdatedAt = null,
                        ParentCommentId = id,
                        Replies = new List<CommentDto>(),
                    },
                    new CommentDto
                    {
                        Id = id + 2,
                        StreetcodeId = streetcodeId,
                        AuthorName = "Bob Johnson",
                        Content = "This is the second reply.",
                        CreatedAt = now,
                        UpdatedAt = null,
                        ParentCommentId = id,
                        Replies = new List<CommentDto>(),
                    },
                    new CommentDto
                    {
                        Id = id + 3,
                        StreetcodeId = streetcodeId,
                        AuthorName = "Alice Brown",
                        Content = "This is the third reply.",
                        CreatedAt = now,
                        UpdatedAt = null,
                        ParentCommentId = id,
                        Replies = new List<CommentDto>(),
                    },
                },
            };
        }

        /// <summary>
        /// Creates a <see cref="Comment"/> entity with multiple replies for testing purposes.
        /// </summary>
        /// <param name="id">The ID of the parent comment.</param>
        /// <param name="streetcodeId">The streetcode ID associated with the comment.</param>
        /// <param name="authorName">The author name of the parent comment.</param>
        /// <returns>A <see cref="Comment"/> object with nested replies.</returns>
        public static Comment CreateCommentWithReplies(int id = 1, int streetcodeId = 101, string authorName = "John Doe")
        {
            var now = DateTime.UtcNow;

            var parentComment = new Comment
            {
                Id = id,
                StreetcodeId = streetcodeId,
                AuthorName = authorName,
                Content = "This is a parent comment.",
                CreatedAt = now,
                UpdatedAt = null,
                ParentCommentId = null,
                Streetcode = null,
                ParentComment = null,
                IsDeleted = false,
                Replies = new List<Comment>(),
            };

            var reply1 = new Comment
            {
                Id = id + 1,
                StreetcodeId = streetcodeId,
                AuthorName = "Jane Smith",
                Content = "This is the first reply.",
                CreatedAt = now,
                UpdatedAt = null,
                ParentCommentId = id,
                ParentComment = parentComment,
                Streetcode = null,
                IsDeleted = false,
                Replies = new List<Comment>(),
            };

            var reply2 = new Comment
            {
                Id = id + 2,
                StreetcodeId = streetcodeId,
                AuthorName = "Bob Johnson",
                Content = "This is the second reply.",
                CreatedAt = now,
                UpdatedAt = null,
                ParentCommentId = id,
                ParentComment = parentComment,
                Streetcode = null,
                IsDeleted = false,
                Replies = new List<Comment>(),
            };

            var reply3 = new Comment
            {
                Id = id + 3,
                StreetcodeId = streetcodeId,
                AuthorName = "Alice Brown",
                Content = "This is the third reply.",
                CreatedAt = now,
                UpdatedAt = null,
                ParentCommentId = id,
                ParentComment = parentComment,
                Streetcode = null,
                IsDeleted = false,
                Replies = new List<Comment>(),
            };

            parentComment.Replies.Add(reply1);
            parentComment.Replies.Add(reply2);
            parentComment.Replies.Add(reply3);

            return parentComment;
        }
    }
}