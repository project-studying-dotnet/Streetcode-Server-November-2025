// <copyright file="GetTextByIdHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.TextTests
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
            this.mockRepoWrapper = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();
            this.mockLogger = new Mock<ILoggerService>();
            this.handler = new GetTextByIdHandler(this.mockRepoWrapper.Object, this.mockMapper.Object, this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenTextExists()
        {
            // Arrange
            int id = 1;
            var textEntity = new Text { Id = id, TextContent = "content" };
            var textDto = new TextDTO { Id = id, TextContent = "content" };
            var query = new GetTextByIdQuery(id);

            this.mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Text, bool>>>(), null))
                .ReturnsAsync(textEntity);

            this.mockMapper.Setup(m => m.Map<TextDTO>(textEntity)).Returns(textDto);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

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

            this.mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Text, bool>>>(), null))
                    .ReturnsAsync((Text?)null);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Contain($"Cannot find any text with corresponding id: {id}");
            this.mockLogger.Verify(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()), Times.Once);
        }
    }
}
