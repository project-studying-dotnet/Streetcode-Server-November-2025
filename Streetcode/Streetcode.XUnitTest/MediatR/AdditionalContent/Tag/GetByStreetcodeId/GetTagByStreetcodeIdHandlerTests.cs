namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.GetByStreetcodeId
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.AdditionalContent.Tag;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    public class GetTagByStreetcodeIdHandlerTests
    {
        private const int StreetcodeId = 1;
        private const string ErrorMsg = "Cannot find any tag by the streetcode id: {0}";

        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTagByStreetcodeIdHandler handler;

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