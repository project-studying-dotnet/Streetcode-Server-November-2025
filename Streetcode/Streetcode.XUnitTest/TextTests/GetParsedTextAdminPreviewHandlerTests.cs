// <copyright file="GetParsedTextAdminPreviewHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.TextTests
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Text;
    using Streetcode.BLL.MediatR.Streetcode.Text.GetParsed;
    using Xunit;

    public class GetParsedTextAdminPreviewHandlerTests
    {
        private readonly Mock<ITextService> mockTextService;
        private readonly GetParsedTextAdminPreviewHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetParsedTextAdminPreviewHandlerTests"/> class.
        /// </summary>
        public GetParsedTextAdminPreviewHandlerTests()
        {
            this.mockTextService = new Mock<ITextService>();
            this.handler = new GetParsedTextAdminPreviewHandler(this.mockTextService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenTextIsParsedSuccessfully()
        {
            // Arrange
            const string InputRawText = "some text";
            const string ParsedText = "<p>some text</p>";
            var command = new GetParsedTextForAdminPreviewCommand(InputRawText);

            this.mockTextService.Setup(s => s.AddTermsTag(InputRawText))
                .ReturnsAsync(ParsedText);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(ParsedText);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenParsingReturnsNull()
        {
            // Arrange
            const string InputRawText = "bad text";
            const string ErrorMsg = "text was not parsed successfully";
            var command = new GetParsedTextForAdminPreviewCommand(InputRawText);

            this.mockTextService.Setup(s => s.AddTermsTag(InputRawText))
                .Returns(Task.FromResult<string?>(null));

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message == ErrorMsg);
        }
    }
}
