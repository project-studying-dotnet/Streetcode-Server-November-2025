namespace Streetcode.XUnitTest.MediatR.Fact.Create
{
    using AutoMapper;
    using Fixtures;
    using Helpers;
    using Moq;
    using Repositories.Interfaces;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Fact.Create;
    using Streetcode.BLL.MediatR.Streetcode.Fact.Create;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Entities.Streetcode.TextContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;
    using Streetcode.BLL.DTO.Media.Images;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
    using Streetcode.XUnitTest.Helpers;
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

            this.repositoryWrapperMock.SetupRepositoryWrapper(
                factRepositoryMock,
                imageRepositoryMock,
                streetcodeRepositoryMock);
            imageRepositoryMock.SetupGetFirstOrDefaultAsync(image);
            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);
            factRepositoryMock.SetupGetFirstOrDefaultAsync<IFactRepository, Fact>(entity: null);
            this.mapperMock.SetupMapper(createFactDto, newFact);
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
            this.mapperMock.VerifyMapCalledOnce<CreateFactDTO, Fact>(createFactDto);
            factRepositoryMock.VerifyCreateAsyncCalledOnce<IFactRepository, Fact>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce<Fact, FactDto>(newFact);
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        [Fact]
        public async Task Handle_WhenImageDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            const string errorMsg = "Image with provided ImageId does not exist";
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
            Assert.Equal(errorMsg, result.Errors.FirstOrDefault()?.Message);

            // Verify
            imageRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IImageRepository, Image>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenStreetcodeDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            const string errorMsg = "Streetcode with provided StreetcodeId does not exist";
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
            Assert.Equal(errorMsg, result.Errors.FirstOrDefault()?.Message);

            // Verify
            imageRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IImageRepository, Image>();
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenFactExists_ShouldReturnFailureResult()
        {
            // Arrange
            const string errorMsg = "Fact with the same title already exists for this Streetcode";
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
            const string errorMsg = "Failed to map CreateFactDTO to Fact entity";
            var imageRepositoryMock = new Mock<IImageRepository>(MockBehavior.Strict);
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var createFactDto = FactTestData.CreateCreateFactDto();
            var command = new CreateFactCommand(createFactDto);
            var image = new Image { Id = createFactDto.ImageId };
            var streetcode = new StreetcodeContent { Id = createFactDto.StreetcodeId };
            var newFact = FactTestData.CreateFact(streetcodeId: createFactDto.StreetcodeId);
            var factDto = FactTestData.CreateFactDto();

            this.repositoryWrapperMock.SetupRepositoryWrapper(
                factRepositoryMock,
                imageRepositoryMock,
                streetcodeRepositoryMock);
            imageRepositoryMock.SetupGetFirstOrDefaultAsync(image);
            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(streetcode);
            factRepositoryMock.SetupGetFirstOrDefaultAsync<IFactRepository, Fact>(entity: null);
            this.mapperMock.SetupMapper<CreateFactDTO, Fact>(createFactDto, null!);

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
            this.mapperMock.VerifyMapCalledOnce<CreateFactDTO, Fact>(createFactDto);
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}
