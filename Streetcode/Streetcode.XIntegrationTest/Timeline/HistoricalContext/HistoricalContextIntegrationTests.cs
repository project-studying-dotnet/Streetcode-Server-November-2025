namespace Streetcode.XIntegrationTest.Timeline.HistoricalContext
{
    using System.Net;
    using System.Net.Http.Json;
    using Streetcode.BLL.DTO.Timeline;
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
            var createDto = new CreateHistoricalContextDto
            {
                Title = new string('А', 51), // 51 characters, exceeds max of 50
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
            var createDto = new CreateHistoricalContextDto
            {
                Title = new string('А', 50), // Exactly 50 characters (max allowed)
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(50, result.Title.Length);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithNumerals_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Контекст 123", // Contains numerals
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
                Title = "Контекст!@#", // Contains special characters
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
                Title = "Києво-Русь", // Contains hyphen
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
                Title = "Давня Україна",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Давня Україна", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithLatinLetters_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Ancient History",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Ancient History", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithMixedCyrillicAndLatin_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Україна Ukraine",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Україна Ukraine", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithUkrainianSpecificLetters_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Їжак і єнот у Ґданську",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Їжак і єнот у Ґданську", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_WithMultipleSpaces_CreatesSuccessfully()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Період   між   війнами",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Період   між   війнами", result.Title);
        }

        [Fact]
        public async Task CreateHistoricalContext_VerifiesInDatabase()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Нова епоха",
            };

            // Act
            var (response, result) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(
                BaseUrl,
                createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Verify in database
            var dbContext = this.ExecuteWithContext(db =>
                db.HistoricalContexts.FirstOrDefault(c => c.Title == "Нова епоха"));
            
            Assert.NotNull(dbContext);
            Assert.Equal("Нова епоха", dbContext.Title);
            Assert.True(dbContext.Id > 0);
        }

        [Fact]
        public async Task CreateHistoricalContext_MultipleContexts_AllCreatedIndependently()
        {
            // Arrange
            var createDto1 = new CreateHistoricalContextDto { Title = "Перший контекст" };
            var createDto2 = new CreateHistoricalContextDto { Title = "Другий контекст" };
            var createDto3 = new CreateHistoricalContextDto { Title = "Третій контекст" };

            // Act
            var (response1, result1) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto1);
            var (response2, result2) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto2);
            var (response3, result3) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto3);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response2.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response3.StatusCode);

            // Verify all contexts exist in database
            var allContexts = this.ExecuteWithContext(db => db.HistoricalContexts.ToList());
            Assert.Equal(3, allContexts.Count);
            Assert.Contains(allContexts, c => c.Title == "Перший контекст");
            Assert.Contains(allContexts, c => c.Title == "Другий контекст");
            Assert.Contains(allContexts, c => c.Title == "Третій контекст");
        }

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
                Title = "Context 1",
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

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
        public async Task GetHistoricalContextById_WithNonExistentId_ReturnsNotFound()
        {
            // Act
            var response = await this.Client.GetAsync($"{BaseUrl}/999");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
