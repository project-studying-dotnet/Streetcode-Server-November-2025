namespace Streetcode.XUnitTest.MediatR.Team.Position.Create
{
    using AutoMapper;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Team;
 using global::Streetcode.BLL.MediatR.Team.Create;
 using global::Streetcode.DAL.Entities.Team;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Team;
    using Xunit;

    public class CreatePositionHandlerTests
    {
        private const string TestPositionName = "Developer";
        private readonly string TestExceptionMessage = ErrorMessages.DatabaseConntectionFailed;
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;
        private readonly Mock<IPositionRepository> mockPositionRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly CreatePositionHandler handler;

        public CreatePositionHandlerTests()
        {
            this.mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            this.mockPositionRepository = new Mock<IPositionRepository>();
            this.mockMapper = new Mock<IMapper>();

            this.mockRepositoryWrapper
                .Setup(w => w.PositionRepository)
                .Returns(this.mockPositionRepository.Object);

            this.handler = new CreatePositionHandler(
                this.mockMapper.Object,
                this.mockRepositoryWrapper.Object);
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
        public async Task Handle_ShouldThrowException_WhenSaveChangesFails()
        {
            // Arrange
            var position = GetTestPosition();
            var positionDTO = GetTestPositionDTO();

            this.SetupRepositoryCreateAsync(position);
            this.SetupRepositorySaveChangesAsyncFails();

            var query = new CreatePositionQuery(positionDTO);

            // Act
            Func<Task> act = async () => await this.handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage(TestExceptionMessage);
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

        private void SetupMapperToPositionDTO(PositionDto positionDTO)
        {
            this.mockMapper
                .Setup(m => m.Map<PositionDto>(It.IsAny<Positions>()))
                .Returns(positionDTO);
        }

        private static Positions GetTestPosition() => new Positions { Position = TestPositionName };

        private static PositionDto GetTestPositionDTO() => new PositionDto { Position = TestPositionName };
    }
}
