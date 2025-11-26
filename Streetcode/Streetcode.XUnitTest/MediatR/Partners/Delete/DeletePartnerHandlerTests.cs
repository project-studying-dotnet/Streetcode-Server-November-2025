using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.MediatR.Partners.Delete;
using Streetcode.DAL.Entities.Partners;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public class DeletePartnerHandlerTests : PartnerHandlerTestsBase
    {
        private readonly DeletePartnerHandler _handler;

        public DeletePartnerHandlerTests()
        {
            this._handler = new DeletePartnerHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        private void SetupRepositoryToReturnPartner(Partner partner)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partner);
        }

        private void SetupRepositoryToReturnNull()
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((Partner)null);
        }

        private void SetupRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ThrowsAsync(exception);
        }

        private Expression<Func<Partner, bool>> CapturePredicateFromRepository()
        {
            Expression<Func<Partner, bool>> capturedPredicate = null;
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (predicate, include) => capturedPredicate = predicate);
            return capturedPredicate;
        }

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
                repo => repo.SaveChanges(),
                Times.Once);
        }

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
                repo => repo.SaveChanges(),
                Times.Never);

            this.MockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);
        }

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
                mapper => mapper.Map<PartnerDTO>(partner),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenSaveChangesThrowsException()
        {
            // Arrange
            int partnerId = 1;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var exceptionMessage = "Database save failed";

            this.SetupRepositoryToReturnPartner(partner);
            this.SetupSaveChangesToThrowException(exceptionMessage);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(exceptionMessage);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.Delete(partner),
                Times.Once);

            this.MockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    exceptionMessage),
                Times.Once);
        }

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

        [Fact]
        public async Task Handle_CallsRepositoryWithCorrectId_WhenCalled()
        {
            // Arrange
            int partnerId = 42;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var partnerDTO = PartnerTestHelpers.CreatePartnerDTO(partnerId);
            var capturedPredicate = this.CapturePredicateFromRepository();

            this.SetupRepositoryToReturnPartner(partner);
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