using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.MediatR.Partners.GetByStreetcodeId;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Streetcode;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public class GetByStreetcodeIdPartnerHandlerTests : PartnerHandlerTestsBase
    {
        private readonly GetPartnersByStreetcodeIdHandler _handler;

        public GetByStreetcodeIdPartnerHandlerTests()
        {
            this._handler = new GetPartnersByStreetcodeIdHandler(
                this.MockMapper.Object,
                this.MockRepository.Object,
                this.MockLogger.Object);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnersExist()
        {
            // Arrange
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };
            var partners = new List<Partner>
            {
                PartnerTestHelpers.CreatePartnerEntity(1),
                PartnerTestHelpers.CreatePartnerEntity(2),
            };
            var partnerDTOs = new List<PartnerDTO>
            {
                PartnerTestHelpers.CreatePartnerDTO(1),
                PartnerTestHelpers.CreatePartnerDTO(2),
            };

            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcode);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);

            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(partnerDTOs);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(partners.Count);
            result.Value.Should().BeEquivalentTo(partnerDTOs);

            this.MockRepository.Verify(
                repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()),
                Times.Once);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenStreetcodeDoesNotExist()
        {
            // Arrange
            int streetcodeId = 999;

            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync((StreetcodeContent)null);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Contain($"Cannot find any partners with corresponding streetcode id: {streetcodeId}");

            this.MockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Never);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenPartnersRepositoryReturnsNull()
        {
            // Arrange
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };

            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcode);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((IEnumerable<Partner>)null);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Contain($"Cannot find a partners by a streetcode id: {streetcodeId}");

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
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };
            var emptyPartners = new List<Partner>();
            var emptyPartnerDTOs = new List<PartnerDTO>();

            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcode);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(emptyPartners);

            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(emptyPartnerDTOs);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_CallsMapper_WhenPartnersExist()
        {
            // Arrange
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };
            var partners = new List<Partner>
            {
                PartnerTestHelpers.CreatePartnerEntity(1),
                PartnerTestHelpers.CreatePartnerEntity(2),
            };
            var partnerDTOs = new List<PartnerDTO>
            {
                PartnerTestHelpers.CreatePartnerDTO(1),
                PartnerTestHelpers.CreatePartnerDTO(2),
            };

            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcode);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);

            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(partners))
                .Returns(partnerDTOs);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockMapper.Verify(
                mapper => mapper.Map<IEnumerable<PartnerDTO>>(partners),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ThrowsInvalidOperationException_WhenStreetcodeRepositoryThrowsException()
        {
            // Arrange
            int streetcodeId = 1;
            var expectedException = new InvalidOperationException("Database error");

            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ThrowsAsync(expectedException);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            Func<Task> act = async () => await this._handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Database error");
        }

        [Fact]
        public async Task Handle_ThrowsInvalidOperationException_WhenPartnersRepositoryThrowsException()
        {
            // Arrange
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };
            var expectedException = new InvalidOperationException("Database error");

            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcode);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ThrowsAsync(expectedException);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            Func<Task> act = async () => await this._handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Database error");
        }

        [Fact]
        public async Task Handle_CallsRepositoryWithInclude_WhenCalled()
        {
            // Arrange
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };
            var partners = new List<Partner> { PartnerTestHelpers.CreatePartnerEntity(1) };
            var partnerDTOs = new List<PartnerDTO> { PartnerTestHelpers.CreatePartnerDTO(1) };
            Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>> capturedInclude = null;

            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcode);

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (predicate, include) => capturedInclude = include)
                .ReturnsAsync(partners);

            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(partnerDTOs);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedInclude.Should().NotBeNull("because include expression should be provided");

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }
    }
}

