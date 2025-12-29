namespace Streetcode.XUnitTest.MediatR.Transactions.TransactionLink.GetByStreetcodeId
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Transactions.TransactionLink.GetByStreetcodeId;
 using global::Streetcode.DAL.Entities.Streetcode;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.RelatedTerm.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Transactions.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Transactions.TransactionLink.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetTransactLinkByStreetcodeIdHandler"/>.
    /// </summary>
    public class GetTransactLinkByStreetcodeIdHandlerTests
    {
        private const int StreetcodeId = 1;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTransactLinkByStreetcodeIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactLinkByStreetcodeIdHandlerTests"/> class.
        /// </summary>
        public GetTransactLinkByStreetcodeIdHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetTransactLinkByStreetcodeIdHandler(
                this.repositoryMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that Handle returns success with value when transaction link is found by streetcode id.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTransactLinkFoundByStreetcodeId()
        {
            // Arrange.
            var transactionLink = TransactionLinkTestData.CreateTransactionLink(1, StreetcodeId);
            var transactLinkDto = TransactionLinkTestData.CreateTransactLinkDto(1, StreetcodeId);

            this.repositoryMock.SetupGetFirstOrDefaultAsync(transactionLink);
            this.mapperMock.SetupMapper(transactionLink, transactLinkDto);

            var query = new GetTransactLinkByStreetcodeIdQuery(StreetcodeId);

            // Act.
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(transactLinkDto);
            result.Value!.StreetcodeId.Should().Be(StreetcodeId);

            // Verify.
            TransactionLinkVerifications.VerifyGetFirstOrDefaultAsyncCalledOnce(this.repositoryMock);
            this.mapperMock.VerifyMapNullableCalledOnce();
            this.loggerMock.VerifyLogErrorNeverCalled();
        }

        /// <summary>
        /// Tests that Handle returns success with null when transaction link not found but streetcode exists.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccessWithNull_WhenTransactLinkNotFoundButStreetcodeExists()
        {
            // Arrange.
            var streetcode = new StreetcodeContent { Id = StreetcodeId };

            this.repositoryMock.SetupGetFirstOrDefaultAsync((DAL.Entities.Transactions.TransactionLink?)null);
            this.repositoryMock.SetupGetStreetcodeAsync(streetcode);
            this.mapperMock.SetupMapperNull();

            var query = new GetTransactLinkByStreetcodeIdQuery(StreetcodeId);

            // Act.
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeNull();

            // Verify.
            TransactionLinkVerifications.VerifyGetFirstOrDefaultAsyncCalledOnce(this.repositoryMock);
            this.repositoryMock.VerifyGetStreetcodeAsyncCalled(Times.Once());
            this.mapperMock.VerifyMapNullableCalledOnce();
            this.loggerMock.VerifyLogErrorNeverCalled();
        }

        /// <summary>
        /// Tests that Handle returns failure when both transaction link and streetcode are not found.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenStreetcodeNotFound()
        {
            // Arrange.
            string expectedError = string.Format(ErrorMessages.TransactionLinkNotFoundByStreetcodeId, StreetcodeId);

            this.repositoryMock.SetupGetFirstOrDefaultAsync((DAL.Entities.Transactions.TransactionLink?)null);
            this.repositoryMock.SetupGetStreetcodeAsync(null);
            CommonRepositorySetups.SetupLogger(this.loggerMock);

            var query = new GetTransactLinkByStreetcodeIdQuery(StreetcodeId);

            // Act.
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(expectedError);

            // Verify.
            TransactionLinkVerifications.VerifyGetFirstOrDefaultAsyncCalledOnce(this.repositoryMock);
            this.repositoryMock.VerifyGetStreetcodeAsyncCalled(Times.Once());
            CommonRepositoryVerifications.VerifyLogErrorCalledOnce(this.loggerMock);
        }

        /// <summary>
        /// Tests that Handle works correctly with different streetcode IDs.
        /// </summary>
        /// <param name="streetcodeId">The streetcode ID to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(99)]
        public async Task Handle_ShouldReturnCorrectTransactLink_ForDifferentStreetcodeIds(int streetcodeId)
        {
            // Arrange.
            var transactionLink = TransactionLinkTestData.CreateTransactionLink(1, streetcodeId);
            var transactLinkDto = TransactionLinkTestData.CreateTransactLinkDto(1, streetcodeId);

            this.repositoryMock.SetupGetFirstOrDefaultAsync(transactionLink);
            this.mapperMock.SetupMapper(transactionLink, transactLinkDto);

            var query = new GetTransactLinkByStreetcodeIdQuery(streetcodeId);

            // Act.
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.StreetcodeId.Should().Be(streetcodeId);

            // Verify.
            TransactionLinkVerifications.VerifyGetFirstOrDefaultAsyncCalledOnce(this.repositoryMock);
            this.mapperMock.VerifyMapNullableCalledOnce();
        }
    }
}