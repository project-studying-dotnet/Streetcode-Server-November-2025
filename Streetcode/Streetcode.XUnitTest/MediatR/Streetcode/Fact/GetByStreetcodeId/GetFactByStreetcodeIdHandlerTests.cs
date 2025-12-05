namespace Streetcode.XUnitTest.MediatR.Fact.GetByStreetcodeId
{
    using AutoMapper;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Fact.GetByStreetcodeId;
    using Streetcode.DAL.Entities.Streetcode.TextContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Fact.Fixtures;
    using Streetcode.XUnitTest.MediatR.Fact.Helpers;
    using Xunit;

    public class GetFactByStreetcodeIdHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetFactByStreetcodeIdHandler handler;

        public GetFactByStreetcodeIdHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetFactByStreetcodeIdHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenFactsDoNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            const int streetcodeId = 1;
            string errorMsg = $"Cannot find any fact by the streetcode id: {streetcodeId}";
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var query = new GetFactByStreetcodeIdQuery(streetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);
            factRepositoryMock.SetupGetAllAsync<IFactRepository, Fact>(entities: null);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(errorMsg, result.Errors.FirstOrDefault()?.Message);

            // Verify
            factRepositoryMock.VerifyGetAllAsyncCalledOnce<IFactRepository, Fact>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenFactsExist_ShouldReturnSuccessResult()
        {
            // Arrange
            const int streetcodeId = 1;
            var factEntities = FactTestData.CreateFacts(streetcodeId: streetcodeId);
            var factDtos = FactTestData.CreateFactDtos();
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var query = new GetFactByStreetcodeIdQuery(streetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);
            factRepositoryMock.SetupGetAllAsync(factEntities);
            this.mapperMock.SetupMapper<IEnumerable<Fact>, IEnumerable<FactDto>>(factEntities, factDtos);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(factDtos.Count(), result.Value.Count());

            // Verify
            factRepositoryMock.VerifyGetAllAsyncCalledOnce<IFactRepository, Fact>();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<FactDto>>();
        }
    }
}
