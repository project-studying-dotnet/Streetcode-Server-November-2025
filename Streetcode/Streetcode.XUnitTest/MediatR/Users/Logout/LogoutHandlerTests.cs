namespace Streetcode.XUnitTest.MediatR.Users.Logout
{
    using System.Linq.Expressions;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Users;
    using Streetcode.BLL.MediatR.Users.Logout;
    using Streetcode.DAL.Entities.Jwt;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class LogoutHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepo;
        private readonly LogoutHandler handler;

        public LogoutHandlerTests()
        {
            this.mockRepo = new Mock<IRepositoryWrapper>();
            this.handler = new LogoutHandler(this.mockRepo.Object);
        }

        [Fact]
        public async Task Handle_ValidTokenAndUser_ReturnsSuccess()
        {
            var tokenString = "valid_refresh_token";
            int userId = 1;

            var requestDto = new LogoutRequestDto { RefreshToken = tokenString };
            var command = new LogoutCommand(requestDto, userId);

            var existingTokenEntity = new RefreshToken { Token = tokenString, Id = 1, UserId = userId };

            this.mockRepo.Setup(r => r.RefreshTokenRepository
                .GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>(), null))
                .ReturnsAsync(existingTokenEntity);

            this.mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            var result = await this.handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();

            this.mockRepo.Verify(r => r.RefreshTokenRepository.Delete(existingTokenEntity), Times.Once);
            this.mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_TokenNotFound_ReturnsSuccess_Idempotency()
        {
            var tokenString = "invalid_or_missing_token";
            int userId = 1;

            var command = new LogoutCommand(new LogoutRequestDto { RefreshToken = tokenString }, userId);

            this.mockRepo.Setup(r => r.RefreshTokenRepository
                .GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>(), null))
                .ReturnsAsync((RefreshToken)null);

            var result = await this.handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();

            this.mockRepo.Verify(r => r.RefreshTokenRepository.Delete(It.IsAny<RefreshToken>()), Times.Never);
            this.mockRepo.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_TokenExistsButWrongUser_ReturnsSuccess_DoesNotDelete()
        {
            var tokenString = "token_belongs_to_other";
            int requestUserId = 1; 

            var command = new LogoutCommand(new LogoutRequestDto { RefreshToken = tokenString }, requestUserId);

            this.mockRepo.Setup(r => r.RefreshTokenRepository
                .GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>(), null))
                .ReturnsAsync((RefreshToken)null);

            var result = await this.handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue(); 
            this.mockRepo.Verify(r => r.RefreshTokenRepository.Delete(It.IsAny<RefreshToken>()), Times.Never);
        }
    }
}