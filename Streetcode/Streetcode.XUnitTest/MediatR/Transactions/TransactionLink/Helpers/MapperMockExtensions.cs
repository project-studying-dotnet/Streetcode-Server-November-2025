using AutoMapper;
using Moq;

namespace Streetcode.XUnitTest.MediatR.Transactions.TransactionLink.Helpers
{
    public static class MapperMockExtensions
    {
        public static void SetupMapperNull(this Mock<IMapper> mapperMock)
        {
            mapperMock
                .Setup(m => m.Map<object>(It.IsAny<object>(), It.IsAny<System.Action<IMappingOperationOptions<object, object>>>()))
                .Returns((object)null);
        }
    }
}