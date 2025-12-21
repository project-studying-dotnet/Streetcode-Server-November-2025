namespace Streetcode.XUnitTest.MediatR.Users.Logout
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using Streetcode.BLL.DTO.Users;
    using Streetcode.BLL.MediatR.Users.Logout;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using System.Linq.Expressions;
    using Streetcode.DAL.Entities.Jwt;
    public class LogoutHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepo;
        private readonly LogoutHandler _handler;

        public LogoutHandlerTests()
        {
            this._mockRepo = new Mock<IRepositoryWrapper>();
            this._handler = new LogoutHandler(this._mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ValidToken_ReturnsSuccess()
        {
            var tokenString = "valid_refresh_token";
            var requestDto = new LogoutRequestDto { RefreshToken = tokenString };
            var command = new LogoutCommand(requestDto);

            var existingTokenEntity = new RefreshToken { Token = tokenString, Id = 1 };

            this._mockRepo.Setup(r => r.RefreshTokenRepository
                .GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>(), null))
                .ReturnsAsync(existingTokenEntity);

            this._mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            var result = await this._handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();

            this._mockRepo.Verify(r => r.RefreshTokenRepository.Delete(existingTokenEntity), Times.Once);
            this._mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidToken_ReturnsFail()
        {
            var tokenString = "invalid_token";
            var command = new LogoutCommand(new LogoutRequestDto { RefreshToken = tokenString });

            this._mockRepo.Setup(r => r.RefreshTokenRepository
                .GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>(), null))
                .ReturnsAsync((RefreshToken)null);

            var result = await this._handler.Handle(command, CancellationToken.None);

            result.IsFailed.Should().BeTrue();
            result.Reasons[0].Message.Should().Contain("not found");

            this._mockRepo.Verify(r => r.RefreshTokenRepository.Delete(It.IsAny<RefreshToken>()), Times.Never);
        }
    }
}