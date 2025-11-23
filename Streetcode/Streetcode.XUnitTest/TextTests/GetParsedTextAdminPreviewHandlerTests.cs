
using Xunit;
using Moq;
using FluentAssertions;
using FluentResults;
using Streetcode.BLL.Interfaces.Text;
using Streetcode.BLL.MediatR.Streetcode.Text.GetParsed;

namespace Streetcode.XUnitTest.TextTests
{
    public class GetParsedTextAdminPreviewHandlerTests
    {
        private readonly Mock<ITextService> _mockTextService;
        private readonly GetParsedTextAdminPreviewHandler _handler;

        public GetParsedTextAdminPreviewHandlerTests()
        {
            _mockTextService = new Mock<ITextService>();
            _handler = new GetParsedTextAdminPreviewHandler(_mockTextService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenTextIsParsedSuccessfully()
        {
            // Arrange
            string inputRawText = "some text";
            string parsedText = "<p>some text</p>";
            var command = new GetParsedTextForAdminPreviewCommand(inputRawText);

            _mockTextService.Setup(s => s.AddTermsTag(inputRawText))
                .ReturnsAsync(parsedText);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(parsedText);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenParsingReturnsNull()
        {
            // Arrange
            string inputRawText = "bad text";
            var command = new GetParsedTextForAdminPreviewCommand(inputRawText);

            _mockTextService.Setup(s => s.AddTermsTag(inputRawText))
                .ReturnsAsync((string?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message == "text was not parsed successfully");
        }
    }
}
