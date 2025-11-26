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

        public static void SetupMapCollection<TSource, TDestination>(
                Mock<IMapper> mapper,
                IEnumerable<TSource> source,
                IEnumerable<TDestination> destination)
        {
            mapper.Setup(m => m.Map<IEnumerable<TDestination>>(It.IsAny<IEnumerable<TSource>>()))
                  .Returns(destination);
        }

        public static void VerifyMap<TSource, TDestination>(Mock<IMapper> mapper, Times times)
        {
            mapper.Verify(m => m.Map<TDestination>(It.IsAny<TSource>()), times);
        }

        public static void VerifyMapCollection<TSource, TDestination>(Mock<IMapper> mapper, Times times)
        {
            mapper.Verify(m => m.Map<IEnumerable<TDestination>>(It.IsAny<IEnumerable<TSource>>()), times);
        }
    }
}
