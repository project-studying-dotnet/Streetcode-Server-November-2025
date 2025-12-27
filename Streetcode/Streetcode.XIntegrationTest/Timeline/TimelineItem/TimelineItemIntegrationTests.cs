namespace Streetcode.XIntegrationTest.Timeline.TimelineItem
{
    using System.Net;
    using System.Net.Http.Json;
    using Microsoft.EntityFrameworkCore;
    using Streetcode.BLL.DTO.Timeline;
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
