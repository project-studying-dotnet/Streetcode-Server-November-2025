namespace Streetcode.XUnitTest.MediatR.Team.TeamMembersLinks.Create
{
    using AutoMapper;
    using FluentAssertions;
    using FluentAssertions.Execution;
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
            this.mockRepositoryWrapper = new Mock<IRepositoryWrapper>();
            this.mockTeamLinkRepository = new Mock<ITeamLinkRepository>();
            this.mockLogger = new Mock<ILoggerService>();
            this.mockMapper = new Mock<IMapper>();

            this.mockRepositoryWrapper
                .Setup(w => w.TeamLinkRepository)
                .Returns(this.mockTeamLinkRepository.Object);

            this.handler = new CreateTeamLinkHandler(
                this.mockMapper.Object,
                this.mockRepositoryWrapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOkResult_WhenTeamLinkIsCreated()
        {
            // Arrange
            var teamLink = GetTestTeamLink();
            var teamLinkDTO = GetTestTeamLinkDTO();

            this.SetupMapperToTeamLink(teamLink);
            this.SetupRepositoryCreate(teamLink);
            this.SetupRepositorySaveChangesSuccess();
            this.SetupMapperToTeamLinkDTO(teamLinkDTO);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                result.IsSuccess.Should().BeTrue();
                result.Value.Should().BeEquivalentTo(teamLinkDTO);
            }
        }

        [Fact]
        public async Task Handle_ShouldReturnFailResult_WhenMapperReturnsNull()
        {
            // Arrange
            var teamLinkDTO = GetTestTeamLinkDTO();

            this.SetupMapperToTeamLink(null!);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenMapperReturnsNull()
        {
            // Arrange
            var teamLinkDTO = GetTestTeamLinkDTO();

            this.SetupMapperToTeamLink(null!);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            await this.handler.Handle(query, CancellationToken.None);

            // Assert
            this.mockLogger.Verify(
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

            this.SetupMapperToTeamLink(teamLink);
            this.SetupRepositoryCreate(null!);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenRepositoryCreateReturnsNull()
        {
            // Arrange
            var teamLink = GetTestTeamLink();
            var teamLinkDTO = GetTestTeamLinkDTO();

            this.SetupMapperToTeamLink(teamLink);
            this.SetupRepositoryCreate(null!);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            await this.handler.Handle(query, CancellationToken.None);

            // Assert
            this.mockLogger.Verify(
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

            this.SetupMapperToTeamLink(teamLink);
            this.SetupRepositoryCreate(teamLink);
            this.SetupRepositorySaveChangesFailure();

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldLogError_WhenSaveChangesFails()
        {
            // Arrange
            var teamLink = GetTestTeamLink();
            var teamLinkDTO = GetTestTeamLinkDTO();

            this.SetupMapperToTeamLink(teamLink);
            this.SetupRepositoryCreate(teamLink);
            this.SetupRepositorySaveChangesFailure();

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            await this.handler.Handle(query, CancellationToken.None);

            // Assert
            this.mockLogger.Verify(
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

            this.SetupMapperToTeamLink(teamLink);
            this.SetupRepositoryCreate(teamLink);
            this.SetupRepositorySaveChangesSuccess();
            this.SetupMapperToTeamLinkDTO(null!);

            var query = new CreateTeamLinkQuery(teamLinkDTO);

            // Act
            await this.handler.Handle(query, CancellationToken.None);

            // Assert
            this.mockLogger.Verify(
                logger => logger.LogError(
                    It.Is<CreateTeamLinkQuery>(q => q == query),
                    It.Is<string>(msg => msg == ErrorMsgFailedToMap)),
                Times.Once);
        }

        private void SetupMapperToTeamLink(TeamMemberLink teamLink)
        {
            this.mockMapper
                .Setup(m => m.Map<TeamMemberLink>(It.IsAny<TeamMemberLinkDTO>()))
                .Returns(teamLink);
        }

        private void SetupMapperToTeamLinkDTO(TeamMemberLinkDTO teamLinkDTO)
        {
            this.mockMapper
                .Setup(m => m.Map<TeamMemberLinkDTO>(It.IsAny<TeamMemberLink>()))
                .Returns(teamLinkDTO);
        }

        private void SetupRepositoryCreate(TeamMemberLink teamLink)
        {
            this.mockTeamLinkRepository
                .Setup(r => r.Create(It.IsAny<TeamMemberLink>()))
                .Returns(teamLink);
        }

        private void SetupRepositorySaveChangesSuccess()
        {
            this.mockRepositoryWrapper
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);
        }

        private void SetupRepositorySaveChangesFailure()
        {
            this.mockRepositoryWrapper
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);
        }

        private static TeamMemberLink GetTestTeamLink() => new TeamMemberLink
        {
            Id = 1,
            LogoType = LogoType.Instagram,
            TargetUrl = "https://instagram.com/",
            TeamMemberId = 1,
        };

        private static TeamMemberLinkDTO GetTestTeamLinkDTO() => new TeamMemberLinkDTO
        {
            Id = 1,
            LogoType = LogoTypeDTO.Instagram,
            TargetUrl = "https://instagram.com/",
            TeamMemberId = 1,
        };
    }
}