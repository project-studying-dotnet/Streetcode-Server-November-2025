using System.Collections.Generic;
using MediatR;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.MediatR.Partners.GetAllPartnerShort;
using Streetcode.DAL.Entities.Partners;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public class GetAllPartnerShortHandlerTests : GetAllPartnersTestsBase<GetAllPartnersShortQuery, PartnerShortDTO>
    {
        private readonly GetAllPartnerShortHandler _handler;

        public GetAllPartnerShortHandlerTests()
        {
            this._handler = new GetAllPartnerShortHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        protected override IRequestHandler<GetAllPartnersShortQuery, FluentResults.Result<IEnumerable<PartnerShortDTO>>> Handler => this._handler;

        protected override IEnumerable<PartnerShortDTO> CreateDtos(int count)
        {
            return PartnerTestHelpers.CreatePartnerShortDTOs(count);
        }

        protected override IEnumerable<PartnerShortDTO> CreateEmptyDtos()
        {
            return new List<PartnerShortDTO>();
        }

        protected override void SetupMapperForDtos(IEnumerable<Partner> partners, IEnumerable<PartnerShortDTO> dtos)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerShortDTO>>(partners))
                .Returns(dtos);
        }

        protected override void SetupMapperForAnyPartners(IEnumerable<PartnerShortDTO> dtos)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerShortDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(dtos);
        }

        protected override void VerifyMapperWasCalled(IEnumerable<Partner> partners)
        {
            this.MockMapper.Verify(
                mapper => mapper.Map<IEnumerable<PartnerShortDTO>>(partners),
                Times.Once);
        }
    }
}