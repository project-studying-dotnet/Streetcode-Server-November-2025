using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Partners.GetAllPartnerShort;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public class GetAllPartnerShortHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly GetAllPartnerShortHandler _handler;

        public GetAllPartnerShortHandlerTests()
        {
            this._mockRepository = new Mock<IRepositoryWrapper>();
            this._mockMapper = new Mock<IMapper>();
            this._mockLogger = new Mock<ILoggerService>();
            this._handler = new GetAllPartnerShortHandler(
                this._mockRepository.Object,
                this._mockMapper.Object,
                this._mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnersExist()
        {
            // Arrange
            var partners = new List<Partner>
            {
                PartnerTestHelpers.CreatePartnerEntity(1),
                PartnerTestHelpers.CreatePartnerEntity(2),
                PartnerTestHelpers.CreatePartnerEntity(3),
            };
            var partnerShortDTOs = new List<PartnerShortDTO>
            {
                new PartnerShortDTO { Id = 1, Title = "Test Partner 1" },
                new PartnerShortDTO { Id = 2, Title = "Test Partner 2" },
                new PartnerShortDTO { Id = 3, Title = "Test Partner 3" },
            };

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);

            this._mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerShortDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(partnerShortDTOs);

            var query = new GetAllPartnersShortQuery();

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(partners.Count);
            result.Value.Should().BeEquivalentTo(partnerShortDTOs);

            this._mockRepository.Verify(
                repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenRepositoryReturnsNull()
        {
            // Arrange
            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((IEnumerable<Partner>)null);

            var query = new GetAllPartnersShortQuery();

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Contain("Cannot find any partners");

            this._mockLogger.Verify(
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
            var emptyPartnerShortDTOs = new List<PartnerShortDTO>();

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(emptyPartners);

            this._mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerShortDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(emptyPartnerShortDTOs);

            var query = new GetAllPartnersShortQuery();

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
            var partners = new List<Partner>
            {
                PartnerTestHelpers.CreatePartnerEntity(1),
                PartnerTestHelpers.CreatePartnerEntity(2),
            };
            var partnerShortDTOs = new List<PartnerShortDTO>
            {
                new PartnerShortDTO { Id = 1, Title = "Test Partner 1" },
                new PartnerShortDTO { Id = 2, Title = "Test Partner 2" },
            };

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);

            this._mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerShortDTO>>(partners))
                .Returns(partnerShortDTOs);

            var query = new GetAllPartnersShortQuery();

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockMapper.Verify(
                mapper => mapper.Map<IEnumerable<PartnerShortDTO>>(partners),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ThrowsInvalidOperationException_WhenRepositoryThrowsException()
        {
            // Arrange
            var expectedException = new InvalidOperationException("Database connection failed");

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ThrowsAsync(expectedException);

            var query = new GetAllPartnersShortQuery();

            // Act
            Func<Task> act = async () => await this._handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Database connection failed");
        }
    }
}
