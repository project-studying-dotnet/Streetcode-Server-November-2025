namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Helpers
{
    using Moq;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Toponyms;

    /// <summary>
    /// Provides extension methods for configuring mocked repository, mapper, and logger behavior
    /// when testing StreetcodeToponym-related handlers.
    /// </summary>
    public static class StreetcodeToponymSetups
    {
        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="IStreetcodeToponymRepository"/> instance when accessing the StreetcodeToponymRepository property.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="streetcodeToponymRepositoryMock">The mocked streetcode toponym repository to be returned.</param>
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<IStreetcodeToponymRepository> streetcodeToponymRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.StreetcodeToponymRepository)
                .Returns(streetcodeToponymRepositoryMock.Object);
        }

        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="IStreetcodeToponymRepository"/> and <see cref="IToponymRepository"/> instances.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="streetcodeToponymRepositoryMock">The mocked streetcode toponym repository to be returned.</param>
        /// <param name="toponymRepositoryMock">The mocked toponym repository to be returned.</param>
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<IStreetcodeToponymRepository> streetcodeToponymRepositoryMock,
            Mock<IToponymRepository> toponymRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.StreetcodeToponymRepository)
                .Returns(streetcodeToponymRepositoryMock.Object);
            repositoryWrapperMock
                .Setup(rw => rw.ToponymRepository)
                .Returns(toponymRepositoryMock.Object);
        }
    }
}