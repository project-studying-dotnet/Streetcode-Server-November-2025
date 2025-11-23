using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Team;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Team.GetAll;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Interfaces.Team;
using System.Linq.Expressions;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Team
{
    public class GetAllTeamHandlerTests
    {
        private const string ErrorMsg = "Cannot find any team";
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;
        private readonly Mock<ITeamRepository> mockTeamRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetAllTeamHandler handler;

        public GetAllTeamHandlerTests()
        {
            mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockTeamRepository = new Mock<ITeamRepository>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILoggerService>();

            mockRepositoryWrapper
                .Setup(w => w.TeamRepository)
                .Returns(mockTeamRepository.Object);

            handler = new GetAllTeamHandler(
                mockRepositoryWrapper.Object,
                mockMapper.Object,
                mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenTeamMembersExist()
        {
            // Arrange
            var teamMembers = GetTestTeamMembers();
            var teamMemberDTOs = GetTestTeamMemberDTOs();

            SetupRepositoryGetAllAsync(teamMembers);
            SetupMapper(teamMemberDTOs);

            var query = new GetAllTeamQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Multiple(
                () => Assert.True(result.IsSuccess),
                () => Assert.Equal(result.Value, teamMemberDTOs));
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenTeamIsNull()
        {
            // Arrange
            SetupRepositoryGetAllAsync(null);

            var query = new GetAllTeamQuery();

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<GetAllTeamQuery>(q => q == query),
                    It.Is<string>(msg => msg.Contains(ErrorMsg))),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenTeamIsNull()
        {
            // Arrange
            SetupRepositoryGetAllAsync(null);

            var query = new GetAllTeamQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }

        private void SetupRepositoryGetAllAsync(IEnumerable<TeamMember> teamMembers)
        {
            mockTeamRepository
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<TeamMember, bool>>>(),
                    It.IsAny<Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>>()))
                .ReturnsAsync(teamMembers);
        }

        private void SetupMapper(IEnumerable<TeamMemberDTO> teamMemberDTOs)
        {
            mockMapper
                .Setup(m => m.Map<IEnumerable<TeamMemberDTO>>(It.IsAny<IEnumerable<TeamMember>>()))
                .Returns(teamMemberDTOs);
        }

        private static List<TeamMember> GetTestTeamMembers() => new()
        {
            new TeamMember() { Id = 1 },
            new TeamMember() { Id = 4 },
        };

        private static List<TeamMemberDTO> GetTestTeamMemberDTOs() => new()
        {
            new TeamMemberDTO() { Id = 1 },
            new TeamMemberDTO() { Id = 4 },
        };
    }
}