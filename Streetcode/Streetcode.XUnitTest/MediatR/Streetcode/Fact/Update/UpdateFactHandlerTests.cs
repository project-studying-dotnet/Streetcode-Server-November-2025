namespace Streetcode.XUnitTest.MediatR.Fact.Update
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
    using Streetcode.BLL.MediatR.Streetcode.Fact.Update;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Query;
    using Streetcode.BLL;

    public class UpdateFactHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly UpdateFactHandler handler;

        public UpdateFactHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new UpdateFactHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_WhenFactDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            string errorMsg = ErrorMessages.FactNotFound;
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var updateFactDto = FactTestData.CreateUpdateFactDto(id: 1);
            var command = new UpdateFactCommand(updateFactDto);

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
        public async Task Handle_WhenImageIdChangedAndImageDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            string errorMsg = ErrorMessages.ImageNotFound;
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var imageRepositoryMock = new Mock<IImageRepository>(MockBehavior.Strict);
            var existingFact = FactTestData.CreateFact(id: 1, imageId: 1);
            var updateFactDto = FactTestData.CreateUpdateFactDto(id: 1, imageId: 999);
            var command = new UpdateFactCommand(updateFactDto);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock, imageRepositoryMock);
            factRepositoryMock.SetupGetFirstOrDefaultAsync(existingFact);
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
            factRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IFactRepository, Fact>();
            imageRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IImageRepository, Image>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenTitleChangedAndDuplicateTitleExists_ShouldReturnFailureResult()
        {
            // Arrange
            string errorMsg = ErrorMessages.FactTitleAlreadyExists;
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var existingFact = FactTestData.CreateFact(id: 1, imageId: 1);
            existingFact.Title = "Original Title";

            var updateFactDto = FactTestData.CreateUpdateFactDto(id: 1, imageId: 1);
            updateFactDto.Title = "Duplicate Title";

            var duplicateFact = FactTestData.CreateFact(id: 2, imageId: 1);
            duplicateFact.Title = "Duplicate Title";

            var command = new UpdateFactCommand(updateFactDto);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);

            factRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Fact, bool>>>(),
                    It.IsAny<Func<IQueryable<Fact>, IIncludableQueryable<Fact, object>>>()))
                .ReturnsAsync(existingFact)
                .ReturnsAsync(duplicateFact);

            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(errorMsg, result.Errors.FirstOrDefault()?.Message);

            // Verify
            this.loggerMock.VerifyLogErrorCalledOnce();

        }

        [Fact]
        public async Task Handle_WhenImageIdNotChangedAndTitleNotChanged_ShouldUpdateSuccessfully()
        {
            // Arrange
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var existingFact = FactTestData.CreateFact(id: 1, imageId: 1);
            var updateFactDto = FactTestData.CreateUpdateFactDto(id: 1, imageId: 1);
            updateFactDto.Title = existingFact.Title;
            updateFactDto.FactContent = "Updated content";

            var command = new UpdateFactCommand(updateFactDto);
            var resultDto = FactTestData.CreateFactDto(id: 1, imageId: 1);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);

            factRepositoryMock.SetupGetFirstOrDefaultAsync(existingFact);
            this.mapperMock.SetupMapper(updateFactDto, existingFact);
            factRepositoryMock.SetupUpdate<IFactRepository, Fact>(existingFact);
            this.repositoryWrapperMock.SetupSaveChangesAsync();
            this.mapperMock.SetupMapper(existingFact, resultDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(resultDto.Id, result.Value.Id);

            // Verify
            factRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IFactRepository, Fact>();
            factRepositoryMock.VerifyUpdateCalledOnce<IFactRepository, Fact>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        [Fact]
        public async Task Handle_WhenImageIdChangedToValidImage_ShouldUpdateSuccessfully()
        {
            // Arrange
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var imageRepositoryMock = new Mock<IImageRepository>(MockBehavior.Strict);
            var existingFact = FactTestData.CreateFact(id: 1, imageId: 1);
            var updateFactDto = FactTestData.CreateUpdateFactDto(id: 1, imageId: 2);
            updateFactDto.Title = existingFact.Title;

            var newImage = new Image { Id = 2 };
            var command = new UpdateFactCommand(updateFactDto);
            var resultDto = FactTestData.CreateFactDto(id: 1, imageId: 2);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock, imageRepositoryMock);
            factRepositoryMock.SetupGetFirstOrDefaultAsync<IFactRepository, Fact>(existingFact);
            imageRepositoryMock.SetupGetFirstOrDefaultAsync<IImageRepository, Image>(newImage);
            this.mapperMock.SetupMapper(updateFactDto, existingFact);
            factRepositoryMock.SetupUpdate<IFactRepository, Fact>(existingFact);
            this.repositoryWrapperMock.SetupSaveChangesAsync();
            this.mapperMock.SetupMapper(existingFact, resultDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(resultDto.Id, result.Value.Id);

            // Verify
            factRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IFactRepository, Fact>();
            imageRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IImageRepository, Image>();
            factRepositoryMock.VerifyUpdateCalledOnce<IFactRepository, Fact>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        [Fact]
        public async Task Handle_WhenTitleChangedToUniqueTitleAndImageNotChanged_ShouldUpdateSuccessfully()
        {
            // Arrange
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var existingFact = FactTestData.CreateFact(id: 1, imageId: 1);
            existingFact.Title = "Original Title";

            var updateFactDto = FactTestData.CreateUpdateFactDto(id: 1, imageId: 1);
            updateFactDto.Title = "New Unique Title";

            var command = new UpdateFactCommand(updateFactDto);
            var resultDto = FactTestData.CreateFactDto(id: 1, imageId: 1);

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);

            factRepositoryMock
                .SetupSequence(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Fact, bool>>>(),
                    It.IsAny<Func<IQueryable<Fact>, IIncludableQueryable<Fact, object>>>()))
                .ReturnsAsync(existingFact)
                .ReturnsAsync((Fact?)null);

            this.mapperMock.SetupMapper(updateFactDto, existingFact);
            factRepositoryMock.SetupUpdate<IFactRepository, Fact>(existingFact);
            this.repositoryWrapperMock.SetupSaveChangesAsync();
            this.mapperMock.SetupMapper(existingFact, resultDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(resultDto.Id, result.Value.Id);

            // Verify
            factRepositoryMock.VerifyUpdateCalledOnce<IFactRepository, Fact>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        [Fact]
        public async Task Handle_WhenExceptionIsThrown_ShouldReturnFailureResult()
        {
            // Arrange
            var factRepositoryMock = new Mock<IFactRepository>(MockBehavior.Strict);
            var updateFactDto = FactTestData.CreateUpdateFactDto(id: 1);
            var command = new UpdateFactCommand(updateFactDto);
            var exceptionMessage = ErrorMessages.DatabaseConntectionFailed;

            this.repositoryWrapperMock.SetupRepositoryWrapper(factRepositoryMock);

            factRepositoryMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<Fact, bool>>>(),
                    It.IsAny<Func<IQueryable<Fact>, IIncludableQueryable<Fact, object>>>()))
                .ThrowsAsync(new Exception(exceptionMessage));

            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(exceptionMessage, result.Errors.FirstOrDefault()?.Message);

            // Verify
            factRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IFactRepository, Fact>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}
