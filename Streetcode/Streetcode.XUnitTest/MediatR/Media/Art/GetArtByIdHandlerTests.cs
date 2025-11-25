namespace Streetcode.XUnitTest.MediatR.Media.Art
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Repositories.Interfaces;
    using Streetcode.BLL.DTO.Media.Art;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Art.GetById;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetArtByIdHandlerTests
    {
        private const string ERRORMESSAGE = "Cannot find an art with corresponding id: {0}";
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly Mock<IArtRepository> artRepositoryMock;
        private readonly GetArtByIdHandler handler;

        public GetArtByIdHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.artRepositoryMock = new Mock<IArtRepository>();

            this.repositoryWrapperMock.Setup(rw => rw.ArtRepository)
                .Returns(this.artRepositoryMock.Object);

            this.handler = new GetArtByIdHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public async Task Handle_ReturnsFail_WhenArtsNull(int requestedId)
        {
            var result = await this.handler
                .Handle(new GetArtByIdQuery(requestedId), CancellationToken.None);

            Assert.True(result.IsFailed);
            this.VerifyMockersNegativeFlow();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public async Task Handle_ReturnsProperErrorMessage_WhenArtsNull(int requestedId)
        {
            var result = await this.handler
                .Handle(new GetArtByIdQuery(requestedId), CancellationToken.None);

            Assert.Equal(string.Format(ERRORMESSAGE, $"{requestedId}"), result.Errors[0].Message);
            this.VerifyMockersNegativeFlow();
        }

        [Theory]
        [InlineData(1)]
        public async Task Handle_ReturnsSucces_WhenArtsNonEmpty(int requestedId)
        {
            Art art = GetArt();
            ArtDTO artDTO = GetArtDTO();

            this.SetupRepositoryMapper(art, artDTO);

            var result = await this.handler
                .Handle(new GetArtByIdQuery(requestedId), CancellationToken.None);

            Assert.True(result.IsSuccess);
            this.VerifyMockersPositiveFlow();
        }

        [Theory]
        [InlineData(1)]
        public async Task Handle_ReturnsProperArt_WhenArtsNonEmpty(int requestedId)
        {
            Art art = GetArt();
            ArtDTO artDTO = GetArtDTO();

            this.SetupRepositoryMapper(art, artDTO);

            var result = await this.handler
                .Handle(new GetArtByIdQuery(1), CancellationToken.None);

            Assert.Equal(requestedId, result.Value.Id);
            this.VerifyMockersPositiveFlow();
        }

        private void SetupRepositoryMapper(Art art, ArtDTO artDTO)
        {
            this.artRepositoryMock.Setup(repo => repo.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Art, bool>>>(),
                It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()))
                .ReturnsAsync(art);

            this.mapperMock.Setup(map => map.Map<ArtDTO>(It.IsAny<Art>()))
                .Returns(artDTO);
        }

        private static Art GetArt()
        {
            return new Art()
            {
                Id = 1,
            };
        }

        private static ArtDTO GetArtDTO()
        {
            return new ArtDTO()
            {
                Id = 1,
            };
        }

        private void VerifyMockersPositiveFlow()
        {
            this.mapperMock.Verify(
                m => m.Map<ArtDTO>(It.IsAny<Art>()),
                Times.Once);

            this.artRepositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Art, bool>>>(),
                    It.IsAny<Func<IQueryable<Art>, IIncludableQueryable<Art, object>>>()),
                Times.Once);

            this.repositoryWrapperMock
                .Verify(rw => rw.ArtRepository, Times.Once);
        }

        private void VerifyMockersNegativeFlow()
        {
            this.mapperMock.Verify(
                m => m.Map<ArtDTO>(It.IsAny<Art>()),
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
