namespace Streetcode.XUnitTest.MediatR.Team.Position.Create
{
    using AutoMapper;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using Moq;
    using Streetcode.BLL.DTO.Team;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Team.Create;
    using Streetcode.DAL.Entities.Team;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Team;
    using Xunit;

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
            this.mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            this.mockPositionRepository = new Mock<IPositionRepository>();
            this.mockLogger = new Mock<ILoggerService>();
            this.mockMapper = new Mock<IMapper>();

            this.mockRepositoryWrapper
                .Setup(w => w.PositionRepository)
                .Returns(this.mockPositionRepository.Object);

            this.handler = new CreatePositionHandler(
                this.mockMapper.Object,
                this.mockRepositoryWrapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenPositionIsCreated()
        {
            // Arrange
            var position = GetTestPosition();
            var positionDTO = GetTestPositionDTO();

            this.SetupRepositoryCreateAsync(position);
            this.SetupRepositorySaveChangesAsync();
            this.SetupMapperToPositionDTO(positionDTO);

            var query = new CreatePositionQuery(positionDTO);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.Should().BeEquivalentTo(positionDTO);
            }
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenSaveChangesThrowsException()
        {
            // Arrange
            var position = GetTestPosition();
            var positionDTO = GetTestPositionDTO();

            this.SetupRepositoryCreateAsync(position);
            this.SetupRepositorySaveChangesAsyncFails();

            var query = new CreatePositionQuery(positionDTO);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {

                result.IsFailed.Should().BeTrue();
                result.Errors.Should().ContainSingle();
                result.Errors.First().Message.Should().Be(TestExceptionMessage);

                this.mockLogger.Verify(
                    logger => logger.LogError(
                        It.Is<CreatePositionQuery>(q => q == query),
                        It.Is<string>(msg => msg == TestExceptionMessage)),
                    Times.Once);
            }
        }

        private void SetupRepositoryCreateAsync(Positions position)
        {
            this.mockPositionRepository
                .Setup(r => r.CreateAsync(It.IsAny<Positions>()))
                .ReturnsAsync(position);
        }

        private void SetupRepositorySaveChangesAsync()
        {
            this.mockRepositoryWrapper
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);
        }

        private void SetupRepositorySaveChangesAsyncFails()
        {
            this.mockRepositoryWrapper
                .Setup(r => r.SaveChangesAsync())
                .ThrowsAsync(new Exception(TestExceptionMessage));
        }

        private void SetupMapperToPositionDTO(PositionDTO positionDTO)
        {
            this.mockMapper
                .Setup(m => m.Map<PositionDTO>(It.IsAny<Positions>()))
                .Returns(positionDTO);
        }

        private static Positions GetTestPosition() => new Positions { Position = TestPositionName };

        private static PositionDTO GetTestPositionDTO() => new PositionDTO { Position = TestPositionName };
    }
}
