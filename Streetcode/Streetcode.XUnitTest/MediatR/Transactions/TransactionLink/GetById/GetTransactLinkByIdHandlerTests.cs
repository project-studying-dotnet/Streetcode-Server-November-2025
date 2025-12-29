namespace Streetcode.XUnitTest.MediatR.Transactions.TransactionLink.GetById
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Transactions.TransactionLink.GetById;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.XUnitTest.MediatR.Transactions.Fixtures;
 using global::Streetcode.XUnitTest.MediatR.Transactions.TransactionLink.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetTransactLinkByIdHandler"/>.
    /// </summary>
    public class GetTransactLinkByIdHandlerTests
    {
        private const int TransactionLinkId = 1;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetTransactLinkByIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactLinkByIdHandlerTests"/> class.
        /// </summary>
        public GetTransactLinkByIdHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetTransactLinkByIdHandler(
                this.repositoryMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that Handle returns success when transaction link is found by id.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenTransactLinkFoundById()
        {
            // Arrange.
            var transactionLink = TransactionLinkTestData.CreateTransactionLink(TransactionLinkId);
            var transactLinkDto = TransactionLinkTestData.CreateTransactLinkDto(TransactionLinkId);

            this.repositoryMock.SetupGetFirstOrDefaultAsync(transactionLink);
            this.mapperMock.SetupMapper(transactionLink, transactLinkDto);

            var query = new GetTransactLinkByIdQuery(TransactionLinkId);

            // Act.
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(transactLinkDto);
            result.Value.Id.Should().Be(TransactionLinkId);

            // Verify.
            this.repositoryMock.VerifyGetFirstOrDefaultAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce();
            this.loggerMock.VerifyLogErrorNeverCalled();
        }

        /// <summary>
        /// Tests that Handle returns failure when transaction link is not found by id.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenTransactLinkNotFoundById()
        {
            // Arrange.
            string expectedError = string.Format(ErrorMessages.TransactionLinkNotFoundById, TransactionLinkId);

            this.repositoryMock.SetupGetFirstOrDefaultAsync(null);
            this.loggerMock.SetupLogger();

            var query = new GetTransactLinkByIdQuery(TransactionLinkId);

            // Act.
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle();
            result.Errors.First().Message.Should().Be(expectedError);

            // Verify.
            this.repositoryMock.VerifyGetFirstOrDefaultAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Tests that Handle works with different transaction link IDs.
        /// </summary>
        /// <param name="id">The transaction link ID to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous test operation.</returns>
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public async Task Handle_ShouldReturnCorrectTransactLink_ForDifferentIds(int id)
        {
            // Arrange.
            var transactionLink = TransactionLinkTestData.CreateTransactionLink(id);
            var transactLinkDto = TransactionLinkTestData.CreateTransactLinkDto(id);

            this.repositoryMock.SetupGetFirstOrDefaultAsync(transactionLink);
            this.mapperMock.SetupMapper(transactionLink, transactLinkDto);

            var query = new GetTransactLinkByIdQuery(id);

            // Act.
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert.
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Id.Should().Be(id);

            // Verify.
            this.repositoryMock.VerifyGetFirstOrDefaultAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce();
        }
    }
}