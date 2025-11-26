// <copyright file="GetTextByStreetcodeIdHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.TextTests
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
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
    using Xunit;

    public class GetTextByStreetcodeIdHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepoWrapper;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ITextService> mockTextService;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetTextByStreetcodeIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTextByStreetcodeIdHandlerTests"/> class.
        /// </summary>
        public GetTextByStreetcodeIdHandlerTests()
        {
            this.mockRepoWrapper = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();
            this.mockTextService = new Mock<ITextService>();
            this.mockLogger = new Mock<ILoggerService>();

            this.handler = new GetTextByStreetcodeIdHandler(
                this.mockRepoWrapper.Object,
                this.mockMapper.Object,
                this.mockTextService.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenTextExists()
        {
            // Arrange
            int streetcodeId = 1;
            var textEntity = new Text { Id = 1, StreetcodeId = streetcodeId, TextContent = "raw" };
            var textDto = new TextDTO { Id = 1, TextContent = "parsed" };
            var query = new GetTextByStreetcodeIdQuery(streetcodeId);

            this.mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<Text, bool>>>(), null))
                .ReturnsAsync(textEntity);

            this.mockTextService.Setup(s => s.AddTermsTag(It.IsAny<string>()))
                .ReturnsAsync("parsed");

            this.mockMapper.Setup(m => m.Map<TextDTO?>(textEntity))
                .Returns(textDto);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value!.TextContent.Should().Be("parsed");
        }

        [Theory]
        [InlineData(999)]
        [InlineData(-1)]
        public async Task Handle_ShouldReturnFail_WhenTextAndStreetcodeDoNotExist(int invalidStreetcodeId)
        {
            // Arrange
            string errorMsg = $"Cannot find a transaction link by a streetcode id: {invalidStreetcodeId}, because such streetcode doesn`t exist";
            var query = new GetTextByStreetcodeIdQuery(invalidStreetcodeId);

            this.mockRepoWrapper.Setup(r => r.TextRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Text, bool>>>(), null))
                    .ReturnsAsync((Text?)null);

            this.mockRepoWrapper.Setup(r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<StreetcodeContent, bool>>>(), null))
                .ReturnsAsync((StreetcodeContent?)null);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle(e => e.Message == errorMsg);
        }
    }
}
