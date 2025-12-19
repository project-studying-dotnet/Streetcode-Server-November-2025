namespace Streetcode.XUnitTest.MediatR.Team.GetAllMain
{
    using AutoMapper;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using Moq;
    using Streetcode.BLL.DTO.Team;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Team.GetAll;
    using Streetcode.DAL.Entities.Team;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Team;
    using Streetcode.DAL.Specifications.Team;
    using Xunit;

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
            this.mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            this.mockTeamRepository = new Mock<ITeamRepository>();
            this.mockMapper = new Mock<IMapper>();
            this.mockLogger = new Mock<ILoggerService>();

            this.mockRepositoryWrapper
                .Setup(w => w.TeamRepository)
                .Returns(this.mockTeamRepository.Object);

            this.handler = new GetAllMainTeamHandler(
                this.mockRepositoryWrapper.Object,
                this.mockMapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenMainTeamMembersExist()
        {
            // Arrange
            var teamMembers = GetTestMainTeamMembersOnly();
            var teamMemberDTOs = GetTestMainTeamMemberDTOsOnly();

            this.SetupRepositoryListAsync(teamMembers);
            this.SetupMapper(teamMemberDTOs);

            var query = new GetAllMainTeamQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.Should().BeEquivalentTo(teamMemberDTOs);
            }
        }

        [Fact]
        public async Task Handle_ShouldReturnOnlyMainTeamMembers_WhenMixedTeamMembersExist()
        {
            // Arrange
            var mixedTeamMembers = GetTestMixedTeamMembers();

            var mixedTeamMemberDTOs = GetTestMixedTeamMemberDTOs();
            var mainTeamMemberDTOs = mixedTeamMemberDTOs.Where(dto => dto.IsMain);

            this.SetupRepositoryListAsync(mixedTeamMembers);
            this.SetupMapper(mainTeamMemberDTOs);

            var query = new GetAllMainTeamQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResultWithErrorMessage_WhenTeamIsNull()
        {
            // Arrange
            this.SetupRepositoryListAsync(null);

            var query = new GetAllMainTeamQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsFailed.Should().BeTrue();
                result.Errors.Should().ContainSingle();
                result.Errors.First().Message.Should().Be(ErrorMsg);

                this.mockLogger.Verify(
                    logger => logger.LogError(
                        It.Is<GetAllMainTeamQuery>(q => q == query),
                        It.Is<string>(msg => msg.Contains(ErrorMsg))),
                    Times.Once);
            }
        }

        [Fact]
        public async Task Handle_ShouldCallRepositoryWithMainTeamSpecification()
        {
            // Arrange
            var teamMembers = GetTestMainTeamMembersOnly();
            var teamMemberDTOs = GetTestMainTeamMemberDTOsOnly();

            this.SetupRepositoryListAsync(teamMembers);
            this.SetupMapper(teamMemberDTOs);

            var query = new GetAllMainTeamQuery();

            // Act
            await this.handler.Handle(query, CancellationToken.None);

            // Assert
            this.mockTeamRepository.Verify(
                r => r.ListAsync(
                    It.IsAny<MainTeamSpecification>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        private void SetupRepositoryListAsync(IEnumerable<TeamMember>? teamMembers)
        {
            this.mockTeamRepository
                .Setup(r => r.ListAsync(
                    It.IsAny<MainTeamSpecification>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(teamMembers);
        }

        private void SetupMapper(IEnumerable<TeamMemberDto> teamMemberDTOs)
        {
            this.mockMapper
                .Setup(m => m.Map<IEnumerable<TeamMemberDto>>(It.IsAny<IEnumerable<TeamMember>>()))
                .Returns(teamMemberDTOs);
        }

        private static List<TeamMember> GetTestMainTeamMembersOnly() => new List<TeamMember>
        {
            new TeamMember { Id = 1, IsMain = true },
            new TeamMember { Id = 2, IsMain = true },
        };

        private static List<TeamMemberDto> GetTestMainTeamMemberDTOsOnly() => new List<TeamMemberDto>
        {
            new TeamMemberDto { Id = 1, IsMain = true },
            new TeamMemberDto { Id = 2, IsMain = true },
        };

        private static List<TeamMember> GetTestMixedTeamMembers() => new List<TeamMember>
        {
            new TeamMember { Id = 1, IsMain = true },
            new TeamMember { Id = 4, IsMain = false },
            new TeamMember { Id = 5, IsMain = false },
        };

        private static List<TeamMemberDto> GetTestMixedTeamMemberDTOs() => new List<TeamMemberDto>
        {
            new TeamMemberDto { Id = 1, IsMain = true },
            new TeamMemberDto { Id = 4, IsMain = false },
            new TeamMemberDto { Id = 5, IsMain = false },
        };
    }
}
