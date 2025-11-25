using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.MediatR.Partners.GetAll;
using Streetcode.DAL.Entities.Partners;
using Xunit;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public class GetAllPartnersHandlerTests : GetAllPartnersTestsBase<GetAllPartnersQuery, PartnerDTO>
    {
        private readonly GetAllPartnersHandler _handler;

        public GetAllPartnersHandlerTests()
        {
            this._handler = new GetAllPartnersHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        protected override IRequestHandler<GetAllPartnersQuery, FluentResults.Result<IEnumerable<PartnerDTO>>> Handler => this._handler;

        protected override IEnumerable<PartnerDTO> CreateDtos(int count)
        {
            return PartnerTestHelpers.CreatePartnerDTOs(count);
        }

        protected override IEnumerable<PartnerDTO> CreateEmptyDtos()
        {
            return new List<PartnerDTO>();
        }

        protected override void SetupMapperForDtos(IEnumerable<Partner> partners, IEnumerable<PartnerDTO> dtos)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(partners))
                .Returns(dtos);
        }

        protected override void SetupMapperForAnyPartners(IEnumerable<PartnerDTO> dtos)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(dtos);
        }

        protected override void VerifyMapperWasCalled(IEnumerable<Partner> partners)
        {
            this.MockMapper.Verify(
                mapper => mapper.Map<IEnumerable<PartnerDTO>>(partners),
                Times.Once);
        }

        [Fact]
        public async Task Handle_CallsRepositoryWithInclude_WhenCalled()
        {
            // Arrange
            var partners = new List<Partner> { PartnerTestHelpers.CreatePartnerEntity(1) };
            var partnerDTOs = new List<PartnerDTO> { PartnerTestHelpers.CreatePartnerDTO(1) };
            Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>> capturedInclude = null;

            this.MockRepository
                .Setup(repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()))
                .Callback<Expression<Func<Partner, bool>>, Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>(
                    (predicate, include) => capturedInclude = include)
                .ReturnsAsync(partners);

            this.MockMapper
                .Setup(mapper => mapper.Map<IEnumerable<PartnerDTO>>(It.IsAny<IEnumerable<Partner>>()))
                .Returns(partnerDTOs);

            var query = new GetAllPartnersQuery();

            // Act
            var result = await this._handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            capturedInclude.Should().NotBeNull("because include expression should be provided");
            
            this.MockRepository.Verify(
                repo => repo.PartnersRepository.GetAllAsync(
                    It.IsAny<Expression<Func<Partner, bool>>>(),
                    It.IsAny<Func<IQueryable<Partner>, IIncludableQueryable<Partner, object>>>()),
                Times.Once);
        }
    }
}
