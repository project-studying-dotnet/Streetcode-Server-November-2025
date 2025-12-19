namespace Streetcode.XUnitTest.MediatR.AdditionalContent.Tag.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.AdditionalContent;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.Create;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.AdditionalContent.Helpers;
    using Xunit;

    public class CreateTagHandlerTests
    {
        private const string ExeptionText = "DB error";
        private readonly Mock<IRepositoryWrapper> repoWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly CreateTagHandler handler;

        public CreateTagHandlerTests()
        {
            this.repoWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new CreateTagHandler(
                this.repoWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTagCreated()
        {
            // Arange
            var tagEntity = TestDataHelper.CreateTag();
            var tagDto = TestDataHelper.CreateTagDto();
            var createTagDto = TestDataHelper.CreateCreateTagDto();

            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);
            tagRepoMock.SetupCreateAsync(tagEntity);

            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);
            this.repoWrapperMock.SetupSaveChangesAsync();
            this.mapperMock.SetupMapper(tagEntity, tagDto);

            var query = new CreateTagQuery(createTagDto);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(tagDto);

            // Verify
            this.mapperMock.VerifyMapCalledOnce<TagDto>();
            tagRepoMock.VerifyCreateAsyncCalledOnce<ITagRepository, Tag>();
            this.repoWrapperMock.VerifySaveChangesAsyncCalledOnce();
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenSaveChangesThrowsExeption()
        {
            // Arange
            var tagEntity = TestDataHelper.CreateTag();
            var createTagDto = TestDataHelper.CreateCreateTagDto();

            var tagRepoMock = new Mock<ITagRepository>(MockBehavior.Strict);
            tagRepoMock.SetupCreateAsync(tagEntity);

            this.repoWrapperMock.SetupRepository(
                r => r.TagRepository,
                tagRepoMock);
            this.repoWrapperMock.SetupNotSaveChangesAsync();
            this.loggerMock.SetupLogger();
            this.repoWrapperMock.Setup(r => r.SaveChangesAsync()).ThrowsAsync(new Exception(ExeptionText));

            var query = new CreateTagQuery(createTagDto);

            // Act
            var result = await this.handler.Handle(query, default);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Contain(ExeptionText);

            // Verify
            tagRepoMock.VerifyCreateAsyncCalledOnce<ITagRepository, Tag>();
            this.repoWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledNever<TagDto>();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}
