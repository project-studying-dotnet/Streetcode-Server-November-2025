namespace Streetcode.XUnitTest.MediatR.Partners
{
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
    using Streetcode.BLL.MediatR.Partners.GetAll;
    using Streetcode.DAL.Entities.Partners;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetAllPartnersHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly GetAllPartnersHandler _handler;

        public GetAllPartnersHandlerTests()
        {
            this._mockRepository = new Mock<IRepositoryWrapper>();
            this._mockMapper = new Mock<IMapper>();
            this._mockLogger = new Mock<ILoggerService>();
            this._handler = new GetAllPartnersHandler(
                this._mockRepository.Object,
                this._mockMapper.Object,
                this._mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnersExist()
        {
            // Arrange
            var partners = new List<Partner> { PartnerTestHelpers.CreatePartnerEntity(1), PartnerTestHelpers.CreatePartnerEntity(2), PartnerTestHelpers.CreatePartnerEntity(3) };
            var partnerDTOs = new List<PartnerDTO> { PartnerTestHelpers.CreatePartnerDTO(1), PartnerTestHelpers.CreatePartnerDTO(2), PartnerTestHelpers.CreatePartnerDTO(3) };

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);

            this._mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(partnerDTOs);

            var query = new GetAllPartnersQuery();

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(partners.Count);
            result.Value.Should().BeEquivalentTo(partnerDTOs);

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

            var query = new GetAllPartnersQuery();

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
            var emptyPartnerDTOs = new List<PartnerDTO>();

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(emptyPartners);

            this._mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(emptyPartnerDTOs);

            var query = new GetAllPartnersQuery();

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
            var partners = new List<Partner> { PartnerTestHelpers.CreatePartnerEntity(1), PartnerTestHelpers.CreatePartnerEntity(2) };
            var partnerDTOs = new List<PartnerDTO> { PartnerTestHelpers.CreatePartnerDTO(1), PartnerTestHelpers.CreatePartnerDTO(2) };

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);

            this._mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(partners))
                .Returns(partnerDTOs);

            var query = new GetAllPartnersQuery();

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockMapper.Verify(
                mapper => mapper.Map<IEnumerable<PartnerDTO>>(partners),
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

            var query = new GetAllPartnersQuery();

            // Act
            Func<Task> act = async () => await this._handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Database connection failed");
        }

        [Fact]
        public async Task Handle_CallsRepositoryWithInclude_WhenCalled()
        {
            // Arrange
            var partners = new List<Partner> { PartnerTestHelpers.CreatePartnerEntity(1) };
            var partnerDTOs = new List<PartnerDTO> { PartnerTestHelpers.CreatePartnerDTO(1) };
            Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>> capturedInclude = null;

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (predicate, include) => capturedInclude = include)
                .ReturnsAsync(partners);

            this._mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(partnerDTOs);

            var query = new GetAllPartnersQuery();

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedInclude.Should().NotBeNull("because include expression should be provided");
            
            this._mockRepository.Verify(
                repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }
    }
}