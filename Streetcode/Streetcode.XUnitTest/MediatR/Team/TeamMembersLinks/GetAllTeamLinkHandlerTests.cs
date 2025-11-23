using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.DTO.Team;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Team.TeamMembersLinks.GetAll;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Interfaces.Team;
using System.Linq.Expressions;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Team.TeamMembersLinks
{
    public class GetAllTeamLinkHandlerTests
    {
        private const string ErrorMsg = "Cannot find any team links";
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;
        private readonly Mock<ITeamLinkRepository> mockTeamLinkRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetAllTeamLinkHandler handler;

        public GetAllTeamLinkHandlerTests()
        {
            mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockTeamLinkRepository = new Mock<ITeamLinkRepository>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILoggerService>();

            mockRepositoryWrapper
                .Setup(w => w.TeamLinkRepository)
                .Returns(mockTeamLinkRepository.Object);

            handler = new GetAllTeamLinkHandler(
                mockRepositoryWrapper.Object,
                mockMapper.Object,
                mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenTeamLinksExist()
        {
            // Arrange
            var teamLinks = GetTestTeamLinks();
            var teamLinkDTOs = GetTestTeamLinkDTOs();

            SetupRepositoryGetAllAsync(teamLinks);
            SetupMapper(teamLinkDTOs);

            var query = new GetAllTeamLinkQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.Should().BeEquivalentTo(teamLinkDTOs);
            }
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenTeamLinksIsNull()
        {
            // Arrange
            SetupRepositoryGetAllAsync(null);

            var query = new GetAllTeamLinkQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenTeamLinksIsNull()
        {
            // Arrange
            SetupRepositoryGetAllAsync(null);

            var query = new GetAllTeamLinkQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<GetAllTeamLinkQuery>(q => q == query),
                    It.Is<string>(msg => msg.Contains(ErrorMsg))),
                Times.Once);
        }

        private void SetupRepositoryGetAllAsync(IEnumerable<TeamMemberLink> teamLinks)
        {
            mockTeamLinkRepository
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<TeamMemberLink, bool>>>(),
                    It.IsAny<Func<IQueryable<TeamMemberLink>, IIncludableQueryable<TeamMemberLink, object>>>()))
                .ReturnsAsync(teamLinks);
        }

        private void SetupMapper(IEnumerable<TeamMemberLinkDTO> teamLinkDTOs)
        {
            mockMapper
                .Setup(m => m.Map<IEnumerable<TeamMemberLinkDTO>>(It.IsAny<IEnumerable<TeamMemberLink>>()))
                .Returns(teamLinkDTOs);
        }

        private static List<TeamMemberLink> GetTestTeamLinks() => new()
        {
            new TeamMemberLink
            {
                Id = 1,
                LogoType = LogoType.Instagram,
                TargetUrl = "https://instagram.com/",
                TeamMemberId = 1
            },
            new TeamMemberLink
            {
                Id = 2,
                LogoType = LogoType.Facebook,
                TargetUrl = "https://facebook.com/",
                TeamMemberId = 2
            }
        };

        private static List<TeamMemberLinkDTO> GetTestTeamLinkDTOs() => new()
        {
            new TeamMemberLinkDTO
            {
                Id = 1,
                LogoType = LogoTypeDTO.Instagram,
                TargetUrl = "https://instagram.com/",
                TeamMemberId = 1
            },
            new TeamMemberLinkDTO
            {
                Id = 2,
                LogoType = LogoTypeDTO.Facebook,
                TargetUrl = "https://facebook.com/",
                TeamMemberId = 2
            }
        };
    }
}
