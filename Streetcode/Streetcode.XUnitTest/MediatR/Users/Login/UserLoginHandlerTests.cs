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
            var user = new UserLoginDto
            {
                Email = "john.doe@gmail.com",
                Password = "Password123@",
            };
            var userLoginCommand = new UserLoginCommand(user);

            this.userManagerMock
                .Setup(u => u.FindByEmailAsync(user.Email))
                .ReturnsAsync((User)null!);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(userLoginCommand, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal(ErrorMessages.UserEmailOrPasswordInvalid, result.Errors[0].Message);

            // Verify
            this.userManagerMock.Verify(u => u.FindByEmailAsync(user.Email), Times.Once);
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}
