using AutoMapper;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.XUnitTest.MediatR.Partners
{
    /// <summary>
    /// Base class for Partner handler tests providing common mock setup functionality.
    /// </summary>
    public abstract class PartnerHandlerTestsBase
    {
        /// <summary>
        /// Gets the mock repository wrapper for testing.
        /// </summary>
        protected Mock<IRepositoryWrapper> MockRepository { get; }

        /// <summary>
        /// Gets the mock mapper for testing.
        /// </summary>
        protected Mock<IMapper> MockMapper { get; }

        /// <summary>
        /// Gets the mock logger service for testing.
        /// </summary>
        protected Mock<ILoggerService> MockLogger { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerHandlerTestsBase"/> class.
        /// </summary>
        protected PartnerHandlerTestsBase()
        {
            this.MockRepository = new Mock<IRepositoryWrapper>();
            this.MockMapper = new Mock<IMapper>();
            this.MockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Sets up the mapper to return a specific PartnerDTO for any Partner entity.
        /// </summary>
        /// <param name="partnerDTO">The PartnerDTO to return.</param>
        protected void SetupMapperForPartnerDTO(PartnerDTO partnerDTO)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(It.IsAny<Partner>()))
                .Returns(partnerDTO);
        }

        /// <summary>
        /// Sets up the mapper to return a specific PartnerDTO for a specific Partner entity.
        /// </summary>
        /// <param name="partner">The Partner entity to map from.</param>
        /// <param name="partnerDTO">The PartnerDTO to return.</param>
        protected void SetupMapperForSpecificPartner(Partner partner, PartnerDTO partnerDTO)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<PartnerDTO>(partner))
                .Returns(partnerDTO);
        }

        /// <summary>
        /// Sets up the mapper to return a collection of DTOs for any collection of Partner entities.
        /// </summary>
        /// <typeparam name="T">The type of DTO to map to.</typeparam>
        /// <param name="partnerDTOs">The collection of DTOs to return.</param>
        protected void SetupMapperForPartnerDTOs<T>(System.Collections.Generic.IEnumerable<T> partnerDTOs)
            where T : class
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<System.Collections.Generic.IEnumerable<T>>(It.IsAny<System.Collections.Generic.IEnumerable<Partner>>()))
                .Returns(partnerDTOs);
        }

        /// <summary>
        /// Sets up the repository SaveChanges method to throw an exception.
        /// </summary>
        /// <param name="exceptionMessage">The exception message.</param>
        protected void SetupSaveChangesToThrowException(string exceptionMessage)
        {
            this.MockRepository
                .Setup(repo => repo.SaveChanges())
                .Throws(new Exception(exceptionMessage));
        }
    }
}
