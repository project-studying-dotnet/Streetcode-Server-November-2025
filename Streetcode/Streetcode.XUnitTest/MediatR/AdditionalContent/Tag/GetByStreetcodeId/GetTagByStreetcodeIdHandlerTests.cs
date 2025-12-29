namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.GetByStreetcodeId
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL.DTO.AdditionalContent.Tag;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;
 using global::Streetcode.DAL.Entities.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetTagByStreetcodeIdHandler"/>.
    /// Covers scenarios for retrieving tags associated with a specific streetcode,
    /// including successful retrieval with ordering, handling null repository responses,
    /// and cases where no tags are found.
    /// </summary>
    public class GetTagByStreetcodeIdHandlerTests
    {
        private const int StreetcodeId = 1;
        private const string ErrorMsg = "Cannot find any tag by the streetcode id: {0}";

        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTagByStreetcodeIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTagByStreetcodeIdHandlerTests"/> class,
        /// setting up the mock dependencies and the handler instance.
        /// </summary>
        public GetTagByStreetcodeIdHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();

            this.handler = new GetTagByStreetcodeIdHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns a successful result with an ordered list of tag DTOs
        /// when tags exist for the given Streetcode ID.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> with a strictly ordered list of tag DTOs.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTagsExist()
        {
            // Arrange
            var entities = TestDataHelper.CreateStreetcodeTagIndexList();
            var dtos = TestDataHelper.CreateStreetcodeTagDtoList();

            var streetcodeTagIndexRepo = new Mock<IStreetcodeTagIndexRepository>(MockBehavior.Strict);
            streetcodeTagIndexRepo.SetupGetAllAsync(entities);

            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeTagIndexRepository,
                streetcodeTagIndexRepo);
            this.mapperMock
                .SetupMapper<IEnumerable<StreetcodeTagIndex>, IEnumerable<StreetcodeTagDto>>(entities.OrderBy(e => e.Index), dtos);

            var query = new GetTagByStreetcodeIdQuery(StreetcodeId);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dtos, options => options.WithStrictOrdering());

            // Verify
            streetcodeTagIndexRepo
                .VerifyGetAllAsyncCalledOnce<IStreetcodeTagIndexRepository, StreetcodeTagIndex>();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<StreetcodeTagDto>>();
        }

        /// <summary>
        /// Tests that the handler returns a failure result and logs an error when the repository
        /// returns null for tag indices.
        /// </summary>
        /// <returns>A failed <see cref="Task"/> with the "Tags not found" error message.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenRepositoryReturnsNull()
        {
            // Arrange
            var streetcodeTagIndexRepo = new Mock<IStreetcodeTagIndexRepository>(MockBehavior.Strict);
            streetcodeTagIndexRepo.SetupGetAllAsync((List<StreetcodeTagIndex>?)null);
            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeTagIndexRepository,
                streetcodeTagIndexRepo);
            this.loggerMock.SetupLogger();

            var query = new GetTagByStreetcodeIdQuery(StreetcodeId);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Contain(string.Format(ErrorMsg, StreetcodeId));

            // Verify
            this.loggerMock.VerifyLogErrorCalledOnce();
            streetcodeTagIndexRepo
                .VerifyGetAllAsyncCalledOnce<IStreetcodeTagIndexRepository, StreetcodeTagIndex>();
        }

        /// <summary>
        /// Tests that the handler returns a successful result with an empty list
        /// when the repository returns an empty collection for the specified Streetcode ID.
        /// </summary>
        /// <returns>A successful <see cref="Task"/> with an empty list of tag DTOs.</returns>
        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoTagsFound()
        {
            // Arrange
            var emptyEntities = new List<StreetcodeTagIndex>();
            var emptyDtos = new List<StreetcodeTagDto>();

            var streetcodeTagIndexRepo = new Mock<IStreetcodeTagIndexRepository>(MockBehavior.Strict);
            streetcodeTagIndexRepo.SetupGetAllAsync(emptyEntities);

            this.repoWrapperMock.SetupRepository(
                r => r.StreetcodeTagIndexRepository,
                streetcodeTagIndexRepo);

            this.mapperMock
                .SetupMapper<IEnumerable<StreetcodeTagIndex>, IEnumerable<StreetcodeTagDto>>(emptyEntities, emptyDtos);

            var query = new GetTagByStreetcodeIdQuery(StreetcodeId);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();

            // Verify
            streetcodeTagIndexRepo
                .VerifyGetAllAsyncCalledOnce<IStreetcodeTagIndexRepository, StreetcodeTagIndex>();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<StreetcodeTagDto>>();
        }
    }
}