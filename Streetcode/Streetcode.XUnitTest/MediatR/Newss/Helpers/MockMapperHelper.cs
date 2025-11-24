namespace Streetcode.XUnitTest.MediatR.Newss.Helpers
{
    using AutoMapper;
    using Moq;
    using Streetcode.BLL.DTO.News;
    using Streetcode.DAL.Entities.News;

    public static class MockMapperHelper
    {
        public static void SetupMapper<TSource, TDestination>(
            Mock<IMapper> mapper,
            TSource source,
            TDestination destination)
        {
            mapper.Setup(m => m.Map<TDestination>(It.IsAny<TSource>()))
                  .Returns(destination);
        }

        public static void SetupMapNewsList(Mock<IMapper> mapper, IEnumerable<News> source, IEnumerable<NewsDTO> destination)
        {
            mapper.Setup(m => m.Map<IEnumerable<NewsDTO>>(It.IsAny<IEnumerable<News>>()))
                  .Returns(destination);
        }

        public static void VerifyMapOnce<TSource, TDestination>(Mock<IMapper> mapper)
        {
            mapper.Verify(m => m.Map<TDestination>(It.IsAny<TSource>()), Times.Once);
        }

        public static void VerifyMapNever<TSource, TDestination>(Mock<IMapper> mapper)
        {
            mapper.Verify(m => m.Map<TDestination>(It.IsAny<TSource>()), Times.Never);
        }
    }
}
