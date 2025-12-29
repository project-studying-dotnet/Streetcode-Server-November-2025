using MediatR;
using Moq;
using global::Streetcode.BLL.DTO.Partners;
using global::Streetcode.BLL.MediatR.Partners.GetAllPartnerShort;
using global::Streetcode.DAL.Entities.Partners;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    /// <summary>
    /// Unit tests for <see cref="GetAllPartnerShortHandler"/>.
    /// </summary>
    public class GetAllPartnerShortHandlerTests : GetAllPartnersTestsBase<GetAllPartnersShortQuery, PartnerShortDto>
    {
        private readonly GetAllPartnerShortHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllPartnerShortHandlerTests"/> class.
        /// </summary>
        public GetAllPartnerShortHandlerTests()
        {
            this._handler = new GetAllPartnerShortHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        /// <inheritdoc/>
        protected override IRequestHandler<GetAllPartnersShortQuery, FluentResults.Result<IEnumerable<PartnerShortDto>>> Handler => this._handler;

        /// <inheritdoc/>
        protected override IEnumerable<PartnerShortDto> CreateDtos(int count)
        {
            return PartnerTestHelpers.CreatePartnerShortDTOs(count);
        }

        /// <inheritdoc/>
        protected override IEnumerable<PartnerShortDto> CreateEmptyDtos()
        {
            return new List<PartnerShortDto>();
        }

        /// <inheritdoc/>
        protected override void SetupMapperForDtos(IEnumerable<Partner> partners, IEnumerable<PartnerShortDto> dtos)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerShortDto>>(partners))
                .Returns(dtos);
        }

        /// <inheritdoc/>
        protected override void SetupMapperForAnyPartners(IEnumerable<PartnerShortDto> dtos)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerShortDto>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(dtos);
        }

        /// <inheritdoc/>
        protected override void VerifyMapperWasCalled(IEnumerable<Partner> partners)
        {
            this.MockMapper.Verify(
                mapper => mapper.Map<IEnumerable<PartnerShortDto>>(partners),
                Times.Once);
        }
    }
}