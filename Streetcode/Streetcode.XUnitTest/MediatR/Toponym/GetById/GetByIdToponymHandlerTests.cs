namespace Streetcode.XUnitTest.MediatR.Toponyms.GetById
{
    using System.Linq;
    using AutoMapper;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Toponyms.GetById;
    using Streetcode.DAL.Entities.Toponyms;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Toponyms;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Toponyms.Fixtures;
    using Streetcode.XUnitTest.MediatR.Toponyms.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetToponymByIdHandler"/>.
    /// Covers success and failure scenarios of retrieving a toponym by ID,
    /// including handling of non-existent toponyms and proper error logging.
    /// </summary>
    public class GetByIdToponymHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetToponymByIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByIdToponymHandlerTests"/> class.
        /// Initializes mocks and the <see cref="GetToponymByIdHandler"/> instance.
        /// </summary>
        public GetByIdToponymHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetToponymByIdHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns a successful result when a toponym exists with the given ID.
        /// Ensures that the toponym is correctly retrieved and mapped to a DTO.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenToponymExists_ShouldReturnSuccess()
        {
            // Arrange
            int toponymId = 1;
            var toponymRepositoryMock = new Mock<IToponymRepository>(MockBehavior.Strict);
            var toponym = ToponymTestData.CreateToponym(toponymId);
            var toponymDto = ToponymTestData.CreateToponymDto(toponymId);
            var query = new GetToponymByIdQuery(toponymId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(toponymRepositoryMock);
            toponymRepositoryMock.SetupGetFirstOrDefaultAsync<IToponymRepository, Toponym>(toponym);
            this.mapperMock.SetupMapper<Toponym, ToponymDto>(toponym, toponymDto);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Value);
            Assert.Equal(toponymId, result.Value.Id);
            Assert.Equal(toponym.StreetName, result.Value.StreetName);
            Assert.Equal(toponym.Oblast, result.Value.Oblast);

            toponymRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IToponymRepository, Toponym>();
            this.mapperMock.VerifyMapCalledOnce<ToponymDto>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        /// <summary>
        /// Tests that the handler returns a failed result when no toponym exists with the given ID.
        /// Ensures proper error logging and that mapper is never called.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenToponymDoesNotExist_ShouldReturnFailure()
        {
            // Arrange
            int toponymId = 999;
            var toponymRepositoryMock = new Mock<IToponymRepository>(MockBehavior.Strict);
            var query = new GetToponymByIdQuery(toponymId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(toponymRepositoryMock);
            toponymRepositoryMock.SetupGetFirstOrDefaultAsync<IToponymRepository, Toponym>(null);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Contains(string.Format(ErrorMessages.ToponymNotFoundById, toponymId), result.Errors.First().Message);

            toponymRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IToponymRepository, Toponym>();
            this.mapperMock.VerifyMapCalledNever<ToponymDto>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

    }
}
