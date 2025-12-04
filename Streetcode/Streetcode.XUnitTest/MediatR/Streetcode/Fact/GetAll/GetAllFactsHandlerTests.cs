namespace Streetcode.XUnitTest.MediatR.Fact.GetAll
{
    using AutoMapper;
    using global::MediatR;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Fact.Delete;
    using Streetcode.BLL.MediatR.Streetcode.Fact.GetAll;
    using Streetcode.DAL.Entities.Streetcode.TextContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Fact.Fixtures;
    using Streetcode.XUnitTest.MediatR.Fact.Helpers;
    using Xunit;

    public class GetAllFactsHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetAllFactsHandler handler;

        public GetAllFactsHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetAllFactsHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenFactsDoNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            const string errorMsg = "Cannot find any fact";
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var query = new GetAllFactsQuery();

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
        public async Task Handle_WhenFactsExist_ShouldReturnAllFactsSuccessfully()
        {
            // Arrange
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var facts = FactTestData.CreateFacts();
            var factsDtos = FactTestData.CreateFactDtos();
            var command = new GetAllFactsQuery();

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);
            factRepositoryMock.SetupGetAllAsync(facts);
            this.mapperMock
                .Setup(m => m.Map<IEnumerable<FactDto>>(It.IsAny<IEnumerable<Fact>>()))
                .Returns(factsDtos);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(facts.Count(), result.Value.Count());

            // Verify
            factRepositoryMock.VerifyGetAllAsyncCalledOnce<IFactRepository, Fact>();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<FactDto>>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}
