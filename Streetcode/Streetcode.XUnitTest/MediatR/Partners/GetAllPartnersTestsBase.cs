using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Partners;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public abstract class GetAllPartnersTestsBase<TQuery, TDto> : PartnerHandlerTestsBase
        where TQuery : IRequest<FluentResults.Result<IEnumerable<TDto>>>, new()
    {
        protected abstract IRequestHandler<TQuery, FluentResults.Result<IEnumerable<TDto>>> Handler { get; }

        protected abstract IEnumerable<TDto> CreateDtos(int count);

        protected abstract IEnumerable<TDto> CreateEmptyDtos();

        protected abstract void SetupMapperForDtos(IEnumerable<Partner> partners, IEnumerable<TDto> dtos);

        protected abstract void SetupMapperForAnyPartners(IEnumerable<TDto> dtos);

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnersExist()
        {
            // Arrange
            var partners = PartnerTestHelpers.CreatePartnerEntities(3);
            var dtos = this.CreateDtos(3);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);

            this.SetupMapperForAnyPartners(dtos);

            var query = new TQuery();

            // Act
            var result = await this.Handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(partners.Count);
            result.Value.Should().BeEquivalentTo(dtos);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((IEnumerable<Partner>)null);

            var query = new TQuery();

            // Act
            var result = await this.Handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Contain("Cannot find any partners");

            this.MockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);
        }

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

            this.SetupMapperForDtos(partners, dtos);

            var query = new TQuery();

            // Act
            var result = await this.Handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.VerifyMapperWasCalled(partners);
        }

        [Fact]
        public async Task Handle_ThrowsInvalidOperationException_WhenRepositoryThrowsException()
        {
            // Arrange
            var expectedException = new InvalidOperationException("Database connection failed");

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ThrowsAsync(expectedException);

            var query = new TQuery();

            // Act
            Func<Task> act = async () => await this.Handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Database connection failed");
        }

        protected abstract void VerifyMapperWasCalled(IEnumerable<Partner> partners);
    }
}
