// <copyright file="GetCategoriesByStreetcodeIdHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Sources.SourceLink.GetCategoriesByStreetcodeId
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Media.Images;
    using Streetcode.BLL.DTO.Sources;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoriesByStreetcodeId;
    using Streetcode.DAL.Entities.Sources;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    /// <summary>
    /// Tests for the GetCategoriesByStreetcodeIdHandler class.
    /// </summary>
    public class GetCategoriesByStreetcodeIdHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepoWrapper;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IBlobService> mockBlobService;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetCategoriesByStreetcodeIdHandler handler;

        public GetCategoriesByStreetcodeIdHandlerTests()
        {
            this.mockRepoWrapper = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();
            this.mockBlobService = new Mock<IBlobService>();
            this.mockLogger = new Mock<ILoggerService>();

            this.handler = new GetCategoriesByStreetcodeIdHandler(
                this.mockRepoWrapper.Object,
                this.mockMapper.Object,
                this.mockBlobService.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenCategoriesFound()
        {
            // Arrange
            int streetcodeId = 1;
            var categories = new List<SourceLinkCategory>
            {
                new SourceLinkCategory { Id = 1, Image = new DAL.Entities.Media.Images.Image { BlobName = "blob" } },
            };
            var dtos = new List<SourceLinkCategoryDto>
            {
                new SourceLinkCategoryDto { Id = 1, Image = new ImageDto { BlobName = "blob" } },
            };

            this.mockRepoWrapper.Setup(r => r.SourceCategoryRepository.GetAllAsync(
                It.IsAny<Expression<Func<SourceLinkCategory, bool>>>(),
                It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()))
                .ReturnsAsync(categories);

            this.mockMapper.Setup(m => m.Map<IEnumerable<SourceLinkCategoryDto>>(categories))
                .Returns(dtos);

            this.mockBlobService.Setup(b => b.FindFileInStorageAsBase64("blob"))
                .Returns("base64");

            // Act
            var result = await this.handler.Handle(new GetCategoriesByStreetcodeIdQuery(streetcodeId), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(1);
            result.Value.First().Image.Base64.Should().Be("base64");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public async Task Handle_ShouldReturnFail_WhenCategoriesIsNull(int streetcodeId)
        {
            // Arrange
            string errorMsg = $"Cant find any source category with the streetcode id {streetcodeId}";

            this.mockRepoWrapper.Setup(r => r.SourceCategoryRepository.GetAllAsync(
                It.IsAny<Expression<Func<SourceLinkCategory, bool>>>(),
                It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()))
                .ReturnsAsync((IEnumerable<SourceLinkCategory>?)null);

            // Act
            var result = await this.handler.Handle(new GetCategoriesByStreetcodeIdQuery(streetcodeId), CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(errorMsg);

            this.mockLogger.Verify(l => l.LogError(It.IsAny<object>(), errorMsg), Times.Once);
        }
    }
}