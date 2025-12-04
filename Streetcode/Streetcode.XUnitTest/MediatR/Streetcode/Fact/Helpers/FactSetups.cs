namespace Streetcode.XUnitTest.MediatR.Fact.Helpers
{
    using Moq;
    using Repositories.Interfaces;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;

    /// <summary>
    /// Provides extension methods for configuring mocked repository, mapper, and logger behavior
    /// when testing Fact-related handlers.
    /// </summary>
    public static class FactSetups
    {
        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="IFactRepository"/> instance when accessing the FactRepository property.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="factRepositoryMock">The mocked fact repository to be returned.</param>
        /// <param name="imageRepositoryMock">The mocked image repository to be returned.</param>
        /// <param name="streetcodeRepositoryMock">The mocked streetcode repository to be returned.</param>
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<IFactRepository> factRepositoryMock,
            Mock<IImageRepository> imageRepositoryMock,
            Mock<IStreetcodeRepository> streetcodeRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.FactRepository)
                .Returns(factRepositoryMock.Object);
            repositoryWrapperMock
                .Setup(rw => rw.ImageRepository)
                .Returns(imageRepositoryMock.Object);
            repositoryWrapperMock
                .Setup(rw => rw.StreetcodeRepository)
                .Returns(streetcodeRepositoryMock.Object);
        }

        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="IFactRepository"/> instance when accessing the FactRepository property.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="factRepositoryMock">The mocked fact repository to be returned.</param>
        /// <param name="imageRepositoryMock">The mocked image repository to be returned.</param>
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<IFactRepository> factRepositoryMock,
            Mock<IImageRepository> imageRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.FactRepository)
                .Returns(factRepositoryMock.Object);
            repositoryWrapperMock
                .Setup(rw => rw.ImageRepository)
                .Returns(imageRepositoryMock.Object);
        }

        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="IFactRepository"/> instance when accessing the FactRepository property.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="imageRepositoryMock">The mocked image repository to be returned.</param>
        /// <param name="streetcodeRepositoryMock">The mocked streetcode repository to be returned.</param>
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<IImageRepository> imageRepositoryMock,
            Mock<IStreetcodeRepository> streetcodeRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.ImageRepository)
                .Returns(imageRepositoryMock.Object);
            repositoryWrapperMock
                .Setup(rw => rw.StreetcodeRepository)
                .Returns(streetcodeRepositoryMock.Object);
        }

        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="IFactRepository"/> instance when accessing the FactRepository property.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="imageRepositoryMock">The mocked image repository to be returned.</param>
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<IImageRepository> imageRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.ImageRepository)
                .Returns(imageRepositoryMock.Object);
        }

        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="IFactRepository"/> instance when accessing the FactRepository property.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="factRepositoryMock">The mocked fact repository to be returned.</param>
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<IFactRepository> factRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.FactRepository)
                .Returns(factRepositoryMock.Object);
        }
    }
}