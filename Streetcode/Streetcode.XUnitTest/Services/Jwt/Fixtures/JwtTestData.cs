namespace Streetcode.XUnitTest.Services.Jwt.Fixtures
{
    using Streetcode.DAL.Entities.Users;

    /// <summary>
    /// Provides factory methods for creating test instances of <see cref="User"/>
    /// and related data for JWT service unit tests.
    /// </summary>
    public static class JwtTestData
    {
        /// <summary>
        /// Creates a single <see cref="User"/> entity instance with default values.
        /// </summary>
        /// <param name="id">The ID of the user. Default is 1.</param>
        /// <param name="userName">The username. Default is "testuser".</param>
        /// <param name="email">The email address. Default is "test@example.com".</param>
        /// <param name="name">The display name. Default is "Test User".</param>
        /// <returns>A fully initialized <see cref="User"/> object for testing.</returns>
        public static User CreateUser(
            int id = 1,
            string userName = "testuser",
            string email = "test@example.com",
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
        /// Creates a <see cref="User"/> with a custom ID and email.
        /// </summary>
        /// <param name="id">The custom user ID.</param>
        /// <param name="email">The custom email address.</param>
        /// <returns>A <see cref="User"/> with the specified ID and email.</returns>
        public static User CreateUserWithCustomId(int id, string email)
        {
            return CreateUser(
                id: id,
                userName: "testuser",
                email: email,
                name: "Test User");
        }

        /// <summary>
        /// Creates a list with a single "Admin" role.
        /// </summary>
        /// <returns>A list containing the "Admin" role.</returns>
        public static List<string> CreateSingleAdminRole()
        {
            return new List<string> { "Admin" };
        }

        /// <summary>
        /// Creates a list with "Admin" and "User" roles.
        /// </summary>
        /// <returns>A list containing "Admin" and "User" roles.</returns>
        public static List<string> CreateAdminUserRoles()
        {
            return new List<string> { "Admin", "User" };
        }

        /// <summary>
        /// Creates a list with multiple roles: "Admin", "User", and "Moderator".
        /// </summary>
        /// <returns>A list containing "Admin", "User", and "Moderator" roles.</returns>
        public static List<string> CreateMultipleRoles()
        {
            return new List<string> { "Admin", "User", "Moderator" };
        }

        /// <summary>
        /// Creates an empty list of roles.
        /// </summary>
        /// <returns>An empty list of roles.</returns>
        public static List<string> CreateEmptyRoles()
        {
            return new List<string>();
        }

        /// <summary>
        /// Creates a user for admin testing scenarios.
        /// </summary>
        /// <returns>A <see cref="User"/> configured as an admin.</returns>
        public static User CreateAdminUser()
        {
            return CreateUser(
                id: 1,
                userName: "admin",
                email: "admin@example.com",
                name: "Admin User");
        }

        /// <summary>
        /// Creates a user for regular user testing scenarios.
        /// </summary>
        /// <returns>A <see cref="User"/> configured as a regular user.</returns>
        public static User CreateRegularUser()
        {
            return CreateUser(
                id: 2,
                userName: "regularuser",
                email: "user@example.com",
                name: "Regular User");
        }
    }
}