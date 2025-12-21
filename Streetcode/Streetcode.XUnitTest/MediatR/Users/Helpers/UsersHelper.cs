namespace Streetcode.XUnitTest.MediatR.Users.Helpers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Moq;

    /// <summary>
    /// Provides helper methods for creating mocked instances of Identity-related classes
    /// for unit testing purposes.
    /// </summary>
    public static class UsersHelper
    {
        /// <summary>
        /// Creates a mocked instance of <see cref="UserManager{TUser}"/> with default null parameters.
        /// </summary>
        /// <typeparam name="TUser">The type representing a user in the system.</typeparam>
        /// <returns>
        /// A <see cref="Mock{T}"/> of <see cref="UserManager{TUser}"/> configured with a mocked 
        /// <see cref="IUserStore{TUser}"/> and all other dependencies set to null.
        /// </returns>
        /// <remarks>
        /// This method simplifies the creation of <see cref="UserManager{TUser}"/> mocks by handling
        /// the complex constructor requirements. The returned mock can be further configured with
        /// specific behavior using Moq's setup methods.
        /// </remarks>
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        /// <summary>
        /// Creates a mocked instance of <see cref="SignInManager{TUser}"/> with default null parameters.
        /// </summary>
        /// <typeparam name="TUser">The type representing a user in the system.</typeparam>
        /// <returns>
        /// A <see cref="Mock{T}"/> of <see cref="SignInManager{TUser}"/> configured with mocked dependencies
        /// including <see cref="UserManager{TUser}"/>, <see cref="IHttpContextAccessor"/>, 
        /// and <see cref="IUserClaimsPrincipalFactory{TUser}"/>, with all other dependencies set to null.
        /// </returns>
        /// <remarks>
        /// This method simplifies the creation of <see cref="SignInManager{TUser}"/> mocks by handling
        /// the complex constructor requirements. The returned mock can be further configured with
        /// specific behavior using Moq's setup methods.
        /// </remarks>
        public static Mock<SignInManager<TUser>> MockSignInManager<TUser>() where TUser : class
        {
            var userManager = MockUserManager<TUser>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<TUser>>();
            return new Mock<SignInManager<TUser>>(
                userManager.Object,
                contextAccessor.Object,
                claimsFactory.Object,
                null, null, null, null);
        }
    }
}