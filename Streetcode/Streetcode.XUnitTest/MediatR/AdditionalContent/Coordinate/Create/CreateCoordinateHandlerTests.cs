namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Coordinate.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
    using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="CreateCoordinateHandler"/>.
    /// Covers scenarios for successful coordinate creation, failure due to null mapping,
    /// and failure due to unsuccessful save operation (SaveChanges returns zero).
    /// </summary>
    public class CreateCoordinateHandlerTests
    {
        private const string MappingNullError =
            "Cannot convert null to streetcodeCoordinate";

        private const string SaveFailedError =
            "Failed to create a streetcodeCoordinate";

        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly CreateCoordinateHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCoordinateHandlerTests"/> class,
        /// setting up the mock dependencies and the handler instance.
        /// </summary>
        public CreateCoordinateHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.handler = new CreateCoordinateHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns success when all steps (mapping, creation, saving) are successful.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnOk_WhenCreationSucceeds()
        {
            // Arrange
            var coordinateRepositoryMock = new Mock<IStreetcodeCoordinateRepository>(MockBehavior.Strict);

            var fakeDto = TestDataHelper.CreateStreetcodeCoordinateDTO();
            var command = new CreateCoordinateCommand(fakeDto);
            var mappedEntity = TestDataHelper.CreateMappedCoordinate();

            this.mapperMock.SetupMapper(fakeDto, mappedEntity);
            coordinateRepositoryMock.SetupCreateAsync(mappedEntity);

            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeCoordinateRepository,
                coordinateRepositoryMock);

            this.repoWrapperMock.SetupSaveChangesAsync();

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeTrue();

            // Verify
            this.mapperMock.VerifyMapCalledOnce<StreetcodeCoordinate>();
        }

        /// <summary>
        /// Tests that the handler returns failure when AutoMapper returns null during the DTO to Entity mapping process.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the appropriate error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenMapperReturnsNull()
        {
            // Arrange
            var coordinateRepositoryMock = new Mock<IStreetcodeCoordinateRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeCoordinateRepository,
                coordinateRepositoryMock);

            var fakeDto = TestDataHelper.CreateStreetcodeCoordinateDTO();
            var command = new CreateCoordinateCommand(fakeDto);
            this.mapperMock.SetupMapper(fakeDto, (StreetcodeCoordinate?)null);

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.First().Message.Should().Be(MappingNullError);

            // Verify
            coordinateRepositoryMock
                .VerifyCreateAsyncCalledNever<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
            this.repoWrapperMock.VerifySaveChangesAsyncCalledNever();
        }

        /// <summary>
        /// Tests that the handler returns failure when the repository's SaveChangesAsync returns 0 (no changes saved).
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the save failure error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenSaveChangesReturnsZero()
        {
            // Arrange
            var coordinateRepositoryMock = new Mock<IStreetcodeCoordinateRepository>(MockBehavior.Strict);
            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeCoordinateRepository,
                coordinateRepositoryMock);

            var fakeDto = TestDataHelper.CreateStreetcodeCoordinateDTO();
            var mappedEntity = TestDataHelper.CreateMappedCoordinate();
            var command = new CreateCoordinateCommand(fakeDto);

            this.mapperMock.SetupMapper(fakeDto, mappedEntity);
            coordinateRepositoryMock.SetupCreateAsync(mappedEntity);
            this.repoWrapperMock.SetupNotSaveChangesAsync();

            // Act
            var result = await this.handler.Handle(command, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.First().Message.Should().Be(SaveFailedError);

            // Verify
            this.mapperMock.VerifyMapCalledOnce<StreetcodeCoordinate>();
            coordinateRepositoryMock
                .VerifyCreateAsyncCalledOnce<IStreetcodeCoordinateRepository, StreetcodeCoordinate>();
        }
    }
}