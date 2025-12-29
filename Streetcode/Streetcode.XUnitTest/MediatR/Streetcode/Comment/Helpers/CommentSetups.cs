namespace Streetcode.XUnitTest.MediatR.Comments.Helpers
{
    using Moq;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Streetcode;

    /// <summary>
    /// Provides extension methods for configuring mocked repository, mapper, and logger behavior
    /// when testing Comment-related handlers.
    /// </summary>
    public static class CommentSetups
    {
        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="IStreetcodeRepository"/> and <see cref="ICommentsRepository"/> instances.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="streetcodeRepositoryMock">The mocked streetcode repository to be returned.</param>
        /// <param name="commentsRepositoryMock">The mocked comments repository to be returned.</param>
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<IStreetcodeRepository> streetcodeRepositoryMock,
            Mock<ICommentsRepository> commentsRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.StreetcodeRepository)
                .Returns(streetcodeRepositoryMock.Object);
            repositoryWrapperMock
                .Setup(rw => rw.CommentsRepository)
                .Returns(commentsRepositoryMock.Object);
        }

        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="IStreetcodeRepository"/> instance when accessing the StreetcodeRepository property.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="streetcodeRepositoryMock">The mocked streetcode repository to be returned.</param>
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<IStreetcodeRepository> streetcodeRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.StreetcodeRepository)
                .Returns(streetcodeRepositoryMock.Object);
        }

        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked
        /// <see cref="IStreetcodeRepository"/> instance when accessing the CommentsRepository property.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        /// <param name="commentsRepositoryMock">The mocked comments repository to be returned.</param>
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<ICommentsRepository> commentsRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.CommentsRepository)
                .Returns(commentsRepositoryMock.Object);
        }
    }
}