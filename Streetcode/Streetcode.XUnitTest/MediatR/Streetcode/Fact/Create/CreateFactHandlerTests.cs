namespace Streetcode.XUnitTest.MediatR.Fact.Create
{
    using AutoMapper;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Fact.Create;
    using Streetcode.DAL.Repositories.Interfaces.Base;
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
        public async Task Handle_WhenImageExists_ShouldReturnFactDto()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public async Task Handle_WhenImageDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public async Task Handle_WhenStreetcodeDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public async Task Handle_WhenFactDoesNotExist_ShouldReturnFailureResult()
        {
            // Arrange
            // Act
            // Assert
        }

        [Fact]
        public async Task Handle_WhenCreateFactDtoMappingFails_ShouldReturnFailureResult()
        {
            // Arrange
            // Act
            // Assert
        }
    }
}
