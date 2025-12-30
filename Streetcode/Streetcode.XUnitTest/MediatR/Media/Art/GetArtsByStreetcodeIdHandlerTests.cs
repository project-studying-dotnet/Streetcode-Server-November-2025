namespace Streetcode.XUnitTest.MediatR.Media.Art
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Repositories.Interfaces;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Media.Art;
 using global::Streetcode.BLL.DTO.Media.Images;
 using global::Streetcode.BLL.Interfaces.BlobStorage;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Media.Art.GetByStreetcodeId;
 using global::Streetcode.DAL.Entities.Media.Images;
 using global::Streetcode.DAL.Entities.Streetcode;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using System;
    using System.Buffers.Text;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Xunit;

    public class GetArtsByStreetcodeIdHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<IArtRepository> artRepositoryMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IBlobService> blobServiceMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly VerifyMockersHandler verifyMockersHandler;
        private readonly GetArtsByStreetcodeIdHandler handler;

        public GetArtsByStreetcodeIdHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.artRepositoryMock = new Mock<IArtRepository>();
            this.mapperMock = new Mock<IMapper>();
            this.blobServiceMock = new Mock<IBlobService>();
            this.loggerMock = new Mock<ILoggerService>();

            this.repositoryWrapperMock.Setup(rw => rw.ArtRepository)
                .Returns(this.artRepositoryMock.Object);
            this.verifyMockersHandler = new VerifyMockersHandler(
                this.mapperMock,
                this.artRepositoryMock,
                this.repositoryWrapperMock,
                this.loggerMock);


            this.handler = new GetArtsByStreetcodeIdHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.blobServiceMock.Object,
                this.loggerMock.Object);
        }

        [Theory]
        [InlineData(1)]
        public async Task Handle_ReturnsSuccess_WhenArtsExist(int requestedId)
        {
            List<Art> arts = GetArts();
            this.SetupRepositoryMapper(arts);

            GetArtsByStreetcodeIdQuery query = new GetArtsByStreetcodeIdQuery(requestedId);

            var result = await this.handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            this.verifyMockersHandler.VerifyMockersPositiveFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();

            this.blobServiceMock.Verify(
                b => b.FindFileInStorageAsBase64Async("test.png"),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsCorrectAmount_WhenArtsExist()
        {
            List<Art> arts = GetArts();
            this.SetupRepositoryMapper(arts);

            GetArtsByStreetcodeIdQuery query = new GetArtsByStreetcodeIdQuery(1);

            var result = await this.handler.Handle(query, CancellationToken.None);

            Assert.Equal(2, result.Value.Count());
            this.verifyMockersHandler.VerifyMockersPositiveFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();

            this.blobServiceMock.Verify(
                b => b.FindFileInStorageAsBase64Async("test.png"),
                Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public async Task Handle_ReturnsEmpty_WhenNoArtsFound(int requestedId)
        {
            List<Art> arts = GetArts();
            this.SetupRepositoryMapper(arts);

            GetArtsByStreetcodeIdQuery query = new GetArtsByStreetcodeIdQuery(requestedId);

            var result = await this.handler.Handle(query, CancellationToken.None);

            Assert.Empty(result.Value);
            this.verifyMockersHandler.VerifyMockersPositiveFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();
        }

        [Fact]
        public async Task Handle_ReturnsError_WhenArtsNull()
        {
            this.artRepositoryMock.Setup(r =>
            r.GetAllAsync(
                It.IsAny<Expression<Func<Art, bool>>>(),
                It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()))
                .ReturnsAsync((List<Art>)null);

            GetArtsByStreetcodeIdQuery query = new GetArtsByStreetcodeIdQuery(1);

            var result = await this.handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsFailed);
            this.verifyMockersHandler.VerifyMockersNegativeFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();
            this.verifyMockersHandler.VerifyLoggerMock();
        }

        [Theory]
        [InlineData(1)]
        public async Task Handle_ReturnsErrorMessage_WhenArtsNull(int requestedId)
        {
            this.artRepositoryMock.Setup(r =>
            r.GetAllAsync(
                It.IsAny<Expression<Func<Art, bool>>>(),
                It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()))
                .ReturnsAsync((List<Art>)null);

            GetArtsByStreetcodeIdQuery query = new GetArtsByStreetcodeIdQuery(requestedId);

            var result = await this.handler.Handle(query, CancellationToken.None);

            Assert.Equal(string.Format(ErrorMessages.ArtNotFoundByStreetcodeId, requestedId), result.Errors[0].Message);
            this.verifyMockersHandler.VerifyMockersNegativeFlowGetAll();
            this.verifyMockersHandler.VerifyWrapperMock();
            this.verifyMockersHandler.VerifyLoggerMock();
        }

        private void SetupRepositoryMapper(List<Art> arts)
        {
            this.artRepositoryMock.Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Art, bool>>>(),
                It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()))
                .ReturnsAsync((
                    Expression<Func<Art, bool>> predicate,
                    Func<IQueryable<Art>, IIncludableQueryable<Art, object>> include) =>
                {
                    var query = arts.AsQueryable();
                    if (predicate != null)
                    {
                        query = query.Where(predicate);
                    }

                    return query;
                });

            this.mapperMock.Setup(m => m.Map<IEnumerable<ArtDto>>(It.IsAny<IEnumerable<Art>>()))
                .Returns((IEnumerable<Art> artsList) =>
                {
                    return artsList.Select(a => new ArtDto
                    {
                        Id = a.Id,
                        Image = a.Image is null ? null
                        : new ImageDto
                        {
                             Id = a.Image.Id,
                             BlobName = a.Image.BlobName
                        },
                    });
                });
        }

        private static List<Art> GetArts()
        {
            return new List<Art>
            {
                new Art
                {
                    Id = 1,
                    Image = new Image { Id = 1, BlobName = "test.png" },
                    StreetcodeArts = new List<StreetcodeArt>
                    {
                        new StreetcodeArt { StreetcodeId = 1 },
                    },
                },
                new Art
                {
                    Id = 2,
                    StreetcodeArts = new List<StreetcodeArt>
                    {
                        new StreetcodeArt { StreetcodeId = 1 },
                    },
                },
            };
        }
    }
}
