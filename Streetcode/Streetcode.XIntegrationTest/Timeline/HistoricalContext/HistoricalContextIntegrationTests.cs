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
