namespace Streetcode.XUnitTest.MediatR.Team.Position.GetAll
{
    using System.Linq.Expressions;
    using AutoMapper;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Team;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Team.GetById;
    using Streetcode.BLL.MediatR.Team.Position.GetAll;
    using Streetcode.DAL.Entities.Team;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Team;
    using Xunit;

    public class GetAllPositionsHandlerTests
    {
        private const string ErrorMsg = "Cannot find any positions";
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;
        private readonly Mock<IPositionRepository> mockPositionRepository;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly Mock<IMapper> mockMapper;
        private readonly GetAllPositionsHandler handler;

        public GetAllPositionsHandlerTests()
        {
            this.mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            this.mockPositionRepository = new Mock<IPositionRepository>();
            this.mockLogger = new Mock<ILoggerService>();
            this.mockMapper = new Mock<IMapper>();

            this.mockRepositoryWrapper
                .Setup(w => w.PositionRepository)
                .Returns(this.mockPositionRepository.Object);

            this.handler = new GetAllPositionsHandler(
                this.mockRepositoryWrapper.Object,
                this.mockMapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenPositionsExist()
        {
            // Arrange
            var positions = new List<Positions>
            {
                new Positions { Position = "Product Owner" },
                new Positions { Position = "Manager" },
            };

            var positionDTOs = new List<PositionDTO>
            {
                new PositionDTO { Position = "Product Owner" },
                new PositionDTO { Position = "Manager" },
            };

            this.SetupRepositoryGetAllAsync(positions);
            this.SetupMapper(positionDTOs);

            var query = new GetAllPositionsQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.Should().BeEquivalentTo(positionDTOs);
            }
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenPositionsIsNull()
        {
            // Arrange
            this.SetupRepositoryGetAllAsync(null!);

            var query = new GetAllPositionsQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenPositionsIsNull()
        {
            // Arrange
            this.SetupRepositoryGetAllAsync(null!);

            var query = new GetAllPositionsQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            this.mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<GetAllPositionsQuery>(q => q == query),
                    It.Is<string>(msg => msg.Contains(ErrorMsg))),
                Times.Once);
        }

        private void SetupRepositoryGetAllAsync(IEnumerable<Positions> positions)
        {
            this.mockPositionRepository
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<Positions, bool>>>(),
                    It.IsAny<Func<IQueryable<Positions>, IIncludableQueryable<Positions, object>>>()))
                .ReturnsAsync(positions);
        }

        private void SetupMapper(IEnumerable<PositionDTO> positionsDTO)
        {
            this.mockMapper
                .Setup(m => m.Map<IEnumerable<PositionDTO>>(It.IsAny<IEnumerable<Positions>>()))
                .Returns(positionsDTO);
        }
    }
}
