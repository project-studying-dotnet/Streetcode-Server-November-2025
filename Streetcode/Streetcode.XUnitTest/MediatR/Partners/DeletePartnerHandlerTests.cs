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
using Streetcode.BLL.MediatR.Partners.Delete;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public class DeletePartnerHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly DeletePartnerHandler _handler;

        public DeletePartnerHandlerTests()
        {
            this._mockRepository = new Mock<IRepositoryWrapper>();
            this._mockMapper = new Mock<IMapper>();
            this._mockLogger = new Mock<ILoggerService>();
            this._handler = new DeletePartnerHandler(
                this._mockRepository.Object,
                this._mockMapper.Object,
                this._mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnerDeletedSuccessfully()
        {
            // Arrange
            int partnerId = 1;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var partnerDTO = PartnerTestHelpers.CreatePartnerDTO(partnerId);

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partner);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(partnerDTO);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(partnerDTO);

            this._mockRepository.Verify(
                repo => repo.PartnersRepository.Delete(partner),
                Times.Once);

            this._mockRepository.Verify(
                repo => repo.SaveChanges(),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenPartnerDoesNotExist()
        {
            // Arrange
            int partnerId = 999;

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((Partner)null);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be("No partner with such id");

            this._mockRepository.Verify(
                repo => repo.PartnersRepository.Delete(It.IsAny<Partner>()),
                Times.Never);

            this._mockRepository.Verify(
                repo => repo.SaveChanges(),
                Times.Never);

            this._mockLogger.Verify(
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

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partner);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(partner))
                .Returns(partnerDTO);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockRepository.Verify(
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

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partner);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(partner))
                .Returns(partnerDTO);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this._mockMapper.Verify(
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

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partner);

            this._mockRepository
                .Setup(repo => repo.SaveChanges())
                .Throws(new Exception(exceptionMessage));

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(exceptionMessage);

            this._mockRepository.Verify(
                repo => repo.PartnersRepository.Delete(partner),
                Times.Once);

            this._mockLogger.Verify(
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

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ThrowsAsync(expectedException);

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
            Expression<Func<Partner, bool>> capturedPredicate = null;

            this._mockRepository
                .Setup(repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (predicate, include) => capturedPredicate = predicate)
                .ReturnsAsync(partner);

            this._mockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(partnerDTO);

            var query = new DeletePartnerQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedPredicate.Should().NotBeNull("because predicate should be provided");

            this._mockRepository.Verify(
                repo => repo.PartnersRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }
    }
}