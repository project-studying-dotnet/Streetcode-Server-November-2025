namespace Streetcode.XUnitTest.MediatR.Team.GetById
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
    using Streetcode.DAL.Entities.Team;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Team;
    using Xunit;

    public class GetByIdTeamHandlerTests
    {
        private const int TestMemberId = 1;
        private const string ErrorMsgTemplate = "Cannot find any team with corresponding id: {0}";
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;
        private readonly Mock<ITeamRepository> mockTeamRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetByIdTeamHandler handler;

        public GetByIdTeamHandlerTests()
        {
            this.mockTeamRepository = new Mock<ITeamRepository>();
            this.mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();
            this.mockLogger = new Mock<ILoggerService>();

            this.mockRepositoryWrapper
                .Setup(w => w.TeamRepository)
                .Returns(this.mockTeamRepository.Object);

            this.handler = new GetByIdTeamHandler(
                this.mockRepositoryWrapper.Object,
                this.mockMapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenMemberExists()
        {
            // Arrange
            var teamMember = new TeamMember { Id = TestMemberId };
            var teamMemberDTO = new TeamMemberDto { Id = TestMemberId };

            this.SetupRepositoryGetByIdAsync(teamMember);
            this.SetupMapper(teamMemberDTO);

            var query = new GetByIdTeamQuery(TestMemberId);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.Should().Be(teamMemberDTO);
            }
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResultWithErrorMessage_WhenMemberIsNull()
        {
            // Arrange
            this.SetupRepositoryGetByIdAsync(null!);

            var query = new GetByIdTeamQuery(TestMemberId);
            var expectedErrorMessage = string.Format(ErrorMsgTemplate, TestMemberId);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsFailed.Should().BeTrue();
                result.Errors.Should().ContainSingle();
                result.Errors.First().Message.Should().Be(expectedErrorMessage);

                this.mockLogger.Verify(
                    logger => logger.LogError(
                        It.Is<GetByIdTeamQuery>(q => q == query),
                        It.Is<string>(msg => msg.Contains(expectedErrorMessage))),
                    Times.Once);
            }
        }

        private void SetupRepositoryGetByIdAsync(TeamMember teamMember)
        {
            this.mockTeamRepository
                .Setup(r => r.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<TeamMember, bool>>>(),
                    It.IsAny<Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>>()))
                .ReturnsAsync(teamMember);
        }

        private void SetupMapper(TeamMemberDto teamMemberDTO)
        {
            this.mockMapper
                .Setup(m => m.Map<TeamMemberDto>(It.IsAny<TeamMember>()))
                .Returns(teamMemberDTO);
        }
    }
}
