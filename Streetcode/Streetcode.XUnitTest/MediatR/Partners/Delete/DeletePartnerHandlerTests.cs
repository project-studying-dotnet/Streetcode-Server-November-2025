using System.Linq.Expressions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.MediatR.Partners.Delete;
using Streetcode.DAL.Entities.Partners;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    /// <summary>
    /// Unit tests for <see cref="DeletePartnerHandler"/>.
    /// </summary>
    public class DeletePartnerHandlerTests : PartnerHandlerTestsBase
    {
        private readonly DeletePartnerHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePartnerHandlerTests"/> class.
        /// </summary>
        public DeletePartnerHandlerTests()
        {
            this._handler = new DeletePartnerHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        /// <summary>
        /// Sets up the repository to return a specific partner when queried.
        /// </summary>
        /// <param name="partner">The partner to return.</param>
        private void SetupRepositoryToReturnPartner(Partner partner)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partner);
        }

        /// <summary>
        /// Sets up the repository to return null when queried.
        /// </summary>
        private void SetupRepositoryToReturnNull()
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((Partner)null);
        }

        /// <summary>
        /// Sets up the repository to throw an exception when queried.
        /// </summary>
        /// <param name="exception">The exception to throw.</param>
        private void SetupRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ThrowsAsync(exception);
        }

        /// <summary>
        /// Verifies that the handler returns success when a partner is deleted successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnerDeletedSuccessfully()
        {
            // Arrange
            int partnerId = 1;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var partnerDTO = PartnerTestHelpers.CreatePartnerDTO(partnerId);

            this.SetupRepositoryToReturnPartner(partner);
            this.SetupMapperForPartnerDTO(partnerDTO);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(partnerDTO);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.Delete(partner),
                Times.Once);

            this.MockRepository.Verify(
                repo => repo.SaveChangesAsync(),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler returns failure when attempting to delete a partner that does not exist.
        /// </summary>
        /// <param name="partnerId">The ID of the non-existent partner.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(-1)]
        public async Task Handle_ReturnsFailure_WhenPartnerDoesNotExist(int partnerId)
        {
            // Arrange
            this.SetupRepositoryToReturnNull();
            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be("No partner with such id");

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.Delete(It.IsAny<Partner>()),
                Times.Never);

            this.MockRepository.Verify(
                repo => repo.SaveChangesAsync(),
                Times.Never);

            this.MockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler calls Delete method when a partner exists.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CallsDelete_WhenPartnerExists()
        {
            // Arrange
            int partnerId = 5;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var partnerDTO = PartnerTestHelpers.CreatePartnerDTO(partnerId);

            this.SetupRepositoryToReturnPartner(partner);
            this.SetupMapperForSpecificPartner(partner, partnerDTO);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockRepository.Verify(
                repo => repo.PartnersRepository.Delete(partner),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler calls the mapper when a partner is deleted successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CallsMapper_WhenPartnerDeletedSuccessfully()
        {
            // Arrange
            int partnerId = 3;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var partnerDTO = PartnerTestHelpers.CreatePartnerDTO(partnerId);

            this.SetupRepositoryToReturnPartner(partner);
            this.SetupMapperForSpecificPartner(partner, partnerDTO);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockMapper.Verify(
                mapper => mapper.Map<PartnerDto>(partner),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler throws an exception when SaveChanges fails.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ThrowsException_WhenSaveChangesThrowsException()
        {
            // Arrange
            int partnerId = 1;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var exceptionMessage = "Database save failed";

            this.SetupRepositoryToReturnPartner(partner);
            this.SetupSaveChangesToThrowException(exceptionMessage);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            Func<Task> act = async () => await this._handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage(exceptionMessage);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.Delete(partner),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler propagates exceptions thrown by the repository.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            int partnerId = 1;
            var expectedException = new InvalidOperationException("Database error");

            this.SetupRepositoryToThrowException(expectedException);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            Func<Task> act = async () => await this._handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Database error");
        }

        /// <summary>
        /// Verifies that the handler calls the repository with the correct partner ID.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CallsRepositoryWithCorrectId_WhenCalled()
        {
            // Arrange
            int partnerId = 42;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var partnerDTO = PartnerTestHelpers.CreatePartnerDTO(partnerId);

            Expression<Func<Partner, bool>> capturedPredicate = null;
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (pred, include) => capturedPredicate = pred)
                .ReturnsAsync(partner);
            this.SetupMapperForPartnerDTO(partnerDTO);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedPredicate.Should().NotBeNull("because predicate should be provided");

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }
    }
}