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
    public class GetAllMainTeamHandlerTests
    {
        private const string ErrorMsg = "Cannot find any team";
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;
        private readonly Mock<ITeamRepository> mockTeamRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetAllMainTeamHandler handler;

        public GetAllMainTeamHandlerTests()
        {
            mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockTeamRepository = new Mock<ITeamRepository>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILoggerService>();

            mockRepositoryWrapper
                .Setup(w => w.TeamRepository)
                .Returns(mockTeamRepository.Object);

            handler = new GetAllMainTeamHandler(
                mockRepositoryWrapper.Object,
                mockMapper.Object,
                mockLogger.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenMainTeamMembersExist()
        {
            // Arrange
            var teamMembers = GetTestMainTeamMembersOnly();
            var teamMemberDTOs = GetTestMainTeamMemberDTOsOnly();

            SetupRepositoryGetAllMainAsync(teamMembers);
            SetupMapper(teamMemberDTOs);

            var query = new GetAllMainTeamQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Multiple(
                () => Assert.Equal(teamMemberDTOs, result.Value),
                () => Assert.True(result.IsSuccess));
        }

        [Fact]
        public async Task Handle_ShouldReturnOnlyMainTeamMembers_WhenMixedTeamMembersExist()
        {
            // Arrange
            var mixedTeamMembers = GetTestMixedTeamMembers();
            var mainTeamMembers = mixedTeamMembers.Where(m => m.IsMain);

            var mixedTeamMemberDTOs = GetTestMixedTeamMemberDTOs();
            var mainTeamMemberDTOs = mixedTeamMemberDTOs.Where(dto => dto.IsMain);

            SetupRepositoryGetAllMainAsync(mixedTeamMembers);
            SetupMapper(mainTeamMemberDTOs);

            var query = new GetAllMainTeamQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenTeamIsNull()
        {
            // Arrange
            SetupRepositoryGetAllMainAsync(null);

            var query = new GetAllMainTeamQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenTeamIsNull()
        {
            // Arrange
            SetupRepositoryGetAllMainAsync(null);

            var query = new GetAllMainTeamQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<GetAllMainTeamQuery>(q => q == query),
                    It.Is<string>(msg => msg.Contains(ErrorMsg))),
                Times.Once);
        }

        private void SetupRepositoryGetAllMainAsync(IEnumerable<TeamMember>? teamMembers)
        {
            mockTeamRepository
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<TeamMember, bool>>>(),
                    It.IsAny<Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>>()))
                .ReturnsAsync(teamMembers?.Where(m => m.IsMain));
        }

        private void SetupMapper(IEnumerable<TeamMemberDTO> teamMemberDTOs)
        {
            mockMapper
                .Setup(m => m.Map<IEnumerable<TeamMemberDTO>>(It.IsAny<IEnumerable<TeamMember>>()))
                .Returns(teamMemberDTOs);
        }

        private static List<TeamMember> GetTestMainTeamMembersOnly() => new()
        {
            new TeamMember { Id = 1, IsMain = true },
            new TeamMember { Id = 2, IsMain = true },
        };

        private static List<TeamMemberDTO> GetTestMainTeamMemberDTOsOnly() => new()
        {
            new TeamMemberDTO { Id = 1, IsMain = true },
            new TeamMemberDTO { Id = 2, IsMain = true },
        };

        private static List<TeamMember> GetTestMixedTeamMembers() => new()
        {
            new TeamMember { Id = 1, IsMain = true },
            new TeamMember { Id = 4, IsMain = false },
            new TeamMember { Id = 5, IsMain = false },
        };

        private static List<TeamMemberDTO> GetTestMixedTeamMemberDTOs() => new()
        {
            new TeamMemberDTO { Id = 1, IsMain = true },
            new TeamMemberDTO { Id = 4, IsMain = false },
            new TeamMemberDTO { Id = 5, IsMain = false },
        };
    }
}
