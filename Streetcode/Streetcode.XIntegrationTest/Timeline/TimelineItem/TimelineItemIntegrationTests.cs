namespace Streetcode.XIntegrationTest.Timeline.TimelineItem
{
    using System.Net;
    using System.Net.Http.Json;
    using Microsoft.EntityFrameworkCore;
    using Streetcode.BLL.DTO.Timeline;
    using Streetcode.DAL.Entities.Timeline;
    using Streetcode.DAL.Enums;
    using Streetcode.XIntegrationTest.Base;
    using Streetcode.XIntegrationTest.Timeline.Fixtures;
    using Xunit;

    /// <summary>
    /// Integration tests for TimelineItem CRUD operations.
    /// </summary>
    public class TimelineItemIntegrationTests : BaseIntegrationTest<Program>
    {
        private const string BaseUrl = "/api/Timeline";

        public TimelineItemIntegrationTests()
            : base()
        {
        }

        [Fact]
        public async Task GetTimelineItemsByStreetcodeId_WithExistingStreetcode_ReturnsTimelineItems()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var (timelineItems, contexts) = TimelineIntegrationTestData.CreateTimelineTestData(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.HistoricalContexts.AddRange(contexts);
                db.TimelineItems.AddRange(timelineItems);
            });

            // Act
            var result = await this.GetAsync<List<TimelineItemDto>>($"{BaseUrl}/streetcode/{streetcodeId}");

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(timelineItems.Count, result.Count);
        }

        [Fact]
        public async Task GetTimelineItemById_WithExistingId_ReturnsTimelineItem()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Test Event");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            // Act
            var result = await this.GetAsync<TimelineItemDto>($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(timelineItemId, result.Id);
            Assert.Equal("Test Event", result.Title);
        }

        [Fact]
        public async Task CreateTimelineItem_WithValidData_CreatesSuccessfully()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "New Event",
                Description = "Description of new event",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var (response, result) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(createDto.Title, result.Title);
            Assert.Equal(createDto.Description, result.Description);

            // Verify in database
            var dbItem = this.ExecuteWithContext(db =>
                db.TimelineItems.FirstOrDefault(t => t.Title == "New Event"));
            
            Assert.NotNull(dbItem);
            Assert.Equal(createDto.Title, dbItem.Title);
        }

        [Fact]
        public async Task CreateTimelineItem_WithHistoricalContexts_AssociatesContextsCorrectly()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var context1 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(1, "Context 1");
            var context2 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(2, "Context 2");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.HistoricalContexts.Add(context1);
                db.HistoricalContexts.Add(context2);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "Event with Contexts",
                Description = "Event description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int> { 1, 2 },
            };

            // Act
            var (response, result) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.NotNull(result.HistoricalContexts);
            Assert.Equal(2, result.HistoricalContexts.Count());
        }

        [Fact]
        public async Task UpdateTimelineItem_WithValidData_UpdatesSuccessfully()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Updated Title",
                Description = "Updated description",
                Date = new DateTime(2024, 6, 1),
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("Updated Title", result.Title);
            Assert.Equal("Updated description", result.Description);

            // Verify in database
            var dbItem = this.ExecuteWithContext(db =>
                db.TimelineItems.FirstOrDefault(t => t.Id == timelineItemId));
            
            Assert.NotNull(dbItem);
            Assert.Equal("Updated Title", dbItem.Title);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            var updateDto = new UpdateTimelineItemDto
            {
                Id = 999, // Non-existent ID
                Title = "Updated Title",
                Description = "Updated description",
                Date = new DateTime(2024, 6, 1),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = 1,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithZeroId_ReturnsBadRequest()
        {
            // Arrange
            var updateDto = new UpdateTimelineItemDto
            {
                Id = 0,
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = 1,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithNegativeId_ReturnsBadRequest()
        {
            // Arrange
            var updateDto = new UpdateTimelineItemDto
            {
                Id = -1,
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = 1,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithEmptyTitle_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = string.Empty,
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithTitleTooLong_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = new string('A', 29), // 29 characters, exceeds max of 28
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithMaxLengthTitle_UpdatesSuccessfully()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = new string('A', 28), // Exactly 28 characters (max allowed)
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(28, result.Title.Length);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithEmptyDescription_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Valid Title",
                Description = string.Empty,
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithDescriptionTooLong_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Valid Title",
                Description = new string('B', 401), // 401 characters, exceeds max of 400
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithMaxLengthDescription_UpdatesSuccessfully()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Valid Title",
                Description = new string('B', 400), // Exactly 400 characters (max allowed)
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(400, result.Description?.Length);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithMinValueDate_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Valid Title",
                Description = "Valid description",
                Date = DateTime.MinValue,
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithInvalidDateViewPattern_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = (DateViewPattern)999, // Invalid enum value
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithZeroStreetcodeId_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = 0,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithNonExistentStreetcodeId_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = 999, // Non-existent streetcode
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithInvalidHistoricalContextIds_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int> { 0, -1 }, // Invalid IDs
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_WithNonExistentHistoricalContextIds_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int> { 999, 888 }, // Non-existent IDs
            };

            // Act
            var response = await this.Client.PutAsJsonAsync(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateTimelineItem_ChangingDateViewPattern_UpdatesSuccessfully()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");
            timelineItem.DateViewPattern = DateViewPattern.Year;

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Updated Title",
                Description = "Updated description",
                Date = new DateTime(2024, 6, 15),
                DateViewPattern = DateViewPattern.DateMonthYear, // Changed from Year
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(DateViewPattern.DateMonthYear, result.DateViewPattern);

            // Verify in database
            var dbItem = this.ExecuteWithContext(db =>
                db.TimelineItems.FirstOrDefault(t => t.Id == timelineItemId));
            
            Assert.NotNull(dbItem);
            Assert.Equal(DateViewPattern.DateMonthYear, dbItem.DateViewPattern);
        }

        [Fact]
        public async Task UpdateTimelineItem_AddingHistoricalContexts_AssociatesContextsCorrectly()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");
            var context1 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(1, "Context 1");
            var context2 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(2, "Context 2");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
                db.HistoricalContexts.Add(context1);
                db.HistoricalContexts.Add(context2);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Updated Title",
                Description = "Updated description",
                Date = new DateTime(2024, 6, 1),
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int> { 1, 2 },
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.NotNull(result.HistoricalContexts);
            Assert.Equal(2, result.HistoricalContexts.Count());

            // Verify in database
            var dbItem = this.ExecuteWithContext(db =>
                db.TimelineItems
                    .Include(t => t.HistoricalContextTimelines)
                    .FirstOrDefault(t => t.Id == timelineItemId));

            Assert.NotNull(dbItem);
            Assert.Equal(2, dbItem.HistoricalContextTimelines.Count);
        }

        [Fact]
        public async Task UpdateTimelineItem_RemovingHistoricalContexts_RemovesAssociations()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var context1 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(1, "Context 1");
            var context2 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(2, "Context 2");
            
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Original Title");
            timelineItem.HistoricalContextTimelines = new List<HistoricalContextTimeline>
            {
                new HistoricalContextTimeline { TimelineId = timelineItemId, HistoricalContextId = 1 },
                new HistoricalContextTimeline { TimelineId = timelineItemId, HistoricalContextId = 2 },
            };

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.HistoricalContexts.Add(context1);
                db.HistoricalContexts.Add(context2);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Updated Title",
                Description = "Updated description",
                Date = new DateTime(2024, 6, 1),
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(), // Remove all contexts
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.NotNull(result.HistoricalContexts);
            Assert.Empty(result.HistoricalContexts);

            // Verify in database
            var dbItem = this.ExecuteWithContext(db =>
                db.TimelineItems
                    .Include(t => t.HistoricalContextTimelines)
                    .FirstOrDefault(t => t.Id == timelineItemId));

            Assert.NotNull(dbItem);
            Assert.Empty(dbItem.HistoricalContextTimelines);
        }

        [Fact]
        public async Task UpdateTimelineItem_ChangingStreetcode_UpdatesSuccessfully()
        {
            // Arrange
            var streetcode1 = TimelineIntegrationTestData.CreateTestStreetcode(1);
            var streetcode2 = TimelineIntegrationTestData.CreateTestStreetcode(2);
            var timelineItemId = 1;
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, 1, "Original Title");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode1);
                db.Streetcodes.Add(streetcode2);
                db.TimelineItems.Add(timelineItem);
            });

            var updateDto = new UpdateTimelineItemDto
            {
                Id = timelineItemId,
                Title = "Updated Title",
                Description = "Updated description",
                Date = new DateTime(2024, 6, 1),
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = 2, // Changed from 1 to 2
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var (response, result) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);

            // Verify in database
            var dbItem = this.ExecuteWithContext(db =>
                db.TimelineItems.FirstOrDefault(t => t.Id == timelineItemId));
            
            Assert.NotNull(dbItem);
            Assert.Equal(2, dbItem.StreetcodeId);
        }

        [Fact]
        public async Task DeleteTimelineItem_WithExistingId_DeletesSuccessfully()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Item to Delete");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Verify deletion in database
            var dbItem = this.ExecuteWithContext(db =>
                db.TimelineItems.FirstOrDefault(t => t.Id == timelineItemId));
            
            Assert.Null(dbItem);
        }

        [Fact]
        public async Task DeleteTimelineItem_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            var nonExistentId = 999;

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{nonExistentId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTimelineItem_WithZeroId_ReturnsBadRequest()
        {
            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/0");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTimelineItem_WithNegativeId_ReturnsBadRequest()
        {
            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/-1");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTimelineItem_WithHistoricalContexts_DeletesItemAndRelationships()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var context1 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(1, "Context 1");
            var context2 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(2, "Context 2");
            
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Item with Contexts");
            timelineItem.HistoricalContextTimelines = new List<HistoricalContextTimeline>
            {
                new HistoricalContextTimeline { TimelineId = timelineItemId, HistoricalContextId = 1 },
                new HistoricalContextTimeline { TimelineId = timelineItemId, HistoricalContextId = 2 },
            };

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.HistoricalContexts.Add(context1);
                db.HistoricalContexts.Add(context2);
                db.TimelineItems.Add(timelineItem);
            });

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Verify timeline item is deleted
            var dbItem = this.ExecuteWithContext(db =>
                db.TimelineItems.FirstOrDefault(t => t.Id == timelineItemId));
            Assert.Null(dbItem);

            // Verify relationships are deleted
            var relationships = this.ExecuteWithContext(db =>
                db.HistoricalContextsTimelines
                    .Where(hct => hct.TimelineId == timelineItemId)
                    .ToList());
            Assert.Empty(relationships);

            // Verify historical contexts still exist (not cascade deleted)
            var contexts = this.ExecuteWithContext(db =>
                db.HistoricalContexts.ToList());
            Assert.Equal(2, contexts.Count);
        }

        [Fact]
        public async Task DeleteTimelineItem_MultipleItems_DeletesOnlySpecifiedItem()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var item1 = TimelineIntegrationTestData.CreateSimpleTimelineItem(1, streetcodeId, "Item 1");
            var item2 = TimelineIntegrationTestData.CreateSimpleTimelineItem(2, streetcodeId, "Item 2");
            var item3 = TimelineIntegrationTestData.CreateSimpleTimelineItem(3, streetcodeId, "Item 3");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(item1);
                db.TimelineItems.Add(item2);
                db.TimelineItems.Add(item3);
            });

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/2");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Verify only item 2 is deleted
            var remainingItems = this.ExecuteWithContext(db =>
                db.TimelineItems.Select(t => t.Id).OrderBy(id => id).ToList());
            
            Assert.Equal(2, remainingItems.Count);
            Assert.Contains(1, remainingItems);
            Assert.Contains(3, remainingItems);
            Assert.DoesNotContain(2, remainingItems);
        }

        [Fact]
        public async Task DeleteTimelineItem_DeleteTwice_SecondAttemptReturnsNotFound()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Item to Delete");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            // Act - First deletion
            var firstResponse = await this.DeleteAsync($"{BaseUrl}/{timelineItemId}");
            Assert.Equal(HttpStatusCode.OK, firstResponse.StatusCode);

            // Act - Second deletion attempt
            var secondResponse = await this.DeleteAsync($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, secondResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteTimelineItem_DoesNotAffectStreetcode()
        {
            // Arrange
            var streetcodeId = 1;
            var timelineItemId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var timelineItem = TimelineIntegrationTestData.CreateSimpleTimelineItem(timelineItemId, streetcodeId, "Item to Delete");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.TimelineItems.Add(timelineItem);
            });

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Verify streetcode still exists
            var dbStreetcode = this.ExecuteWithContext(db =>
                db.Streetcodes.FirstOrDefault(s => s.Id == streetcodeId));
            
            Assert.NotNull(dbStreetcode);
        }

        [Fact]
        public async Task DeleteTimelineItem_WithMaxIntId_ReturnsNotFound()
        {
            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{int.MaxValue}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetTimelineItemsByStreetcodeId_WithNonExistentStreetcode_ReturnsNotFound()
        {
            // Arrange
            var nonExistentStreetcodeId = 999;

            // Act
            var response = await this.Client.GetAsync($"{BaseUrl}/streetcode/{nonExistentStreetcodeId}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithNonExistentStreetcode_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateTimelineItemDto
            {
                Title = "New Event",
                Description = "Description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = 999,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithEmptyTitle_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = string.Empty,
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithTitleTooLong_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = new string('A', 29), // 29 characters, exceeds max of 28
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithMaxLengthTitle_CreatesSuccessfully()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = new string('A', 28), // Exactly 28 characters (max allowed)
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var (response, result) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(28, result.Title.Length);
        }

        [Fact]
        public async Task CreateTimelineItem_WithEmptyDescription_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "Valid Title",
                Description = string.Empty,
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithDescriptionTooLong_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "Valid Title",
                Description = new string('B', 401), // 401 characters, exceeds max of 400
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithMaxLengthDescription_CreatesSuccessfully()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "Valid Title",
                Description = new string('B', 400), // Exactly 400 characters (max allowed)
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var (response, result) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(400, result.Description?.Length);
        }

        [Fact]
        public async Task CreateTimelineItem_WithMinValueDate_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "Valid Title",
                Description = "Valid description",
                Date = DateTime.MinValue,
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithInvalidDateViewPattern_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = (DateViewPattern)999, // Invalid enum value
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithZeroStreetcodeId_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateTimelineItemDto
            {
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = 0,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithNegativeStreetcodeId_ReturnsBadRequest()
        {
            // Arrange
            var createDto = new CreateTimelineItemDto
            {
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = -1,
                HistoricalContextIds = new List<int>(),
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithInvalidHistoricalContextId_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int> { 0, -1 }, // Invalid IDs
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithNonExistentHistoricalContextIds_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "Valid Title",
                Description = "Valid description",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int> { 999, 888 }, // Non-existent IDs
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateTimelineItem_WithAllDateViewPatterns_CreatesSuccessfully()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            // Test Year pattern
            var yearDto = new CreateTimelineItemDto
            {
                Title = "Year Event",
                Description = "Year pattern description",
                Date = new DateTime(2024, 1, 1),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Test MonthYear pattern
            var monthYearDto = new CreateTimelineItemDto
            {
                Title = "Month Year Event",
                Description = "MonthYear pattern description",
                Date = new DateTime(2024, 6, 1),
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Test DateMonthYear pattern
            var dateMonthYearDto = new CreateTimelineItemDto
            {
                Title = "Full Date Event",
                Description = "DateMonthYear pattern description",
                Date = new DateTime(2024, 12, 25),
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Act & Assert - Year
            var (yearResponse, yearResult) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, yearDto);
            Assert.Equal(HttpStatusCode.OK, yearResponse.StatusCode);
            Assert.NotNull(yearResult);
            Assert.Equal(DateViewPattern.Year, yearResult.DateViewPattern);

            // Act & Assert - MonthYear
            var (monthYearResponse, monthYearResult) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, monthYearDto);
            Assert.Equal(HttpStatusCode.OK, monthYearResponse.StatusCode);
            Assert.NotNull(monthYearResult);
            Assert.Equal(DateViewPattern.MonthYear, monthYearResult.DateViewPattern);

            // Act & Assert - DateMonthYear
            var (dateMonthYearResponse, dateMonthYearResult) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, dateMonthYearDto);
            Assert.Equal(HttpStatusCode.OK, dateMonthYearResponse.StatusCode);
            Assert.NotNull(dateMonthYearResult);
            Assert.Equal(DateViewPattern.DateMonthYear, dateMonthYearResult.DateViewPattern);
        }

        [Fact]
        public async Task CreateTimelineItem_WithMultipleValidHistoricalContexts_AssociatesAllContexts()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var context1 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(1, "Context 1");
            var context2 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(2, "Context 2");
            var context3 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(3, "Context 3");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.HistoricalContexts.Add(context1);
                db.HistoricalContexts.Add(context2);
                db.HistoricalContexts.Add(context3);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "Multi-Context Event",
                Description = "Event with multiple contexts",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int> { 1, 2, 3 },
            };

            // Act
            var (response, result) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.NotNull(result.HistoricalContexts);
            Assert.Equal(3, result.HistoricalContexts.Count());

            // Verify in database
            var dbItem = this.ExecuteWithContext(db =>
                db.TimelineItems
                    .Include(t => t.HistoricalContextTimelines)
                    .FirstOrDefault(t => t.Title == "Multi-Context Event"));

            Assert.NotNull(dbItem);
            Assert.Equal(3, dbItem.HistoricalContextTimelines.Count);
        }

        [Fact]
        public async Task CreateTimelineItem_WithMixedValidAndInvalidContextIds_ReturnsBadRequest()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);
            var context1 = TimelineIntegrationTestData.CreateSimpleHistoricalContext(1, "Context 1");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
                db.HistoricalContexts.Add(context1);
            });

            var createDto = new CreateTimelineItemDto
            {
                Title = "Mixed Context Event",
                Description = "Event with mixed context IDs",
                Date = new DateTime(2024, 1, 15),
                DateViewPattern = DateViewPattern.Year,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int> { 1, 999 }, // 1 exists, 999 doesn't
            };

            // Act
            var response = await this.Client.PostAsJsonAsync(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAllTimelineItems_ReturnsAllItems()
        {
            // Arrange
            var streetcode1 = TimelineIntegrationTestData.CreateTestStreetcode(1);
            var streetcode2 = TimelineIntegrationTestData.CreateTestStreetcode(2);
            var item1 = TimelineIntegrationTestData.CreateSimpleTimelineItem(1, 1, "Event 1");
            var item2 = TimelineIntegrationTestData.CreateSimpleTimelineItem(2, 1, "Event 2");
            var item3 = TimelineIntegrationTestData.CreateSimpleTimelineItem(3, 2, "Event 3");

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode1);
                db.Streetcodes.Add(streetcode2);
                db.TimelineItems.Add(item1);
                db.TimelineItems.Add(item2);
                db.TimelineItems.Add(item3);
            });

            // Act
            var result = await this.GetAsync<List<TimelineItemDto>>(BaseUrl);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }
    }
}
