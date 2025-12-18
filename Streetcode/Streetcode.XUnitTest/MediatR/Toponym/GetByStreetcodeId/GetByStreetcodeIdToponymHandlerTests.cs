namespace Streetcode.XUnitTest.MediatR.Toponyms.GetByStreetcodeId
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Toponyms.GetByStreetcodeId;
    using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Entities.Toponyms;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Toponyms;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Toponyms.Fixtures;
    using Streetcode.XUnitTest.MediatR.Toponyms.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetToponymsByStreetcodeIdHandler"/>.
    /// Covers success and failure scenarios of retrieving toponyms by streetcode ID,
    /// including handling of empty results, null results, and duplicate street names.
    /// </summary>
    public class GetByStreetcodeIdToponymHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetToponymsByStreetcodeIdHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByStreetcodeIdToponymHandlerTests"/> class.
        /// Initializes mocks and the <see cref="GetToponymsByStreetcodeIdHandler"/> instance.
        /// </summary>
        public GetByStreetcodeIdToponymHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetToponymsByStreetcodeIdHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns a successful result when toponyms exist for the given streetcode ID.
        /// Ensures that distinct toponyms are returned and mapped correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenToponymsExist_ShouldReturnSuccess()
        {
            // Arrange
            int streetcodeId = 1;
            var toponymRepositoryMock = new Mock<IToponymRepository>(MockBehavior.Strict);
            var toponyms = ToponymTestData.CreateToponymsWithStreetcodes(streetcodeId);
            var query = new GetToponymsByStreetcodeIdQuery(streetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(toponymRepositoryMock);
            toponymRepositoryMock.SetupGetAllAsync<IToponymRepository, Toponym>(toponyms.ToList());

            var expectedDistinctCount = toponyms.DistinctBy(t => t.StreetName).Count();

            foreach (var toponym in toponyms)
            {
                var dto = new ToponymDto
                {
                    Id = toponym.Id,
                    Oblast = toponym.Oblast,
                    StreetName = toponym.StreetName,
                    StreetType = toponym.StreetType,
                };
                this.mapperMock.SetupMapper(toponym, dto);
            }

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Value);

            var resultList = result.Value.ToList();
            Assert.NotEmpty(resultList);
            Assert.Equal(expectedDistinctCount, resultList.Count);

            toponymRepositoryMock.VerifyGetAllAsyncCalledOnce<IToponymRepository, Toponym>();
            this.mapperMock.Verify(m => m.Map<ToponymDto>(It.IsAny<Toponym>()), Times.Exactly(resultList.Count));
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        /// <summary>
        /// Tests that the handler returns a failed result when no toponyms exist for the given streetcode ID.
        /// Ensures proper error logging and that mapper is never called.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenToponymsDoNotExist_ShouldReturnFailure()
        {
            // Arrange
            int streetcodeId = 999;
            var toponymRepositoryMock = new Mock<IToponymRepository>(MockBehavior.Strict);
            var emptyToponyms = Enumerable.Empty<Toponym>().ToList();
            var query = new GetToponymsByStreetcodeIdQuery(streetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(toponymRepositoryMock);
            toponymRepositoryMock.SetupGetAllAsync<IToponymRepository, Toponym>(emptyToponyms);
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Contains($"Cannot find any toponym by the streetcode id: {streetcodeId}", result.Errors.First().Message);

            toponymRepositoryMock.VerifyGetAllAsyncCalledOnce<IToponymRepository, Toponym>();
            this.mapperMock.Verify(m => m.Map<ToponymDto>(It.IsAny<Toponym>()), Times.Never);
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Tests that the handler returns a failed result when the repository returns an empty list.
        /// Ensures proper error logging and that mapper is never called.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenRepositoryReturnsNull_ShouldReturnFailure()
        {
            // Arrange
            int streetcodeId = 1;
            var toponymRepositoryMock = new Mock<IToponymRepository>(MockBehavior.Strict);
            var query = new GetToponymsByStreetcodeIdQuery(streetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(toponymRepositoryMock);
            toponymRepositoryMock.SetupGetAllAsync<IToponymRepository, Toponym>(new List<Toponym>());
            this.loggerMock.SetupLogger();

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailed);
            Assert.NotEmpty(result.Errors);
            Assert.Contains($"Cannot find any toponym by the streetcode id: {streetcodeId}", result.Errors.First().Message);

            toponymRepositoryMock.VerifyGetAllAsyncCalledOnce<IToponymRepository, Toponym>();
            this.mapperMock.Verify(m => m.Map<ToponymDto>(It.IsAny<Toponym>()), Times.Never);
            this.loggerMock.VerifyLogErrorCalledOnce();
        }

        /// <summary>
        /// Tests that the handler returns only distinct street names when multiple toponyms have duplicate street names.
        /// Ensures that duplicate street names are filtered out correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenMultipleToponymsWithDuplicateStreetNames_ShouldReturnOnlyDistinctStreetNames()
        {
            // Arrange
            int streetcodeId = 1;
            var toponymRepositoryMock = new Mock<IToponymRepository>(MockBehavior.Strict);
            var toponyms = ToponymTestData.CreateToponymsWithDuplicateStreetNames(streetcodeId);
            var toponymDtos = ToponymTestData.CreateToponymDtosWithDuplicates();
            var query = new GetToponymsByStreetcodeIdQuery(streetcodeId);

            this.repositoryWrapperMock.SetupRepositoryWrapper(toponymRepositoryMock);
            toponymRepositoryMock.SetupGetAllAsync<IToponymRepository, Toponym>(toponyms.ToList());

            foreach (var dto in toponymDtos)
            {
                this.mapperMock.SetupMapper<Toponym, ToponymDto>(
                    t => t.Id == dto.Id,
                    dto);
            }

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);

            var resultList = result.Value.ToList();

            Assert.Equal(2, resultList.Count);

            var streetNames = resultList.Select(t => t.StreetName).ToList();
            Assert.Contains("Main Street", streetNames);
            Assert.Contains("Second Avenue", streetNames);

            toponymRepositoryMock.VerifyGetAllAsyncCalledOnce<IToponymRepository, Toponym>();
            this.mapperMock.Verify(m => m.Map<ToponymDto>(It.IsAny<Toponym>()), Times.Exactly(2));
            this.loggerMock.VerifyLogErrorCalledNever();
        }
    }
}