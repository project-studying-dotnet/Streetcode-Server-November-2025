namespace Streetcode.XUnitTest.MediatR.Newss.Helpers
{
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using System.Linq.Expressions;

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

        public static void SetupGetAllNews(Mock<IRepositoryWrapper> repo, IEnumerable<News> newsList)
        {
            repo.Setup(r => r.NewsRepository.GetAllAsync(
                It.IsAny<Expression<Func<News, bool>>>(),
                It.IsAny<Func<IQueryable<News>, IIncludableQueryable<News, object>>>()))
                .ReturnsAsync(newsList);
        }
    }
}
