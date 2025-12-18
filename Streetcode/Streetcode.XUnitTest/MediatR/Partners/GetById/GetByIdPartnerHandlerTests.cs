using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.MediatR.Partners.GetById;
using Streetcode.DAL.Entities.Partners;
using System.Linq.Expressions;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    /// <summary>
    /// Unit tests for <see cref="GetPartnerByIdHandler"/>.
    /// </summary>
    public class GetByIdPartnerHandlerTests : PartnerHandlerTestsBase
    {
        private readonly GetPartnerByIdHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByIdPartnerHandlerTests"/> class.
        /// </summary>
        public GetByIdPartnerHandlerTests()
        {
            this._handler = new GetPartnerByIdHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        /// <summary>
        /// Sets up the repository mock to return the specified partner.
        /// </summary>
        /// <param name="partner">The partner to return from the repository.</param>
        private void SetupGetSingleOrDefaultAsync(Partner partner)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync(partner);
        }

        /// <summary>
        /// Sets up the repository mock to return null.
        /// </summary>
        private void SetupGetSingleOrDefaultAsyncToReturnNull()
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ReturnsAsync((Partner)null);
        }

        /// <summary>
        /// Sets up the repository mock to throw the specified exception.
        /// </summary>
        /// <param name="exception">The exception to throw.</param>
        private void SetupRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .ThrowsAsync(exception);
        }

        /// <summary>
        /// Verifies that the handler returns success when a partner with the given ID exists.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Verifies that the handler returns failure when a partner with the given ID does not exist.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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
            result.Errors.First().Message.Should().Contain(string.Format(ErrorMessages.PartnerNotFoundById, partnerId));

            this.MockLogger.Verify(
                logger => logger.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler calls the mapper when a partner exists.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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
                mapper => mapper.Map<PartnerDto>(partner),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler propagates InvalidOperationException when the repository throws an exception.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ThrowsInvalidOperationException_WhenRepositoryThrowsException()
        {
            // Arrange
            int partnerId = 1;
            var expectedException = new InvalidOperationException(ErrorMessages.DataBaseError);

            this.SetupRepositoryToThrowException(expectedException);

            var query = new GetPartnerByIdQuery(partnerId);

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
                .Setup(repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (pred, include) => capturedPredicate = pred)
                .ReturnsAsync(partner);
            this.SetupMapperForPartnerDTO(partnerDTO);

            var query = new GetPartnerByIdQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedPredicate.Should().NotBeNull(ErrorMessages.PredicateNotProvided);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }

        /// <summary>
        /// Verifies that the handler calls the repository with include expressions for related entities.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_CallsRepositoryWithInclude_WhenCalled()
        {
            // Arrange
            int partnerId = 1;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var partnerDTO = PartnerTestHelpers.CreatePartnerDTO(partnerId);

            Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>> capturedInclude = null;
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (pred, include) => capturedInclude = include)
                .ReturnsAsync(partner);
            this.SetupMapperForPartnerDTO(partnerDTO);

            var query = new GetPartnerByIdQuery(partnerId);

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedInclude.Should().NotBeNull(ErrorMessages.IncludeExpressionNotProvided);

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }
    }
}
