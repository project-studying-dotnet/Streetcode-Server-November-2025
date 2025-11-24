namespace Streetcode.XUnitTest.MediatR.Newss.Helpers
{
    using AutoMapper;
    using Moq;

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

        public static void VerifyMapOnce<TSource, TDestination>(Mock<IMapper> mapper)
        {
            mapper.Verify(m => m.Map<TDestination>(It.IsAny<TSource>()), Times.Once);
        }
    }
}
