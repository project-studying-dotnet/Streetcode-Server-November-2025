namespace Streetcode.XUnitTest.MediatR.Team.TeamMembersLinks.GetAll
{
    using System.Linq.Expressions;
    using AutoMapper;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.Partners;
    using Streetcode.BLL.DTO.Team;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Team.TeamMembersLinks.GetAll;
    using Streetcode.DAL.Entities.Team;
    using Streetcode.DAL.Enums;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Team;
    using Xunit;

    public class GetAllTeamLinkHandlerTests
    {
        private readonly string ErrorMsg = ErrorMessages.TeamMemberLinkNotFound;
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;
        private readonly Mock<ITeamLinkRepository> mockTeamLinkRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetAllTeamLinkHandler handler;

        public GetAllTeamLinkHandlerTests()
        {
            this.mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            this.mockTeamLinkRepository = new Mock<ITeamLinkRepository>();
            this.mockMapper = new Mock<IMapper>();
            this.mockLogger = new Mock<ILoggerService>();

            this.mockRepositoryWrapper
                .Setup(w => w.TeamLinkRepository)
                .Returns(this.mockTeamLinkRepository.Object);

            this.handler = new GetAllTeamLinkHandler(
                this.mockRepositoryWrapper.Object,
                this.mockMapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenTeamLinksExist()
        {
            // Arrange
            var teamLinks = GetTestTeamLinks();
            var teamLinkDTOs = GetTestTeamLinkDTOs();

            this.SetupRepositoryGetAllAsync(teamLinks);
            this.SetupMapper(teamLinkDTOs);

            var query = new GetAllTeamLinkQuery();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.Should().BeEquivalentTo(teamLinkDTOs);
            }
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResultWithErrorMessage_WhenTeamLinksIsNull()
        {
            // Arrange
            this.SetupRepositoryGetAllAsync(null!);

            var query = new GetAllTeamLinkQuery();

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
                        It.Is<GetAllTeamLinkQuery>(q => q == query),
                        It.Is<string>(msg => msg.Contains(ErrorMsg))),
                    Times.Once);
            }
        }

        private void SetupRepositoryGetAllAsync(IEnumerable<TeamMemberLink> teamLinks)
        {
            this.mockTeamLinkRepository
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<TeamMemberLink, bool>>>(),
                    It.IsAny<Func<IQueryable<TeamMemberLink>, IIncludableQueryable<TeamMemberLink, object>>>()))
                .ReturnsAsync(teamLinks);
        }

        private void SetupMapper(IEnumerable<TeamMemberLinkDto> teamLinkDTOs)
        {
            this.mockMapper
                .Setup(m => m.Map<IEnumerable<TeamMemberLinkDto>>(It.IsAny<IEnumerable<TeamMemberLink>>()))
                .Returns(teamLinkDTOs);
        }

        private static IEnumerable<TeamMemberLink> GetTestTeamLinks() => new List<TeamMemberLink>
        {
            new TeamMemberLink
            {
                Id = 1,
                LogoType = LogoType.Instagram,
                TargetUrl = "https://instagram.com/",
                TeamMemberId = 1,
            },
            new TeamMemberLink
            {
                Id = 2,
                LogoType = LogoType.Facebook,
                TargetUrl = "https://facebook.com/",
                TeamMemberId = 2,
            },
        };

        private static List<TeamMemberLinkDto> GetTestTeamLinkDTOs() => new List<TeamMemberLinkDto>
        {
            new TeamMemberLinkDto
            {
                Id = 1,
                LogoType = LogoTypeDto.Instagram,
                TargetUrl = "https://instagram.com/",
                TeamMemberId = 1,
            },
            new TeamMemberLinkDto
            {
                Id = 2,
                LogoType = LogoTypeDto.Facebook,
                TargetUrl = "https://facebook.com/",
                TeamMemberId = 2,
            },
        };
    }
}
