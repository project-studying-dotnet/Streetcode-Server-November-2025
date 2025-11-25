using AutoMapper;
using Moq;
using Streetcode.BLL.Interfaces.Logging;
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
    }
}
