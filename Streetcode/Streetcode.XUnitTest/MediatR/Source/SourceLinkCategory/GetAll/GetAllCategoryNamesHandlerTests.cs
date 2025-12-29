// <copyright file="GetAllCategoryNamesHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatR.Sources.SourceLinkCategory.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
 using global::Streetcode.BLL;
 using global::Streetcode.BLL.DTO.Sources;
 using global::Streetcode.BLL.Interfaces.Logging;
 using global::Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll;
 using global::Streetcode.DAL.Entities.Sources;
 using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Tests for the GetAllCategoryNamesHandler class.
    /// </summary>
    public class GetAllCategoryNamesHandlerTests
    {
        private readonly Mock<IRepositoryWrapper> mockRepoWrapper;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly GetAllCategoryNamesHandler handler;

        public GetAllCategoryNamesHandlerTests()
        {
            this.mockRepoWrapper = new Mock<IRepositoryWrapper>();
            this.mockMapper = new Mock<IMapper>();
            this.mockLogger = new Mock<ILoggerService>();

            this.handler = new GetAllCategoryNamesHandler(
                this.mockRepoWrapper.Object,
                this.mockMapper.Object,
                this.mockLogger.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOk_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<SourceLinkCategory> { new SourceLinkCategory { Id = 1 } };
            var dtos = new List<CategoryWithNameDto> { new CategoryWithNameDto { Id = 1 } };

            this.mockRepoWrapper.Setup(r => r.SourceCategoryRepository.GetAllAsync(null, null))
                .ReturnsAsync(categories);

            this.mockMapper.Setup(m => m.Map<IEnumerable<CategoryWithNameDto>>(categories))
                .Returns(dtos);

            // Act
            var result = await this.handler.Handle(new GetAllCategoryNamesQuery(), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(1);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenRepositoryReturnsNull()
        {
            // Arrange
            string ErrorMsg = ErrorMessages.CategoriesNotFound;

            this.mockRepoWrapper.Setup(r => r.SourceCategoryRepository.GetAllAsync(null, null))
                .ReturnsAsync((IEnumerable<SourceLinkCategory>?)null);

            // Act
            var result = await this.handler.Handle(new GetAllCategoryNamesQuery(), CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Message.Should().Be(ErrorMsg);

            this.mockLogger.Verify(l => l.LogError(It.IsAny<object>(), ErrorMsg), Times.Once);
        }
    }
}