namespace Streetcode.XUnitTest.MediatR.Users.Register
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Moq;
 using global::Streetcode.BLL.DTO.Users;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Users.Register;
 using global::Streetcode.DAL.Entities.Users;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.Users.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Users.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="RegisterUserHandler"/>.
    /// Covers success and failure scenarios for user registration,
    /// including validation of existing users, user creation failures, and role assignment failures.
    /// </summary>
    public class RegisterUserHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<UserManager<User>> userManagerMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly RegisterUserHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserHandlerTests"/> class.
        /// Initializes mocks and the <see cref="RegisterUserHandler"/> instance.
        /// </summary>
        public RegisterUserHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.userManagerMock = UsersHelper.MockUserManager<User>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new RegisterUserHandler(
                this.userManagerMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler successfully registers a new user when the username does not exist.
        /// Ensures that user creation and role assignment are completed successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_Success_WhenUserDoesNotExist()
        {
            // Arrange
            var dto = UserTestData.CreateRegisterUserDto();
            var command = new RegisterUserCommand(dto);
            var mappedUser = UserTestData.CreateUser();
            var responseDto = UserTestData.CreateRegisterUserResponseDto();

            this.userManagerMock.Setup(m => m.FindByNameAsync(dto.UserName)).ReturnsAsync((User?)null);
            this.userManagerMock.Setup(m => m.CreateAsync(It.IsAny<User>(), dto.Password)).ReturnsAsync(IdentityResult.Success);
            this.userManagerMock.Setup(m => m.AddToRoleAsync(It.IsAny<User>(), dto.Role.ToString())).ReturnsAsync(IdentityResult.Success);
            this.mapperMock.SetupMapper(dto, mappedUser);
            this.mapperMock.SetupMapperAny<User, RegisterUserResponseDto>(responseDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        /// <summary>
        /// Tests that the handler returns a failure result when attempting to register a user with an existing username.
        /// Ensures that the appropriate error message is returned.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_Fail_WhenUserExists()
        {
            // Arrange
            var dto = UserTestData.CreateRegisterUserDto(userName: "existing", email: "existing@email.com");
            var command = new RegisterUserCommand(dto);
            var existingUser = UserTestData.CreateUser(userName: "existing", email: "existing@email.com");

            this.userManagerMock.Setup(m => m.FindByNameAsync(dto.UserName)).ReturnsAsync(existingUser);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("User already exists", result.Errors.Select(e => e.Message));
        }

        /// <summary>
        /// Tests that the handler returns a failure result when <see cref="UserManager{TUser}.CreateAsync"/> fails.
        /// Ensures that the error description from the failed result is propagated to the response.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_Fail_WhenUserManagerCreateFails()
        {
            // Arrange
            var dto = UserTestData.CreateRegisterUserDto(userName: "unique2", email: "test@email.com");
            var command = new RegisterUserCommand(dto);
            var mappedUser = UserTestData.CreateUser(userName: "unique2", email: "test@email.com");

            this.userManagerMock.Setup(m => m.FindByNameAsync(dto.UserName)).ReturnsAsync((User?)null);
            this.userManagerMock.Setup(m => m.CreateAsync(It.IsAny<User>(), dto.Password)).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));
            this.mapperMock.SetupMapper(dto, mappedUser);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Error", result.Errors.Select(e => e.Message));
        }

        /// <summary>
        /// Tests that the handler returns a failure result when <see cref="UserManager{TUser}.AddToRoleAsync"/> fails.
        /// Ensures that the role assignment failure is properly handled and returns the expected error message.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_Fail_WhenAddToRoleFails()
        {
            // Arrange
            var dto = UserTestData.CreateRegisterUserDto(userName: "unique3", email: "test@email.com");
            var command = new RegisterUserCommand(dto);
            var mappedUser = UserTestData.CreateUser(userName: "unique3", email: "test@email.com");

            this.userManagerMock.Setup(m => m.FindByNameAsync(dto.UserName)).ReturnsAsync((User?)null);
            this.userManagerMock.Setup(m => m.CreateAsync(It.IsAny<User>(), dto.Password)).ReturnsAsync(IdentityResult.Success);
            this.userManagerMock.Setup(m => m.AddToRoleAsync(It.IsAny<User>(), dto.Role.ToString())).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "AddRoleError" }));
            this.mapperMock.SetupMapper(dto, mappedUser);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Failed to assign role", result.Errors[0].Message);
        }
    }
}