namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Merge
{
    using System.Transactions;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.BLL.MediatR.Toponyms.Merge;
    using Streetcode.DAL.Entities.Toponyms;
    using Streetcode.DAL.Repositories.Interfaces.Toponyms;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Fixtures;
    using Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Helpers;
    using Streetcode.XUnitTest.MediatR.Toponyms.Fixtures;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="MergeToponymsHandler"/>.
    /// </summary>
    public class MergeToponymsHandlerTests : StreetcodeToponymHandlerTestsBase
    {
        private readonly MergeToponymsHandler handler;
        private readonly Mock<IStreetcodeToponymRepository> streetcodeToponymRepositoryMock;
        private readonly Mock<IToponymRepository> toponymRepositoryMock;

        /// <summary>
        /// Initializes a new instance of the <see cref="MergeToponymsHandlerTests"/> class.
        /// </summary>
        public MergeToponymsHandlerTests()
        {
            this.streetcodeToponymRepositoryMock = new Mock<IStreetcodeToponymRepository>();
            this.toponymRepositoryMock = new Mock<IToponymRepository>();
            this.handler = new MergeToponymsHandler(
                this.MockRepository.Object,
                this.MockMapper.Object,
                this.MockLogger.Object);
        }

        [Fact]
        public async Task Handle_WhenTargetToponymDoesNotExist_ShouldReturnFailure()
        {
            // Arrange.
            int targetToponymId = 999;
            string expectedError = $"Target toponym with Id={targetToponymId} not found.";
            var mergeDto = StreetcodeToponymTestData.CreateMergeToponymsDto(targetToponymId);
            var command = new MergeToponymsCommand(mergeDto);

            this.MockRepository.SetupRepositoryWrapper(
                this.streetcodeToponymRepositoryMock,
                this.toponymRepositoryMock);
            this.toponymRepositoryMock.SetupGetFirstOrDefaultAsync<IToponymRepository, Toponym>(null);
            this.SetupLogger();

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(expectedError);

            // Verify.
            this.MockLogger.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenTargetToponymExists_ShouldMergeSuccessfully()
        {
            // Arrange.
            int targetToponymId = 1;
            var targetToponym = ToponymTestData.CreateToponym(targetToponymId);
            var targetToponymDto = ToponymTestData.CreateToponymDto(targetToponymId);
            var sourceRelationships = StreetcodeToponymTestData.CreateStreetcodeToponyms(2, 1);
            var mergeDto = StreetcodeToponymTestData.CreateMergeToponymsDto(targetToponymId, new List<int> { 2 });
            var command = new MergeToponymsCommand(mergeDto);

            this.MockRepository.SetupRepositoryWrapper(
                this.streetcodeToponymRepositoryMock,
                this.toponymRepositoryMock);

            this.toponymRepositoryMock.SetupGetFirstOrDefaultAsync(targetToponym);

            this.MockRepository
                .Setup(repo => repo.BeginTransaction())
                .Returns(new TransactionScope());

            this.streetcodeToponymRepositoryMock.SetupGetAllAsync(sourceRelationships);

            this.streetcodeToponymRepositoryMock
                .SetupSequence(repo => repo.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<
                        System.Func<StreetcodeToponym, bool>>>(),
                    It.IsAny<System.Func<IQueryable<StreetcodeToponym>,
                        Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<StreetcodeToponym, object>>>()))
                .ReturnsAsync((StreetcodeToponym?)null)
                .ReturnsAsync((StreetcodeToponym?)null);

            this.streetcodeToponymRepositoryMock.SetupCreateAsync(StreetcodeToponymTestData.CreateStreetcodeToponym());
            this.streetcodeToponymRepositoryMock
                .SetupDelete<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.toponymRepositoryMock.SetupDelete<IToponymRepository, Toponym>();

            this.SetupSaveChangesAsyncSuccess();
            this.MockMapper
                .Setup(m => m.Map<ToponymDto>(It.IsAny<Toponym>()))
                .Returns(targetToponymDto);

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(targetToponymDto);

            // Verify.
            this.MockRepository.VerifySaveChangesAsyncCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenExceptionOccursDuringMerge_ShouldReturnFailure()
        {
            // Arrange.
            int targetToponymId = 1;
            var targetToponym = ToponymTestData.CreateToponym(targetToponymId);
            var mergeDto = StreetcodeToponymTestData.CreateMergeToponymsDto(targetToponymId, new List<int> { 2 });
            var command = new MergeToponymsCommand(mergeDto);

            this.MockRepository.SetupRepositoryWrapper(
                this.streetcodeToponymRepositoryMock,
                this.toponymRepositoryMock);

            this.toponymRepositoryMock.SetupGetFirstOrDefaultAsync(targetToponym);

            this.MockRepository
                .Setup(repo => repo.BeginTransaction())
                .Returns(new TransactionScope());

            this.streetcodeToponymRepositoryMock
                .Setup(repo => repo.GetAllAsync(
                    It.IsAny<System.Linq.Expressions.Expression<
                        System.Func<DAL.Entities.Toponyms.StreetcodeToponym, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<DAL.Entities.Toponyms.StreetcodeToponym>,
                        Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<DAL.Entities.Toponyms.StreetcodeToponym, object>>>()))
                .ThrowsAsync(new System.Exception("Database error"));

            this.SetupLogger();

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Contain("Failed to merge toponyms");
            result.Errors.First().Message.Should().Contain("Database error");

            // Verify.
            this.MockLogger.VerifyLogErrorCalledOnce();
        }

        [Fact]
        public async Task Handle_WhenSaveChangesFailsDuringMerge_ShouldReturnFailure()
        {
            // Arrange.
            int targetToponymId = 1;
            var targetToponym = ToponymTestData.CreateToponym(targetToponymId);
            var sourceRelationships = StreetcodeToponymTestData.CreateStreetcodeToponyms(2, 1);
            var mergeDto = StreetcodeToponymTestData.CreateMergeToponymsDto(targetToponymId, new List<int> { 2 });
            var command = new MergeToponymsCommand(mergeDto);

            this.MockRepository.SetupRepositoryWrapper(
                this.streetcodeToponymRepositoryMock,
                this.toponymRepositoryMock);

            this.toponymRepositoryMock.SetupGetFirstOrDefaultAsync(targetToponym);

            this.MockRepository
                .Setup(repo => repo.BeginTransaction())
                .Returns(new TransactionScope());

            this.streetcodeToponymRepositoryMock.SetupGetAllAsync(sourceRelationships);

            this.streetcodeToponymRepositoryMock
                .Setup(repo => repo.GetFirstOrDefaultAsync(
                    It.IsAny<System.Linq.Expressions.Expression<
                        System.Func<DAL.Entities.Toponyms.StreetcodeToponym, bool>>>(),
                    It.IsAny<System.Func<System.Linq.IQueryable<DAL.Entities.Toponyms.StreetcodeToponym>,
                        Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<DAL.Entities.Toponyms.StreetcodeToponym, object>>>()))
                .ReturnsAsync((DAL.Entities.Toponyms.StreetcodeToponym?)null);

            this.streetcodeToponymRepositoryMock.SetupCreateAsync(StreetcodeToponymTestData.CreateStreetcodeToponym());
            this.streetcodeToponymRepositoryMock
                .SetupDelete<IStreetcodeToponymRepository, DAL.Entities.Toponyms.StreetcodeToponym>();
            this.toponymRepositoryMock.SetupDelete<IToponymRepository, Toponym>();

            this.MockRepository
                .Setup(repo => repo.SaveChangesAsync())
                .ThrowsAsync(new System.Exception("SaveChanges failed"));

            this.SetupLogger();

            // Act.
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Contain("Failed to merge toponyms");
            result.Errors.First().Message.Should().Contain("SaveChanges failed");

            // Verify.
            this.MockLogger.VerifyLogErrorCalledOnce();
        }
    }
}