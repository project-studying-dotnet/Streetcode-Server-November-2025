// <copyright file="GetCategoryByIdHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Sources.SourceLink.GetCategoryById
{
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL;
    using Streetcode.BLL.DTO.Media.Images;
    using Streetcode.BLL.DTO.Sources;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoryById;
    using Streetcode.DAL.Entities.Sources;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Tests for the GetCategoryByIdHandler class.
    /// </summary>
    public class GetCategoryByIdHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepoWrapper;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IBlobService> mockBlobService;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetCategoryByIdHandler handler;

        public GetCategoryByIdHandlerTests()
        {
            this.mockRepoWrapper = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();
            this.mockBlobService = new Mock<IBlobService>();
            this.mockLogger = new Mock<ILoggerService>();

            this.handler = new GetCategoryByIdHandler(
                this.mockRepoWrapper.Object,
                this.mockMapper.Object,
                this.mockBlobService.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenCategoryExists()
        {
            // Arrange
            int id = 1;
            var category = new SourceLinkCategory { Id = id, Image = new DAL.Entities.Media.Images.Image { BlobName = "blob" } };
            var dto = new SourceLinkCategoryDto { Id = id, Image = new ImageDto { BlobName = "blob" } };

            this.mockRepoWrapper.Setup(r => r.SourceCategoryRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<SourceLinkCategory, bool>>>(),
                It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()))
                .ReturnsAsync(category);

            this.mockMapper.Setup(m => m.Map<SourceLinkCategoryDto>(category))
                .Returns(dto);

            this.mockBlobService.Setup(b => b.FindFileInStorageAsBase64("blob"))
                .Returns("base64");

            // Act
            var result = await this.handler.Handle(new GetCategoryByIdQuery(id), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Image.Base64.Should().Be("base64");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public async Task Handle_ShouldReturnFail_WhenCategoryDoesNotExist(int id)
        {
            // Arrange
            string errorMsg = string.Format(ErrorMessages.CategoryNotFoundById, id);

            this.mockRepoWrapper.Setup(r => r.SourceCategoryRepository.GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<SourceLinkCategory, bool>>>(),
                It.IsAny<Func<IQueryable<SourceLinkCategory>, IIncludableQueryable<SourceLinkCategory, object>>>()))
                .ReturnsAsync((SourceLinkCategory?)null);

            // Act
            var result = await this.handler.Handle(new GetCategoryByIdQuery(id), CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(errorMsg);

            this.mockLogger.Verify(l => l.LogError(It.IsAny<object>(), errorMsg), Times.Once);
        }
    }
}