namespace Streetcode.XIntegrationTest.Timeline.HistoricalContext
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Net;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Streetcode.BLL.DTO.Timeline;
    using Streetcode.XIntegrationTest.Base;
    using Xunit;

    /// <summary>
    /// Authorization and authentication integration tests for HistoricalContext endpoints.
    /// Tests verify that protected endpoints require Admin role and public endpoints are accessible.
    /// </summary>
    public class HistoricalContextAuthorizationTests : BaseIntegrationTest<Program>
    {
        private const string BaseUrl = "/api/HistoricalContext";

        public HistoricalContextAuthorizationTests()
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
            var context1 = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Тестовий Контекст" };
            var context2 = new DAL.Entities.Timeline.HistoricalContext { Id = 2, Title = "Test Context" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.AddRange(context1, context2);
            });

            this.ClearAuthorizationHeader();

            // Act
            var response = await this.Client.GetAsync(BaseUrl);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_WithUserRole_ReturnsSuccess()
        {
            // Arrange
            var context1 = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Тестовий Контекст" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context1);
            });

            var userToken = this.GenerateTestToken(1, "User");
            this.SetAuthorizationHeader(userToken);

            // Act
            var response = await this.Client.GetAsync(BaseUrl);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAll_WithAdminRole_ReturnsSuccess()
        {
            // Arrange
            var context1 = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Тестовий Контекст" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(context1);
            });

            var adminToken = this.GenerateTestToken(1, "Admin");
            this.SetAuthorizationHeader(adminToken);

            // Act
            var response = await this.Client.GetAsync(BaseUrl);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region POST Endpoint Tests - Admin Required

        [Fact]
        public async Task Create_WithoutAuthentication_ReturnsUnauthorized()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Тестовий Контекст",
            };

            this.ClearAuthorizationHeader();

            // Act
            var (response, _) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithRegularUserRole_ReturnsForbidden()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Тестовий Контекст",
            };

            var userToken = this.GenerateTestToken(1, "User");
            this.SetAuthorizationHeader(userToken);

            // Act
            var (response, _) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithAdminRole_ReturnsSuccess()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Тестовий Контекст",
            };

            var adminToken = this.GenerateTestToken(1, "Admin");
            this.SetAuthorizationHeader(adminToken);

            // Act
            var (response, data) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(createDto.Title, data.Title);
        }

        [Fact]
        public async Task Create_WithInvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Тестовий Контекст",
            };

            this.SetAuthorizationHeader("invalid.token.here");

            // Act
            var (response, _) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithMalformedToken_ReturnsUnauthorized()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Тестовий Контекст",
            };

            this.SetAuthorizationHeader("notavalidtoken");

            // Act
            var (response, _) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        #endregion

        #region PUT Endpoint Tests - Admin Required

        [Fact]
        public async Task Update_WithoutAuthentication_ReturnsUnauthorized()
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Старий Контекст" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = existingContext.Id,
                Title = "Новий Контекст",
            };

            this.ClearAuthorizationHeader();

            // Act
            var (response, _) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithRegularUserRole_ReturnsForbidden()
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Старий Контекст" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = existingContext.Id,
                Title = "Новий Контекст",
            };

            var userToken = this.GenerateTestToken(1, "User");
            this.SetAuthorizationHeader(userToken);

            // Act
            var (response, _) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithAdminRole_ReturnsSuccess()
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Старий Контекст" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = existingContext.Id,
                Title = "Новий Контекст",
            };

            var adminToken = this.GenerateTestToken(1, "Admin");
            this.SetAuthorizationHeader(adminToken);

            // Act
            var (response, data) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(data);
            Assert.Equal(updateDto.Title, data.Title);
        }

        [Fact]
        public async Task Update_WithExpiredToken_ReturnsUnauthorized()
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Старий Контекст" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = existingContext.Id,
                Title = "Новий Контекст",
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
            var (response, _) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        #endregion

        #region DELETE Endpoint Tests - Admin Required

        [Fact]
        public async Task Delete_WithoutAuthentication_ReturnsUnauthorized()
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Контекст для Видалення" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            this.ClearAuthorizationHeader();

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{existingContext.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithRegularUserRole_ReturnsForbidden()
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Контекст для Видалення" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var userToken = this.GenerateTestToken(1, "User");
            this.SetAuthorizationHeader(userToken);

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{existingContext.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithAdminRole_ReturnsSuccess()
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Контекст для Видалення" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var adminToken = this.GenerateTestToken(1, "Admin");
            this.SetAuthorizationHeader(adminToken);

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{existingContext.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithModeratorRole_ReturnsForbidden()
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Контекст для Видалення" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var moderatorToken = this.GenerateTestToken(1, "Moderator");
            this.SetAuthorizationHeader(moderatorToken);

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{existingContext.Id}");

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
        [InlineData("Contributor")]
        public async Task Create_WithNonAdminRoles_ReturnsForbidden(string role)
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto
            {
                Title = "Тестовий Контекст",
            };

            var token = this.GenerateTestToken(1, role);
            this.SetAuthorizationHeader(token);

            // Act
            var (response, _) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("User")]
        [InlineData("Moderator")]
        [InlineData("Guest")]
        [InlineData("Editor")]
        [InlineData("Contributor")]
        public async Task Update_WithNonAdminRoles_ReturnsForbidden(string role)
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Старий Контекст" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var updateDto = new UpdateHistoricalContextDto
            {
                Id = existingContext.Id,
                Title = "Новий Контекст",
            };

            var token = this.GenerateTestToken(1, role);
            this.SetAuthorizationHeader(token);

            // Act
            var (response, _) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(BaseUrl, updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("User")]
        [InlineData("Moderator")]
        [InlineData("Guest")]
        [InlineData("Editor")]
        [InlineData("Contributor")]
        public async Task Delete_WithNonAdminRoles_ReturnsForbidden(string role)
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Контекст для Видалення" };

            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var token = this.GenerateTestToken(1, role);
            this.SetAuthorizationHeader(token);

            // Act
            var response = await this.DeleteAsync($"{BaseUrl}/{existingContext.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        #endregion

        #region Cross-Operation Authorization Tests

        [Fact]
        public async Task MultipleOperations_WithDifferentAuthStates_EnforcesCorrectAuthorization()
        {
            // Arrange & Act & Assert 1: GET without auth - should succeed
            this.ClearAuthorizationHeader();
            var getResponse1 = await this.Client.GetAsync(BaseUrl);
            Assert.Equal(HttpStatusCode.OK, getResponse1.StatusCode);

            // Act & Assert 2: POST without auth - should fail
            var createDto = new CreateHistoricalContextDto { Title = "Тестовий Контекст" };
            var (postResponse1, _) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);
            Assert.Equal(HttpStatusCode.Unauthorized, postResponse1.StatusCode);

            // Act & Assert 3: POST with User role - should fail
            var userToken = this.GenerateTestToken(1, "User");
            this.SetAuthorizationHeader(userToken);
            var (postResponse2, _) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);
            Assert.Equal(HttpStatusCode.Forbidden, postResponse2.StatusCode);

            // Act & Assert 4: POST with Admin role - should succeed
            var adminToken = this.GenerateTestToken(1, "Admin");
            this.SetAuthorizationHeader(adminToken);
            var (postResponse3, data) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);
            Assert.Equal(HttpStatusCode.OK, postResponse3.StatusCode);
            Assert.NotNull(data);

            // Act & Assert 5: UPDATE with Admin role - should succeed
            var updateDto = new UpdateHistoricalContextDto { Id = data!.Id, Title = "Оновлений Контекст" };
            var (putResponse, updatedData) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(BaseUrl, updateDto);
            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
            Assert.NotNull(updatedData);

            // Act & Assert 6: DELETE with User role - should fail
            this.SetAuthorizationHeader(userToken);
            var deleteResponse1 = await this.DeleteAsync($"{BaseUrl}/{data.Id}");
            Assert.Equal(HttpStatusCode.Forbidden, deleteResponse1.StatusCode);

            // Act & Assert 7: DELETE with Admin role - should succeed
            this.SetAuthorizationHeader(adminToken);
            var deleteResponse2 = await this.DeleteAsync($"{BaseUrl}/{data.Id}");
            Assert.Equal(HttpStatusCode.OK, deleteResponse2.StatusCode);

            // Act & Assert 8: GET without auth after all operations - should still succeed
            this.ClearAuthorizationHeader();
            var getResponse2 = await this.Client.GetAsync(BaseUrl);
            Assert.Equal(HttpStatusCode.OK, getResponse2.StatusCode);
        }

        [Fact]
        public async Task AdminPerformsAllOperations_SuccessfullyCompletesWorkflow()
        {
            // Arrange
            var adminToken = this.GenerateTestToken(1, "Admin");
            this.SetAuthorizationHeader(adminToken);

            // Act & Assert 1: Create
            var createDto = new CreateHistoricalContextDto { Title = "Workflow Test Context" };
            var (createResponse, createdData) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);
            Assert.NotNull(createdData);

            // Act & Assert 2: Update
            var updateDto = new UpdateHistoricalContextDto { Id = createdData!.Id, Title = "Updated Context" };
            var (updateResponse, updatedData) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(BaseUrl, updateDto);
            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
            Assert.NotNull(updatedData);
            Assert.Equal("Updated Context", updatedData.Title);

            // Act & Assert 3: Delete
            var deleteResponse = await this.DeleteAsync($"{BaseUrl}/{createdData.Id}");
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task UserTriesAllOperations_OnlyGetSucceeds()
        {
            // Arrange
            var userToken = this.GenerateTestToken(1, "User");
            this.SetAuthorizationHeader(userToken);

            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Existing Context" };
            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            // Act & Assert 1: GET - should succeed
            var getResponse = await this.Client.GetAsync(BaseUrl);
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            // Act & Assert 2: POST - should fail
            var createDto = new CreateHistoricalContextDto { Title = "New Context" };
            var (postResponse, _) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);
            Assert.Equal(HttpStatusCode.Forbidden, postResponse.StatusCode);

            // Act & Assert 3: PUT - should fail
            var updateDto = new UpdateHistoricalContextDto { Id = existingContext.Id, Title = "Updated Context" };
            var (putResponse, _) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(BaseUrl, updateDto);
            Assert.Equal(HttpStatusCode.Forbidden, putResponse.StatusCode);

            // Act & Assert 4: DELETE - should fail
            var deleteResponse = await this.DeleteAsync($"{BaseUrl}/{existingContext.Id}");
            Assert.Equal(HttpStatusCode.Forbidden, deleteResponse.StatusCode);
        }

        #endregion

        #region Token Edge Cases

        [Fact]
        public async Task Create_WithEmptyToken_ReturnsUnauthorized()
        {
            // Arrange
            var createDto = new CreateHistoricalContextDto { Title = "Test Context" };
            this.SetAuthorizationHeader(string.Empty);

            // Act
            var (response, _) = await this.PostAsync<CreateHistoricalContextDto, HistoricalContextDto>(BaseUrl, createDto);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Update_WithTokenMissingRole_ReturnsForbidden()
        {
            // Arrange
            var existingContext = new DAL.Entities.Timeline.HistoricalContext { Id = 1, Title = "Old Context" };
            this.SeedDatabase(db =>
            {
                db.HistoricalContexts.Add(existingContext);
            });

            var updateDto = new UpdateHistoricalContextDto { Id = existingContext.Id, Title = "New Context" };

            // Generate token without role claim
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(JwtRegisteredClaimNames.Sub, "1"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TestSecretKeyForIntegrationTests12345"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "TestIssuer",
                audience: "TestAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            this.SetAuthorizationHeader(tokenString);

            // Act
            var (response, _) = await this.PutAsync<UpdateHistoricalContextDto, HistoricalContextDto>(BaseUrl, updateDto);

            // Assert
            // Without a role claim, the user is authenticated but not authorized for Admin-only endpoints
            Assert.True(response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized);
        }

        #endregion
    }
}
