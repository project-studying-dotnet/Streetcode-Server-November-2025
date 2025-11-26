// <copyright file="GetAllTextsHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.TextTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Streetcode.Text.GetAll;
    using Streetcode.DAL.Entities.Streetcode.TextContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    /// <summary>
    /// GetAllTextsHandlerTests.
    /// </summary>
    public class GetAllTextsHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepoWrapper;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetAllTextsHandler handler;


        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllTextsHandlerTests"/> class.
        /// </summary>
        public GetAllTextsHandlerTests()
        {
            this.mockRepoWrapper = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();
            this.mockLogger = new Mock<ILoggerService>();
            this.handler = new GetAllTextsHandler(
                this.mockRepoWrapper.Object,
                this.mockMapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenTextsExist()
        {
            // Arrange
            var textsList = new List<Text> { new Text { Id = 1 }, new Text { Id = 2 } };
            var textsDtoList = new List<TextDTO> { new TextDTO { Id = 1 }, new TextDTO { Id = 2 } };

            this.mockRepoWrapper.Setup(r => r.TextRepository.GetAllAsync(null, null))
                .ReturnsAsync(textsList);

            this.mockMapper.Setup(m => m.Map<IEnumerable<TextDTO>>(textsList))
                .Returns(textsDtoList);

            // Act
            var result = await this.handler.Handle(new GetAllTextsQuery(), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(2);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenRepositoryReturnsNull()
        {
            // Arrange
        const string ErrorMsg = "Cannot find any text";
        this.mockRepoWrapper.Setup(r => r.TextRepository.GetAllAsync(null, null))
                .ReturnsAsync((IEnumerable<Text>?)null);

        // Act
        var result = await this.handler.Handle(new GetAllTextsQuery(), CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be(ErrorMsg);
        }
    }
}
