namespace Streetcode.XUnitTest.Services.Jwt
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Identity;
    using Moq;
 using global::Streetcode.BLL.Services.Jwt;
 using global::Streetcode.DAL.Entities.Users;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.XUnitTest.MediatR.Users.Helpers;
 using global::Streetcode.XUnitTest.Services.Jwt.Fixtures;
    using Xunit;

    /// <summary>
    /// Contains unit tests for the <see cref="JwtService"/> class.
    /// Tests JWT token generation, validation, and expiration logic.
    /// </summary>
    public class JwtServiceTests
    {
        private const string SecretKey = "test-secret-key-that-is-at-least-32-characters-long!";
        private const string Issuer = "TestIssuer";
        private const string Audience = "TestAudience";
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<UserManager<User>> mockUserManager;
        private readonly JwtService jwtService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtServiceTests"/> class.
        /// Sets up mocks for dependencies and creates a JwtService instance for testing.
        /// </summary>
        public JwtServiceTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.mockUserManager = UsersHelper.MockUserManager<User>();

            this.jwtService = new JwtService(
                secretKey: SecretKey,
                issuer: Issuer,
                audience: Audience,
                repository: this.repositoryWrapperMock.Object,
                userManager: this.mockUserManager.Object,
                accessTokenExpirationMinutes: 15);
        }

        /// <summary>
        /// Tests that GenerateAccessTokenAsync returns a valid token with correct expiration
        /// when provided with a valid user and roles.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task GenerateAccessTokenAsync_ReturnsValidToken_WhenUserIsValid()
        {
            // Arrange
            var user = JwtTestData.CreateUser();
            var roles = JwtTestData.CreateAdminUserRoles();

            this.mockUserManager.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(roles);

            // Act
            var result = await this.jwtService.GenerateAccessTokenAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
            Assert.NotEmpty(result.Token);
            Assert.True(result.ExpiresAt > DateTime.UtcNow);
        }

        /// <summary>
        /// Tests that GenerateAccessTokenAsync creates a token containing all required claims
        /// including user ID, email, token type, and roles.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task GenerateAccessTokenAsync_ContainsCorrectClaims_WhenTokenIsGenerated()
        {
            // Arrange
            var user = JwtTestData.CreateUser();
            var roles = JwtTestData.CreateSingleAdminRole();

            this.mockUserManager.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(roles);

            // Act
            var result = await this.jwtService.GenerateAccessTokenAsync(user);

            // Assert
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(result.Token);

            Assert.Equal(user.Id.ToString(), jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
            Assert.Equal(user.Email, jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value);
            Assert.Equal("access", jwtToken.Claims.First(c => c.Type == "token_type").Value);
            Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        }

        /// <summary>
        /// Tests that GenerateAccessTokenAsync includes all user roles in the token claims
        /// when a user has multiple roles assigned.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task GenerateAccessTokenAsync_IncludesAllRoles_WhenUserHasMultipleRoles()
        {
            // Arrange
            var user = JwtTestData.CreateUser();
            var roles = JwtTestData.CreateMultipleRoles();

            this.mockUserManager.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(roles);

            // Act
            var result = await this.jwtService.GenerateAccessTokenAsync(user);

            // Assert
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(result.Token);

            var roleClaims = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            Assert.Equal(3, roleClaims.Count);
            Assert.Contains(roleClaims, c => c.Value == "Admin");
            Assert.Contains(roleClaims, c => c.Value == "User");
            Assert.Contains(roleClaims, c => c.Value == "Moderator");
        }

        /// <summary>
        /// Tests that GenerateAccessTokenAsync sets the token expiration time
        /// to exactly 15 minutes from the generation time.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task GenerateAccessTokenAsync_SetsCorrectExpiration_WhenTokenIsGenerated()
        {
            // Arrange
            var user = JwtTestData.CreateUser();

            this.mockUserManager.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(new List<string>());

            var beforeGeneration = DateTime.UtcNow;

            // Act
            var result = await this.jwtService.GenerateAccessTokenAsync(user);

            var afterGeneration = DateTime.UtcNow;

            // Assert
            var expectedMinExpiration = beforeGeneration.AddMinutes(15);
            var expectedMaxExpiration = afterGeneration.AddMinutes(15);

            Assert.True(result.ExpiresAt >= expectedMinExpiration);
            Assert.True(result.ExpiresAt <= expectedMaxExpiration);
        }

        /// <summary>
        /// Tests that ValidateToken returns a valid ClaimsPrincipal with authenticated identity
        /// when provided with a valid JWT token.
        /// </summary>
        [Fact]
        public void ValidateToken_ReturnsClaimsPrincipal_WhenTokenIsValid()
        {
            // Arrange
            var user = JwtTestData.CreateUser();
            var roles = JwtTestData.CreateSingleAdminRole();

            this.mockUserManager.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(roles);

            var tokenResult = this.jwtService.GenerateAccessTokenAsync(user).Result;

            // Act
            var principal = this.jwtService.ValidateToken(tokenResult.Token);

            // Assert
            Assert.NotNull(principal);
            Assert.NotNull(principal.Identity);
            Assert.True(principal.Identity.IsAuthenticated);
        }

        /// <summary>
        /// Tests that ValidateToken returns null when provided with an invalid or malformed token.
        /// </summary>
        [Fact]
        public void ValidateToken_ReturnsNull_WhenTokenIsInvalid()
        {
            // Arrange
            var invalidToken = "invalid.token.here";

            // Act
            var principal = this.jwtService.ValidateToken(invalidToken);

            // Assert
            Assert.Null(principal);
        }

        /// <summary>
        /// Tests that ValidateToken returns null when provided with an expired token,
        /// ensuring proper lifetime validation.
        /// </summary>
        [Fact]
        public void ValidateToken_ReturnsNull_WhenTokenIsExpired()
        {
            // Arrange
            var expiredJwtService = new JwtService(
                secretKey: SecretKey,
                issuer: Issuer,
                audience: Audience,
                repository: this.repositoryWrapperMock.Object,
                userManager: this.mockUserManager.Object,
                accessTokenExpirationMinutes: -1);

            var user = JwtTestData.CreateUser();
            this.mockUserManager.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(new List<string>());

            var tokenResult = expiredJwtService.GenerateAccessTokenAsync(user).Result;

            // Act
            var principal = this.jwtService.ValidateToken(tokenResult.Token);

            // Assert
            Assert.Null(principal);
        }

        /// <summary>
        /// Tests that ValidateToken correctly extracts and returns all claims from a valid token,
        /// including user ID, email, and roles.
        /// </summary>
        [Fact]
        public void ValidateToken_ExtractsCorrectClaims_WhenTokenIsValid()
        {
            // Arrange
            var user = JwtTestData.CreateUserWithCustomId(123, "user@test.com");
            var roles = JwtTestData.CreateAdminUserRoles();
            this.mockUserManager.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(roles);
            var tokenResult = this.jwtService.GenerateAccessTokenAsync(user).Result;

            // Act
            var principal = this.jwtService.ValidateToken(tokenResult.Token);

            // Assert
            Assert.NotNull(principal);

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            Assert.NotNull(userIdClaim);
            Assert.Equal("123", userIdClaim.Value);

            var emailClaim = principal.FindFirst(ClaimTypes.Email);
            Assert.NotNull(emailClaim);
            Assert.Equal("user@test.com", emailClaim.Value);

            var roleClaims = principal.FindAll(ClaimTypes.Role).ToList();
            Assert.Equal(2, roleClaims.Count);
            Assert.Contains(roleClaims, c => c.Value == "Admin");
            Assert.Contains(roleClaims, c => c.Value == "User");
        }

        /// <summary>
        /// Tests that ValidateToken returns null when provided with null, empty, or whitespace token strings.
        /// </summary>
        /// <param name="token">The invalid token string to test.</param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void ValidateToken_ReturnsNull_WhenTokenIsNullOrEmpty(string token)
        {
            // Act
            var principal = this.jwtService.ValidateToken(token);

            // Assert
            Assert.Null(principal);
        }
    }
}