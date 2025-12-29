// <copyright file="GetParsedTextAdminPreviewHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Text.GetParsed
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.Interfaces.Text;
 using global::Streetcode.BLL.MediatR.Streetcode.Text.GetParsed;
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
            mockTextService = new Mock<ITextService>();
            handler = new GetParsedTextAdminPreviewHandler(mockTextService.Object);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> test for success when parsing success.</returns>
        [Fact]
        public async Task Handle_ShouldReturnOk_WhenTextIsParsedSuccessfully()
        {
            // Arrange
            const string InputRawText = "some text";
            const string ParsedText = "<p>some text</p>";
            var command = new GetParsedTextForAdminPreviewCommand(InputRawText);

            mockTextService.Setup(s => s.AddTermsTag(InputRawText))
                .ReturnsAsync(ParsedText);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(ParsedText);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> test for failing when parsing reurns null.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenParsingReturnsNull()
        {
            // Arrange
            const string InputRawText = "bad text";
            string ErrorMsg = ErrorMessages.TextParsingFailed;
            var command = new GetParsedTextForAdminPreviewCommand(InputRawText);

            mockTextService.Setup(s => s.AddTermsTag(InputRawText))
                .Returns(Task.FromResult<string?>(null));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message == ErrorMsg);
        }
    }
}
