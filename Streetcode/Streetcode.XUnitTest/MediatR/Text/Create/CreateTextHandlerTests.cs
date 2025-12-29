// <copyright file="CreateTextHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Text.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Streetcode.TextContent.Text;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Streetcode.Text.Create;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
 using global::Streetcode.DAL.Repositories.Interfaces.Streetcode.TextContent;
 using global::Streetcode.XUnitTest.Helpers;
    using Xunit;

    using TextEntity = global::Streetcode.DAL.Entities.Streetcode.TextContent.Text;

    /// <summary>
    /// Tests for <see cref="CreateTextHandler"/>
    /// </summary>
    public class CreateTextHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly CreateTextHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTextHandlerTests"/> class.
        /// </summary>
        public CreateTextHandlerTests()
        {
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.mapperMock = new Mock<IMapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new CreateTextHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Test case for handling creation when the request is null.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenRequestIsNull_ShouldReturnFailure()
        {
            // Arrange
            string errorMsg = ErrorMessages.TextDataRequired;
            var command = new CreateTextCommand(null!);

            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(errorMsg);
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Test case for handling creation when the creation is successful.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenCreateSuccessful_ShouldReturnCreatedDto()
        {
            // Arrange
            var textRepoMock = new Mock<ITextRepository>(MockBehavior.Strict);
            var create = new TextCreateDto { Title = "Test Title", TextContent = "Test Content" };
            var mapped = new TextEntity { Title = "Test Title", TextContent = "Test Content" };
            var mappedDto = new TextDto { Title = "Test Title", TextContent = "Test Content" };

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(textRepoMock.Object);

            var command = new CreateTextCommand(create);

            this.mapperMock.SetupMapper<TextCreateDto, TextEntity>(create, mapped);

            textRepoMock.SetupCreateAsync(mapped);
            this.repositoryWrapperMock.SetupSaveChangesAsync();
            this.mapperMock
                .Setup(m => m.Map<TextDto>(mapped))
                .Returns(mappedDto);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(mappedDto);

            textRepoMock.VerifyCreateAsyncCalledOnce<ITextRepository, TextEntity>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce<TextEntity>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        /// <summary>
        /// Test case for handling creation when mapping returns null.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenTextIsNull_ShouldReturnFailure()
        {
            // Arrange
            string errorMsg = ErrorMessages.CannotMapEntity;
            var create = new TextCreateDto { Title = "Test Title", TextContent = "Test Content" };

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(new Mock<ITextRepository>().Object);

            var command = new CreateTextCommand(create);

            this.mapperMock.SetupMapper<TextCreateDto, TextEntity>(create, null!);

            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(errorMsg);

            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Test case for handling creation when AdditionalText equals the default authorship text.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenAdditionalTextEqualsDefault_AdditionalTextShouldBeNull()
        {
            const string defaultAuthorship = "Текст підготовлений спільно з";

            var textRepoMock = new Mock<ITextRepository>(MockBehavior.Strict);
            var create = new TextCreateDto { Title = "Test Title", AdditionalText = defaultAuthorship };
            var mapped = new TextEntity { Title = "Test Title", AdditionalText = defaultAuthorship };
            var mappedAfterFix = new TextDto { Title = "Test Title", AdditionalText = null };

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(textRepoMock.Object);

            var command = new CreateTextCommand(create);

            this.mapperMock.SetupMapper<TextCreateDto, TextEntity>(create, mapped);

            textRepoMock.SetupCreateAsync(mapped);
            this.repositoryWrapperMock.SetupSaveChangesAsync();

            this.mapperMock.SetupMapper<TextEntity, TextDto>(e => e.AdditionalText == null, mappedAfterFix);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.AdditionalText.Should().BeNull();

            textRepoMock.VerifyCreateAsyncCalledOnce<ITextRepository, TextEntity>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.mapperMock.VerifyMapCalledOnce<TextEntity>();
        }

        /// <summary>
        /// Test case for handling creation when saving changes fails.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_WhenSaveChangesFails_ShouldReturnFailure()
        {
            string errorMsg = ErrorMessages.CannotSaveChangesInDatabase;
            var textRepoMock = new Mock<ITextRepository>(MockBehavior.Strict);
            var create = new TextCreateDto { Title = "Test Title", TextContent = "Test Content" };
            var mapped = new TextEntity { Title = "Test Title", TextContent = "Test Content" };

            this.repositoryWrapperMock
                .Setup(r => r.TextRepository)
                .Returns(textRepoMock.Object);

            this.mapperMock.SetupMapper<TextCreateDto, TextEntity>(create, mapped);

            textRepoMock.SetupCreateAsync(mapped);

            this.repositoryWrapperMock
                .Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(0);

            this.loggerMock.SetupLogger();

            var command = new CreateTextCommand(create);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(errorMsg);

            textRepoMock.VerifyCreateAsyncCalledOnce<ITextRepository, TextEntity>();
            this.repositoryWrapperMock.VerifySaveChangesAsyncCalledOnce();
            this.loggerMock.VerifyLogErrorCalledOnce();
        }
    }
}
