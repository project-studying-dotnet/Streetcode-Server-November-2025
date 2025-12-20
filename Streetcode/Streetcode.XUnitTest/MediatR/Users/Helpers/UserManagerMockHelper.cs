namespace Streetcode.XUnitTest.MediatR.Users.Helpers
{
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Streetcode.DAL.Entities.Users;

    /// <summary>
    /// Provides helper methods for creating mocked instances of <see cref="UserManager{TUser}"/>
    /// for unit testing purposes.
    /// </summary>
    public static class UserManagerMockHelper
    {
        /// <summary>
        /// Creates a mocked instance of <see cref="UserManager{User}"/> with default null parameters.
        /// </summary>
        /// <returns>
        /// A <see cref="Mock{UserManager}"/> instance configured with a mocked <see cref="IUserStore{User}"/>
        /// and all other dependencies set to null.
        /// </returns>
        /// <remarks>
        /// This method simplifies the creation of <see cref="UserManager{User}"/> mocks by handling
        /// the complex constructor requirements. The returned mock can be further configured with
        /// specific behavior using Moq's setup methods.
        /// </remarks>
        public static Mock<UserManager<User>> CreateMock()
        {
            var store = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
                store.Object,
                null, // IOptions<IdentityOptions>
                null, // IPasswordHasher<User>
                null, // IEnumerable<IUserValidator<User>>
                null, // IEnumerable<IPasswordValidator<User>>
                null, // ILookupNormalizer
                null, // IdentityErrorDescriber
                null, // IServiceProvider
                null); // ILogger<UserManager<User>>
        }
    }
}