namespace Streetcode.XUnitTest.MediatR.FullDelete
{
    using AutoMapper;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.Interfaces.Cache;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Streetcode.DeleteFull;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Base;
    using System.Linq.Expressions;
    using Xunit;

    public class DeleteFullStreetcodeHandlerTests
    {
        private readonly DeleteFullStreetcodeHandler handler;

        private readonly StreetcodeHandlersTestsHelper streetcodeHandlersTestsHelper;

        private Mock<IRepositoryWrapper> repositoryMock = new Mock<IRepositoryWrapper>();

        private Mock<ILoggerService> loggerMock = new Mock<ILoggerService>();
        private Mock<ICacheService> cacheServiceMock = new Mock<ICacheService>();


        public DeleteFullStreetcodeHandlerTests()
        {
            this.handler = new DeleteFullStreetcodeHandler(
                this.repositoryMock.Object,
                this.loggerMock.Object,
                this.cacheServiceMock.Object);

            this.streetcodeHandlersTestsHelper =
                new StreetcodeHandlersTestsHelper(this.repositoryMock, new Mock<IMapper>(), this.loggerMock);
        }

        [Fact]
        public async Task Handler_Returns_Success_WhenProper_Id()
        {
            this.repositoryMock.SetupSaveChangesAsync();

            var streetcodeRepoMock = new Mock<IStreetcodeRepository>();

            this.repositoryMock
            .Setup(r => r.StreetcodeRepository)
            .Returns(streetcodeRepoMock.Object);

            streetcodeRepoMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    null))
                .ReturnsAsync(new StreetcodeContent { Id = 1, Index = 1, AudioId = 7 });

            this.streetcodeHandlersTestsHelper.SetMocksForDelete();

            this.streetcodeHandlersTestsHelper.SetupAudioRepoMocks();

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
            this.repositoryMock.SetupSaveChangesAsync();

            var streetcodeRepoMock = new Mock<IStreetcodeRepository>();

            this.repositoryMock
            .Setup(r => r.StreetcodeRepository)
            .Returns(streetcodeRepoMock.Object);

            streetcodeRepoMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    null))
                .ReturnsAsync((StreetcodeContent?)null);

            this.streetcodeHandlersTestsHelper.SetMocksForDelete();

            this.streetcodeHandlersTestsHelper.SetupAudioRepoMocks();

            var result = await this.handler.Handle(new DeleteFullStreetcodeCommand(id), CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal(result.Errors[0].Message, string.Format(ErrorMessages.StreetcodeNotFoundByCategoryId, id));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(2)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public async Task Handler_Returns_Fail_SaveChangesFailed(int id)
        {
            this.repositoryMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);

            var streetcodeRepoMock = new Mock<IStreetcodeRepository>();

            this.repositoryMock
            .Setup(r => r.StreetcodeRepository)
            .Returns(streetcodeRepoMock.Object);

            streetcodeRepoMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    null))
                .ReturnsAsync(new StreetcodeContent { Id = 1, Index = 1, AudioId = 7 });

            this.streetcodeHandlersTestsHelper.SetMocksForDelete();

            this.streetcodeHandlersTestsHelper.SetupAudioRepoMocks();

            var result = await this.handler.Handle(new DeleteFullStreetcodeCommand(id), CancellationToken.None);

            Assert.True(result.IsFailed);
            Assert.Equal(result.Errors[0].Message, ErrorMessages.StreetcodeFullDeletionFailed);
        }
    }
}
