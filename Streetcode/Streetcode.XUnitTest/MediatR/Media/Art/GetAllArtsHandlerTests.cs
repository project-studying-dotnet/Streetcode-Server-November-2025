namespace Streetcode.XUnitTest.MediatR.Media.Art
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Org.BouncyCastle.Asn1.Esf;
    using Repositories.Interfaces;
    using Streetcode.BLL.DTO.Media.Art;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Art.GetAll;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetAllArtsHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepositoryWrapper> _repositoryWrapperMock;
        private readonly Mock<ILoggerService> _loggerMock;
        private readonly Mock<IArtRepository> _artRepositoryMock;
        private readonly GetAllArtsHandler _handler;
        private const string ERROR_MESSAGE = "Cannot find any arts";

        public GetAllArtsHandlerTests()
        {
            this._repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this._mapperMock = new Mock<IMapper>();
            this._loggerMock = new Mock<ILoggerService>();
            this._artRepositoryMock = new Mock<IArtRepository>();

            _repositoryWrapperMock.Setup(rw => rw.ArtRepository)
                .Returns(this._artRepositoryMock.Object);

            this._handler = new GetAllArtsHandler
                (
                _repositoryWrapperMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
                );
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenArtsAreEmpty()
        {
            var result = await _handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenArtsExist()
        {
            List<Art> arts = this.GetArts();
            List<ArtDTO> artDTOs = this.GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this._handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsCorrectNumberOfArts()
        {
            List<Art> arts = this.GetArts();
            List<ArtDTO> artDTOs = this.GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this._handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.Equal(arts.Count, result.Value.Count());
        }

        [Fact]
        public async Task Handle_LogsErrorAndReturnsFail_WhenArtsAreNull()
        {
            List<Art> arts = null;
            List<ArtDTO> artDTOs = this.GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this._handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Handle_LogsErrorAndReturnsErrorMessage_WhenArtsAreNull()
        {
            List<Art> arts = null;
            List<ArtDTO> artDTOs = this.GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this._handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.Equal(ERROR_MESSAGE, result.Errors[0].Message);
        }



        private void SetupRepositoryMapper(List<Art> arts, List<ArtDTO> artDTOs)
        {
            this._artRepositoryMock.Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Art, bool>>>(),
                It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()))
                .ReturnsAsync(arts);

            this._mapperMock.Setup(map => map.Map<IEnumerable<ArtDTO>>(It.IsAny<IEnumerable<Art>>()))
                .Returns(artDTOs);
        }

        private List<Art> GetArts()
        {
            return new List<Art>()
            {
                new Art()
                {
                    Id = 1,
                    Title = "Art 1",
                    Description = "Description 1",
                },
                new Art()
                {
                    Id = 2,
                    Title = "Art 2",
                    Description = "Description 2",
                },
            };
        }

        private List<ArtDTO> GetArtsDTO()
        {
            return new List<ArtDTO>()
            {
                new ArtDTO()
                {
                    Id = 1,
                    Title = "Art 1",
                    Description = "Description 1",
                },
                new ArtDTO()
                {
                    Id = 2,
                    Title = "Art 2",
                    Description = "Description 2",
                },
            };
        }
    }
}
