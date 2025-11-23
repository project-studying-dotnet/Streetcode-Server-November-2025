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

        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<ILoggerService> _loggerServiceMock;
        private readonly SendEmailHandler handler;
        public SendEmailHandlerTests()
        {
            this._emailServiceMock = new Mock<IEmailService>();
            this._loggerServiceMock = new Mock<ILoggerService>();
            this.handler = new SendEmailHandler(this._emailServiceMock.Object, this._loggerServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess_WhenEmailSentSuccessfully()
        {
            // Arrange
            this._emailServiceMock
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
        public async Task Handle_Should_ReturnFail_WhenEmailNotSent()
        {
            // Arrange
            this._emailServiceMock
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
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(EmailSentErrorMessage, result.Errors[0].Message);
            this._loggerServiceMock.Verify(l => l.LogError(command, EmailSentErrorMessage));
        }
    }
}
