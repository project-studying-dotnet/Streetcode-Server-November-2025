namespace Streetcode.XUnitTest.MediatR.Fact.GetById
{
    using AutoMapper;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Fact.GetById;
    using Streetcode.DAL.Entities.Streetcode.TextContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Fact.Fixtures;
    using Streetcode.XUnitTest.MediatR.Fact.Helpers;
    using Xunit;

    public class GetFactByIdHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetFactByIdHandler handler;

        public GetFactByIdHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetFactByIdHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenFactDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            var factId = 1;
            string errorMsg = $"Cannot find any fact with corresponding id: {factId}";
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var query = new GetFactByIdQuery(factId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);
            factRepositoryMock.SetupGetFirstOrDefaultAsync<IFactRepository, Fact>(entity: null);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(errorMsg, result.Errors.FirstOrDefault()?.Message);

            // Verify
            factRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IFactRepository, Fact>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenFactExists_ShouldReturnSuccessResult()
        {
            // Arrange
            var factId = 1;
            var factEntity = FactTestData.CreateFact(factId);
            var factDto = FactTestData.CreateFactDto(factId);
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var query = new GetFactByIdQuery(factId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);
            factRepositoryMock.SetupGetFirstOrDefaultAsync<IFactRepository, Fact>(entity: factEntity);
            this.mapperMock.SetupMapper(factEntity, factDto);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(factDto, result.Value);

            // Verify
            factRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IFactRepository, Fact>();
            this.mapperMock.VerifyMapCalledOnce<FactDto>();
        }
    }
}
