namespace Streetcode.XUnitTest.MediatR.FullDelete
{
    using System.Linq.Expressions;
    using Moq;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.DeleteFull;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Base;
    using Xunit;

    public class DeleteFullStreetcodeHandlerTests : StreetcodeHandlersTestsBase
    {
        private DeleteFullStreetcodeHandler handler;

        public DeleteFullStreetcodeHandlerTests()
        {
            this.handler = new DeleteFullStreetcodeHandler(
                this._repositoryMock.Object,
                this._loggerMock.Object);
        }

        [Fact]
        public async Task Handler_Returns_Success_WhenProper_Id()
        {
            this._repositoryMock.SetupSaveChangesAsync();

            var streetcodeRepoMock = new Mock<IStreetcodeRepository>();

            this._repositoryMock
            .Setup(r => r.StreetcodeRepository)
            .Returns(streetcodeRepoMock.Object);

            streetcodeRepoMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    null))
                .ReturnsAsync(new StreetcodeContent { Id = 1, Index = 1, AudioId = 7 });

            this.SetMocksForDelete();

            this.SetupAudioRepoMocks();

            var result = await this.handler.Handle(new DeleteFullStreetcodeCommand(1), CancellationToken.None);

            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(2)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public async Task Handler_Returns_Fail_WhenWrong_Id(int id)
        {
            this._repositoryMock.SetupSaveChangesAsync();

            var streetcodeRepoMock = new Mock<IStreetcodeRepository>();

            this._repositoryMock
            .Setup(r => r.StreetcodeRepository)
            .Returns(streetcodeRepoMock.Object);

            streetcodeRepoMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    null))
                .ReturnsAsync((StreetcodeContent?)null);

            this.SetMocksForDelete();

            this.SetupAudioRepoMocks();

            var result = await this.handler.Handle(new DeleteFullStreetcodeCommand(id), CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal(result.Errors[0].Message, $"Cannot find a streetcode with corresponding categoryId: {id}");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(2)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public async Task Handler_Returns_Fail_SaveChangesFailed(int id)
        {
            this._repositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);

            var streetcodeRepoMock = new Mock<IStreetcodeRepository>();

            this._repositoryMock
            .Setup(r => r.StreetcodeRepository)
            .Returns(streetcodeRepoMock.Object);

            streetcodeRepoMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    null))
                .ReturnsAsync(new StreetcodeContent { Id = 1, Index = 1, AudioId = 7 });

            this.SetMocksForDelete();

            this.SetupAudioRepoMocks();

            var result = await this.handler.Handle(new DeleteFullStreetcodeCommand(id), CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal(result.Errors[0].Message, $"Failed to delete streetcode fully");
        }
    }
}
