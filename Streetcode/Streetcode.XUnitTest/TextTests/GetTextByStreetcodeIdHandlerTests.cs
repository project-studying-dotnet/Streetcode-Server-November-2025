using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Interfaces.Text;
using Streetcode.BLL.MediatR.Streetcode.Text.GetByStreetcodeId;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Entities.Streetcode.TextContent;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;
using Xunit;

namespace Streetcode.XUnitTest.TextTests
{
	public class GetTextByStreetcodeIdHandlerTests
	{
		private readonly Mock<IRepositoryWrapper> _mockRepoWrapper;
		private readonly Mock<IMapper> _mockMapper;
		private readonly Mock<ITextService> _mockTextService;
		private readonly Mock<ILoggerService> _mockLogger;
		private readonly GetTextByStreetcodeIdHandler _handler;

		public GetTextByStreetcodeIdHandlerTests()
		{
			_mockRepoWrapper = new Mock<IRepositoryWrapper>();
			_mockMapper = new Mock<IMapper>();
			_mockTextService = new Mock<ITextService>();
			_mockLogger = new Mock<ILoggerService>();

			_handler = new GetTextByStreetcodeIdHandler(
				_mockRepoWrapper.Object,
				_mockMapper.Object,
				_mockTextService.Object,
				_mockLogger.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnOk_WhenTextExists()
		{
			// Arrange
			int streetcodeId = 1;
			var textEntity = new Text { Id = 1, StreetcodeId = streetcodeId, TextContent = "raw" };
			var textDto = new TextDTO { Id = 1, TextContent = "parsed" };
			var query = new GetTextByStreetcodeIdQuery(streetcodeId);

			_mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
				It.IsAny<Expression<Func<Text, bool>>>(), null))
				.ReturnsAsync(textEntity);

			_mockTextService.Setup(s => s.AddTermsTag(It.IsAny<string>()))
				.ReturnsAsync("parsed");

			_mockMapper.Setup(m => m.Map<TextDTO?>(textEntity))
				.Returns(textDto);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.IsSuccess.Should().BeTrue();
			result.Value.Should().NotBeNull();
			result.Value.TextContent.Should().Be("parsed");
		}

		[Fact]
		public async Task Handle_ShouldReturnFail_WhenTextAndStreetcodeDoNotExist()
		{
			// Arrange
			int streetcodeId = 999;
			var query = new GetTextByStreetcodeIdQuery(streetcodeId);

			// Text not found
			_mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
				It.IsAny<Expression<Func<Text, bool>>>(), null))
				.ReturnsAsync((Text?)null);

			// Streetcode not found
			_mockRepoWrapper.Setup(r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
				It.IsAny<Expression<Func<StreetcodeContent, bool>>>(), null))
				.ReturnsAsync((StreetcodeContent?)null);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.IsFailed.Should().BeTrue();
			result.Errors.First().Message.Should().Contain($"Cannot find a transaction link by a streetcode id: {streetcodeId}");

			// Verify logger was called
			_mockLogger.Verify(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()), Times.Once);
		}

		[Fact]
		public async Task Handle_ShouldReturnNullResult_WhenTextIsNull_But_StreetcodeExists()
		{
			// Arrange
			int streetcodeId = 1;
			var query = new GetTextByStreetcodeIdQuery(streetcodeId);

			// Text not found
			_mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
				It.IsAny<Expression<Func<Text, bool>>>(), null))
				.ReturnsAsync((Text?)null);

			// Streetcode IS found
			_mockRepoWrapper.Setup(r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
				It.IsAny<Expression<Func<StreetcodeContent, bool>>>(), null))
				.ReturnsAsync(new StreetcodeContent { Id = streetcodeId });

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.IsSuccess.Should().BeTrue();
			result.Value.Should().BeNull();
		}
	}
}
