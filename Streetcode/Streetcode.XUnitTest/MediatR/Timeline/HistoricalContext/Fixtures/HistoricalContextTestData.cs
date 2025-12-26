namespace Streetcode.XUnitTest.MediatR.Timeline.HistoricalContext.Fixtures
{
    using global::Streetcode.BLL.DTO.Timeline;
    using global::Streetcode.DAL.Entities.Timeline;

    /// <summary>
    /// Provides factory methods for creating test instances of <see cref="HistoricalContext"/>,
    /// <see cref="HistoricalContextDto"/>, <see cref="CreateHistoricalContextDto"/>, and
    /// <see cref="UpdateHistoricalContextDto"/> objects for use in unit tests.
    /// </summary>
    public static class HistoricalContextTestData
    {
        /// <summary>
        /// Creates a single <see cref="HistoricalContext"/> entity with predefined values.
        /// </summary>
        /// <param name="id">The ID of the historical context.</param>
        /// <param name="title">The title of the historical context.</param>
        /// <returns>A fully initialized <see cref="HistoricalContext"/> object for testing.</returns>
        public static HistoricalContext CreateHistoricalContext(int id = 1, string? title = null)
        {
            return new HistoricalContext
            {
                Id = id,
                Title = title ?? "Тестовий контекст",
                HistoricalContextTimelines = new List<HistoricalContextTimeline>()
            };
        }

        /// <summary>
        /// Creates a collection of <see cref="HistoricalContext"/> entities.
        /// </summary>
        /// <param name="count">The number of contexts to generate.</param>
        /// <returns>A list of <see cref="HistoricalContext"/> objects.</returns>
        public static List<HistoricalContext> CreateHistoricalContexts(int count = 5)
        {
            var contexts = new List<HistoricalContext>(count);

            for (int i = 0; i < count; i++)
            {
                contexts.Add(new HistoricalContext
                {
                    Id = i + 1,
                    Title = $"Історичний контекст {i + 1}",
                    HistoricalContextTimelines = new List<HistoricalContextTimeline>()
                });
            }

            return contexts;
        }

        /// <summary>
        /// Creates a single <see cref="HistoricalContextDto"/> with predefined values.
        /// </summary>
        /// <param name="id">The ID of the context DTO.</param>
        /// <param name="title">The title of the context.</param>
        /// <returns>A fully initialized <see cref="HistoricalContextDto"/> for testing.</returns>
        public static HistoricalContextDto CreateHistoricalContextDto(int id = 1, string? title = null)
        {
            return new HistoricalContextDto
            {
                Id = id,
                Title = title ?? "Тестовий контекст"
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateHistoricalContextDto"/> with valid test data.
        /// </summary>
        /// <param name="title">The title for the new context.</param>
        /// <returns>A valid <see cref="CreateHistoricalContextDto"/> for testing create operations.</returns>
        public static CreateHistoricalContextDto CreateHistoricalContextCreateDto(string? title = null)
        {
            return new CreateHistoricalContextDto
            {
                Title = title ?? "Новий історичний контекст"
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateHistoricalContextDto"/> with maximum allowed character length (50 chars).
        /// </summary>
        /// <returns>A <see cref="CreateHistoricalContextDto"/> at character boundary.</returns>
        public static CreateHistoricalContextDto CreateHistoricalContextCreateDtoAtMaxLength()
        {
            return new CreateHistoricalContextDto
            {
                Title = new string('А', 50) // Max 50 characters - using Cyrillic 'А'
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateHistoricalContextDto"/> exceeding character limit.
        /// </summary>
        /// <returns>An invalid <see cref="CreateHistoricalContextDto"/> for testing validation.</returns>
        public static CreateHistoricalContextDto CreateHistoricalContextCreateDtoExceedingMaxLength()
        {
            return new CreateHistoricalContextDto
            {
                Title = new string('А', 51) // Exceeds 50 character limit
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateHistoricalContextDto"/> with invalid characters (numerals).
        /// </summary>
        /// <returns>An invalid <see cref="CreateHistoricalContextDto"/> with numerals.</returns>
        public static CreateHistoricalContextDto CreateHistoricalContextWithNumerals()
        {
            return new CreateHistoricalContextDto
            {
                Title = "Context with numbers 123"
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateHistoricalContextDto"/> with invalid characters (special symbols).
        /// </summary>
        /// <returns>An invalid <see cref="CreateHistoricalContextDto"/> with special characters.</returns>
        public static CreateHistoricalContextDto CreateHistoricalContextWithSpecialChars()
        {
            return new CreateHistoricalContextDto
            {
                Title = "Context with symbols @#$"
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateHistoricalContextDto"/> with valid Cyrillic characters.
        /// </summary>
        /// <returns>A valid <see cref="CreateHistoricalContextDto"/> with Cyrillic text.</returns>
        public static CreateHistoricalContextDto CreateHistoricalContextWithCyrillic()
        {
            return new CreateHistoricalContextDto
            {
                Title = "Українська історія"
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateHistoricalContextDto"/> with valid Latin characters.
        /// </summary>
        /// <returns>A valid <see cref="CreateHistoricalContextDto"/> with Latin text.</returns>
        public static CreateHistoricalContextDto CreateHistoricalContextWithLatin()
        {
            return new CreateHistoricalContextDto
            {
                Title = "Historical Context"
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateHistoricalContextDto"/> with mixed valid characters (Cyrillic + Latin).
        /// </summary>
        /// <returns>A valid <see cref="CreateHistoricalContextDto"/> with mixed alphabets.</returns>
        public static CreateHistoricalContextDto CreateHistoricalContextWithMixedAlphabets()
        {
            return new CreateHistoricalContextDto
            {
                Title = "Ukrainian History Українська історія"
            };
        }

        /// <summary>
        /// Creates an <see cref="UpdateHistoricalContextDto"/> with valid test data.
        /// </summary>
        /// <param name="id">The ID of the context to update.</param>
        /// <param name="title">The new title for the context.</param>
        /// <returns>A valid <see cref="UpdateHistoricalContextDto"/> for testing update operations.</returns>
        public static UpdateHistoricalContextDto CreateHistoricalContextUpdateDto(int id = 1, string? title = null)
        {
            return new UpdateHistoricalContextDto
            {
                Id = id,
                Title = title ?? "Оновлений контекст"
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateHistoricalContextDto"/> with empty title.
        /// </summary>
        /// <returns>An invalid <see cref="CreateHistoricalContextDto"/> for testing validation.</returns>
        public static CreateHistoricalContextDto CreateHistoricalContextWithEmptyTitle()
        {
            return new CreateHistoricalContextDto
            {
                Title = string.Empty
            };
        }

        /// <summary>
        /// Creates a <see cref="CreateHistoricalContextDto"/> with whitespace-only title.
        /// </summary>
        /// <returns>An invalid <see cref="CreateHistoricalContextDto"/> for testing validation.</returns>
        public static CreateHistoricalContextDto CreateHistoricalContextWithWhitespaceTitle()
        {
            return new CreateHistoricalContextDto
            {
                Title = "   "
            };
        }

        /// <summary>
        /// Creates multiple <see cref="CreateHistoricalContextDto"/> for testing various validation scenarios.
        /// </summary>
        /// <returns>A list of DTOs with different validation test cases.</returns>
        public static List<CreateHistoricalContextDto> CreateHistoricalContextsForValidationTests()
        {
            return new List<CreateHistoricalContextDto>
            {
                CreateHistoricalContextCreateDto("Valid Context"),
                CreateHistoricalContextCreateDtoAtMaxLength(),
                CreateHistoricalContextCreateDtoExceedingMaxLength(),
                CreateHistoricalContextWithNumerals(),
                CreateHistoricalContextWithSpecialChars(),
                CreateHistoricalContextWithEmptyTitle(),
                CreateHistoricalContextWithWhitespaceTitle()
            };
        }
    }
}
