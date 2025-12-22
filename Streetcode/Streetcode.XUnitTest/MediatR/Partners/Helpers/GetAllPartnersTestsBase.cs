using Ardalis.Specification;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL;
using Streetcode.DAL.Entities.Partners;
using System.Linq.Expressions;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    /// <summary>
    /// Base class for testing GetAll partner handlers with shared test logic.
    /// </summary>
    /// <typeparam name="TQuery">The query type for retrieving partners.</typeparam>
    /// <typeparam name="TDto">The DTO type for partner data.</typeparam>
    public abstract class GetAllPartnersTestsBase<TQuery, TDto> : PartnerHandlerTestsBase
        where TQuery : IRequest<FluentResults.Result<IEnumerable<TDto>>>, new()
    {
        /// <summary>
        /// Gets the handler being tested.
        /// </summary>
        protected abstract IRequestHandler<TQuery, FluentResults.Result<IEnumerable<TDto>>> Handler { get; }

        /// <summary>
        /// Creates a collection of DTOs for testing.
        /// </summary>
        /// <param name="count">The number of DTOs to create.</param>
        /// <returns>A collection of DTOs.</returns>
        protected abstract IEnumerable<TDto> CreateDtos(int count);

        /// <summary>
        /// Creates an empty collection of DTOs for testing.
        /// </summary>
        /// <returns>An empty collection of DTOs.</returns>
        protected abstract IEnumerable<TDto> CreateEmptyDtos();

        /// <summary>
        /// Sets up the mapper to map specific partners to specific DTOs.
        /// </summary>
        /// <param name="partners">The partners to map from.</param>
        /// <param name="dtos">The DTOs to return.</param>
        protected abstract void SetupMapperForDtos(IEnumerable<Partner> partners, IEnumerable<TDto> dtos);

        /// <summary>
        /// Sets up the mapper to map any partners to specific DTOs.
        /// </summary>
        /// <param name="dtos">The DTOs to return.</param>
        protected abstract void SetupMapperForAnyPartners(IEnumerable<TDto> dtos);

        /// <summary>
        /// Verifies that the handler returns success when partners exist.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnersExist()
        {
            // Arrange
            var partners = PartnerTestHelpers.CreatePartnerEntities(2);
            var dtos = this.CreateDtos(2);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.ListAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(partners);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);

            this.SetupMapperForDtos(partners, dtos);

            var query = new TQuery();

            // Act
            var result = await this.Handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();

            this.VerifyMapperWasCalled(partners);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.ListAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()),
                Times.AtMostOnce);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.AtMostOnce);
        }

        /// <summary>
        /// Verifies that the handler returns failure when the repository returns null.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((IEnumerable<Partner>)null);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.ListAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<Partner>)null);

            var query = new TQuery();

            // Act
            var result = await this.Handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Contain(ErrorMessages.PartnerNotFound);

            this.MockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler returns success with an empty list when no partners exist.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnersListIsEmpty()
        {
            // Arrange
            var emptyPartners = new List<Partner>();
            var emptyDtos = this.CreateEmptyDtos();

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(emptyPartners);

            this.SetupMapperForAnyPartners(emptyDtos);

            var query = new TQuery();

            // Act
            var result = await this.Handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEmpty();
        }

        /// <summary>
        /// Verifies that the handler calls the mapper when partners exist.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CallsMapper_WhenPartnersExist()
        {
            // Arrange
            var partners = PartnerTestHelpers.CreatePartnerEntities(2);
            var dtos = this.CreateDtos(2);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.ListAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(partners);

            this.SetupMapperForDtos(partners, dtos);

            var query = new TQuery();

            // Act
            var result = await this.Handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.VerifyMapperWasCalled(partners);
        }

        /// <summary>
        /// Verifies that the handler propagates InvalidOperationException when the repository throws an exception.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ThrowsInvalidOperationException_WhenRepositoryThrowsException()
        {
            // Arrange
            var expectedException = new InvalidOperationException(ErrorMessages.DatabaseConntectionFailed);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ThrowsAsync(expectedException);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.ListAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(expectedException);

            var query = new TQuery();

            // Act
            Func<Task> act = async () => await this.Handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage(ErrorMessages.DatabaseConntectionFailed);
        }

        /// <summary>
        /// Verifies that the mapper was called with the expected partners.
        /// </summary>
        /// <param name="partners">The partners that should have been passed to the mapper.</param>
        protected abstract void VerifyMapperWasCalled(IEnumerable<Partner> partners);
    }
}
