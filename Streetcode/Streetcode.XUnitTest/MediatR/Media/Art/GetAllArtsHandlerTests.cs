namespace Streetcode.XUnitTest.MediatR.Media.Art
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Repositories.Interfaces;
    using Streetcode.BLL.DTO.Media.Art;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Art.GetAll;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetAllArtsHandlerTests
    {
        private const string ERRORMESSAGE = "Cannot find any arts";
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IArtRepository> artRepositoryMock;
        private readonly GetAllArtsHandler handler;

        public GetAllArtsHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.artRepositoryMock = new Mock<IArtRepository>();

            this.repositoryWrapperMock.Setup(rw => rw.ArtRepository)
                .Returns(this.artRepositoryMock.Object);

            this.handler = new GetAllArtsHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenArtsAreEmpty()
        {
            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            this.VerifyMockersPositiveFlow();
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenArtsExist()
        {
            List<Art> arts = GetArts();
            List<ArtDTO> artDTOs = GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            this.VerifyMockersPositiveFlow();
        }

        [Fact]
        public async Task Handle_ReturnsCorrectNumberOfArts_WhenArtsExist()
        {
            List<Art> arts = GetArts();
            List<ArtDTO> artDTOs = GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.Equal(arts.Count, result.Value.Count());
            this.VerifyMockersPositiveFlow();
        }

        [Fact]
        public async Task Handle_ReturnsCorrectType_WhenArtsExist()
        {
            List<Art> arts = GetArts();
            List<ArtDTO> artDTOs = GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.IsType<List<ArtDTO>>(result.Value);
            this.VerifyMockersPositiveFlow();
        }

        [Fact]
        public async Task Handle_ReturnsFail_WhenArtsAreNull()
        {
            List<Art>? arts = null;
            List<ArtDTO> artDTOs = GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.True(result.IsFailed);
            this.VerifyMockersNegativeFlow();
        }

        [Fact]
        public async Task Handle_ReturnsErrorMessage_WhenArtsAreNull()
        {
            List<Art>? arts = null;
            List<ArtDTO> artDTOs = GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.Equal(ERRORMESSAGE, result.Errors[0].Message);
            this.VerifyMockersNegativeFlow();
        }

        private void SetupRepositoryMapper(List<Art> arts, List<ArtDTO> artDTOs)
        {
            this.artRepositoryMock.Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Art, bool>>>(),
                It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()))
                .ReturnsAsync(arts);

            this.mapperMock.Setup(map => map.Map<IEnumerable<ArtDTO>>(It.IsAny<IEnumerable<Art>>()))
                .Returns(artDTOs);
        }

        private static List<Art> GetArts()
        {
            return new List<Art>()
            {
                new Art()
                {
                    Id = 1,
                },
                new Art()
                {
                    Id = 2,
                },
            };
        }

        private static List<ArtDTO> GetArtsDTO()
        {
            return new List<ArtDTO>()
            {
                new ArtDTO()
                {
                    Id = 1,
                },
                new ArtDTO()
                {
                    Id = 2,
                },
            };
        }

        private void VerifyMockersPositiveFlow()
        {
            this.mapperMock.Verify(
                m => m.Map<IEnumerable<ArtDTO>>(It.IsAny<IEnumerable<Art>>()),
                Times.Once);

            this.artRepositoryMock.Verify(
                r => r.GetAllAsync(
                    It.IsAny<Expression<Func<Art, bool>>>(),
                    It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()),
                Times.Once);

            this.repositoryWrapperMock
                .Verify(rw => rw.ArtRepository, Times.Once);
        }

        private void VerifyMockersNegativeFlow()
        {
            this.mapperMock.Verify(
                m => m.Map<IEnumerable<ArtDTO>>(It.IsAny<IEnumerable<Art>>()),
                Times.Never);

            this.loggerMock.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once);

            this.repositoryWrapperMock
                .Verify(rw => rw.ArtRepository, Times.Once);
        }
    }
}
