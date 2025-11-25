namespace Streetcode.XUnitTest.MediatR.Newss.Helpers
{
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.DAL.Entities.Media.Images;
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

        public static void SetupGetAllNews(Mock<IRepositoryWrapper> repo, IEnumerable<News> newsList)
        {
            repo.Setup(r => r.NewsRepository.GetAllAsync(
                It.IsAny<Expression<Func<News, bool>>>(),
                It.IsAny<Func<IQueryable<News>, IIncludableQueryable<News, object>>>()))
                .ReturnsAsync(newsList);
        }

        public static void SetupGetNewsById(Mock<IRepositoryWrapper> repo, News news)
        {
            repo.Setup(r => r.NewsRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<News, bool>>>(),
                It.IsAny<Func<IQueryable<News>, IIncludableQueryable<News, object>>>()))
                .ReturnsAsync(news);
        }

        public static void SetupGetNewsByUrl(Mock<IRepositoryWrapper> repo, News news)
        {
            repo.Setup(r => r.NewsRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<News, bool>>>(),
                It.IsAny<Func<IQueryable<News>, IIncludableQueryable<News, object>>>()))
                .ReturnsAsync(news);
        }

        public static void SetupUpdate(Mock<IRepositoryWrapper> repoMock)
        {
            repoMock.Setup(r => r.NewsRepository.Update(It.IsAny<News>()));
        }

        public static void VerifySaveChangesOnce(Mock<IRepositoryWrapper> repo)
        {
            repo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        public static void SetupGetImageById(Mock<IRepositoryWrapper> repo, Image image)
        {
            repo.Setup(r => r.ImageRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Image, bool>>>(),
                It.IsAny<Func<IQueryable<Image>, IIncludableQueryable<Image, object>>>()))
                .ReturnsAsync(image);
        }

        public static void VerifyImageDeleteOnce(Mock<IRepositoryWrapper> repo)
        {
            repo.Verify(r => r.ImageRepository.Delete(It.IsAny<Image>()), Times.Once);
        }

        public static void VerifyImageDeleteNever(Mock<IRepositoryWrapper> repo)
        {
            repo.Verify(r => r.ImageRepository.Delete(It.IsAny<Image>()), Times.Never);
        }

        public static void VerifyNewsUpdateOnce(Mock<IRepositoryWrapper> repo, int newsId)
        {
            repo.Verify(r => r.NewsRepository.Update(It.Is<News>(n => n.Id == newsId)), Times.Once);
        }

        public static void VerifyNewsUpdateOnce(Mock<IRepositoryWrapper> repo)
        {
            repo.Verify(r => r.NewsRepository.Update(It.IsAny<News>()), Times.Once);
        }
    }
}
