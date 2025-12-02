// <copyright file="GetCategoryContentByStreetcodeIdHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Sources.SourceLinkCategory.GetCategoryContentByStreetcodeId
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.DTO.Sources;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetCategoryContentByStreetcodeId;
    using Streetcode.DAL.Entities.Sources;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    /// <summary>
    /// Tests for the GetCategoryContentByStreetcodeIdHandler class.
    /// </summary>
    public class GetCategoryContentByStreetcodeIdHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepoWrapper;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetCategoryContentByStreetcodeIdHandler handler;

        public GetCategoryContentByStreetcodeIdHandlerTests()
        {
            this.mockRepoWrapper = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();
            this.mockLogger = new Mock<ILoggerService>();

            this.handler = new GetCategoryContentByStreetcodeIdHandler(
                this.mockRepoWrapper.Object,
                this.mockMapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenContentExists()
        {
            // Arrange
            int streetcodeId = 1;
            int categoryId = 1;
            var content = new StreetcodeCategoryContent { StreetcodeId = streetcodeId, SourceLinkCategoryId = categoryId };
            var dto = new StreetcodeCategoryContentDto { StreetcodeId = streetcodeId, SourceLinkCategoryId = categoryId };
            var query = new GetCategoryContentByStreetcodeIdQuery(streetcodeId, categoryId);

            // Mock Streetcode existance
            this.mockRepoWrapper.Setup(r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<StreetcodeContent, bool>>>(), null))
                .ReturnsAsync(new StreetcodeContent { Id = streetcodeId });

            // Mock Content existance
            this.mockRepoWrapper.Setup(r => r.StreetcodeCategoryContentRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<StreetcodeCategoryContent, bool>>>(), null))
                .ReturnsAsync(content);

            this.mockMapper.Setup(m => m.Map<StreetcodeCategoryContentDto>(content))
                .Returns(dto);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(dto);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task Handle_ShouldReturnFail_WhenStreetcodeDoesNotExist(int streetcodeId, int categoryId)
        {
            // Arrange
            string errorMsg = $"No such streetcode with id = {streetcodeId}";
            var query = new GetCategoryContentByStreetcodeIdQuery(streetcodeId, categoryId);

            this.mockRepoWrapper.Setup(r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<StreetcodeContent, bool>>>(), null))
                .ReturnsAsync((StreetcodeContent?)null);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(errorMsg);

            this.mockLogger.Verify(l => l.LogError(It.IsAny<object>(), errorMsg), Times.Once);
        }

        [Theory]
        [InlineData(1, 1)]
        public async Task Handle_ShouldReturnFail_WhenStreetcodeExistsButContentDoesNot(int streetcodeId, int categoryId)
        {
            // Arrange
            const string ErrorMsg = "The streetcode content is null";
            var query = new GetCategoryContentByStreetcodeIdQuery(streetcodeId, categoryId);

            // Streetcode exists
            this.mockRepoWrapper.Setup(r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<StreetcodeContent, bool>>>(), null))
                .ReturnsAsync(new StreetcodeContent { Id = streetcodeId });

            // Content does NOT exist
            this.mockRepoWrapper.Setup(r => r.StreetcodeCategoryContentRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<StreetcodeCategoryContent, bool>>>(), null))
                .ReturnsAsync((StreetcodeCategoryContent?)null);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(ErrorMsg);

            this.mockLogger.Verify(l => l.LogError(It.IsAny<object>(), ErrorMsg), Times.Once);
        }
    }
}