using System;
using AutoMapper;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    public abstract class PartnerHandlerTestsBase
    {
        protected Mock<IRepositoryWrapper> MockRepository { get; }

        protected Mock<IMapper> MockMapper { get; }

        protected Mock<ILoggerService> MockLogger { get; }

        protected PartnerHandlerTestsBase()
        {
            this.MockRepository = new Mock<IRepositoryWrapper>();
            this.MockMapper = new Mock<IMapper>();
            this.MockLogger = new Mock<ILoggerService>();
        }

        // Shared Mapper setup methods
        protected void SetupMapperForPartnerDTO(PartnerDTO partnerDTO)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(partnerDTO);
        }

        protected void SetupMapperForSpecificPartner(Partner partner, PartnerDTO partnerDTO)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(partner))
                .Returns(partnerDTO);
        }

        protected void SetupMapperForPartnerDTOs<T>(System.Collections.Generic.IEnumerable<T> partnerDTOs)
            where T : class
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<System.Collections.Generic.IEnumerable<T>>(It.IsAny<System.Collections.Generic.IEnumerable<Partner>>()))
                .Returns(partnerDTOs);
        }

        // Shared exception setup methods
        protected void SetupSaveChangesToThrowException(string exceptionMessage)
        {
            this.MockRepository
                .Setup(repo => repo.SaveChanges())
                .Throws(new Exception(exceptionMessage));
        }
    }
}
