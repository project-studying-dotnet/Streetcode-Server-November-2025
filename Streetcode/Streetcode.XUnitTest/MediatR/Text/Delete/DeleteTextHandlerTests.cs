// <copyright file="DeleteTextHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Text.Delete
{
    using FluentAssertions;
    using FluentResults;
    using global::MediatR;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Text.Delete;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;
    using Streetcode.XUnitTest.Helpers;
    using Xunit;

    using TextEntity = Streetcode.DAL.Entities.Streetcode.TextContent.Text;

    /// <summary>
    /// Tests for <see cref="DeleteTextHandler"/>.
    /// </summary>
    public class DeleteTextHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly DeleteTextHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTextHandlerTests"/> class.
        /// </summary>
        public DeleteTextHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new DeleteTextHandler(
                this.repositoryWrapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Test case for handling deletion when the text is not found.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenTextNotFound_ShouldReturnFailure()
        {
            // Arrange
            string errorMsg = ErrorMessages.TextNotFoundById;
            var textRepoMock = new Mock<ITextRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(textRepoMock.Object);
            textRepoMock.SetupGetFirstOrDefaultAsync<ITextRepository, TextEntity>(null);

            this.loggerMock.SetupLogger();

            var command = new DeleteTextCommand(99);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(errorMsg);

            textRepoMock.VerifyGetFirstOrDefaultCalledOnce<ITextRepository, TextEntity>();
            textRepoMock.VerifyDeleteCalledNever<ITextRepository, TextEntity>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledNever();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Test case for handling successful deletion of a text.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenTextFound_ShouldReturnOk()
        {
            // Arrange
            var textEntity = new TextEntity { Id = 1, TextContent = "Sample content" };

            var textRepoMock = new Mock<ITextRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(textRepoMock.Object);

            textRepoMock.SetupGetFirstOrDefaultAsync<ITextRepository, TextEntity>(textEntity);
            textRepoMock.SetupDelete<ITextRepository, TextEntity>();

            this.repositoryWrapperMock.SetupSaveChangesAsync();

            var command = new DeleteTextCommand(1);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Should().BeOfType<Result<Unit>>();

            textRepoMock.VerifyGetFirstOrDefaultCalledOnce<ITextRepository, TextEntity>();
            textRepoMock.VerifyDeleteCalledOnce<ITextRepository, TextEntity>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        /// <summary>
        /// Test case for handling deletion when saving changes fails.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenSaveChangesFails_ShouldReturnFailure()
        {
            // Arrange
            string errorMsg = ErrorMessages.CannotSaveChangesInDatabase;
            var textEntity = new TextEntity { Id = 1, TextContent = "Sample content" };

            var textRepoMock = new Mock<ITextRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(textRepoMock.Object);

            textRepoMock.SetupGetFirstOrDefaultAsync<ITextRepository, TextEntity>(textEntity);
            textRepoMock.SetupDelete<ITextRepository, TextEntity>();

            this.repositoryWrapperMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);

            this.loggerMock.SetupLogger();

            var command = new DeleteTextCommand(1);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(errorMsg);

            textRepoMock.VerifyGetFirstOrDefaultCalledOnce<ITextRepository, TextEntity>();
            textRepoMock.VerifyDeleteCalledOnce<ITextRepository, TextEntity>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}
