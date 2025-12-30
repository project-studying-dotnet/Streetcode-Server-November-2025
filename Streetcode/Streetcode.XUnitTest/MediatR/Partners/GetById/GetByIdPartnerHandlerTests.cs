using Ardalis.Specification;
using FluentAssertions;
using Moq;
using global::Streetcode.BLL;
using global::Streetcode.BLL.DTO.Partners;
using global::Streetcode.BLL.MediatR.Partners.GetById;
using global::Streetcode.DAL.Entities.Partners;
using global::Streetcode.DAL.Specifications.Partners;
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
        private void SetupGetBySpecAsync(Partner partner)
        {
            this.MockRepository
                .Setup(r => r.PartnersRepository.GetBySpecAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(partner);
        }

        /// <summary>
        /// Sets up the repository mock to return null.
        /// </summary>
        private void SetupGetBySpecAsyncToReturnNull()
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetBySpecAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Partner?)null);
        }

        /// <summary>
        /// Sets up the repository mock to throw the specified exception.
        /// </summary>
        /// <param name="exception">The exception to throw.</param>
        private void SetupRepositoryToThrowException(Exception exception)
        {
            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetBySpecAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()))
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

            this.SetupGetBySpecAsync(partner);
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
                repo => repo.PartnersRepository.GetBySpecAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()),
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

            this.SetupGetBySpecAsyncToReturnNull();

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

            this.SetupGetBySpecAsync(partner);
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
                .WithMessage(ErrorMessages.DataBaseError);
        }

        /// <summary>
        /// Verifies that the specification filters partners by the correct ID when applied.
        /// </summary>
        [Fact]
        public void Handle_FiltersByCorrectPartnerId_WhenSpecificationIsApplied()
        {
            // Arrange
            int partnerId = 42;
            var partner = PartnerTestHelpers.CreatePartnerEntity(partnerId);
            var otherPartner = PartnerTestHelpers.CreatePartnerEntity(999);

            var spec = new PartnerByIdSpecification(partnerId);

            // Assert
            spec.WhereExpressions.Should().HaveCount(1);

            var whereExpression = spec.WhereExpressions.First().Filter;

            whereExpression.Compile()(partner).Should().BeTrue();
            whereExpression.Compile()(otherPartner).Should().BeFalse();
        }

        /// <summary>
        /// Verifies that the specification includes PartnerSourceLinks when applied.
        /// </summary>
        [Fact]
        public void Handle_IncludesPartnerSourceLinks_WhenSpecificationIsApplied()
        {
            // Arrange
            var spec = new PartnerByIdSpecification(1);

            // Assert
            spec.IncludeExpressions.Should().HaveCount(1);

            var includeExpression = spec.IncludeExpressions.First().LambdaExpression;

            includeExpression.Body.ToString()
                .Should().Contain(nameof(Partner.PartnerSourceLinks));
        }
    }
}
