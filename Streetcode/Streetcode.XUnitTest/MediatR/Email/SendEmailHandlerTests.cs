namespace Streetcode.XUnitTest.MediatR.Email
{
    using global::MediatR;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.Email;
    using Streetcode.BLL.Interfaces.Email;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Email;
    using Streetcode.DAL.Entities.AdditionalContent.Email;
    using Xunit;

    public class SendEmailHandlerTests
    {
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

            var email = "test@test.com";
            var content = "test content";

            SendEmailCommand command = new SendEmailCommand(new EmailDto()
            {
                From = email,
                Content = content,
            });

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(Unit.Value, result.Value);

            this.loggerServiceMock.Verify(
                l => l.LogError(It.IsAny<object>(), It.IsAny<string>()),
                Times.Never,
                ErrorMessages.VerifyLoggerCalledEmailSentSuccess);

            this.emailServiceMock.Verify(
                s => s.SendEmailAsync(It.Is<Message>(m =>
                    m.From == email &&
                    m.Content == content)),
                Times.Once,
                ErrorMessages.VerifyEmailSentOnce);
        }

        [Fact]
        public async Task Handle_EmailSendFails_ReturnsFail()
        {
            // Arrange
            this.emailServiceMock
                .Setup(s => s.SendEmailAsync(It.IsAny<Message>()))
                .ReturnsAsync(false);

            var email = "fail@email.com";
            var content = "fail attempt";

            SendEmailCommand command = new SendEmailCommand(new EmailDto()
            {
                From = email,
                Content = content,
            });

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(ErrorMessages.CannotSaveChangesInDatabase, result.Errors[0].Message);

            this.loggerServiceMock.Verify(
                l => l.LogError(command, ErrorMessages.CannotSaveChangesInDatabase),
                Times.Once,
                ErrorMessages.VerifyLoggerCalledOnceEmailSentFail);

            this.emailServiceMock.Verify(
                s => s.SendEmailAsync(It.Is<Message>(m =>
                    m.From == email &&
                    m.Content == content)),
                Times.Once,
                ErrorMessages.VerifyEmailSentOnce);
        }
    }
}
