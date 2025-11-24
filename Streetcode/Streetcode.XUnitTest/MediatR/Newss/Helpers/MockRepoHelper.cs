namespace Streetcode.XUnitTest.MediatR.Newss.Helpers
{
    using Moq;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;

    public static class MockRepoHelper
    {
        public static void SetupNewsCreate(Mock<IRepositoryWrapper> repo, News news)
        {
            repo.Setup(r => r.NewsRepository.Create(It.IsAny<News>()))
                .Returns(news);
        }

        public static void SetupSaveSuccess(Mock<IRepositoryWrapper> repo)
        {
            repo.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);
        }

        public static void SetupSaveFail(Mock<IRepositoryWrapper> repo)
        {
            repo.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);
        }

        public static void VerifyNewsCreateOnce(Mock<IRepositoryWrapper> repo)
        {
            repo.Verify(r => r.NewsRepository.Create(It.IsAny<News>()), Times.Once);
        }

        public static void VerifyNewsCreateNever(Mock<IRepositoryWrapper> repo)
        {
            repo.Verify(r => r.NewsRepository.Create(It.IsAny<News>()), Times.Never);
        }
    }
}
