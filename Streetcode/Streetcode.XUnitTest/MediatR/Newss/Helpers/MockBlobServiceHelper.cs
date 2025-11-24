using Moq;
using Streetcode.BLL.Interfaces.BlobStorage;

namespace Streetcode.XUnitTest.MediatR.Newss.Helpers
{
    public static class MockBlobServiceHelper
    {
        public static void SetupBlobService(Mock<IBlobService> blobService, string base64 = "base64string")
        {
            blobService.Setup(b => b.FindFileInStorageAsBase64(It.IsAny<string>()))
                       .Returns(base64);
        }
    }
}
