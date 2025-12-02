// <copyright file="GetTextByIdHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Text.GetById
{
    using System.Linq.Expressions;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Text.GetById;
    using Streetcode.DAL.Entities.Streetcode.TextContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetTextByIdHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepoWrapper;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetTextByIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTextByIdHandlerTests"/> class.
        /// </summary>
        public GetTextByIdHandlerTests()
        {
            mockRepoWrapper = new Mock<IRepositoryWrapper>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILoggerService>();
            handler = new GetTextByIdHandler(mockRepoWrapper.Object, mockMapper.Object, mockLogger.Object);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> test for success when text exist.</returns>
        [Fact]
        public async Task Handle_ShouldReturnOk_WhenTextExists()
        {
            // Arrange
            int id = 1;
            var textEntity = new Text { Id = id, TextContent = "content" };
            var textDto = new TextDTO { Id = id, TextContent = "content" };
            var query = new GetTextByIdQuery(id);

            mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Text, bool>>>(), null))
                .ReturnsAsync(textEntity);

            mockMapper.Setup(m => m.Map<TextDTO>(textEntity)).Returns(textDto);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(textDto);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="Task"/> test for failing when text don't exist.</returns>
        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public async Task Handle_ShouldReturnFail_WhenTextDoesNotExist(int id)
        {
            // Arrange
            var query = new GetTextByIdQuery(id);
            string errorMsg = $"Cannot find any text with corresponding id: {id}";

            mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Text, bool>>>(), null))
                .ReturnsAsync((Text?)null);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Contain(errorMsg);

            mockLogger.Verify(l => l.LogError(It.IsAny<object>(), errorMsg), Times.Once);
        }
    }
}
