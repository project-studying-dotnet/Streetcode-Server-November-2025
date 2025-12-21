namespace Streetcode.XUnitTest.MediatR.Users.Login
{
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.Users;
    using Streetcode.BLL.Interfaces.Jwt;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Users.Login;
    using Streetcode.DAL.Entities.Users;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Users.Fixtures;
    using Streetcode.XUnitTest.MediatR.Users.Helpers;
    using Xunit;

    public class UserLoginHandlerTests
    {
        private readonly Mock<UserManager<User>> userManagerMock;
        private readonly Mock<SignInManager<User>> signInManagerMock;
        private readonly Mock<IJwtService> jwtServiceMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly UserLoginHandler handler;

        public UserLoginHandlerTests()
        {
            this.userManagerMock = UsersHelper.MockUserManager<User>();
            this.signInManagerMock = UsersHelper.MockSignInManager<User>();
            this.jwtServiceMock = new Mock<IJwtService>();
            this.loggerMock = new Mock<ILoggerService>();

            this.handler = new UserLoginHandler(
                this.userManagerMock.Object,
                this.signInManagerMock.Object,
                this.jwtServiceMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenUserIsNotFound_ReturnsFailureResult()
        {
            // Arrange
            var userDto = UserTestData.CreateUserLoginDto();
            var userLoginCommand = new UserLoginCommand(userDto);

            this.userManagerMock
                .Setup(u => u.FindByEmailAsync(userDto.Email))
                .ReturnsAsync((User)null!);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(userLoginCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal(ErrorMessages.UserEmailOrPasswordInvalid, result.Errors[0].Message);

            // Verify
            this.userManagerMock.Verify(u => u.FindByEmailAsync(userDto.Email), Times.Once);
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenPasswordIsInvalid_ReturnsFailureResult()
        {
            // Arrange
            var userDto = UserTestData.CreateUserLoginDto();
            var user = UserTestData.CreateUser();
            var userLoginCommand = new UserLoginCommand(userDto);

            this.userManagerMock
                .Setup(u => u.FindByEmailAsync(userDto.Email))
                .ReturnsAsync(user);
            this.signInManagerMock
                .Setup(s => s.CheckPasswordSignInAsync(user, userDto.Password, false))
                .ReturnsAsync(SignInResult.Failed);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(userLoginCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal(ErrorMessages.UserEmailOrPasswordInvalid, result.Errors[0].Message);

            // Verify
            this.userManagerMock.Verify(u => u.FindByEmailAsync(userDto.Email), Times.Once);
            this.signInManagerMock.Verify(s => s.CheckPasswordSignInAsync(user, userDto.Password, false), Times.Once);
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenCredentialsAreValid_ReturnsSuccessResult()
        {
            // Arrange
            var userDto = UserTestData.CreateUserLoginDto();
            var user = UserTestData.CreateUser();
            var accessTokenDto = new TokenResultDto
            {
                Token = "access-token",
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
            };
            var refreshTokenDto = new TokenResultDto
            {
                Token = "refresh-token",
                ExpiresAt = DateTime.UtcNow.AddDays(7),
            };

            var userLoginCommand = new UserLoginCommand(userDto);

            this.userManagerMock
                .Setup(u => u.FindByEmailAsync(userDto.Email))
                .ReturnsAsync(user);
            this.signInManagerMock
                .Setup(s => s.CheckPasswordSignInAsync(user, userDto.Password, false))
                .ReturnsAsync(SignInResult.Success);
            this.jwtServiceMock
                .Setup(j => j.GenerateAccessTokenAsync(user))
                .ReturnsAsync(accessTokenDto);
            this.jwtServiceMock
                .Setup(j => j.GenerateRefreshTokenAsync(user))
                .ReturnsAsync(refreshTokenDto);

            // Act
            var result = await this.handler.Handle(userLoginCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(user.Id, result.Value.UserId);
            Assert.Equal(accessTokenDto.Token, result.Value.AccessToken);
            Assert.Equal(refreshTokenDto.Token, result.Value.RefreshToken);
            Assert.Equal(accessTokenDto.ExpiresAt, result.Value.AccessTokenExpiresAt);
            Assert.Equal(refreshTokenDto.ExpiresAt, result.Value.RefreshTokenExpiresAt);

            // Verify
            this.userManagerMock.Verify(u => u.FindByEmailAsync(userDto.Email), Times.Once);
            this.signInManagerMock.Verify(s => s.CheckPasswordSignInAsync(user, userDto.Password, false), Times.Once);
            this.jwtServiceMock.Verify(j => j.GenerateAccessTokenAsync(user), Times.Once);
            this.jwtServiceMock.Verify(j => j.GenerateRefreshTokenAsync(user), Times.Once);
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}