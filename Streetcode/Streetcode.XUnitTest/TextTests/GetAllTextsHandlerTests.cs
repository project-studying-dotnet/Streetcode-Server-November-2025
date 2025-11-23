using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Text.GetAll;
using Streetcode.DAL.Entities.Streetcode.TextContent;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Streetcode.XUnitTest.TextTests
{
	public class GetAllTextsHandlerTests
	{
		private readonly Mock<IRepositoryWrapper> _mockRepoWrapper;
		private readonly Mock<IMapper> _mockMapper;
		private readonly Mock<ILoggerService> _mockLogger;
		private readonly GetAllTextsHandler _handler;

		public GetAllTextsHandlerTests()
		{
			_mockRepoWrapper = new Mock<IRepositoryWrapper>();
			_mockMapper = new Mock<IMapper>();
			_mockLogger = new Mock<ILoggerService>();
			_handler = new GetAllTextsHandler(_mockRepoWrapper.Object, _mockMapper.Object, _mockLogger.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnOk_WhenTextsExist()
		{
			// Arrange
			var textsList = new List<Text> { new Text { Id = 1 }, new Text { Id = 2 } };
			var textsDtoList = new List<TextDTO> { new TextDTO { Id = 1 }, new TextDTO { Id = 2 } };

			_mockRepoWrapper.Setup(r => r.TextRepository.GetAllAsync(null, null))
				.ReturnsAsync(textsList);

			_mockMapper.Setup(m => m.Map<IEnumerable<TextDTO>>(textsList))
				.Returns(textsDtoList);

			// Act
			var result = await _handler.Handle(new GetAllTextsQuery(), CancellationToken.None);

			// Assert
			result.IsSuccess.Should().BeTrue();
			result.Value.Should().HaveCount(2);
		}

		[Fact]
		public async Task Handle_ShouldReturnFail_WhenRepositoryReturnsNull()
		{
			// Arrange
			_mockRepoWrapper.Setup(r => r.TextRepository.GetAllAsync(null, null))
				.ReturnsAsync((IEnumerable<Text>?)null);

			// Act
			var result = await _handler.Handle(new GetAllTextsQuery(), CancellationToken.None);

			// Assert
			result.IsFailed.Should().BeTrue();
			result.Errors.First().Message.Should().Be("Cannot find any text");
			_mockLogger.Verify(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()), Times.Once);
		}
	}
}
