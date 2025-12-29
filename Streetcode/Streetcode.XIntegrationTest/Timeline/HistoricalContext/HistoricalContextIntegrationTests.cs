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
    }
}
