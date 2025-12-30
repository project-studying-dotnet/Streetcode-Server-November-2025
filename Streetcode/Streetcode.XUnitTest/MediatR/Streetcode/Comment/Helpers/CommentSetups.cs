namespace Streetcode.XUnitTest.MediatR.Comments.Helpers
{
    using Ardalis.Specification;
    using DAL.Repositories.Interfaces.Streetcode;
    using DAL.Entities.Streetcode;
    using DAL.Repositories.Interfaces.Base;
    using Moq;


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

        /// <summary>
        /// Sets up the mocked <see cref="ICommentsRepository"/> to return the provided
        /// <see cref="Comment"/> entity when calling <see cref="ICommentsRepository.GetBySpecAsync"/>
        /// with any specification and cancellation token.
        /// </summary>
        /// <param name="commentsRepositoryMock">The mocked comments repository.</param>
        /// <param name="result">
        /// The <see cref="Comment"/> instance to be returned by the repository,
        /// or <c>null</c> if no entity should be found.
        /// </param>
        public static void SetupGetBySpec(
            this Mock<ICommentsRepository> commentsRepositoryMock,
            Comment? result)
        {
            commentsRepositoryMock
                .Setup(r => r.GetBySpecAsync(
                    It.IsAny<ISpecification<Comment>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
        }

        /// <summary>
        /// Verifies that the mocked <see cref="ICommentsRepository.GetBySpecAsync"/> method
        /// was called with any specification and cancellation token.
        /// </summary>
        /// <param name="commentsRepositoryMock">The mocked comments repository.</param>
        /// <param name="times">The expected number of times the method should have been called.</param>
        public static void VerifyGetBySpec(
            this Mock<ICommentsRepository> commentsRepositoryMock,
            Times times)
        {
            commentsRepositoryMock.Verify(
                r => r.GetBySpecAsync(
                    It.IsAny<ISpecification<Comment>>(),
                    It.IsAny<CancellationToken>()),
                times);
        }

        /// <summary>
        /// Verifies that the mocked <see cref="ICommentsRepository.GetBySpecAsync"/> method
        /// was called exactly once with any specification and cancellation token.
        /// </summary>
        /// <param name="commentsRepositoryMock">The mocked comments repository.</param>
        public static void VerifyGetBySpecOnce(
            this Mock<ICommentsRepository> commentsRepositoryMock)
        {
            commentsRepositoryMock.VerifyGetBySpec(Times.Once());
        }

        /// <summary>
        /// Verifies that the mocked <see cref="ICommentsRepository.GetBySpecAsync"/> method
        /// was never called.
        /// </summary>
        /// <param name="commentsRepositoryMock">The mocked comments repository.</param>
        public static void VerifyGetBySpecNever(
            this Mock<ICommentsRepository> commentsRepositoryMock)
        {
            commentsRepositoryMock.VerifyGetBySpec(Times.Never());
        }
    }
}