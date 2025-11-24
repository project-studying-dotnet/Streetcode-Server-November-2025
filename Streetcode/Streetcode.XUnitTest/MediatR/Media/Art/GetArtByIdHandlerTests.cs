namespace Streetcode.XUnitTest.MediatR.Media.Art
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.DAL.Repositories.Interfaces.Base;

    class GetArtByIdHandlerTests
    {
        private readonly Mock<IBlobService> _blobServiceMock;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMock;
        private readonly Mock<ILoggerService> _loggerMock;


    }
}
