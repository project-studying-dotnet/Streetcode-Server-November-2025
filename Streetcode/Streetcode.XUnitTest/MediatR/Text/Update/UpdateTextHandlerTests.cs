// <copyright file="UpdateTextHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Text.Update
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Text.Update;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;
    using Streetcode.XUnitTest.Helpers;
    using Xunit;

    using TextEntity = Streetcode.DAL.Entities.Streetcode.TextContent.Text;

    /// <summary>
    /// Tests for <see cref="UpdateTextHandler"/>.
    /// </summary>
    public class UpdateTextHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;

        private readonly UpdateTextHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTextHandlerTests"/> class.
        /// </summary>
        public UpdateTextHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();

            this.handler = new UpdateTextHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Test case for handling update when the text is not found.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenTextNotFound_ShouldReturnFailure()
        {
            // Arrange
            const string errorMsg = "Cannot find text with corresponding id.";

            var textRepoMock = new Mock<ITextRepository>(MockBehavior.Strict);

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(textRepoMock.Object);

            textRepoMock.SetupGetFirstOrDefaultAsync<ITextRepository, TextEntity>(null);
            this.loggerMock.SetupLogger();

            var command = new UpdateTextCommand(99, new TextUpdateDto());

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(errorMsg);

            textRepoMock.VerifyGetFirstOrDefaultCalledOnce<ITextRepository, TextEntity>();
            textRepoMock.VerifyUpdateCalledNever<ITextRepository, TextEntity>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledNever();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Test case for handling successful update.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenUpdateSuccessful_ShouldReturnUpdatedDto()
        {
            // Arrange
            var textRepoMock = new Mock<ITextRepository>(MockBehavior.Strict);
            var existing = new TextEntity { Id = 10, AdditionalText = "Old" };
            var update = new TextUpdateDto { AdditionalText = "New!" };
            var mapped = new TextEntity { Id = 10, AdditionalText = "New!" };
            var mappedDto = new TextDto { Id = 10, AdditionalText = "New!" };

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(textRepoMock.Object);

            textRepoMock.SetupGetFirstOrDefaultAsync<ITextRepository, TextEntity>(existing);
            this.mapperMock.Setup(m => m.Map(update, existing))
                .Returns(mapped);
            textRepoMock.SetupUpdate(mapped);
            this.repositoryWrapperMock.SetupSaveChangesAsync();
            this.mapperMock.Setup(m => m.Map<TextDto>(mapped))
                .Returns(mappedDto);

            var command = new UpdateTextCommand(10, update);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(mappedDto);

            textRepoMock.VerifyGetFirstOrDefaultCalledOnce<ITextRepository, TextEntity>();
            textRepoMock.VerifyUpdateCalledOnce<ITextRepository, TextEntity>();

            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce<TextDto>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        /// <summary>
        /// Test case for handling update when AdditionalText equals default value.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenAdditionalTextEqualsDefault_AdditionalTextShouldBeNull()
        {
            // Arrange
            const string defaultAuthorship = "Текст підготовлений спільно з";
            var textRepoMock = new Mock<ITextRepository>(MockBehavior.Strict);
            var existing = new TextEntity { Id = 1, AdditionalText = "something" };
            var update = new TextUpdateDto { AdditionalText = defaultAuthorship };
            var mappedBeforeFix = new TextEntity { Id = 1, AdditionalText = defaultAuthorship };

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(textRepoMock.Object);

            textRepoMock.SetupGetFirstOrDefaultAsync<ITextRepository, TextEntity>(existing);

            this.mapperMock.Setup(m => m.Map(update, existing)).Returns(mappedBeforeFix);

            textRepoMock.SetupUpdate(It.IsAny<TextEntity>());
            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.mapperMock.Setup(m =>
                m.Map<TextDto>(It.Is<TextEntity>(t => t.AdditionalText == null)))
                .Returns(new TextDto { Id = 1, AdditionalText = null });

            var command = new UpdateTextCommand(1, update);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.AdditionalText.Should().BeNull();

            textRepoMock.VerifyGetFirstOrDefaultCalledOnce<ITextRepository, TextEntity>();
            textRepoMock.VerifyUpdateCalledOnce<ITextRepository, TextEntity>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce<TextDto>();
        }

        /// <summary>
        /// Test case for handling update when saving changes fails.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenSaveChangesFails_ShouldReturnFailure()
        {
            // Arrange
            const string errorMsg = "Cannot save changes in the database.";
            var textRepoMock = new Mock<ITextRepository>(MockBehavior.Strict);
            var existing = new TextEntity { Id = 33 };
            var update = new TextUpdateDto();
            var mapped = new TextEntity { Id = 33 };

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(textRepoMock.Object);

            textRepoMock.SetupGetFirstOrDefaultAsync<ITextRepository, TextEntity>(existing);
            this.mapperMock.Setup(m => m.Map(update, existing)).Returns(mapped);
            textRepoMock.SetupUpdate(mapped);

            this.repositoryWrapperMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);

            this.loggerMock.SetupLogger();

            var command = new UpdateTextCommand(33, update);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(errorMsg);

            textRepoMock.VerifyGetFirstOrDefaultCalledOnce<ITextRepository, TextEntity>();
            textRepoMock.VerifyUpdateCalledOnce<ITextRepository, TextEntity>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}
