namespace Streetcode.XIntegrationTest.Timeline.TimelineItem
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Streetcode.BLL.DTO.Timeline;
    using Streetcode.DAL.Enums;
    using Streetcode.XIntegrationTest.Base;
    using Streetcode.XIntegrationTest.Timeline.Fixtures;
    using Xunit;

    /// <summary>
    /// Authorization and authentication integration tests for TimelineItem endpoints.
    /// Tests verify that protected endpoints require Admin role and public endpoints are accessible.
    /// </summary>
    public class TimelineItemAuthorizationTests : BaseIntegrationTest<Program>
    {
        private const string BaseUrl = "/api/Timeline";

        public TimelineItemAuthorizationTests()
            : base()
        {
        }

        #region Helper Methods

        /// <summary>
        /// Generates a JWT token for testing with specified role.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="role">User role (Admin, User, etc.).</param>
        /// <returns>JWT token string.</returns>
        private string GenerateTestToken(int userId, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TestSecretKeyForIntegrationTests12345"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "TestIssuer",
                audience: "TestAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

        #region GET Endpoint Tests - Public Access

        [Fact]
        public async Task GetAll_WithoutAuthentication_ReturnsSuccess()
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

            this.ClearAuthorizationHeader();

            // Act
            var response = await this.Client.GetAsync(BaseUrl);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_WithoutAuthentication_ReturnsSuccess()
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

            var timelineItemId = timelineItems.First().Id;
            this.ClearAuthorizationHeader();

            // Act
            var response = await this.Client.GetAsync($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetByStreetcodeId_WithoutAuthentication_ReturnsSuccess()
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

            this.ClearAuthorizationHeader();

            // Act
            var response = await this.Client.GetAsync($"{BaseUrl}/streetcode/{streetcodeId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region POST Endpoint Tests - Admin Required

        [Fact]
        public async Task Create_WithoutAuthentication_ReturnsUnauthorized()
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
                Title = "Test Timeline Item",
                Description = "Test Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            this.ClearAuthorizationHeader();

            // Act
            var (response, _) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithRegularUserRole_ReturnsForbidden()
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
                Title = "Test Timeline Item",
                Description = "Test Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            var userToken = this.GenerateTestToken(1, "User");
            this.SetAuthorizationHeader(userToken);

            // Act
            var (response, _) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithAdminRole_ReturnsSuccess()
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
                Title = "Test Timeline Item",
                Description = "Test Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            var adminToken = this.GenerateTestToken(1, "Admin");
            this.SetAuthorizationHeader(adminToken);

            // Act
            var (response, data) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(createDto.Title, data.Title);
        }

        [Fact]
        public async Task Create_WithInvalidToken_ReturnsUnauthorized()
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
                Title = "Test Timeline Item",
                Description = "Test Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            this.SetAuthorizationHeader("invalid.token.here");

            // Act
            var (response, _) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        #endregion

        #region PUT Endpoint Tests - Admin Required

        [Fact]
        public async Task Update_WithoutAuthentication_ReturnsUnauthorized()
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

            var existingItem = timelineItems.First();
            var updateDto = new UpdateTimelineItemDto
            {
                Id = existingItem.Id,
                Title = "Updated Title",
                Description = "Updated Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            this.ClearAuthorizationHeader();

            // Act
            var (response, _) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithRegularUserRole_ReturnsForbidden()
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

            var existingItem = timelineItems.First();
            var updateDto = new UpdateTimelineItemDto
            {
                Id = existingItem.Id,
                Title = "Updated Title",
                Description = "Updated Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            var userToken = this.GenerateTestToken(1, "User");
            this.SetAuthorizationHeader(userToken);

            // Act
            var (response, _) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithAdminRole_ReturnsSuccess()
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

            var existingItem = timelineItems.First();
            var updateDto = new UpdateTimelineItemDto
            {
                Id = existingItem.Id,
                Title = "Updated Title",
                Description = "Updated Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            var adminToken = this.GenerateTestToken(1, "Admin");
            this.SetAuthorizationHeader(adminToken);

            // Act
            var (response, data) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(updateDto.Title, data.Title);
        }

        [Fact]
        public async Task Update_WithExpiredToken_ReturnsUnauthorized()
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

            var existingItem = timelineItems.First();
            var updateDto = new UpdateTimelineItemDto
            {
                Id = existingItem.Id,
                Title = "Updated Title",
                Description = "Updated Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            // Generate expired token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TestSecretKeyForIntegrationTests12345"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiredToken = new JwtSecurityToken(
                issuer: "TestIssuer",
                audience: "TestAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(-1), // Expired 1 hour ago
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(expiredToken);
            this.SetAuthorizationHeader(tokenString);

            // Act
            var (response, _) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        #endregion

        #region DELETE Endpoint Tests - Admin Required

        [Fact]
        public async Task Delete_WithoutAuthentication_ReturnsUnauthorized()
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

            var timelineItemId = timelineItems.First().Id;
            this.ClearAuthorizationHeader();

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithRegularUserRole_ReturnsForbidden()
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

            var timelineItemId = timelineItems.First().Id;

            var userToken = this.GenerateTestToken(1, "User");
            this.SetAuthorizationHeader(userToken);

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithAdminRole_ReturnsSuccess()
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

            var timelineItemId = timelineItems.First().Id;

            var adminToken = this.GenerateTestToken(1, "Admin");
            this.SetAuthorizationHeader(adminToken);

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithModeratorRole_ReturnsForbidden()
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

            var timelineItemId = timelineItems.First().Id;

            var moderatorToken = this.GenerateTestToken(1, "Moderator");
            this.SetAuthorizationHeader(moderatorToken);

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        #endregion

        #region Role-Based Access Tests

        [Theory]
        [InlineData("User")]
        [InlineData("Moderator")]
        [InlineData("Guest")]
        [InlineData("Editor")]
        public async Task Create_WithNonAdminRoles_ReturnsForbidden(string role)
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
                Title = "Test Timeline Item",
                Description = "Test Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            var token = this.GenerateTestToken(1, role);
            this.SetAuthorizationHeader(token);

            // Act
            var (response, _) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("User")]
        [InlineData("Moderator")]
        [InlineData("Guest")]
        [InlineData("Editor")]
        public async Task Update_WithNonAdminRoles_ReturnsForbidden(string role)
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

            var existingItem = timelineItems.First();
            var updateDto = new UpdateTimelineItemDto
            {
                Id = existingItem.Id,
                Title = "Updated Title",
                Description = "Updated Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.MonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            var token = this.GenerateTestToken(1, role);
            this.SetAuthorizationHeader(token);

            // Act
            var (response, _) = await this.PutAsync<UpdateTimelineItemDto, TimelineItemDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("User")]
        [InlineData("Moderator")]
        [InlineData("Guest")]
        [InlineData("Editor")]
        public async Task Delete_WithNonAdminRoles_ReturnsForbidden(string role)
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

            var timelineItemId = timelineItems.First().Id;

            var token = this.GenerateTestToken(1, role);
            this.SetAuthorizationHeader(token);

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{timelineItemId}");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        #endregion

        #region Multiple Operations with Different Auth States

        [Fact]
        public async Task MultipleOperations_SwitchingBetweenAuthStates_WorksCorrectly()
        {
            // Arrange
            var streetcodeId = 1;
            var streetcode = TimelineIntegrationTestData.CreateTestStreetcode(streetcodeId);

            this.SeedDatabase(db =>
            {
                db.Streetcodes.Add(streetcode);
            });

            // Act & Assert 1: GET without auth - should succeed
            this.ClearAuthorizationHeader();
            var getResponse1 = await this.Client.GetAsync(BaseUrl);
            Assert.Equal(HttpStatusCode.OK, getResponse1.StatusCode);

            // Act & Assert 2: POST without auth - should fail
            var createDto = new CreateTimelineItemDto
            {
                Title = "Test Timeline Item",
                Description = "Test Description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.DateMonthYear,
                StreetcodeId = streetcodeId,
                HistoricalContextIds = new List<int>(),
            };

            var (postResponse1, _) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);
            Assert.Equal(HttpStatusCode.Unauthorized, postResponse1.StatusCode);

            // Act & Assert 3: POST with Admin auth - should succeed
            var adminToken = this.GenerateTestToken(1, "Admin");
            this.SetAuthorizationHeader(adminToken);
            var (postResponse2, data) = await this.PostAsync<CreateTimelineItemDto, TimelineItemDto>(BaseUrl, createDto);
            Assert.Equal(HttpStatusCode.OK, postResponse2.StatusCode);
            Assert.NotNull(data);

            // Act & Assert 4: GET with Admin auth - should still succeed
            var getResponse2 = await this.Client.GetAsync(BaseUrl);
            Assert.Equal(HttpStatusCode.OK, getResponse2.StatusCode);

            // Act & Assert 5: Clear auth and GET again - should succeed
            this.ClearAuthorizationHeader();
            var getResponse3 = await this.Client.GetAsync(BaseUrl);
            Assert.Equal(HttpStatusCode.OK, getResponse3.StatusCode);
        }

        #endregion
    }
}
