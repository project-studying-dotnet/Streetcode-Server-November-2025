namespace Streetcode.XUnitTest.MediatR.Users.Fixtures
{
    using Streetcode.BLL.DTO.Users;
    using Streetcode.DAL.Entities.Users;
    using Streetcode.DAL.Enums;

    /// <summary>
    /// Provides factory methods for creating test instances of <see cref="User"/>
    /// and related DTO objects for use in unit tests.
    /// </summary>
    public static class UserTestData
    {
        /// <summary>
        /// Creates a single <see cref="User"/> entity instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="userName">The username.</param>
        /// <param name="email">The email address.</param>
        /// <param name="name">The display name.</param>
        /// <returns>A fully initialized <see cref="User"/> object for testing.</returns>
        public static User CreateUser(
            int id = 1,
            string userName = "test.user",
            string email = "test@email.com",
            string name = "Test User")
        {
            return new User
            {
                Id = id,
                UserName = userName,
                Email = email,
                Name = name,
            };
        }

        /// <summary>
        /// Creates a single <see cref="RegisterUserDto"/> instance with predefined values.
        /// </summary>
        /// <param name="userName">The username for registration.</param>
        /// <param name="email">The email address.</param>
        /// <param name="password">The password.</param>
        /// <param name="name">The display name.</param>
        /// <param name="role">The user role.</param>
        /// <returns>A fully initialized <see cref="RegisterUserDto"/> object for testing.</returns>
        public static RegisterUserDto CreateRegisterUserDto(
            string userName = "unique.user",
            string email = "unique@email.com",
            string password = "Aa123456",
            string name = "Test User",
            UserRole role = UserRole.Administrator)
        {
            return new RegisterUserDto
            {
                UserName = userName,
                Email = email,
                Password = password,
                Name = name,
                Role = role,
            };
        }

        /// <summary>
        /// Creates a <see cref="RegisterUserDto"/> for an existing user scenario.
        /// </summary>
        /// <returns>A <see cref="RegisterUserDto"/> with username "existing".</returns>
        public static RegisterUserDto CreateExistingUserDto()
        {
            return CreateRegisterUserDto(
                userName: "existing",
                email: "existing@email.com",
                name: "Existing User");
        }

        /// <summary>
        /// Creates a single <see cref="RegisterUserResponseDto"/> instance with predefined values.
        /// </summary>
        /// <param name="id">The ID of the registered user.</param>
        /// <param name="userName">The username.</param>
        /// <param name="email">The email address.</param>
        /// <returns>A fully initialized <see cref="RegisterUserResponseDto"/> object for testing.</returns>
        public static RegisterUserResponseDto CreateRegisterUserResponseDto(
            int id = 1,
            string userName = "unique.user",
            string email = "unique@email.com")
        {
            return new RegisterUserResponseDto
            {
                Id = id,
                UserName = userName,
                Email = email,
            };
        }
    }
}