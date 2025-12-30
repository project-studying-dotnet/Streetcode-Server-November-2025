namespace Streetcode.XUnitTest.MediatR.Comments.Helpers
{
    using System.Linq.Expressions;
    using Moq;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;

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
        /// Sets up the mocked <see cref="IRepositoryWrapper"/> to return the provided mocked repository
        /// instance for the specified repository selector.
        /// </summary>
        /// <typeparam name="TRepo">The type of the repository to set up.</typeparam>
        /// <param name="wrapperMock">The mocked repository wrapper.</param>
        /// <param name="repoSelector">An expression selecting the repository property from the wrapper.</param>
        /// <param name="repoMock">The mocked repository to be returned.</param>
        public static void SetupRepository<TRepo>(
            this Mock<IRepositoryWrapper> wrapperMock,
            Expression<Func<IRepositoryWrapper, TRepo>> repoSelector,
            Mock<TRepo> repoMock)
        where TRepo : class
        {
            wrapperMock.Setup(repoSelector).Returns(repoMock.Object);
        }

        /// <summary>
        /// Sets up the mocked <see cref="ICommentsRepository"/> Update method.
        /// </summary>
        /// <param name="commentsRepositoryMock">The mocked comments repository.</param>
        public static void SetupUpdate(
            this Mock<ICommentsRepository> commentsRepositoryMock)
        {
            commentsRepositoryMock
                .Setup(r => r.Update(It.IsAny<Comment>()));
        }

        /// <summary>
        /// Verifies that the mocked <see cref="ICommentsRepository.GetFirstOrDefaultAsync"/> method
        /// was called exactly once.
        /// </summary>
        /// <param name="commentsRepositoryMock">The mocked comments repository.</param>
        public static void VerifyGetFirstOrDefaultCalledOnce(
            this Mock<ICommentsRepository> commentsRepositoryMock)
        {
            commentsRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Comment, bool>>>(),
                    null),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the mocked <see cref="ICommentsRepository.Update"/> method
        /// was called exactly once with a specific comment.
        /// </summary>
        /// <param name="commentsRepositoryMock">The mocked comments repository.</param>
        /// <param name="comment">The comment that should have been updated.</param>
        public static void VerifyUpdateCalledOnce(
            this Mock<ICommentsRepository> commentsRepositoryMock,
            Comment comment)
        {
            commentsRepositoryMock.Verify(
                r => r.Update(comment),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the mocked <see cref="ICommentsRepository.Update"/> method
        /// was never called.
        /// </summary>
        /// <param name="commentsRepositoryMock">The mocked comments repository.</param>
        public static void VerifyUpdateCalledNever(
            this Mock<ICommentsRepository> commentsRepositoryMock)
        {
            commentsRepositoryMock.Verify(
                r => r.Update(It.IsAny<Comment>()),
                Times.Never);
        }

        /// <summary>
        /// Sets up the mocked <see cref="IRepositoryWrapper.SaveChangesAsync"/> method to return 0,
        /// indicating that no changes were saved.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        public static void SetupNotSaveChangesAsync(this Mock<IRepositoryWrapper> repositoryWrapperMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.SaveChangesAsync())
                .ReturnsAsync(0);
        }
    }
}