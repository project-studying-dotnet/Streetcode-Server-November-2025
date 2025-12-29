namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Coordinate.Delete
{
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete;
 using global::Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
 using global::Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="DeleteCoordinateHandler"/>.
    /// Covers scenarios for successful coordinate deletion, coordinate not found,
    /// and failure during the save operation.
    /// </summary>
    public class DeleteCoordinateHandlerTests
    {
        private const string FailedToDelete = "Failed to delete a coordinate";
        private const string CoordinateNotFound = "Cannot find a coordinate with corresponding categoryId: {0}";

        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly DeleteCoordinateHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCoordinateHandlerTests"/> class,
        /// setting up the mock dependencies and the handler instance.
        /// </summary>
        public DeleteCoordinateHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.handler = new DeleteCoordinateHandler(
                this.repoWrapperMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns success when the coordinate is found and successfully deleted.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnOk_WhenCoordinateExists()
        {
            // Arrange
            var coordinateRepositoryMock = new Mock<IStreetcodeCoordinateRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeCoordinateRepository,
                coordinateRepositoryMock);

            var existingCoordinate = TestDataHelper.CreateMappedCoordinate();

            coordinateRepositoryMock.SetupGetFirstOrDefaultAsync(existingCoordinate);
            coordinateRepositoryMock.SetupDelete<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            this.repoWrapperMock.SetupSaveChangesAsync();

            var command = new DeleteCoordinateCommand(existingCoordinate.Id);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();

            // Verify
            coordinateRepositoryMock
                .VerifyGetFirstOrDefaultCalledOnce<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            coordinateRepositoryMock
                .VerifyDeleteCalledOnce<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
        }

        /// <summary>
        /// Tests that the handler returns failure when the specified coordinate ID does not exist in the database.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the "not found" error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenCoordinateDoesNotExist()
        {
            // Arrange
            var coordinateRepositoryMock = new Mock<IStreetcodeCoordinateRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeCoordinateRepository,
                coordinateRepositoryMock);

            var existingCoordinate = TestDataHelper.CreateMappedCoordinate();

            coordinateRepositoryMock.SetupGetFirstOrDefaultAsync((StreetcodeCoordinate?)null);

            var command = new DeleteCoordinateCommand(existingCoordinate.Id);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(string.Format(CoordinateNotFound, existingCoordinate.Id));

            // Verify
            coordinateRepositoryMock
                .VerifyGetFirstOrDefaultCalledOnce<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            coordinateRepositoryMock
                .VerifyDeleteCalledNever<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
        }

        /// <summary>
        /// Tests that the handler returns failure when the database operation (SaveChangesAsync) indicates no changes were saved.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the "failed to delete" error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenSaveChangesFails()
        {
            // Arrange
            var coordinateRepositoryMock = new Mock<IStreetcodeCoordinateRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeCoordinateRepository,
                coordinateRepositoryMock);

            var existingCoordinate = TestDataHelper.CreateMappedCoordinate();

            coordinateRepositoryMock.SetupGetFirstOrDefaultAsync(existingCoordinate);
            coordinateRepositoryMock.SetupDelete<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            this.repoWrapperMock.SetupNotSaveChangesAsync();

            var command = new DeleteCoordinateCommand(existingCoordinate.Id);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(FailedToDelete);

            // Verify
            coordinateRepositoryMock
                .VerifyGetFirstOrDefaultCalledOnce<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            coordinateRepositoryMock
                .VerifyDeleteCalledOnce<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            this.repoWrapperMock.VerifySaveChangesAsyncCalledOnce();
        }
    }
}