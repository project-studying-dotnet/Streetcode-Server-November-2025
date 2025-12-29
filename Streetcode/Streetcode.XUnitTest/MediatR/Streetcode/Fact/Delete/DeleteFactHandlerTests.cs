namespace Streetcode.XUnitTest.MediatR.Fact.Delete
{
    using AutoMapper;
    using global::MediatR;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Streetcode.Fact.Delete;
 using global::Streetcode.DAL.Entities.Streetcode.TextContent;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.Fact.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Fact.Helpers;
    using Xunit;

    public class DeleteFactHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly DeleteFactHandler handler;

        public DeleteFactHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new DeleteFactHandler(
                this.repositoryWrapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenFactDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            string errorMsg = ErrorMessages.FactNotFound;
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var factId = 1;
            var command = new DeleteFactCommand(factId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);

            factRepositoryMock.SetupGetFirstOrDefaultAsync<IFactRepository, Fact>(entity: null);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

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
        public async Task Handle_WhenFactExists_ShouldDeleteSuccessfully()
        {
            // Arrange
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var existingFact = FactTestData.CreateFact(id: 1);
            var command = new DeleteFactCommand(existingFact.Id);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);

            factRepositoryMock.SetupGetFirstOrDefaultAsync<IFactRepository, Fact>(existingFact);
            factRepositoryMock.SetupDelete<IFactRepository, Fact>();
            this.repositoryWrapperMock.SetupSaveChangesAsync();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(Unit.Value, result.Value);

            // Verify
            factRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IFactRepository, Fact>();
            factRepositoryMock.VerifyDeleteCalledOnce<IFactRepository, Fact>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}
