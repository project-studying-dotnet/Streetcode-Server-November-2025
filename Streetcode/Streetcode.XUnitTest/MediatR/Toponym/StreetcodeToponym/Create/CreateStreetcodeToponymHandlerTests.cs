namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Create
{
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.BLL.MediatR.Toponyms.Create;
    using Streetcode.DAL.Repositories.Interfaces.Toponyms;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Fixtures;
    using Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="CreateStreetcodeToponymHandler"/>.
    /// </summary>
    public class CreateStreetcodeToponymHandlerTests : StreetcodeToponymHandlerTestsBase
    {
        private readonly CreateStreetcodeToponymHandler handler;
        private readonly Mock<IStreetcodeToponymRepository> streetcodeToponymRepositoryMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStreetcodeToponymHandlerTests"/> class.
        /// </summary>
        public CreateStreetcodeToponymHandlerTests()
        {
            this.streetcodeToponymRepositoryMock = new Mock<IStreetcodeToponymRepository>(MockBehavior.Strict);
            this.handler = new CreateStreetcodeToponymHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        [Fact]
        public async Task Handle_WhenRelationshipDoesNotExist_ShouldCreateSuccessfully()
        {
            // Arrange.
            var dto = StreetcodeToponymTestData.CreateStreetcodeToponymDto();
            var entity = StreetcodeToponymTestData.CreateStreetcodeToponym();
            var command = new CreateStreetcodeToponymCommand(dto);

            this.MockRepository.SetupRepositoryWrapper(this.streetcodeToponymRepositoryMock);
            this.streetcodeToponymRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>(null);
            this.streetcodeToponymRepositoryMock.SetupCreateAsync(entity);
            this.SetupMapperForStreetcodeToponymEntity(entity);
            this.SetupMapperForStreetcodeToponymDto(dto);
            this.SetupSaveChangesAsyncSuccess();

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dto);

            // Verify.
            this.streetcodeToponymRepositoryMock
                .VerifyGetFirstOrDefaultCalledOnce<IStreetcodeToponymRepository,
                    DAL.Entities.Toponyms.StreetcodeToponym>();
            this.streetcodeToponymRepositoryMock
                .VerifyCreateAsyncCalledOnce<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.MockRepository.VerifySaveChangesAsyncCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenRelationshipAlreadyExists_ShouldReturnFailure()
        {
            // Arrange.
            const string expectedError = "This toponym is already linked to the streetcode.";
            var dto = StreetcodeToponymTestData.CreateStreetcodeToponymDto();
            var existingEntity = StreetcodeToponymTestData.CreateStreetcodeToponym();
            var command = new CreateStreetcodeToponymCommand(dto);

            this.MockRepository.SetupRepositoryWrapper(this.streetcodeToponymRepositoryMock);
            this.streetcodeToponymRepositoryMock.SetupGetFirstOrDefaultAsync(existingEntity);
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
                .VerifyCreateAsyncCalledNever<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.MockRepository.VerifySaveChangesAsyncCalledNever();
            this.MockLogger.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenMappingReturnsNull_ShouldReturnFailure()
        {
            // Arrange.
            const string expectedError = "Cannot map StreetcodeToponymDto to entity.";
            var dto = StreetcodeToponymTestData.CreateStreetcodeToponymDto();
            var command = new CreateStreetcodeToponymCommand(dto);

            this.MockRepository.SetupRepositoryWrapper(this.streetcodeToponymRepositoryMock);
            this.streetcodeToponymRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeToponymRepository,
                    DAL.Entities.Toponyms.StreetcodeToponym>(null);
            this.MockMapper
                .Setup(m => m.Map<DAL.Entities.Toponyms.StreetcodeToponym>(It.IsAny<StreetcodeToponymDto>()))
                .Returns((DAL.Entities.Toponyms.StreetcodeToponym?)null);
            this.SetupLogger();

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(expectedError);

            // Verify.
            this.streetcodeToponymRepositoryMock
                .VerifyCreateAsyncCalledNever<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.MockLogger.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenSaveChangesFails_ShouldReturnFailure()
        {
            // Arrange.
            const string expectedError = "Failed to create streetcode-toponym relationship.";
            var dto = StreetcodeToponymTestData.CreateStreetcodeToponymDto();
            var entity = StreetcodeToponymTestData.CreateStreetcodeToponym();
            var command = new CreateStreetcodeToponymCommand(dto);

            this.MockRepository.SetupRepositoryWrapper(this.streetcodeToponymRepositoryMock);
            this.streetcodeToponymRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeToponymRepository,
                    DAL.Entities.Toponyms.StreetcodeToponym>(null);
            this.streetcodeToponymRepositoryMock.SetupCreateAsync(entity);
            this.SetupMapperForStreetcodeToponymEntity(entity);
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