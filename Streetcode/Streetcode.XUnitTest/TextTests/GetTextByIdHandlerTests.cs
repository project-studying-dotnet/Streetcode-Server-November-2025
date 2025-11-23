using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Text.GetById;
using Streetcode.DAL.Entities.Streetcode.TextContent;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;
using Xunit;

namespace Streetcode.XUnitTest.TextTests
{
	public class GetTextByIdHandlerTests
	{
		private readonly Mock<IRepositoryWrapper> _mockRepoWrapper;
		private readonly Mock<IMapper> _mockMapper;
		private readonly Mock<ILoggerService> _mockLogger;
		private readonly GetTextByIdHandler _handler;

		public GetTextByIdHandlerTests()
		{
			_mockRepoWrapper = new Mock<IRepositoryWrapper>();
			_mockMapper = new Mock<IMapper>();
			_mockLogger = new Mock<ILoggerService>();
			_handler = new GetTextByIdHandler(_mockRepoWrapper.Object, _mockMapper.Object, _mockLogger.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnOk_WhenTextExists()
		{
			// Arrange
			int id = 1;
			var textEntity = new Text { Id = id, TextContent = "content" };
			var textDto = new TextDTO { Id = id, TextContent = "content" };
			var query = new GetTextByIdQuery(id);

			_mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
				It.IsAny<Expression<Func<Text, bool>>>(), null))
				.ReturnsAsync(textEntity);

			_mockMapper.Setup(m => m.Map<TextDTO>(textEntity)).Returns(textDto);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.IsSuccess.Should().BeTrue();
			result.Value.Should().BeEquivalentTo(textDto);
		}

		[Fact]
		public async Task Handle_ShouldReturnFail_WhenTextDoesNotExist()
		{
			// Arrange
			int id = 1;
			var query = new GetTextByIdQuery(id);

			_mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
				It.IsAny<Expression<Func<Text, bool>>>(), null))
				.ReturnsAsync((Text?)null);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.IsFailed.Should().BeTrue();
			result.Errors.First().Message.Should().Contain($"Cannot find any text with corresponding id: {id}");
			_mockLogger.Verify(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()), Times.Once);
		}
	}
}
