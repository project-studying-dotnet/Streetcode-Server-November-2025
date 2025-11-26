namespace Streetcode.XUnitTest.MediatR.Newss.Helpers
{
    using Moq;
    using Streetcode.BLL.Interfaces.BlobStorage;

    public static class MockBlobServiceHelper
    {
        public static void SetupBlobService(Mock<IBlobService> blobService, string base64 = "base64string")
        {
            blobService.Setup(b => b.FindFileInStorageAsBase64(It.IsAny<string>()))
                       .Returns(base64);
        }

        public static void VerifyNever(Mock<IBlobService> blobService)
        {
            blobService.Verify(
                b => b.FindFileInStorageAsBase64(It.IsAny<string>()),
                Times.Never);
        }

        public static void VerifyTimes(Mock<IBlobService> blobService, int times)
        {
            blobService.Verify(
                b => b.FindFileInStorageAsBase64(It.IsAny<string>()),
                Times.Exactly(times));
        }
    }
}