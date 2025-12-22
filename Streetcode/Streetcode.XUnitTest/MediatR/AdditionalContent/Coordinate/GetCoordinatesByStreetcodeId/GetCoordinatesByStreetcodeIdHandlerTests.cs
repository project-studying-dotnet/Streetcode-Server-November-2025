namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Coordinate.GetCoordinatesByStreetcodeId
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.AdditionalContent.Coordinates.Types;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.GetByStreetcodeId;
    using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetCoordinatesByStreetcodeIdHandler"/>.
    /// Covers scenarios for retrieving coordinates associated with a streetcode,
    /// including cases where the streetcode does not exist, no coordinates are found,
    /// and successful retrieval with varying counts.
    /// </summary>
    public class GetCoordinatesByStreetcodeIdHandlerTests
    {
        private const int StreetcodeId = 1;
        private const string StreetcodeNotFound = "Cannot find a coordinates by a streetcode id: {0}, because such streetcode doesn`t exist";
        private const string CoordinatesNotFound = "Cannot find a coordinates by a streetcode id: {0}";

        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetCoordinatesByStreetcodeIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCoordinatesByStreetcodeIdHandlerTests"/> class,
        /// setting up the mock dependencies and the handler instance.
        /// </summary>
        public GetCoordinatesByStreetcodeIdHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetCoordinatesByStreetcodeIdHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns failure when the specified Streetcode ID does not exist in the database.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the "Streetcode not found" error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenStreetcodeDoesNotExist()
        {
            // Arrange
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);

            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeRepository,
                streetcodeRepositoryMock);

            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync((StreetcodeContent?)null);

            var query = new GetCoordinatesByStreetcodeIdQuery(StreetcodeId);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should()
                .Be(string.Format(StreetcodeNotFound, StreetcodeId));

            // Verify
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
        }

        /// <summary>
        /// Tests that the handler returns failure and logs an error when the streetcode exists,
        /// but the repository returns null for coordinates (indicating no coordinates found).
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the "Coordinates not found" error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailAndLogError_WhenCoordinatesAreNull()
        {
            // Arrange
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var coordinateRepositoryMock = new Mock<IStreetcodeCoordinateRepository>(MockBehavior.Strict);

            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeRepository,
                streetcodeRepositoryMock);

            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeCoordinateRepository,
                coordinateRepositoryMock);

            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(new StreetcodeContent { Id = StreetcodeId });
            coordinateRepositoryMock.SetupGetAllAsync((List<StreetcodeCoordinate>?)null);

            var query = new GetCoordinatesByStreetcodeIdQuery(StreetcodeId);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should()
                .Be(string.Format(CoordinatesNotFound, StreetcodeId));

            // Verify
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            coordinateRepositoryMock.VerifyGetAllAsyncCalledOnce<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Tests that the handler successfully retrieves coordinates for a given Streetcode ID
        /// and maps them to DTOs, covering cases with different number of coordinates.
        /// </summary>
        /// <param name="coordinateCount">The number of coordinates to be created and tested.</param>
        /// <returns>A successful <see cref="Task"/> with the list of coordinates DTOs.</returns>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        public async Task Handle_ShouldReturnSuccess_WhenCoordinatesExist_WithDifferentCounts(int coordinateCount)
        {
            // Arrange
            var streetcodeRepositoryMock = new Mock<IStreetcodeRepository>(MockBehavior.Strict);
            var coordinateRepositoryMock = new Mock<IStreetcodeCoordinateRepository>(MockBehavior.Strict);

            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeRepository,
                streetcodeRepositoryMock);

            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeCoordinateRepository,
                coordinateRepositoryMock);

            var coordinates = TestDataHelper.CreateStreetcodeCoordinateList(coordinateCount);
            var expectedDto = TestDataHelper.CreateStreetcodeCoordinateDtoList(coordinateCount);

            streetcodeRepositoryMock.SetupGetFirstOrDefaultAsync(new StreetcodeContent { Id = StreetcodeId });
            coordinateRepositoryMock.SetupGetAllAsync(coordinates);
            this.mapperMock.SetupMapper<IEnumerable<StreetcodeCoordinate>, IEnumerable<StreetcodeCoordinateDto>>(
                coordinates,
                expectedDto);

            var query = new GetCoordinatesByStreetcodeIdQuery(StreetcodeId);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(expectedDto);

            // Verify
            streetcodeRepositoryMock.VerifyGetFirstOrDefaultCalledOnce<IStreetcodeRepository, StreetcodeContent>();
            coordinateRepositoryMock.VerifyGetAllAsyncCalledOnce<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<StreetcodeCoordinateDto>>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}