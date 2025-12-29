namespace Streetcode.XUnitTest.MediatR.Fact.Create
{
    using AutoMapper;
    using MockQueryable;
    using Moq;
    using Repositories.Interfaces;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Fact.Create;
 using global::Streetcode.BLL.MediatR.Streetcode.Fact.Create;
 using global::Streetcode.DAL.Entities.Media.Images;
 using global::Streetcode.DAL.Entities.Streetcode;
 using global::Streetcode.DAL.Entities.Streetcode.TextContent;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Streetcode;
 using global::Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.Fact.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Fact.Helpers;
    using Xunit;

    public class CreateFactHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly CreateFactHandler handler;

        public CreateFactHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new CreateFactHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenAllValidationsPass_ShouldReturnFactDto()
        {
            // Arrange
            var imageRepositoryMock = new Mock<IImageRepository>(MockBehavior.Strict);
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var createFactDto = FactTestData.CreateCreateFactDto();
            var command = new CreateFactCommand(createFactDto);
            var image = new Image { Id = createFactDto.ImageId };
            var streetcode = new StreetcodeContent { Id = createFactDto.StreetcodeId };
            var newFact = FactTestData.CreateFact(streetcodeId: createFactDto.StreetcodeId);
            var factDto = FactTestData.CreateFactDto();
            var existingFactsMockQueryable = new List<Fact>
            {
                new Fact { Id = 1, StreetcodeId = createFactDto.StreetcodeId, Order = 1 },
                new Fact { Id = 2, StreetcodeId = createFactDto.StreetcodeId, Order = 2 },
            }.BuildMock();

            this.repositoryWrapperMock.SetupRepositoryWrapper(
                factRepositoryMock,
                imageRepositoryMock,
                streetcodeRepositoryMock);
            imageRepositoryMock.SetupGetFirstOrDefaultAsync(image);
            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);
            factRepositoryMock.SetupGetFirstOrDefaultAsync<IFactRepository, Fact>(entity: null);
            this.mapperMock.SetupMapper(createFactDto, newFact);
            factRepositoryMock.SetupFindAllAsync(existingFactsMockQueryable);
            factRepositoryMock.SetupCreateAsync(newFact);
            this.repositoryWrapperMock.SetupSaveChangesAsync();
            this.mapperMock.SetupMapper(newFact, factDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(factDto, result.Value);

            // Verify
            imageRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IImageRepository, Image>();
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            factRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IFactRepository, Fact>();
            this.mapperMock.VerifyMapCalledOnce<Fact>();
            factRepositoryMock.VerifyFindAllCalledOnce<IFactRepository, Fact>();
            factRepositoryMock.VerifyCreateAsyncCalledOnce<IFactRepository, Fact>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce<FactDto>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        [Fact]
        public async Task Handle_WhenImageDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            var imageRepositoryMock = new Mock<IImageRepository>(MockBehavior.Strict);
            var createFactDto = FactTestData.CreateCreateFactDto();
            var command = new CreateFactCommand(createFactDto);

            this.repositoryWrapperMock.SetupRepositoryWrapper(imageRepositoryMock);
            imageRepositoryMock.SetupGetFirstOrDefaultAsync<IImageRepository, Image>(entity: null);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(string.Format(ErrorMessages.ImageNotFoundById, createFactDto.ImageId), result.Errors.FirstOrDefault()?.Message);

            // Verify
            imageRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IImageRepository, Image>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenStreetcodeDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            var imageRepositoryMock = new Mock<IImageRepository>(MockBehavior.Strict);
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var createFactDto = FactTestData.CreateCreateFactDto();
            var command = new CreateFactCommand(createFactDto);
            var image = new Image { Id = createFactDto.ImageId };

            this.repositoryWrapperMock.SetupRepositoryWrapper(
                imageRepositoryMock,
                streetcodeRepositoryMock);
            imageRepositoryMock.SetupGetFirstOrDefaultAsync(image);
            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync<IStreetcodeRepository, StreetcodeContent>(entity: null);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(string.Format(ErrorMessages.StreetcodeNotFoundById, createFactDto.StreetcodeId), result.Errors.FirstOrDefault()?.Message);

            // Verify
            imageRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IImageRepository, Image>();
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenFactExists_ShouldReturnFailureResult()
        {
            // Arrange
            string errorMsg = ErrorMessages.FactTitleAlreadyExists;
            var imageRepositoryMock = new Mock<IImageRepository>(MockBehavior.Strict);
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var createFactDto = FactTestData.CreateCreateFactDto();
            var command = new CreateFactCommand(createFactDto);
            var image = new Image { Id = createFactDto.ImageId };
            var streetcode = new StreetcodeContent { Id = createFactDto.StreetcodeId };
            var exisitingFact = FactTestData.CreateFact(streetcodeId: createFactDto.StreetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(
                factRepositoryMock,
                imageRepositoryMock,
                streetcodeRepositoryMock);
            imageRepositoryMock.SetupGetFirstOrDefaultAsync(image);
            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);
            factRepositoryMock.SetupGetFirstOrDefaultAsync(exisitingFact);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(errorMsg, result.Errors.FirstOrDefault()?.Message);

            // Verify
            imageRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IImageRepository, Image>();
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            factRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IFactRepository, Fact>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenCreateFactDtoMappingFails_ShouldReturnFailureResult()
        {
            // Arrange
            string errorMsg = ErrorMessages.FactMappingFailed;
            var imageRepositoryMock = new Mock<IImageRepository>(MockBehavior.Strict);
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var createFactDto = FactTestData.CreateCreateFactDto();
            var command = new CreateFactCommand(createFactDto);
            var image = new Image { Id = createFactDto.ImageId };
            var streetcode = new StreetcodeContent { Id = createFactDto.StreetcodeId };

            this.repositoryWrapperMock.SetupRepositoryWrapper(
                factRepositoryMock,
                imageRepositoryMock,
                streetcodeRepositoryMock);
            imageRepositoryMock.SetupGetFirstOrDefaultAsync(image);
            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);
            factRepositoryMock.SetupGetFirstOrDefaultAsync<IFactRepository, Fact>(entity: null);
            this.mapperMock.SetupMapper<CreateFactDto, Fact>(createFactDto, null!);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(errorMsg, result.Errors.FirstOrDefault()?.Message);

            // Verify
            imageRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IImageRepository, Image>();
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            factRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IFactRepository, Fact>();
            this.mapperMock.VerifyMapCalledOnce<Fact>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}
