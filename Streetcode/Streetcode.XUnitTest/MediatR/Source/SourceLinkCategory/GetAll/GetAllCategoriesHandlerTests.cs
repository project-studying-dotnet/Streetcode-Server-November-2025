// <copyright file="GetAllCategoriesHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Sources.SourceLinkCategory.GetAll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll;
    using Streetcode.DAL.Entities.Sources;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    /// <summary>
    /// Tests for the GetAllCategoriesHandler class.
    /// </summary>
    public class GetAllCategoriesHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepoWrapper;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IBlobService> mockBlobService;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetAllCategoriesHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllCategoriesHandlerTests"/> class.
        /// </summary>
        public GetAllCategoriesHandlerTests()
        {
            this.mockRepoWrapper = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();
            this.mockBlobService = new Mock<IBlobService>();
            this.mockLogger = new Mock<ILoggerService>();

            this.handler = new GetAllCategoriesHandler(
                this.mockRepoWrapper.Object,
                this.mockMapper.Object,
                this.mockBlobService.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<SourceLinkCategory>
            {
                new SourceLinkCategory { Id = 1, Image = new DAL.Entities.Media.Images.Image { BlobName = "blob1" } },
            };
            var dtos = new List<SourceLinkCategoryDto>
            {
                new SourceLinkCategoryDto { Id = 1, Image = new ImageDtoo { BlobName = "blob1" } },
            };
            const string Base64String = "base64_string";

            this.mockRepoWrapper.Setup(r => r.SourceCategoryRepository.GetAllAsync(
                null,
                It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()))
                .ReturnsAsync(categories);

            this.mockMapper.Setup(m => m.Map<IEnumerable<SourceLinkCategoryDto>>(categories))
                .Returns(dtos);

            this.mockBlobService.Setup(b => b.FindFileInStorageAsBase64(It.IsAny<string>()))
                .Returns(Base64String);

            // Act
            var result = await this.handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(1);
            result.Value.First().Image.Base64.Should().Be(Base64String);

            this.mockRepoWrapper.Verify(
                r => r.SourceCategoryRepository.GetAllAsync(
                null,
                It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenRepositoryReturnsNull()
        {
            // Arrange
            const string ErrorMsg = "Categories is null";

            this.mockRepoWrapper.Setup(r => r.SourceCategoryRepository.GetAllAsync(
                null,
                It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()))
                .ReturnsAsync((IEnumerable<SourceLinkCategory>?)null);

            // Act
            var result = await this.handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(ErrorMsg);

            this.mockLogger.Verify(l => l.LogError(It.IsAny<object>(), ErrorMsg), Times.Once);
        }
    }
}