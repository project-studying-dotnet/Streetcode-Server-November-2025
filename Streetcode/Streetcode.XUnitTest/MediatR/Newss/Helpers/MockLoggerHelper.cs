namespace Streetcode.XUnitTest.MediatR.Newss.Helpers
{
    using Moq;
 using global::Streetcode.BLL.Interfaces.Logging;

    public static class MockLoggerHelper
    {
        public static void VerifyLogErrorOnce(Mock<ILoggerService> logger)
        {
            logger.Verify(
                l => l.LogError(It.IsAny<object>(), It.IsAny<string>()),
                Times.Once);
        }

        public static void VerifyLogErrorNever(Mock<ILoggerService> logger)
        {
            logger.Verify(
                l => l.LogError(It.IsAny<object>(), It.IsAny<string>()),
                Times.Never);
        }

        public static void VerifyLogErrorOnceWithMessage(Mock<ILoggerService> logger, string expectedMessage)
        {
            logger.Verify(
                l => l.LogError(It.IsAny<object>(), It.Is<string>(msg => msg == expectedMessage)),
                Times.Once);
        }
    }
}