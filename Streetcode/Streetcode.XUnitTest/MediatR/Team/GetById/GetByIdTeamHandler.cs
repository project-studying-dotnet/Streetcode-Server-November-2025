namespace Streetcode.XUnitTest.MediatR.Team.GetById
{
    using AutoMapper;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Team;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Team.GetById;
 using global::Streetcode.DAL.Entities.Team;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Team;
 using global::Streetcode.DAL.Specifications.Team;
    using Xunit;

    public class GetByIdTeamHandlerTests
    {
        private const int TestMemberId = 1;
        private readonly string ErrorMsgTemplate = ErrorMessages.TeamNotFoundById;
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

            this.SetupRepositoryGetItemBySpecAsync(teamMember);
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
            this.SetupRepositoryGetItemBySpecAsync(null!);

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

        private void SetupRepositoryGetItemBySpecAsync(TeamMember? teamMember)
        {
            this.mockTeamRepository
                .Setup(r => r.GetBySpecAsync(
                    It.IsAny<TeamMemberByIdSpecification>(),
                    It.IsAny<CancellationToken>()))
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
