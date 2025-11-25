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
    using Repositories.Interfaces;
    using Streetcode.BLL.DTO.Media.Art;
    using Streetcode.BLL.DTO.Media.Images;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Art.GetByStreetcodeId;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetArtsByStreetcodeIdHandlerTests
    {
        private const string ERRORMESSAGE = "Cannot find any art with corresponding streetcode id: {0}";
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<IArtRepository> artRepositoryMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IBlobService> blobServiceMock;
        private readonly Mock<ILoggerService> loggerMock;
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
            List<Art> arts = this.GetArts();
            this.SetupRepositoryMapper(arts);

            GetArtsByStreetcodeIdQuery query = new GetArtsByStreetcodeIdQuery(requestedId);

            var result = await this.handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ReturnsCorrectAmount_WhenArtsExist()
        {
            List<Art> arts = this.GetArts();
            this.SetupRepositoryMapper(arts);

            GetArtsByStreetcodeIdQuery query = new GetArtsByStreetcodeIdQuery(1);

            var result = await this.handler.Handle(query, CancellationToken.None);

            Assert.Equal(2, result.Value.Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public async Task Handle_ReturnsEmpty_WhenNoArtsFound(int requestedId)
        {
            List<Art> arts = this.GetArts();
            this.SetupRepositoryMapper(arts);

            GetArtsByStreetcodeIdQuery query = new GetArtsByStreetcodeIdQuery(requestedId);

            var result = await this.handler.Handle(query, CancellationToken.None);

            Assert.Empty(result.Value);
        }

        [Fact]
        public async Task Handle_ReturnsError_WhenArtsAreNull()
        {
            this.artRepositoryMock.Setup(r =>
            r.GetAllAsync(
                It.IsAny<Expression<Func<Art, bool>>>(),
                It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()))
                .ReturnsAsync((List<Art>)null);

            GetArtsByStreetcodeIdQuery query = new GetArtsByStreetcodeIdQuery(1);

            var result = await this.handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsFailed);
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

            Assert.Equal(string.Format(ERRORMESSAGE, requestedId), result.Errors[0].Message);
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
                        var func = predicate.Compile();
                        query = query.Where(predicate);
                    }

                    return query;
                });

            this.mapperMock.Setup(m => m.Map<IEnumerable<ArtDTO>>(It.IsAny<IEnumerable<Art>>()))
                .Returns((IEnumerable<Art> artsList) =>
                {
                    return artsList.Select(a => new ArtDTO
                    {
                        Id = a.Id,
                        Image = a.Image is null ? null
                        : new ImageDTO
                        {
                             Id = a.Image.Id,
                        },
                    });
                });
        }

        private List<Art> GetArts()
        {
            return new List<Art>
        {
            new Art
            {
                Id = 1,
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
