namespace Streetcode.XUnitTest.MediatR.Toponyms.Helpers
{
    using Moq;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Toponyms;

    public static class ToponymSetups
    {
        public static void SetupRepositoryWrapper(
            this Mock<IRepositoryWrapper> repositoryWrapperMock,
            Mock<IToponymRepository> toponymRepositoryMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.ToponymRepository)
                .Returns(toponymRepositoryMock.Object);
        }
    }
}