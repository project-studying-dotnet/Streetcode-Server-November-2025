namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers
{
    using Moq;
 using global::Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using System.Linq.Expressions;

    public static class MockRepoHelper
    {
        public static void SetupRepository<TRepo>(
            this Mock<IRepositoryWrapper> wrapperMock,
            Expression<Func<IRepositoryWrapper, TRepo>> repoSelector,
            Mock<TRepo> repoMock)
            where TRepo : class
        {
            wrapperMock.Setup(repoSelector).Returns(repoMock.Object);
        }

        public static void SetupNotSaveChangesAsync(this Mock<IRepositoryWrapper> repositoryWrapperMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.SaveChangesAsync())
                .ReturnsAsync(0);
        }
    }
}