namespace Streetcode.XUnitTest.MediatR.Email
{
    using global::MediatR;
    using Moq;
    using Streetcode.BLL.DTO.Email;
    using Streetcode.BLL.Interfaces.Email;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Email;
    using Streetcode.DAL.Entities.AdditionalContent.Email;
    using Xunit;

    public class SendEmailHandlerTests
    {
        private const string EmailSentErrorMessage = "Failed to send email message";

        private readonly Mock<IEmailService> emailServiceMock;
        private readonly Mock<ILoggerService> loggerServiceMock;
        private readonly SendEmailHandler handler;
        public SendEmailHandlerTests()
        {
            this.emailServiceMock = new Mock<IEmailService>();
            this.loggerServiceMock = new Mock<ILoggerService>();
            this.handler = new SendEmailHandler(this.emailServiceMock.Object, this.loggerServiceMock.Object);
        }

        [Fact]
        public async Task Handle_EmailSentSuccessfully_ReturnsSuccess()
        {
            // Arrange
            this.emailServiceMock
                .Setup(s => s.SendEmailAsync(It.IsAny<Message>()))
                .ReturnsAsync(true);

            SendEmailCommand command = new SendEmailCommand(new EmailDTO()
            {
                From = "test@test.com",
                Content = "test content",
            });

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(result.Value, Unit.Value);
        }

        [Fact]
        public async Task Handle_EmailSendFails_ReturnsFail()
        {
            // Arrange
            this.emailServiceMock
                .Setup(s => s.SendEmailAsync(It.IsAny<Message>()))
                .ReturnsAsync(false);

            SendEmailCommand command = new SendEmailCommand(new EmailDTO()
            {
                From = "fail@email.com",
                Content = "fail attempt",
            });

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(EmailSentErrorMessage, result.Errors[0].Message);
            this.loggerServiceMock.Verify(
                l => l.LogError(command, EmailSentErrorMessage),
                Times.Once,
                "LogError method should be called exactly once when email sending fails");
        }
    }
}
