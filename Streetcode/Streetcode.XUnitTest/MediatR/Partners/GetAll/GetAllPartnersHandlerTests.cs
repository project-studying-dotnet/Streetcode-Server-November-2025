using Ardalis.Specification;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using global::Streetcode.BLL.DTO.Partners;
using global::Streetcode.BLL.MediatR.Partners.GetAll;
using global::Streetcode.DAL.Entities.Partners;
using global::Streetcode.DAL.Specifications.Partners;
using System.Linq.Expressions;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    /// <summary>
    /// Unit tests for <see cref="GetAllPartnersHandler"/>.
    /// </summary>
    public class GetAllPartnersHandlerTests : GetAllPartnersTestsBase<GetAllPartnersQuery, PartnerDto>
    {
        private readonly GetAllPartnersHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPartnersHandlerTests"/> class.
        /// </summary>
        public GetAllPartnersHandlerTests()
        {
            this._handler = new GetAllPartnersHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        /// <inheritdoc/>
        protected override IRequestHandler<GetAllPartnersQuery, FluentResults.Result<IEnumerable<PartnerDto>>> Handler => this._handler;

        /// <inheritdoc/>
        protected override IEnumerable<PartnerDto> CreateDtos(int count)
        {
            return PartnerTestHelpers.CreatePartnerDTOs(count);
        }

        /// <inheritdoc/>
        protected override IEnumerable<PartnerDto> CreateEmptyDtos()
        {
            return new List<PartnerDto>();
        }

        /// <inheritdoc/>
        protected override void SetupMapperForDtos(IEnumerable<Partner> partners, IEnumerable<PartnerDto> dtos)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDto>>(partners))
                .Returns(dtos);
        }

        /// <inheritdoc/>
        protected override void SetupMapperForAnyPartners(IEnumerable<PartnerDto> dtos)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDto>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(dtos);
        }

        /// <inheritdoc/>
        protected override void VerifyMapperWasCalled(IEnumerable<Partner> partners)
        {
            this.MockMapper.Verify(
                mapper => mapper.Map<IEnumerable<PartnerDto>>(partners),
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
            var partners = new List<Partner> { PartnerTestHelpers.CreatePartnerEntity(1) };
            var partnerDTOs = new List<PartnerDto> { PartnerTestHelpers.CreatePartnerDTO(1) };

            ISpecification<Partner>? capturedSpec = null;

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.ListAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()))
                .Callback<ISpecification<Partner>, CancellationToken>((spec, ct) => capturedSpec = spec)
                .ReturnsAsync(partners);

            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDto>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(partnerDTOs);

            var query = new GetAllPartnersQuery();

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedSpec.Should().NotBeNull("because specification should be provided");
            capturedSpec.Should().BeAssignableTo<PartnersWithDetailsSpecification>();

            this.MockRepository.Verify(
                repo => repo.PartnersRepository.ListAsync(
                    It.IsAny<ISpecification<Partner>>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}