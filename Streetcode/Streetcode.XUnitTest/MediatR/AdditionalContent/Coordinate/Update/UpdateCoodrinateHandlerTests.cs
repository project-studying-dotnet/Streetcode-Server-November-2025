namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Coordinate.Update
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update;
    using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="UpdateCoordinateHandler"/>.
    /// Covers scenarios for successful coordinate update, failure due to null mapping,
    /// and failure due to unsuccessful save operation (SaveChanges returns zero).
    /// </summary>
    public class UpdateCoodrinateHandlerTests
    {
        private const int StreetcodeId = 1;
        private const string NullStreetcodeCoordinate = "Cannot convert null to streetcodeCoordinate";
        private const string FailedToUpdateStreetcodeCoordinate = "Failed to update a streetcodeCoordinate";

        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly UpdateCoordinateHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCoodrinateHandlerTests"/> class,
        /// setting up the mock dependencies and the handler instance.
        /// </summary>
        public UpdateCoodrinateHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.handler = new UpdateCoordinateHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns failure when the mapper returns null (i.e., mapping DTO to Entity fails).
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the null mapping error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenStreetcodeCoordinateIsNull()
        {
            // Arrange
            var dto = TestDataHelper.CreateStreetcodeCoordinateDTO();
            var command = new UpdateCoordinateCommand(dto);

            this.mapperMock.SetupMapper(command.StreetcodeCoordinate, (StreetcodeCoordinate?)null);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(NullStreetcodeCoordinate);

            // Verify
            this.mapperMock.VerifyMapCalledOnce<StreetcodeCoordinate>();
        }

        /// <summary>
        /// Tests that the handler returns success when the coordinate is successfully mapped, updated in the repository, and changes are saved.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenUpdateIsSuccessful()
        {
            // Arrange
            var dto = TestDataHelper.CreateStreetcodeCoordinateDTO();
            var mappedCoordinate = TestDataHelper.CreateMappedCoordinate();

            var streetcodeCoordinateRepoMock = new Mock<IStreetcodeCoordinateRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeCoordinateRepository,
                streetcodeCoordinateRepoMock);

            this.mapperMock.SetupMapper(dto, mappedCoordinate);
            streetcodeCoordinateRepoMock.SetupUpdate(mappedCoordinate);
            this.repoWrapperMock.SetupSaveChangesAsync();

            var command = new UpdateCoordinateCommand(dto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();

            // Verify
            this.mapperMock.VerifyMapCalledOnce<StreetcodeCoordinate>();
            streetcodeCoordinateRepoMock.VerifyUpdateCalledOnce<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            this.repoWrapperMock.VerifySaveChangesAsyncCalledOnce();
        }

        /// <summary>
        /// Tests that the handler returns failure when the database operation (SaveChangesAsync) returns 0 (no changes saved).
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the "failed to update" error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenSaveChangesReturnsZero()
        {
            // Arrange
            var dto = TestDataHelper.CreateStreetcodeCoordinateDTO();
            var mappedCoordinate = TestDataHelper.CreateMappedCoordinate();

            var streetcodeCoordinateRepoMock = new Mock<IStreetcodeCoordinateRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeCoordinateRepository,
                streetcodeCoordinateRepoMock);

            this.mapperMock.SetupMapper(dto, mappedCoordinate);
            streetcodeCoordinateRepoMock.SetupUpdate(mappedCoordinate);
            this.repoWrapperMock.SetupNotSaveChangesAsync();

            var command = new UpdateCoordinateCommand(dto);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(FailedToUpdateStreetcodeCoordinate);

            // Verify
            this.mapperMock.VerifyMapCalledOnce<StreetcodeCoordinate>();
            streetcodeCoordinateRepoMock.VerifyUpdateCalledOnce<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            this.repoWrapperMock.VerifySaveChangesAsyncCalledOnce();
        }
    }
}