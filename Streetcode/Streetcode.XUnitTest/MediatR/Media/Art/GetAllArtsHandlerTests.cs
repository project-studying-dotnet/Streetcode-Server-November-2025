namespace Streetcode.XUnitTest.MediatR.Media.Art
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Repositories.Interfaces;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Media.Art;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Media.Art.GetAll;
 using global::Streetcode.DAL.Entities.Media.Images;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Xunit;

    public class GetAllArtsHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IArtRepository> artRepositoryMock;
        private readonly VerifyMockersHandler verifyMockersHandler;
        private readonly GetAllArtsHandler handler;

        public GetAllArtsHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.artRepositoryMock = new Mock<IArtRepository>();

            this.repositoryWrapperMock.Setup(rw => rw.ArtRepository)
                .Returns(this.artRepositoryMock.Object);

            this.verifyMockersHandler = new VerifyMockersHandler(
                this.mapperMock,
                this.artRepositoryMock,
                this.repositoryWrapperMock,
                this.loggerMock);

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
            this.verifyMockersHandler.VerifyMockersPositiveFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenArtsExist()
        {
            List<Art> arts = GetArts();
            List<ArtDto> artDTOs = GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.True(result.IsSuccess);
            this.verifyMockersHandler.VerifyMockersPositiveFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();
        }

        [Fact]
        public async Task Handle_ReturnsCorrectNumberOfArts_WhenArtsExist()
        {
            List<Art> arts = GetArts();
            List<ArtDto> artDTOs = GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.Equal(arts.Count, result.Value.Count());
            this.verifyMockersHandler.VerifyMockersPositiveFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();
        }

        [Fact]
        public async Task Handle_ReturnsCorrectType_WhenArtsExist()
        {
            List<Art> arts = GetArts();
            List<ArtDto> artDTOs = GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.IsType<List<ArtDto>>(result.Value);
            this.verifyMockersHandler.VerifyMockersPositiveFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();
        }

        [Fact]
        public async Task Handle_ReturnsFail_WhenArtsAreNull()
        {
            List<Art>? arts = null;
            List<ArtDto> artDTOs = GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.True(result.IsFailed);
            this.verifyMockersHandler.VerifyMockersNegativeFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();
            this.verifyMockersHandler.VerifyLoggerMock();
        }

        [Fact]
        public async Task Handle_ReturnsErrorMessage_WhenArtsAreNull()
        {
            List<Art>? arts = null;
            List<ArtDto> artDTOs = GetArtsDTO();

            this.SetupRepositoryMapper(arts, artDTOs);

            var result = await this.handler
                .Handle(new GetAllArtsQuery(), CancellationToken.None);

            Assert.Equal(ErrorMessages.ArtsNotFound, result.Errors[0].Message);
            this.verifyMockersHandler.VerifyMockersNegativeFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();
            this.verifyMockersHandler.VerifyLoggerMock();
        }

        private void SetupRepositoryMapper(List<Art> arts, List<ArtDto> artDTOs)
        {
            this.artRepositoryMock.Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Art, bool>>>(),
                It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()))
                .ReturnsAsync(arts);

            this.mapperMock.Setup(map => map.Map<IEnumerable<ArtDto>>(It.IsAny<IEnumerable<Art>>()))
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

        private static List<ArtDto> GetArtsDTO()
        {
            return new List<ArtDto>()
            {
                new ArtDto()
                {
                    Id = 1,
                },
                new ArtDto()
                {
                    Id = 2,
                },
            };
        }
    }
}
