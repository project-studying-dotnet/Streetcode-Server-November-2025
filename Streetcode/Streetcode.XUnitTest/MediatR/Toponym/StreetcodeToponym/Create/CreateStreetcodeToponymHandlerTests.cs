namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Create
{
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Toponyms;
 using global::Streetcode.BLL.MediatR.Toponyms.Create;
 using global::Streetcode.DAL.Repositories.Interfaces.Toponyms;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Helpers;
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
            this.MockMapper.SetupMapperAny<StreetcodeToponymDto, DAL.Entities.Toponyms.StreetcodeToponym>(entity);
            this.MockMapper.SetupMapperAny<DAL.Entities.Toponyms.StreetcodeToponym, StreetcodeToponymDto>(dto);
            this.MockRepository.SetupSaveChangesAsync();

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
            this.MockMapper.VerifyMapCalledOnce<DAL.Entities.Toponyms.StreetcodeToponym>();
            this.MockMapper.VerifyMapCalledOnce<StreetcodeToponymDto>();
            this.MockRepository.VerifySaveChangesAsyncCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenRelationshipAlreadyExists_ShouldReturnFailure()
        {
            // Arrange.
            string expectedError = ErrorMessages.ToponymAlreadyLinked;
            var dto = StreetcodeToponymTestData.CreateStreetcodeToponymDto();
            var existingEntity = StreetcodeToponymTestData.CreateStreetcodeToponym();
            var command = new CreateStreetcodeToponymCommand(dto);

            this.MockRepository.SetupRepositoryWrapper(this.streetcodeToponymRepositoryMock);
            this.streetcodeToponymRepositoryMock.SetupGetFirstOrDefaultAsync(existingEntity);
            this.MockLogger.SetupLogger();

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
            string expectedError = ErrorMessages.ToponymCantBeMapped;
            var dto = StreetcodeToponymTestData.CreateStreetcodeToponymDto();
            var command = new CreateStreetcodeToponymCommand(dto);

            this.MockRepository.SetupRepositoryWrapper(this.streetcodeToponymRepositoryMock);
            this.streetcodeToponymRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeToponymRepository,
                    DAL.Entities.Toponyms.StreetcodeToponym>(null);
            this.MockMapper.SetupMapperAny<StreetcodeToponymDto, DAL.Entities.Toponyms.StreetcodeToponym>(null!);
            this.MockLogger.SetupLogger();

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(expectedError);

            // Verify.
            this.streetcodeToponymRepositoryMock
                .VerifyGetFirstOrDefaultCalledOnce<IStreetcodeToponymRepository,
                    DAL.Entities.Toponyms.StreetcodeToponym>();
            this.streetcodeToponymRepositoryMock
                .VerifyCreateAsyncCalledNever<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.MockMapper.VerifyMapCalledOnce<DAL.Entities.Toponyms.StreetcodeToponym>();
            this.MockLogger.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenSaveChangesFails_ShouldReturnFailure()
        {
            // Arrange.
            string expectedError = ErrorMessages.ToponymStreetcodeFailedToCreate;
            var dto = StreetcodeToponymTestData.CreateStreetcodeToponymDto();
            var entity = StreetcodeToponymTestData.CreateStreetcodeToponym();
            var command = new CreateStreetcodeToponymCommand(dto);

            this.MockRepository.SetupRepositoryWrapper(this.streetcodeToponymRepositoryMock);
            this.streetcodeToponymRepositoryMock
                .SetupGetFirstOrDefaultAsync<IStreetcodeToponymRepository,
                    DAL.Entities.Toponyms.StreetcodeToponym>(null);
            this.streetcodeToponymRepositoryMock.SetupCreateAsync(entity);
            this.MockMapper.SetupMapperAny<StreetcodeToponymDto, DAL.Entities.Toponyms.StreetcodeToponym>(entity);
            this.MockRepository
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(0);
            this.MockLogger.SetupLogger();

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(expectedError);

            // Verify.
            this.streetcodeToponymRepositoryMock
                .VerifyGetFirstOrDefaultCalledOnce<IStreetcodeToponymRepository,
                    DAL.Entities.Toponyms.StreetcodeToponym>();
            this.streetcodeToponymRepositoryMock
                .VerifyCreateAsyncCalledOnce<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.MockMapper.VerifyMapCalledOnce<DAL.Entities.Toponyms.StreetcodeToponym>();
            this.MockRepository.VerifySaveChangesAsyncCalledOnce();
            this.MockLogger.VerifyLogErrorCalledOnce();
        }
    }
}