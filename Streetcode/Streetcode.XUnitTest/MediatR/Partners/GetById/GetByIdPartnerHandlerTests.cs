using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.MediatR.Partners.GetById;
using Streetcode.DAL.Entities.Partners;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public class GetByIdPartnerHandlerTests : PartnerHandlerTestsBase
    {
        private readonly GetPartnerByIdHandler _handler;

        public GetByIdPartnerHandlerTests()
        {
            this._handler = new GetPartnerByIdHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        private void SetupGetSingleOrDefaultAsync(Partner partner)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partner);
        }

        private void SetupGetSingleOrDefaultAsyncToReturnNull()
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((Partner)null);
        }

        private void SetupRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ThrowsAsync(exception);
        }

        private Expression<Func<Partner, bool>> CapturePredicateFromRepository()
        {
            Expression<Func<Partner, bool>> capturedPredicate = null;
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (predicate, include) => capturedPredicate = predicate)
                .ReturnsAsync(PartnerTestHelpers.CreatePartnerEntity(1));
            return capturedPredicate;
        }

        private Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>> CaptureIncludeFromRepository()
        {
            Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>> capturedInclude = null;
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (predicate, include) => capturedInclude = include)
                .ReturnsAsync(PartnerTestHelpers.CreatePartnerEntity(1));
            return capturedInclude;
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnerExists()
        {
            // Arrange
            int partnerId = 1;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var partnerDTO = PartnerTestHelpers.CreatePartnerDTO(partnerId);

            this.SetupGetSingleOrDefaultAsync(partner);
            this.SetupMapperForPartnerDTO(partnerDTO);

            var query = new GetPartnerByIdQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(partnerId);
            result.Value.Should().BeEquivalentTo(partnerDTO);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenPartnerDoesNotExist()
        {
            // Arrange
            int partnerId = 999;

            this.SetupGetSingleOrDefaultAsyncToReturnNull();

            var query = new GetPartnerByIdQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Contain($"Cannot find any partner with corresponding id: {partnerId}");

            this.MockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_CallsMapper_WhenPartnerExists()
        {
            // Arrange
            int partnerId = 5;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var partnerDTO = PartnerTestHelpers.CreatePartnerDTO(partnerId);

            this.SetupGetSingleOrDefaultAsync(partner);
            this.SetupMapperForSpecificPartner(partner, partnerDTO);

            var query = new GetPartnerByIdQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockMapper.Verify(
                mapper => mapper.Map<PartnerDTO>(partner),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ThrowsInvalidOperationException_WhenRepositoryThrowsException()
        {
            // Arrange
            int partnerId = 1;
            var expectedException = new InvalidOperationException("Database error");

            this.SetupRepositoryToThrowException(expectedException);

            var query = new GetPartnerByIdQuery(partnerId);

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
            this.SetupMapperForPartnerDTO(partnerDTO);

            var query = new GetPartnerByIdQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedPredicate.Should().NotBeNull("because predicate should be provided");

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_CallsRepositoryWithInclude_WhenCalled()
        {
            // Arrange
            int partnerId = 1;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var partnerDTO = PartnerTestHelpers.CreatePartnerDTO(partnerId);

            var capturedInclude = this.CaptureIncludeFromRepository();
            this.SetupMapperForPartnerDTO(partnerDTO);

            var query = new GetPartnerByIdQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedInclude.Should().NotBeNull("because include expression should be provided");

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }
    }
}

