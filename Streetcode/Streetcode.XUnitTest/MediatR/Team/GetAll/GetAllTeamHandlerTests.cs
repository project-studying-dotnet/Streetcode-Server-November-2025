namespace Streetcode.XUnitTest.MediatR.Team.GetAll
{
    using System.Linq.Expressions;
    using AutoMapper;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Team;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Team.GetAll;
    using Streetcode.DAL.Entities.Team;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Team;
    using Xunit;

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
            this.mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            this.mockTeamRepository = new Mock<ITeamRepository>();
            this.mockMapper = new Mock<IMapper>();
            this.mockLogger = new Mock<ILoggerService>();

            this.mockRepositoryWrapper
                .Setup(w => w.TeamRepository)
                .Returns(this.mockTeamRepository.Object);

            this.handler = new GetAllTeamHandler(
                this.mockRepositoryWrapper.Object,
                this.mockMapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenTeamMembersExist()
        {
            // Arrange
            var teamMembers = GetTestTeamMembers();
            var teamMemberDTOs = GetTestTeamMemberDTOs();

            this.SetupRepositoryGetAllAsync(teamMembers);
            this.SetupMapper(teamMemberDTOs);

            var query = new GetAllTeamQuery();

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
        public async Task Handle_ShouldReturnFailResultWithErrorMessage_WhenTeamIsNull()
        {
            // Arrange
            this.SetupRepositoryGetAllAsync(null!);

            var query = new GetAllTeamQuery();

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
                        It.Is<GetAllTeamQuery>(q => q == query),
                        It.Is<string>(msg => msg.Contains(ErrorMsg))),
                    Times.Once);
            }
        }

        private void SetupRepositoryGetAllAsync(IEnumerable<TeamMember> teamMembers)
        {
            this.mockTeamRepository
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<TeamMember, bool>>>(),
                    It.IsAny<Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>>()))
                .ReturnsAsync(teamMembers);
        }

        private void SetupMapper(IEnumerable<TeamMemberDto> teamMemberDTOs)
        {
            this.mockMapper
                .Setup(m => m.Map<IEnumerable<TeamMemberDto>>(It.IsAny<IEnumerable<TeamMember>>()))
                .Returns(teamMemberDTOs);
        }

        private static List<TeamMember> GetTestTeamMembers() => new List<TeamMember>
        {
            new TeamMember() { Id = 1 },
            new TeamMember() { Id = 4 },
        };

        private static List<TeamMemberDto> GetTestTeamMemberDTOs() => new List<TeamMemberDto>
        {
            new TeamMemberDto() { Id = 1 },
            new TeamMemberDto() { Id = 4 },
        };
    }
}