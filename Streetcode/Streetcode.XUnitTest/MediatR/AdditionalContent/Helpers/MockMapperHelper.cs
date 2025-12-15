namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers
{
    using AutoMapper;
    using Moq;

    public static class MockMapperHelper
    {
        public static void SetupMap<TSource, TDestination>(
            this Mock<IMapper> mapperMock,
            TDestination destination)
        {
            mapperMock
                .Setup(m => m.Map<TDestination>(It.IsAny<TSource>()))
                .Returns(destination);
        }
    }
}
