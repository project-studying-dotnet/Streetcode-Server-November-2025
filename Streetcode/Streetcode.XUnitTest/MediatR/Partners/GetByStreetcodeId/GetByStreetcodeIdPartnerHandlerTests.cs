using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using global::Streetcode.BLL;
using global::Streetcode.BLL.DTO.Partners;
using global::Streetcode.BLL.MediatR.Partners.GetByStreetcodeId;
using global::Streetcode.DAL.Entities.Partners;
using global::Streetcode.DAL.Entities.Streetcode;
using System.Linq.Expressions;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    /// <summary>
    /// Unit tests for <see cref="GetPartnersByStreetcodeIdHandler"/>.
    /// </summary>
    public class GetByStreetcodeIdPartnerHandlerTests : PartnerHandlerTestsBase
    {
        private readonly GetPartnersByStreetcodeIdHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByStreetcodeIdPartnerHandlerTests"/> class.
        /// </summary>
        public GetByStreetcodeIdPartnerHandlerTests()
        {
            this._handler = new GetPartnersByStreetcodeIdHandler(
                this.MockMapper.Object,
                this.MockRepository.Object,
                this.MockLogger.Object);
        }

        /// <summary>
        /// Sets up the streetcode repository to return a specific streetcode.
        /// </summary>
        /// <param name="streetcode">The streetcode to return.</param>
        private void SetupStreetcodeRepository(StreetcodeContent streetcode)
        {
            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcode);
        }

        /// <summary>
        /// Sets up the streetcode repository to return null.
        /// </summary>
        private void SetupStreetcodeRepositoryToReturnNull()
        {
            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync((StreetcodeContent)null);
        }

        /// <summary>
        /// Sets up the partners repository to return a list of partners.
        /// </summary>
        /// <param name="partners">The partners to return.</param>
        private void SetupPartnersRepository(List<Partner> partners)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partners);
        }

        /// <summary>
        /// Sets up the partners repository to return null.
        /// </summary>
        private void SetupPartnersRepositoryToReturnNull()
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((IEnumerable<Partner>)null);
        }

        /// <summary>
        /// Sets up the mapper to map partners to PartnerDTOs.
        /// </summary>
        /// <param name="partnerDTOs">The DTOs to return from mapping.</param>
        private void SetupMapperForPartnerDTOs(List<PartnerDto> partnerDTOs)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDto>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(partnerDTOs);
        }

        /// <summary>
        /// Sets up the mapper to map specific partners to specific PartnerDTOs.
        /// </summary>
        /// <param name="partners">The partners to map from.</param>
        /// <param name="partnerDTOs">The DTOs to return.</param>
        private void SetupMapperForSpecificPartners(List<Partner> partners, List<PartnerDto> partnerDTOs)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDto>>(partners))
                .Returns(partnerDTOs);
        }

        /// <summary>
        /// Sets up the streetcode repository to throw an exception.
        /// </summary>
        /// <param name="exception">The exception to throw.</param>
        private void SetupStreetcodeRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.StreetcodeRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ThrowsAsync(exception);
        }

        /// <summary>
        /// Sets up the partners repository to throw an exception.
        /// </summary>
        /// <param name="exception">The exception to throw.</param>
        private void SetupPartnersRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ThrowsAsync(exception);
        }

        /// <summary>
        /// Verifies that the handler returns success when partners exist for the given streetcode ID.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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
            var partnerDTOs = new List<PartnerDto>
            {
                PartnerTestHelpers.CreatePartnerDTO(1),
                PartnerTestHelpers.CreatePartnerDTO(2),
            };

            this.SetupStreetcodeRepository(streetcode);
            this.SetupPartnersRepository(partners);
            this.SetupMapperForPartnerDTOs(partnerDTOs);

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

        /// <summary>
        /// Verifies that the handler returns failure when the streetcode does not exist.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsFailure_WhenStreetcodeDoesNotExist()
        {
            // Arrange
            int streetcodeId = 999;

            this.SetupStreetcodeRepositoryToReturnNull();

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Contain(string.Format(ErrorMessages.PartnersNotFoundByStreetcodeId, streetcodeId));

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

        /// <summary>
        /// Verifies that the handler returns failure when the partners repository returns null.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsFailure_WhenPartnersRepositoryReturnsNull()
        {
            // Arrange
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };

            this.SetupStreetcodeRepository(streetcode);
            this.SetupPartnersRepositoryToReturnNull();

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Contain(string.Format(ErrorMessages.PartnersNotFoundByStreetcodeId, streetcodeId));

            this.MockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler returns success with an empty list when no partners are associated with the streetcode.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ReturnsSuccess_WhenPartnersListIsEmpty()
        {
            // Arrange
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };
            var emptyPartners = new List<Partner>();
            var emptyPartnerDTOs = new List<PartnerDto>();

            this.SetupStreetcodeRepository(streetcode);
            this.SetupPartnersRepository(emptyPartners);
            this.SetupMapperForPartnerDTOs(emptyPartnerDTOs);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

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
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };
            var partners = new List<Partner>
            {
                PartnerTestHelpers.CreatePartnerEntity(1),
                PartnerTestHelpers.CreatePartnerEntity(2),
            };
            var partnerDTOs = new List<PartnerDto>
            {
                PartnerTestHelpers.CreatePartnerDTO(1),
                PartnerTestHelpers.CreatePartnerDTO(2),
            };

            this.SetupStreetcodeRepository(streetcode);
            this.SetupPartnersRepository(partners);
            this.SetupMapperForSpecificPartners(partners, partnerDTOs);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            this.MockMapper.Verify(
                mapper => mapper.Map<IEnumerable<PartnerDto>>(partners),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler propagates InvalidOperationException when the streetcode repository throws an exception.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ThrowsInvalidOperationException_WhenStreetcodeRepositoryThrowsException()
        {
            // Arrange
            int streetcodeId = 1;
            var expectedException = new InvalidOperationException(ErrorMessages.DataBaseError);

            this.SetupStreetcodeRepositoryToThrowException(expectedException);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            Func<Task> act = async () => await this._handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage(ErrorMessages.DataBaseError);
        }

        /// <summary>
        /// Verifies that the handler propagates InvalidOperationException when the partners repository throws an exception.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ThrowsInvalidOperationException_WhenPartnersRepositoryThrowsException()
        {
            // Arrange
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };
            var expectedException = new InvalidOperationException(ErrorMessages.DataBaseError);

            this.SetupStreetcodeRepository(streetcode);
            this.SetupPartnersRepositoryToThrowException(expectedException);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            Func<Task> act = async () => await this._handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage(ErrorMessages.DatabaseErrorOccured);
        }

        /// <summary>
        /// Verifies that the handler calls the repository with include expressions for related entities.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CallsRepositoryWithInclude_WhenCalled()
        {
            // Arrange
            int streetcodeId = 1;
            var streetcode = new StreetcodeContent { Id = streetcodeId };
            var partners = new List<Partner> { PartnerTestHelpers.CreatePartnerEntity(1) };
            var partnerDTOs = new List<PartnerDto> { PartnerTestHelpers.CreatePartnerDTO(1) };

            this.SetupStreetcodeRepository(streetcode);

            Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>> capturedInclude = null;
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (pred, include) => capturedInclude = include)
                .ReturnsAsync(partners);

            this.SetupMapperForPartnerDTOs(partnerDTOs);

            var query = new GetPartnersByStreetcodeIdQuery(streetcodeId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedInclude.Should().NotBeNull(ErrorMessages.IncludeExpressionNotProvided);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }
    }
}
