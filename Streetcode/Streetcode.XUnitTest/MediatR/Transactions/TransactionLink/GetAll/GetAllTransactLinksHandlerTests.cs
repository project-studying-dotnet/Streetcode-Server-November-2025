namespace Streetcode.XUnitTest.MediatR.Transactions.TransactionLink.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Transactions.TransactionLink.GetAll;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.XUnitTest.Helpers;
 using global::Streetcode.XUnitTest.MediatR.Transactions.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Transactions.TransactionLink.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetAllTransactLinksHandler"/>.
    /// </summary>
    public class GetAllTransactLinksHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetAllTransactLinksHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllTransactLinksHandlerTests"/> class.
        /// </summary>
        public GetAllTransactLinksHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetAllTransactLinksHandler(
                this.repositoryMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that Handle returns success when transaction links are found.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTransactLinksExist()
        {
            // Arrange.
            var transactionLinks = TransactionLinkTestData.CreateTransactionLinks(3);
            var transactLinkDtos = TransactionLinkTestData.CreateTransactLinkDtos(3);

            this.repositoryMock.SetupGetAllAsync(transactionLinks);
            this.mapperMock.SetupMapper(transactionLinks, transactLinkDtos);

            var query = new GetAllTransactLinksQuery();

            // Act.
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(transactLinkDtos);
            result.Value.Should().HaveCount(3);

            // Verify.
            this.repositoryMock.VerifyGetAllAsyncCalledOnce();
            this.mapperMock.VerifyMapCollectionCalledOnce();
            this.loggerMock.VerifyLogErrorNeverCalled();
        }

        /// <summary>
        /// Tests that Handle returns failure when no transaction links are found.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenTransactLinksNotFound()
        {
            // Arrange.
            string expectedError = ErrorMessages.TransactionLinkNotFound;

            this.repositoryMock.SetupGetAllAsync(null);
            CommonRepositorySetups.SetupLogger(this.loggerMock);

            var query = new GetAllTransactLinksQuery();

            // Act.
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(expectedError);

            // Verify.
            this.repositoryMock.VerifyGetAllAsyncCalledOnce();
            TransactionLinkVerifications.VerifyLogErrorCalledOnce(this.loggerMock);
        }

        /// <summary>
        /// Tests that Handle returns failure when transaction links collection is empty.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTransactLinksEmpty()
        {
            // Arrange.
            this.repositoryMock.SetupGetAllAsync(Enumerable.Empty<DAL.Entities.Transactions.TransactionLink>());

            var query = new GetAllTransactLinksQuery();

            // Act.
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();

            // Verify.
            this.repositoryMock.VerifyGetAllAsyncCalledOnce();
        }
    }
}