// <copyright file="GetAllTextsHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Text.GetAll
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
            mockRepoWrapper = new Mock<IRepositoryWrapper>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILoggerService>();
            handler = new GetAllTextsHandler(
                mockRepoWrapper.Object,
                mockMapper.Object,
                mockLogger.Object);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> test for ok when text exist.</returns>
        [Fact]
        public async Task Handle_ShouldReturnOk_WhenTextsExist()
        {
            // Arrange
            var textsList = new List<Text> { new Text { Id = 1 }, new Text { Id = 2 } };
            var textsDtoList = new List<TextDto> { new TextDto { Id = 1 }, new TextDto { Id = 2 } };

            mockRepoWrapper.Setup(r => r.TextRepository.GetAllAsync(null, null))
                .ReturnsAsync(textsList);

            mockMapper.Setup(m => m.Map<IEnumerable<TextDto>>(textsList))
                .Returns(textsDtoList);

            // Act
            var result = await handler.Handle(new GetAllTextsQuery(), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(2);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> test for ok when repo returns empty list.</returns>
        [Fact]
        public async Task Handle_ShouldReturnOk_WhenRepositoryReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<Text>();
            var emptyDtoList = new List<TextDto>();

            mockRepoWrapper.Setup(r => r.TextRepository.GetAllAsync(null, null))
                .ReturnsAsync(emptyList);

            mockMapper.Setup(m => m.Map<IEnumerable<TextDto>>(emptyList))
                .Returns(emptyDtoList);

            // Act
            var result = await handler.Handle(new GetAllTextsQuery(), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEmpty();

            mockRepoWrapper.Verify(r => r.TextRepository.GetAllAsync(null, null), Times.Once);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> test for failing when when repo returns null.</returns>
        [Fact]
        public async Task Handle_ShouldReturnFail_WhenRepositoryReturnsNull()
        {
            // Arrange
            const string ErrorMsg = "Cannot find any text";
            mockRepoWrapper.Setup(r => r.TextRepository.GetAllAsync(null, null))
                    .ReturnsAsync((IEnumerable<Text>?)null);

            // Act
            var result = await handler.Handle(new GetAllTextsQuery(), CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(ErrorMsg);
        }
    }
}
