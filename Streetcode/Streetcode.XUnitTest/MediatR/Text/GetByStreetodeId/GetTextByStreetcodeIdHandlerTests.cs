// <copyright file="GetTextByStreetcodeIdHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Text.GetByStreetodeId
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Interfaces.Text;
    using Streetcode.BLL.MediatR.Streetcode.Text.GetByStreetcodeId;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Entities.Streetcode.TextContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetTextByStreetcodeIdHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepoWrapper;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ITextService> mockTextService;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetTextByStreetcodeIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTextByStreetcodeIdHandlerTests"/> class.
        /// </summary>
        public GetTextByStreetcodeIdHandlerTests()
        {
            mockRepoWrapper = new Mock<IRepositoryWrapper>();
            mockMapper = new Mock<IMapper>();
            mockTextService = new Mock<ITextService>();
            mockLogger = new Mock<ILoggerService>();

            handler = new GetTextByStreetcodeIdHandler(
                mockRepoWrapper.Object,
                mockMapper.Object,
                mockTextService.Object,
                mockLogger.Object);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> test for failing when text exist.</returns>
        [Fact]
        public async Task Handle_ShouldReturnOk_WhenTextExists()
        {
            // Arrange
            int streetcodeId = 1;
            var textEntity = new Text { Id = 1, StreetcodeId = streetcodeId, TextContent = "raw" };
            var textDto = new TextDto { Id = 1, TextContent = "parsed" };
            var query = new GetTextByStreetcodeIdQuery(streetcodeId);

            mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Text, bool>>>(), null))
                .ReturnsAsync(textEntity);

            mockTextService.Setup(s => s.AddTermsTag(It.IsAny<string>()))
                .ReturnsAsync("parsed");

            mockMapper.Setup(m => m.Map<TextDto?>(textEntity))
                .Returns(textDto);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.TextContent.Should().Be("parsed");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="invalidStreetcodeId"></param>
        /// <returns>A <see cref="Task"/> test for failing when text and streetcode don't exist.</returns>
        [Theory]
        [InlineData(999)]
        [InlineData(-1)]
        public async Task Handle_ShouldReturnFail_WhenTextAndStreetcodeDoNotExist(int invalidStreetcodeId)
        {
            // Arrange
            string errorMsg = string.Format(ErrorMessages.TransactionLinkNotFoundByStreetcodeId, invalidStreetcodeId);
            var query = new GetTextByStreetcodeIdQuery(invalidStreetcodeId);

            mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Text, bool>>>(), null))
                    .ReturnsAsync((Text?)null);

            mockRepoWrapper.Setup(r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<StreetcodeContent, bool>>>(), null))
                .ReturnsAsync((StreetcodeContent?)null);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message == errorMsg);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> test for ok when text exists but Streetcode doesn't.</returns>
        [Fact]
        public async Task Handle_ShouldReturnOk_WhenTextExists_ButStreetcodeContextDoesNotMatter()
        {
            // Arrange
            int streetcodeId = 1;
            var textEntity = new Text { Id = 1, StreetcodeId = streetcodeId };
            var textDto = new TextDto { Id = 1 };

            mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Text, bool>>>(), null))
                .ReturnsAsync(textEntity);

            mockTextService.Setup(s => s.AddTermsTag(It.IsAny<string>())).ReturnsAsync("");
            mockMapper.Setup(m => m.Map<TextDto?>(textEntity)).Returns(textDto);

            // Act
            var result = await handler.Handle(new GetTextByStreetcodeIdQuery(streetcodeId), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();

            // Verify that we NEVER checked for streetcode existence because we found the text
            mockRepoWrapper.Verify(r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<StreetcodeContent, bool>>>(), null), Times.Never);
        }
    }
}
