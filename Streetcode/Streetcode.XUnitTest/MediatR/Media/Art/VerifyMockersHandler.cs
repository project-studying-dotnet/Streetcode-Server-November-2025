namespace Streetcode.XUnitTest.MediatR.Media.Art
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using NLog;
    using Org.BouncyCastle.Crypto;
    using Repositories.Interfaces;
    using Streetcode.BLL.DTO.Media.Art;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Repositories.Interfaces.Base;

    internal class VerifyMockersHandler
    {
        private Mock<IMapper> mapperMock;
        private Mock<IArtRepository> artRepositoryMock;
        private Mock<IRepositoryWrapper> repositoryWrapperMock;
        private Mock<ILoggerService> loggerMock;

        public VerifyMockersHandler(Mock<IMapper> mapperMock, Mock<IArtRepository> artRepositoryMock, Mock<IRepositoryWrapper> repositoryWrapperMock, Mock<ILoggerService> loggerMock)
        {
            this.mapperMock = mapperMock;
            this.artRepositoryMock = artRepositoryMock;
            this.repositoryWrapperMock = repositoryWrapperMock;
            this.loggerMock = loggerMock;
        }

        internal void VerifyMockersPositiveFlowGetAll()
        {
            this.mapperMock.Verify(
                m => m.Map<IEnumerable<ArtDTO>>(It.IsAny<IEnumerable<Art>>()),
                Times.Once);

            this.artRepositoryMock.Verify(
                r => r.GetAllAsync(
                    It.IsAny<Expression<Func<Art, bool>>>(),
                    It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()),
                Times.Once);
        }

        internal void VerifyMockersPositiveFlowGetFirst()
        {
            this.mapperMock.Verify(
                m => m.Map<ArtDTO>(It.IsAny<Art>()),
                Times.Once);

            this.artRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Art, bool>>>(),
                    It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()),
                Times.Once);
        }

        internal void VerifyMockersNegativeFlowGetAll()
        {
            this.mapperMock.Verify(
                m => m.Map<IEnumerable<ArtDTO>>(It.IsAny<IEnumerable<Art>>()),
                Times.Never);
        }

        internal void VerifyMockersNegativeFlowGetFirst()
        {
            this.mapperMock.Verify(
                m => m.Map<ArtDTO>(It.IsAny<Art>()),
                Times.Never);
        }

        internal void VerifyLoggerMock()
        {
            this.loggerMock.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        internal void VerifyWrapperMock()
        {
            this.repositoryWrapperMock
                .Verify(rw => rw.ArtRepository, Times.Once);
        }
    }
}
