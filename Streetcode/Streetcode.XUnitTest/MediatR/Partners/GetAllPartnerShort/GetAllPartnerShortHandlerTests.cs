using MediatR;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.MediatR.Partners.GetAllPartnerShort;
using Streetcode.DAL.Entities.Partners;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    /// <summary>
    /// Unit tests for <see cref="GetAllPartnerShortHandler"/>.
    /// </summary>
    public class GetAllPartnerShortHandlerTests : GetAllPartnersTestsBase<GetAllPartnersShortQuery, PartnerShortDTO>
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
        protected override IRequestHandler<GetAllPartnersShortQuery, FluentResults.Result<IEnumerable<PartnerShortDTO>>> Handler => this._handler;

        /// <inheritdoc/>
        protected override IEnumerable<PartnerShortDTO> CreateDtos(int count)
        {
            return PartnerTestHelpers.CreatePartnerShortDTOs(count);
        }

        /// <inheritdoc/>
        protected override IEnumerable<PartnerShortDTO> CreateEmptyDtos()
        {
            return new List<PartnerShortDTO>();
        }

        /// <inheritdoc/>
        protected override void SetupMapperForDtos(IEnumerable<Partner> partners, IEnumerable<PartnerShortDTO> dtos)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerShortDTO>>(partners))
                .Returns(dtos);
        }

        /// <inheritdoc/>
        protected override void SetupMapperForAnyPartners(IEnumerable<PartnerShortDTO> dtos)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerShortDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(dtos);
        }

        /// <inheritdoc/>
        protected override void VerifyMapperWasCalled(IEnumerable<Partner> partners)
        {
            this.MockMapper.Verify(
                mapper => mapper.Map<IEnumerable<PartnerShortDTO>>(partners),
                Times.Once);
        }
    }
}