using AutoMapper;
using MediatR;
using Moq;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.XUnitTest.MediatR.Post
{
    public class CreateStreetcodeHandlerTestsBase
    {
        protected Mock<IRepositoryWrapper> _repositoryMock;

        protected Mock<IMapper> _mapperMock;

        protected Mock<ILoggerService> _loggerMock;

        protected Mock<IMediator> _mediatorMock;

        protected CreateStreetcodeHandlerTestsBase()
        {
            this._repositoryMock = new Mock<IRepositoryWrapper>();
            this._mapperMock = new Mock<IMapper>();
            this._loggerMock = new Mock<ILoggerService>();
            this._mediatorMock = new Mock<IMediator>();
        }
    }
}