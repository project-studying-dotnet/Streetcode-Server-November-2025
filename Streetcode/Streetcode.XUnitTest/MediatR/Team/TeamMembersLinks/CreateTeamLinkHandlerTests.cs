using AutoMapper;
using Moq;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.DTO.Team;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Team.TeamMembersLinks.Create;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Interfaces.Team;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Team.TeamMembersLinks
{
    public class CreateTeamLinkHandlerTests
    {
        private const string ErrorMsgCannotConvertNull = "Cannot convert null to team link";
        private const string ErrorMsgCannotCreateTeamLink = "Cannot create team link";
        private const string ErrorMsgFailedToCreate = "Failed to create a team";
        private const string ErrorMsgFailedToMap = "Failed to map created team link";
        private readonly Mock<IRepositoryWrapper> mockRepositoryWrapper;
        private readonly Mock<ITeamLinkRepository> mockTeamLinkRepository;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly Mock<IMapper> mockMapper;
        private readonly CreateTeamLinkHandler handler;

        public CreateTeamLinkHandlerTests()
        {
            mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            mockTeamLinkRepository = new Mock<ITeamLinkRepository>();
            mockLogger = new Mock<ILoggerService>();
            mockMapper = new Mock<IMapper>();

            mockRepositoryWrapper
                .Setup(w => w.TeamLinkRepository)
                .Returns(mockTeamLinkRepository.Object);

            handler = new CreateTeamLinkHandler(
                mockMapper.Object,
                mockRepositoryWrapper.Object,
                mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenTeamLinkIsCreated()
        {
            // Arrange
            var teamLink = GetTestTeamLink();
            var teamLinkDTO = GetTestTeamLinkDTO();

            SetupMapperToTeamLink(teamLink);
            SetupRepositoryCreate(teamLink);
            SetupRepositorySaveChangesSuccess();
            SetupMapperToTeamLinkDTO(teamLinkDTO);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Multiple(
                () => Assert.True(result.IsSuccess),
                () => Assert.Equal(result.Value, teamLinkDTO));
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenMapperReturnsNull()
        {
            // Arrange
            var teamLinkDTO = GetTestTeamLinkDTO();

            SetupMapperToTeamLink(null);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenMapperReturnsNull()
        {
            // Arrange
            var teamLinkDTO = GetTestTeamLinkDTO();

            SetupMapperToTeamLink(null);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<CreateTeamLinkQuery>(q => q == query),
                    It.Is<string>(msg => msg == ErrorMsgCannotConvertNull)),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenRepositoryCreateReturnsNull()
        {
            // Arrange
            var teamLink = GetTestTeamLink();
            var teamLinkDTO = GetTestTeamLinkDTO();

            SetupMapperToTeamLink(teamLink);
            SetupRepositoryCreate(null);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenRepositoryCreateReturnsNull()
        {
            // Arrange
            var teamLink = GetTestTeamLink();
            var teamLinkDTO = GetTestTeamLinkDTO();

            SetupMapperToTeamLink(teamLink);
            SetupRepositoryCreate(null);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<CreateTeamLinkQuery>(q => q == query),
                    It.Is<string>(msg => msg == ErrorMsgCannotCreateTeamLink)),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenSaveChangesFails()
        {
            // Arrange
            var teamLink = GetTestTeamLink();
            var teamLinkDTO = GetTestTeamLinkDTO();

            SetupMapperToTeamLink(teamLink);
            SetupRepositoryCreate(teamLink);
            SetupRepositorySaveChangesFailure();

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailed);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenSaveChangesFails()
        {
            // Arrange
            var teamLink = GetTestTeamLink();
            var teamLinkDTO = GetTestTeamLinkDTO();

            SetupMapperToTeamLink(teamLink);
            SetupRepositoryCreate(teamLink);
            SetupRepositorySaveChangesFailure();

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<CreateTeamLinkQuery>(q => q == query),
                    It.Is<string>(msg => msg == ErrorMsgFailedToCreate)),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenTeamLinkDTOReturnsNull()
        {
            // Arrange
            var teamLink = GetTestTeamLink();
            var teamLinkDTO = GetTestTeamLinkDTO();

            SetupMapperToTeamLink(teamLink);
            SetupRepositoryCreate(teamLink);
            SetupRepositorySaveChangesSuccess();
            SetupMapperToTeamLinkDTO(null);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<CreateTeamLinkQuery>(q => q == query),
                    It.Is<string>(msg => msg == ErrorMsgFailedToMap)),
                Times.Once);
        }

        private void SetupMapperToTeamLink(TeamMemberLink teamLink)
        {
            mockMapper
                .Setup(m => m.Map<TeamMemberLink>(It.IsAny<TeamMemberLinkDTO>()))
                .Returns(teamLink);
        }

        private void SetupMapperToTeamLinkDTO(TeamMemberLinkDTO teamLinkDTO)
        {
            mockMapper
                .Setup(m => m.Map<TeamMemberLinkDTO>(It.IsAny<TeamMemberLink>()))
                .Returns(teamLinkDTO);
        }

        private void SetupRepositoryCreate(TeamMemberLink teamLink)
        {
            mockTeamLinkRepository
                .Setup(r => r.Create(It.IsAny<TeamMemberLink>()))
                .Returns(teamLink);
        }

        private void SetupRepositorySaveChangesSuccess()
        {
            mockRepositoryWrapper
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);
        }

        private void SetupRepositorySaveChangesFailure()
        {
            mockRepositoryWrapper
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);
        }

        private static TeamMemberLink GetTestTeamLink() => new()
        {
            Id = 1,
            LogoType = LogoType.Instagram,
            TargetUrl = "https://instagram.com/",
            TeamMemberId = 1,
        };

        private static TeamMemberLinkDTO GetTestTeamLinkDTO() => new()
        {
            Id = 1,
            LogoType = LogoTypeDTO.Instagram,
            TargetUrl = "https://instagram.com/",
            TeamMemberId = 1,
        };
    }
}