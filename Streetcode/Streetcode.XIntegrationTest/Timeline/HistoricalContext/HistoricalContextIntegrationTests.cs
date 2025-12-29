namespace Streetcode.XIntegrationTest.Timeline.HistoricalContext
{
    using System.Net;
    using System.Net.Http.Json;
    using Microsoft.EntityFrameworkCore;
    using Streetcode.BLL.DTO.Timeline;
    using Streetcode.DAL.Entities.Timeline;
    using Streetcode.XIntegrationTest.Base;
    using Streetcode.XIntegrationTest.Timeline.Fixtures;
    using Xunit;

    /// <summary>
    /// Integration tests for HistoricalContext CRUD operations.
    /// </summary>
    public class HistoricalContextIntegrationTests : BaseIntegrationTest<Program>
    {
        private const string BaseUrl = "/api/HistoricalContext";

        public HistoricalContextIntegrationTests()
            : base()
        {
        }

        #region GET Tests

        [Fact]
        public async Task GetAllHistoricalContexts_ReturnsAllContexts()
        {
            // Arrange
            var context1 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(1, "Context 1");
            var context2 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(2, "Context 2");
            var context3 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(3, "Context 3");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context1);
                db.HistoricalContexts.Add(context2);
                db.HistoricalContexts.Add(context3);
            });

            // Act
            var result = await this.GetAsync<List<HistoricalContextDto>>(BaseUrl);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetHistoricalContextById_WithExistingId_ReturnsContext()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Test Context");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            // Act
            var result = await this.GetAsync<HistoricalContextDto>($"{BaseUrl}/{contextId}");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contextId, result.Id);
            Assert.Equal("Test Context", result.Title);
        }

        [Fact]
        public async Task GetHistoricalContextById_WithNonExistentId_ReturnsNotFound()
        {
            // Act
            var response = await this.Client.GetAsync($"{BaseUrl}/999");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region CREATE Tests

        [Fact]
        public async Task CreateHistoricalContext_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Новий історичний контекст",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(createDto.Title, result.Title);

            // Verify in database
            var dbContext = this.ExecuteWithContext(db =>
                db.HistoricalContexts.FirstOrDefault(c => c.Title == createDto.Title));
            
            Assert.NotNull(dbContext);
            Assert.Equal(createDto.Title, dbContext.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithDuplicateTitle_ReturnsBadRequest()
        {
            // Arrange
            var existingContext = TimelineIntegrationTestData.CreateSimpleHistoricalContext(1, "Existing Context");
            
            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var createDto = new CreateHistoricalContextDto
            {
                Title = "Existing Context",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithEmptyTitle_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = string.Empty,
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithNullTitle_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = null!,
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithWhitespaceTitle_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "   ",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithTitleTooLong_ReturnsBadRequest()
        {
            // Arrange
            var longTitle = new string('а', 51); // Exceeds max length of 50
            var createDto = new CreateHistoricalContextDto
            {
                Title = longTitle,
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithMaxLengthTitle_CreatesSuccessfully()
        {
            // Arrange
            var maxLengthTitle = new string('а', 50); // Exactly max length
            var createDto = new CreateHistoricalContextDto
            {
                Title = maxLengthTitle,
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(maxLengthTitle, result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithNumerals_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Контекст 123",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithSpecialCharacters_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Контекст@#$",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithHyphen_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Контекст-тест",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithCyrillicLetters_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Кириличний контекст",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Кириличний контекст", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithLatinLetters_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Latin Context",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Latin Context", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithMixedCyrillicAndLatin_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Мішаний Mixed контекст",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Мішаний Mixed контекст", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithUkrainianSpecificLetters_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Контекст із ґ є ї і",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Контекст із ґ є ї і", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithMultipleSpaces_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Контекст  з  пробілами",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateHistoricalContext_VerifiesInDatabase()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Database Test Context",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            var dbContext = this.ExecuteWithContext(db =>
                db.HistoricalContexts.FirstOrDefault(c => c.Id == result.Id));
            
            Assert.NotNull(dbContext);
            Assert.Equal(result.Title, dbContext.Title);
            Assert.Equal(result.Id, dbContext.Id);
        }

        [Fact]
        public async Task CreateHistoricalContext_MultipleContexts_AllCreatedIndependently()
        {
            // Arrange
            var createDto1 = new CreateHistoricalContextDto { Title = "First Context" };
            var createDto2 = new CreateHistoricalContextDto { Title = "Second Context" };
            var createDto3 = new CreateHistoricalContextDto { Title = "Third Context" };

            // Act
            var (response1, result1) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto1);
            var (response2, result2) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto2);
            var (response3, result3) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto3);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response2.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response3.StatusCode);

            var allContexts = this.ExecuteWithContext(db => db.HistoricalContexts.ToList());
            Assert.Equal(3, allContexts.Count);
        }

        #endregion

        #region CREATE Validation Edge Cases

        [Fact]
        public async Task CreateHistoricalContext_WithSingleCharacter_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "А",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("А", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithTwoCharacters_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Аб",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Аб", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_With27Characters_CreatesSuccessfully()
        {
            // Arrange
            var title27 = new string('а', 27);
            var createDto = new CreateHistoricalContextDto
            {
                Title = title27,
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(title27, result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_With28Characters_CreatesSuccessfully()
        {
            // Arrange
            var title28 = new string('а', 28);
            var createDto = new CreateHistoricalContextDto
            {
                Title = title28,
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(title28, result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_With29Characters_CreatesSuccessfully()
        {
            // Arrange
            var title29 = new string('а', 29);
            var createDto = new CreateHistoricalContextDto
            {
                Title = title29,
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(title29, result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_With49Characters_CreatesSuccessfully()
        {
            // Arrange
            var title49 = new string('а', 49);
            var createDto = new CreateHistoricalContextDto
            {
                Title = title49,
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(title49, result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithLeadingWhitespace_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "  Leading spaces",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithTrailingWhitespace_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Trailing spaces  ",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithTab_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context\twith tab",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithNewline_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context\nwith newline",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithCarriageReturn_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context\rwith return",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithPeriod_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context.",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithComma_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context, test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithExclamationMark_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context!",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithQuestionMark_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context?",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithColon_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context: test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithSemicolon_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context; test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithParentheses_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context (test)",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithQuotes_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context \"test\"",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithApostrophe_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context's test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithUnderscore_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context_test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithSlash_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context/test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithBackslash_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context\\test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithAmpersand_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context & test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithPercent_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context 100%",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithDollarSign_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context $100",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithPlusSign_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context + test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithEqualsSign_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context = test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithBrackets_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context [test]",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithCurlyBraces_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context {test}",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithAngleBrackets_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context <test>",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithPipe_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context | test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithTilde_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context ~ test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithBacktick_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context ` test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithCaret_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context ^ test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithAsterisk_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Context * test",
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region UPDATE Tests

        [Fact]
        public async Task UpdateHistoricalContext_WithValidData_UpdatesSuccessfully()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Оновлена назва",
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Оновлена назва", result.Title);

            // Verify in database
            var dbContext = this.ExecuteWithContext(db =>
                db.HistoricalContexts.FirstOrDefault(c => c.Id == contextId));
            
            Assert.NotNull(dbContext);
            Assert.Equal("Оновлена назва", dbContext.Title);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = 999,
                Title = "Non-existent Context",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithDuplicateTitle_ReturnsBadRequest()
        {
            // Arrange
            var context1 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(1, "Context 1");
            var context2 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(2, "Context 2");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context1);
                db.HistoricalContexts.Add(context2);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = 2,
                Title = "Context 1", // Duplicate title
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithEmptyTitle_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = string.Empty,
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithNullTitle_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = null!,
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithWhitespaceTitle_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "   ",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithTitleTooLong_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var longTitle = new string('а', 51); // Exceeds max length
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = longTitle,
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithMaxLengthTitle_UpdatesSuccessfully()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var maxLengthTitle = new string('а', 50); // Exactly max length
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = maxLengthTitle,
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(maxLengthTitle, result.Title);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithNumerals_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Context 123",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithSpecialCharacters_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Context@#$",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithZeroId_ReturnsBadRequest()
        {
            // Arrange
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = 0,
                Title = "Updated Title",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithNegativeId_ReturnsBadRequest()
        {
            // Arrange
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = -1,
                Title = "Updated Title",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_SameTitle_UpdatesSuccessfully()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Same Title");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Same Title", // Same as original
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Same Title", result.Title);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithAssociatedTimelineItems_UpdatesSuccessfully()
        {
            // Arrange
            var contextId = 1;
            var streetcodeId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original Context");
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(1, streetcodeId, "Timeline Item");
            
            timelineItem.HistoricalContextTimelines = new List<HistoricalContextTimeline>
            {
                new HistoricalContextTimeline { HistoricalContextId = contextId, TimelineId = 1 },
            };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated Context",
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Updated Context", result.Title);

            // Verify timeline relationship still exists
            var relationship = this.ExecuteWithContext(db =>
                db.HistoricalContextsTimelines
                    .FirstOrDefault(hct => hct.HistoricalContextId == contextId && hct.TimelineId == 1));
            
            Assert.NotNull(relationship);
        }

        #endregion

        #region UPDATE Validation Edge Cases

        [Fact]
        public async Task UpdateHistoricalContext_WithSingleCharacter_UpdatesSuccessfully()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Б",
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Б", result.Title);
        }

        [Fact]
        public async Task UpdateHistoricalContext_With27Characters_UpdatesSuccessfully()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var title27 = new string('б', 27);
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = title27,
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(title27, result.Title);
        }

        [Fact]
        public async Task UpdateHistoricalContext_With28Characters_UpdatesSuccessfully()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var title28 = new string('б', 28);
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = title28,
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(title28, result.Title);
        }

        [Fact]
        public async Task UpdateHistoricalContext_With29Characters_UpdatesSuccessfully()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var title29 = new string('б', 29);
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = title29,
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(title29, result.Title);
        }

        [Fact]
        public async Task UpdateHistoricalContext_With49Characters_UpdatesSuccessfully()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var title49 = new string('б', 49);
            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = title49,
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(title49, result.Title);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithLeadingWhitespace_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "  Leading",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithTrailingWhitespace_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Trailing  ",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithTab_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Update\ttab",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithNewline_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Update\nnewline",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithPeriod_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated.",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithComma_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated, test",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithExclamation_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated!",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithQuestionMark_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated?",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithParentheses_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated (test)",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithApostrophe_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Context's update",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithSlash_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated/test",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithUnderscore_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated_test",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithBrackets_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated [test]",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateHistoricalContext_WithAmpersand_ReturnsBadRequest()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Original");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = contextId,
                Title = "Updated & test",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region DELETE Tests

        [Fact]
        public async Task DeleteHistoricalContext_WithExistingId_DeletesSuccessfully()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Context to Delete");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{contextId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Verify deletion in database
            var dbContext = this.ExecuteWithContext(db =>
                db.HistoricalContexts.FirstOrDefault(c => c.Id == contextId));
            
            Assert.Null(dbContext);
        }

        [Fact]
        public async Task DeleteHistoricalContext_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange & Act
            var response = await this.DeleteAsync($"{BaseUrl}/999");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteHistoricalContext_WithZeroId_ReturnsBadRequest()
        {
            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/0");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteHistoricalContext_WithNegativeId_ReturnsBadRequest()
        {
            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/-1");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteHistoricalContext_WithAssociatedTimelineItems_DeletesContextAndRelationships()
        {
            // Arrange
            var contextId = 1;
            var streetcodeId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Context with Timeline");
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem1 = TimelineIntegrationTestData.CreateSimpleTimelineItem(1, streetcodeId, "Item 1");
            var timelineItem2 = TimelineIntegrationTestData.CreateSimpleTimelineItem(2, streetcodeId, "Item 2");

            timelineItem1.HistoricalContextTimelines = new List<HistoricalContextTimeline>
            {
                new HistoricalContextTimeline { HistoricalContextId = contextId, TimelineId = 1 },
            };

            timelineItem2.HistoricalContextTimelines = new List<HistoricalContextTimeline>
            {
                new HistoricalContextTimeline { HistoricalContextId = contextId, TimelineId = 2 },
            };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem1);
                db.TimelineItems.Add(timelineItem2);
            });

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{contextId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Verify context is deleted
            var dbContext = this.ExecuteWithContext(db =>
                db.HistoricalContexts.FirstOrDefault(c => c.Id == contextId));
            Assert.Null(dbContext);

            // Verify relationships are deleted
            var relationships = this.ExecuteWithContext(db =>
                db.HistoricalContextsTimelines
                    .Where(hct => hct.HistoricalContextId == contextId)
                    .ToList());
            Assert.Empty(relationships);

            // Verify timeline items still exist (not cascade deleted)
            var timelineItems = this.ExecuteWithContext(db =>
                db.TimelineItems.Where(t => t.Id == 1 || t.Id == 2).ToList());
            Assert.Equal(2, timelineItems.Count);
        }

        [Fact]
        public async Task DeleteHistoricalContext_MultipleContexts_DeletesOnlySpecifiedContext()
        {
            // Arrange
            var context1 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(1, "Context 1");
            var context2 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(2, "Context 2");
            var context3 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(3, "Context 3");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context1);
                db.HistoricalContexts.Add(context2);
                db.HistoricalContexts.Add(context3);
            });

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/2");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var remainingContexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.ToList());
            
            Assert.Equal(2, remainingContexts.Count);
            Assert.Contains(remainingContexts, c => c.Id == 1);
            Assert.Contains(remainingContexts, c => c.Id == 3);
            Assert.DoesNotContain(remainingContexts, c => c.Id == 2);
        }

        [Fact]
        public async Task DeleteHistoricalContext_DeleteTwice_SecondAttemptReturnsNotFound()
        {
            // Arrange
            var contextId = 1;
            var context = TimelineIntegrationTestData.CreateSimpleHistoricalContext(contextId, "Context");

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context);
            });

            // Act
            var firstResponse = await this.DeleteAsync($"{BaseUrl}/{contextId}");
            var secondResponse = await this.DeleteAsync($"{BaseUrl}/{contextId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, firstResponse.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, secondResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteHistoricalContext_WithMaxIntId_ReturnsNotFound()
        {
            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{int.MaxValue}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region Concurrent Operations and Race Conditions Tests

        [Fact]
        public async Task CreateHistoricalContext_ConcurrentRequestsWithSameTitle_OnlyOneSucceeds()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Конкурентний Контекст",
            };

            // Act - Execute 5 concurrent requests with the same title
            var tasks = Enumerable.Range(0, 5)
                .Select(_ => this.Client.PostAsJsonAsync(BaseUrl, createDto))
                .ToArray();

            var responses = await Task.WhenAll(tasks);

            // Assert
            var successResponses = responses.Where(r => r.StatusCode == HttpStatusCode.OK).ToList();
            var failureResponses = responses.Where(r => r.StatusCode == HttpStatusCode.BadRequest).ToList();

            // Exactly one should succeed, others should fail due to duplicate title
            Assert.Single(successResponses);
            Assert.Equal(4, failureResponses.Count);

            // Verify only one entry in database
            var dbContexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.Where(hc => hc.Title == "Конкурентний Контекст").ToList());

            Assert.Single(dbContexts);
        }

        [Fact]
        public async Task CreateHistoricalContext_ConcurrentRequestsWithDifferentTitles_AllSucceed()
        {
            // Arrange - Create 10 concurrent requests with different titles
            var tasks = Enumerable.Range(1, 10)
                .Select(i => new CreateHistoricalContextDto
                {
                    Title = $"Контекст Номер {i}",
                })
                .Select(dto => this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, dto))
                .ToArray();

            // Act
            var results = await Task.WhenAll(tasks);

            // Assert
            Assert.All(results, result =>
            {
                Assert.Equal(HttpStatusCode.OK, result.Item1.StatusCode);
                Assert.NotNull(result.Item2);
            });

            // Verify all 10 entries in database
            var dbContexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.Where(hc => hc.Title.StartsWith("Контекст Номер")).ToList());

            Assert.Equal(10, dbContexts.Count);
        }

        [Fact]
        public async Task UpdateHistoricalContext_ConcurrentUpdatesToSameTitle_OnlyOneSucceeds()
        {
            // Arrange
            var context1 = new CreateHistoricalContextDto { Title = "Контекст Перший" };
            var context2 = new CreateHistoricalContextDto { Title = "Контекст Другий" };
            var context3 = new CreateHistoricalContextDto { Title = "Контекст Третій" };

            var (_, result1) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, context1);
            var (_, result2) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, context2);
            var (_, result3) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, context3);

            // Try to update all three to the same title concurrently
            var targetTitle = "Цільовий Заголовок";
            var updateDto1 = new UpdateHistoricalContextDto { Id = result1.Id, Title = targetTitle };
            var updateDto2 = new UpdateHistoricalContextDto { Id = result2.Id, Title = targetTitle };
            var updateDto3 = new UpdateHistoricalContextDto { Id = result3.Id, Title = targetTitle };

            // Act - Execute concurrent updates
            var tasks = new[]
            {
                this.Client.PutAsJsonAsync(BaseUrl, updateDto1),
                this.Client.PutAsJsonAsync(BaseUrl, updateDto2),
                this.Client.PutAsJsonAsync(BaseUrl, updateDto3),
            };

            var responses = await Task.WhenAll(tasks);

            // Assert
            var successResponses = responses.Where(r => r.StatusCode == HttpStatusCode.OK).ToList();
            var failureResponses = responses.Where(r => r.StatusCode == HttpStatusCode.BadRequest).ToList();

            // Exactly one should succeed, others should fail due to duplicate title
            Assert.Single(successResponses);
            Assert.Equal(2, failureResponses.Count);

            // Verify only one has the target title
            var dbContexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.Where(hc => hc.Title == targetTitle).ToList());

            Assert.Single(dbContexts);

            // Verify total count is still 3
            var totalContexts = this.ExecuteWithContext(db => db.HistoricalContexts.Count());
            Assert.Equal(3, totalContexts);
        }

        [Fact]
        public async Task CreateHistoricalContext_RaceConditionWithExistingTitle_SecondRequestFails()
        {
            // Arrange - Create initial context
            var initialDto = new CreateHistoricalContextDto { Title = "Існуючий Контекст" };
            await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, initialDto);

            // Try to create duplicates concurrently
            var duplicateDto = new CreateHistoricalContextDto { Title = "Існуючий Контекст" };

            // Act - Execute 3 concurrent duplicate requests
            var tasks = Enumerable.Range(0, 3)
                .Select(_ => this.Client.PostAsJsonAsync(BaseUrl, duplicateDto))
                .ToArray();

            var responses = await Task.WhenAll(tasks);

            // Assert - All should fail since the title already exists
            Assert.All(responses, response =>
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });

            // Verify only one entry exists
            var dbContexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.Where(hc => hc.Title == "Існуючий Контекст").ToList());

            Assert.Single(dbContexts);
        }

        [Fact]
        public async Task CreateAndUpdateHistoricalContext_ConcurrentOperations_MaintainsDataIntegrity()
        {
            // Arrange - Create initial context
            var createDto = new CreateHistoricalContextDto { Title = "Початковий Контекст" };
            var (_, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);

            // Prepare concurrent operations: creates and updates
            var tasks = new List<Task<HttpResponseMessage>>
            {
                // Try to create with same title (should fail)
                this.Client.PostAsJsonAsync(BaseUrl, new CreateHistoricalContextDto { Title = "Початковий Контекст" }),
                
                // Try to create with different titles (should succeed)
                this.Client.PostAsJsonAsync(BaseUrl, new CreateHistoricalContextDto { Title = "Новий Контекст Один" }),
                this.Client.PostAsJsonAsync(BaseUrl, new CreateHistoricalContextDto { Title = "Новий Контекст Два" }),
                
                // Try to update to existing title (should fail)
                this.Client.PutAsJsonAsync(BaseUrl, new UpdateHistoricalContextDto { Id = result.Id, Title = "Початковий Контекст" }),
                
                // Try to update to new title (should succeed)
                this.Client.PutAsJsonAsync(BaseUrl, new UpdateHistoricalContextDto { Id = result.Id, Title = "Оновлений Контекст" }),
            };

            // Act
            var responses = await Task.WhenAll(tasks);

            // Assert
            var statusCodes = responses.Select(r => r.StatusCode).ToList();

            // Verify expected success/failure pattern
            var successCount = statusCodes.Count(sc => sc == HttpStatusCode.OK);
            var failureCount = statusCodes.Count(sc => sc == HttpStatusCode.BadRequest);

            // Should have some successes and some failures
            Assert.True(successCount >= 2); // At least the 2 new creates
            Assert.True(failureCount >= 1); // At least the duplicate create

            // Verify database integrity
            var allContexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.ToList());

            // Should have at least 3 contexts (initial + 2 new ones)
            Assert.True(allContexts.Count >= 3);

            // Verify all titles are unique
            var titles = allContexts.Select(c => c.Title).ToList();
            Assert.Equal(titles.Count, titles.Distinct().Count());
        }

        [Fact]
        public async Task CreateHistoricalContext_HighConcurrencyWithSameTitle_DatabaseConstraintEnforced()
        {
            // Arrange - Simulate high concurrency (20 simultaneous requests)
            var title = "Високо Конкурентний Заголовок";
            var createDto = new CreateHistoricalContextDto { Title = title };

            // Act - Execute 20 concurrent requests with the same title
            var tasks = Enumerable.Range(0, 20)
                .Select(_ => this.Client.PostAsJsonAsync(BaseUrl, createDto))
                .ToArray();

            var responses = await Task.WhenAll(tasks);

            // Assert
            var successCount = responses.Count(r => r.StatusCode == HttpStatusCode.OK);
            var failureCount = responses.Count(r => r.StatusCode == HttpStatusCode.BadRequest);

            // Exactly one should succeed, all others should fail
            Assert.Equal(1, successCount);
            Assert.Equal(19, failureCount);

            // Verify database has exactly one entry
            var dbContexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.Where(hc => hc.Title == title).ToList());

            Assert.Single(dbContexts);
        }

        [Fact]
        public async Task UpdateHistoricalContext_ConcurrentUpdatesWithDifferentTitles_AllSucceed()
        {
            // Arrange - Create 5 contexts
            var contexts = new List<HistoricalContextDto>();
            for (int i = 1; i <= 5; i++)
            {
                var createDto = new CreateHistoricalContextDto { Title = $"Контекст {i}" };
                var (_, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);
                contexts.Add(result);
            }

            // Prepare concurrent updates with different titles
            var updateTasks = contexts.Select((ctx, index) =>
                this.Client.PutAsJsonAsync(BaseUrl, new UpdateHistoricalContextDto
                {
                    Id = ctx.Id,
                    Title = $"Оновлений Контекст {index + 1}",
                })
            ).ToArray();

            // Act
            var responses = await Task.WhenAll(updateTasks);

            // Assert - All should succeed
            Assert.All(responses, response =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            });

            // Verify all titles are updated and unique
            var dbContexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.Where(hc => hc.Title.StartsWith("Оновлений Контекст")).ToList());

            Assert.Equal(5, dbContexts.Count);
            
            var titles = dbContexts.Select(c => c.Title).ToList();
            Assert.Equal(titles.Count, titles.Distinct().Count());
        }

        [Fact]
        public async Task CreateHistoricalContext_ConcurrentWithValidationErrors_HandlesGracefully()
        {
            // Arrange - Mix of valid and invalid titles
            var tasks = new List<Task<HttpResponseMessage>>
            {
                // Valid titles
                this.Client.PostAsJsonAsync(BaseUrl, new CreateHistoricalContextDto { Title = "Валідний Заголовок Один" }),
                this.Client.PostAsJsonAsync(BaseUrl, new CreateHistoricalContextDto { Title = "Валідний Заголовок Два" }),
                
                // Invalid: too long
                this.Client.PostAsJsonAsync(BaseUrl, new CreateHistoricalContextDto 
                { 
                    Title = new string('А', 51) // 51 characters, exceeds max of 50
                }),
                
                // Invalid: contains numbers
                this.Client.PostAsJsonAsync(BaseUrl, new CreateHistoricalContextDto { Title = "Заголовок123" }),
                
                // Invalid: empty
                this.Client.PostAsJsonAsync(BaseUrl, new CreateHistoricalContextDto { Title = "" }),
                
                // Valid title
                this.Client.PostAsJsonAsync(BaseUrl, new CreateHistoricalContextDto { Title = "Валідний Заголовок Три" }),
            };

            // Act
            var responses = await Task.WhenAll(tasks);

            // Assert
            var successResponses = responses.Where(r => r.StatusCode == HttpStatusCode.OK).ToList();
            var failureResponses = responses.Where(r => r.StatusCode == HttpStatusCode.BadRequest).ToList();

            // 3 valid titles should succeed
            Assert.Equal(3, successResponses.Count);
            
            // 3 invalid titles should fail
            Assert.Equal(3, failureResponses.Count);

            // Verify only valid entries in database
            var dbContexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.Where(hc => hc.Title.StartsWith("Валідний")).ToList());

            Assert.Equal(3, dbContexts.Count);
        }

        [Fact]
        public async Task DeleteHistoricalContext_ConcurrentDeletes_OnlyFirstSucceeds()
        {
            // Arrange - Create a context
            var createDto = new CreateHistoricalContextDto { Title = "Контекст Для Видалення" };
            var (_, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);

            // Act - Try to delete concurrently 5 times
            var deleteTasks = Enumerable.Range(0, 5)
                .Select(_ => this.DeleteAsync($"{BaseUrl}/{result.Id}"))
                .ToArray();

            var responses = await Task.WhenAll(deleteTasks);

            // Assert
            var successResponses = responses.Where(r => r.StatusCode == HttpStatusCode.OK).ToList();
            var failureResponses = responses.Where(r => r.StatusCode == HttpStatusCode.BadRequest).ToList();

            // Only one should succeed
            Assert.Single(successResponses);
            Assert.Equal(4, failureResponses.Count);

            // Verify context is deleted
            var dbContext = this.ExecuteWithContext(db =>
                db.HistoricalContexts.FirstOrDefault(hc => hc.Id == result.Id));

            Assert.Null(dbContext);
        }

        [Fact]
        public async Task CreateHistoricalContext_ConcurrentWithSimilarTitles_AllSucceed()
        {
            // Arrange - Create concurrent requests with similar but different titles
            var baseTitles = new[]
            {
                "Середньовіччя",
                "Середньовіччя Ранній Період",
                "Середньовіччя Пізній Період",
                "Середньовіччя в Україні",
                "Середньовіччя Європейське",
            };

            var tasks = baseTitles
                .Select(title => this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                    BaseUrl, 
                    new CreateHistoricalContextDto { Title = title }))
                .ToArray();

            // Act
            var results = await Task.WhenAll(tasks);

            // Assert - All should succeed since titles are different
            Assert.All(results, result =>
            {
                Assert.Equal(HttpStatusCode.OK, result.Item1.StatusCode);
                Assert.NotNull(result.Item2);
            });

            // Verify all 5 contexts exist with correct titles
            var dbContexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.Where(hc => hc.Title.Contains("Середньовіччя")).ToList());

            Assert.Equal(5, dbContexts.Count);
            Assert.Equal(baseTitles.OrderBy(t => t), dbContexts.Select(c => c.Title).OrderBy(t => t));
        }

        #endregion
    }
}
