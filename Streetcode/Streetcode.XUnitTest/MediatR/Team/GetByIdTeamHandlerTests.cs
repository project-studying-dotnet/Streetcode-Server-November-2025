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
using System.Linq.Expressions;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Team
{
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
            mockTeamRepository = new Mock<ITeamRepository>();
            mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILoggerService>();

            mockRepositoryWrapper
                .Setup(w => w.TeamRepository)
                .Returns(mockTeamRepository.Object);

            handler = new GetByIdTeamHandler(
                mockRepositoryWrapper.Object,
                mockMapper.Object,
                mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenMemberExists()
        {
            // Arrange
            var teamMember = new TeamMember { Id = TestMemberId };
            var teamMemberDTO = new TeamMemberDTO { Id = TestMemberId };

            SetupRepositoryGetByIdAsync(teamMember);
            SetupMapper(teamMemberDTO);

            var query = new GetByIdTeamQuery(TestMemberId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.Should().Be(teamMemberDTO);
            }
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenMemberIsNull()
        {
            // Arrange
            SetupRepositoryGetByIdAsync(null);

            var query = new GetByIdTeamQuery(TestMemberId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenMemberIsNull()
        {
            // Arrange
            SetupRepositoryGetByIdAsync(null);

            var query = new GetByIdTeamQuery(TestMemberId);
            var expectedErrorMessage = string.Format(ErrorMsgTemplate, TestMemberId);

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<GetByIdTeamQuery>(q => q == query),
                    It.Is<string>(msg => msg.Contains(expectedErrorMessage))),
                Times.Once);
        }

        private void SetupRepositoryGetByIdAsync(TeamMember teamMember)
        {
            mockTeamRepository
                .Setup(r => r.GetSingleOrDefaultAsync(
                    It.IsAny<Expression<Func<TeamMember, bool>>>(),
                    It.IsAny<Func<IQueryable<TeamMember>, IIncludableQueryable<TeamMember, object>>>()))
                .ReturnsAsync(teamMember);
        }

        private void SetupMapper(TeamMemberDTO teamMemberDTO)
        {
            mockMapper
                .Setup(m => m.Map<TeamMemberDTO>(It.IsAny<TeamMember>()))
                .Returns(teamMemberDTO);
        }
    }
}
