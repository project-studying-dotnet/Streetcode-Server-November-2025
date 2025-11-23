using AutoMapper;
using Moq;
using Streetcode.BLL.DTO.Team;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Team.Create;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Interfaces.Team;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Team.Position
{
    public class CreatePositionHandlerTests
    {
        private const string TestPositionName = "Developer";
        private const string TestExceptionMessage = "Database connection failed";
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;
        private readonly Mock<IPositionRepository> mockPositionRepository;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly Mock<IMapper> mockMapper;
        private readonly CreatePositionHandler handler;

        public CreatePositionHandlerTests()
        {
            mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockPositionRepository = new Mock<IPositionRepository>();
            mockLogger = new Mock<ILoggerService>();
            mockMapper = new Mock<IMapper>();

            mockRepositoryWrapper
                .Setup(w => w.PositionRepository)
                .Returns(mockPositionRepository.Object);

            handler = new CreatePositionHandler(
                mockMapper.Object,
                mockRepositoryWrapper.Object,
                mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenPositionIsCreated()
        {
            // Arrange
            var position = GetTestPosition();
            var positionDTO = GetTestPositionDTO();

            SetupRepositoryCreateAsync(position);
            SetupRepositorySaveChangesAsync();
            SetupMapperToPositionDTO(positionDTO);

            var query = new CreatePositionQuery(positionDTO);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Multiple(
                () => Assert.True(result.IsSuccess),
                () => Assert.Equal(positionDTO.Position, result.Value.Position));
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenSaveChangesThrowsException()
        {
            // Arrange
            var position = GetTestPosition();
            var positionDTO = GetTestPositionDTO();

            SetupRepositoryCreateAsync(position);
            SetupRepositorySaveChangesAsyncFails();

            var query = new CreatePositionQuery(positionDTO);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenSaveChangesThrowsException()
        {
            // Arrange
            var position = GetTestPosition();
            var positionDTO = GetTestPositionDTO();

            SetupRepositoryCreateAsync(position);
            SetupRepositorySaveChangesAsyncFails();

            var query = new CreatePositionQuery(positionDTO);

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<CreatePositionQuery>(q => q == query),
                    It.Is<string>(msg => msg == TestExceptionMessage)),
                Times.Once);
        }

        private void SetupRepositoryCreateAsync(Positions position)
        {
            mockPositionRepository
                .Setup(r => r.CreateAsync(It.IsAny<Positions>()))
                .ReturnsAsync(position);
        }

        private void SetupRepositorySaveChangesAsync()
        {
            mockRepositoryWrapper
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);
        }

        private void SetupRepositorySaveChangesAsyncFails()
        {
            mockRepositoryWrapper
                .Setup(r => r.SaveChangesAsync())
                .ThrowsAsync(new Exception(TestExceptionMessage));
        }

        private void SetupMapperToPositionDTO(PositionDTO positionDTO)
        {
            mockMapper
                .Setup(m => m.Map<PositionDTO>(It.IsAny<Positions>()))
                .Returns(positionDTO);
        }

        private static Positions GetTestPosition() => new() { Position = TestPositionName };

        private static PositionDTO GetTestPositionDTO() => new() { Position = TestPositionName };
    }
}
