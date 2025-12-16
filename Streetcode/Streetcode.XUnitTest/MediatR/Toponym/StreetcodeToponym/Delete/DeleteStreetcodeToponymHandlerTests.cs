namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Delete
{
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.MediatR.Toponyms.Delete;
    using Streetcode.DAL.Repositories.Interfaces.Toponyms;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Fixtures;
    using Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="DeleteStreetcodeToponymHandler"/>.
    /// </summary>
    public class DeleteStreetcodeToponymHandlerTests : StreetcodeToponymHandlerTestsBase
    {
        private readonly DeleteStreetcodeToponymHandler handler;
        private readonly Mock<IStreetcodeToponymRepository> streetcodeToponymRepositoryMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteStreetcodeToponymHandlerTests"/> class.
        /// </summary>
        public DeleteStreetcodeToponymHandlerTests()
        {
            this.streetcodeToponymRepositoryMock = new Mock<IStreetcodeToponymRepository>(MockBehavior.Strict);
            this.handler = new DeleteStreetcodeToponymHandler(
                this.MockRepository.Object,
                this.MockLogger.Object);
        }

        [Fact]
        public async Task Handle_WhenRelationshipExists_ShouldDeleteSuccessfully()
        {
            // Arrange.
            int streetcodeId = 1;
            int toponymId = 1;
            var entity = StreetcodeToponymTestData.CreateStreetcodeToponym(streetcodeId, toponymId);
            var command = new DeleteStreetcodeToponymCommand(streetcodeId, toponymId);

            this.MockRepository.SetupRepositoryWrapper(this.streetcodeToponymRepositoryMock);
            this.streetcodeToponymRepositoryMock.SetupGetFirstOrDefaultAsync(entity);
            this.streetcodeToponymRepositoryMock
                .SetupDelete<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.SetupSaveChangesAsyncSuccess();

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();

            // Verify.
            this.streetcodeToponymRepositoryMock
                .VerifyGetFirstOrDefaultCalledOnce<IStreetcodeToponymRepository,
                    DAL.Entities.Toponyms.StreetcodeToponym>();
            this.streetcodeToponymRepositoryMock
                .VerifyDeleteCalledOnce<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.MockRepository.VerifySaveChangesAsyncCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenRelationshipDoesNotExist_ShouldReturnFailure()
        {
            // Arrange.
            int streetcodeId = 1;
            int toponymId = 99;
            string expectedError =
                $"Cannot find relationship with StreetcodeId={streetcodeId} and ToponymId={toponymId}";
            var command = new DeleteStreetcodeToponymCommand(streetcodeId, toponymId);

            this.MockRepository.SetupRepositoryWrapper(this.streetcodeToponymRepositoryMock);
            this.streetcodeToponymRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeToponymRepository,
                    DAL.Entities.Toponyms.StreetcodeToponym>(null);
            this.SetupLogger();

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().Message.Should().Be(expectedError);

            // Verify.
            this.streetcodeToponymRepositoryMock
                .VerifyGetFirstOrDefaultCalledOnce<IStreetcodeToponymRepository,
                    DAL.Entities.Toponyms.StreetcodeToponym>();
            this.streetcodeToponymRepositoryMock
                .VerifyDeleteCalledNever<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.MockRepository.VerifySaveChangesAsyncCalledNever();
            this.MockLogger.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenSaveChangesFails_ShouldReturnFailure()
        {
            // Arrange.
            const string expectedError = "Failed to delete streetcode-toponym relationship.";
            int streetcodeId = 1;
            int toponymId = 1;
            var entity = StreetcodeToponymTestData.CreateStreetcodeToponym(streetcodeId, toponymId);
            var command = new DeleteStreetcodeToponymCommand(streetcodeId, toponymId);

            this.MockRepository.SetupRepositoryWrapper(this.streetcodeToponymRepositoryMock);
            this.streetcodeToponymRepositoryMock.SetupGetFirstOrDefaultAsync(entity);
            this.streetcodeToponymRepositoryMock
                .SetupDelete<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.SetupSaveChangesAsyncFailure();
            this.SetupLogger();

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(expectedError);

            // Verify.
            this.MockLogger.VerifyLogErrorCalledOnce();
        }
    }
}