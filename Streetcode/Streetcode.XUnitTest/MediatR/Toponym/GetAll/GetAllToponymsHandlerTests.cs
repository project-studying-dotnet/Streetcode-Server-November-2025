namespace Streetcode.XUnitTest.MediatR.Toponyms.GetAll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Moq;
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Toponyms.GetAll;
    using Streetcode.DAL.Entities.Toponyms;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Repositories.Interfaces.Toponyms;
    using Streetcode.XUnitTest.Helpers;
    using Streetcode.XUnitTest.MediatR.Toponyms.Fixtures;
    using Streetcode.XUnitTest.MediatR.Toponyms.Helpers;
    using Xunit;

    /// <summary>
    /// Unit tests for <see cref="GetAllToponymsHandler"/>.
    /// Covers success scenarios of retrieving all toponyms,
    /// including handling of empty results, filtering by title, and pagination.
    /// </summary>
    public class GetAllToponymsHandlerTests
    {
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IRepositoryWrapper> repositoryWrapperMock;
        private readonly Mock<ILoggerService> loggerMock;
        private readonly GetAllToponymsHandler handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllToponymsHandlerTests"/> class.
        /// Initializes mocks and the <see cref="GetAllToponymsHandler"/> instance.
        /// </summary>
        public GetAllToponymsHandlerTests()
        {
            this.mapperMock = new Mock<IMapper>();
            this.repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            this.loggerMock = new Mock<ILoggerService>();
            this.handler = new GetAllToponymsHandler(
                this.repositoryWrapperMock.Object,
                this.mapperMock.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests that the handler returns a successful result with an empty list when no toponyms exist.
        /// Ensures that pagination is set correctly even when the list is empty.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenToponymsDoNotExist_ShouldReturnSuccessWithEmptyList()
        {
            // Arrange
            var toponymRepositoryMock = new Mock<IToponymRepository>(MockBehavior.Strict);
            var query = new GetAllToponymsQuery(new GetAllToponymsRequestDto());
            var emptyToponyms = Enumerable.Empty<Toponym>().AsQueryable();
            var emptyToponymDtos = Enumerable.Empty<ToponymDto>();

            this.repositoryWrapperMock.SetupRepositoryWrapper(toponymRepositoryMock);
            toponymRepositoryMock.SetupFindAllAsync<IToponymRepository, Toponym>(emptyToponyms);
            this.mapperMock.Setup(m => m.Map<IEnumerable<ToponymDto>>(It.IsAny<IEnumerable<Toponym>>()))
                .Returns(emptyToponymDtos);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.Toponyms);
            Assert.Empty(result.Value.Toponyms);
            Assert.Equal(1, result.Value.Pages);

            toponymRepositoryMock.VerifyFindAllCalledOnce<IToponymRepository, Toponym>();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<ToponymDto>>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        /// <summary>
        /// Tests that the handler returns all toponyms successfully when they exist.
        /// Ensures that all toponyms are correctly retrieved, mapped, and pagination is set correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenToponymsExist_ShouldReturnAllToponymsSuccessfully()
        {
            // Arrange
            var toponymRepositoryMock = new Mock<IToponymRepository>(MockBehavior.Strict);
            var query = new GetAllToponymsQuery(new GetAllToponymsRequestDto());
            var toponyms = ToponymTestData.CreateToponyms();
            var toponymDtos = ToponymTestData.CreateToponymDtos();

            this.repositoryWrapperMock.SetupRepositoryWrapper(toponymRepositoryMock);
            toponymRepositoryMock.SetupFindAllAsync<IToponymRepository, Toponym>(toponyms.AsQueryable());
            this.mapperMock.Setup(m => m.Map<IEnumerable<ToponymDto>>(toponyms))
                .Returns(toponymDtos);

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.Toponyms);
            Assert.Equal(toponyms.Count(), result.Value.Toponyms.Count());
            Assert.Equal(1, result.Value.Pages);

            toponymRepositoryMock.VerifyFindAllCalledOnce<IToponymRepository, Toponym>();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<ToponymDto>>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

        /// <summary>
        /// Tests that the handler returns filtered toponyms when a title filter is provided.
        /// Ensures that only toponyms matching the filter criteria are returned.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous test execution.</returns>
        [Fact]
        public async Task Handle_WhenFilteringByTitle_ShouldReturnFilteredToponyms()
        {
            // Arrange
            var toponymRepositoryMock = new Mock<IToponymRepository>(MockBehavior.Strict);
            var filterTitle = "Main";
            var query = new GetAllToponymsQuery(new GetAllToponymsRequestDto { Title = filterTitle });
            var toponyms = ToponymTestData.CreateToponyms();
            var filteredToponyms = toponyms.Where(t => t.StreetName.Contains(filterTitle, StringComparison.OrdinalIgnoreCase));
            var toponymDtos = ToponymTestData.CreateToponymDtos()
                .Where(dto => dto.StreetName.Contains(filterTitle, StringComparison.OrdinalIgnoreCase));

            this.repositoryWrapperMock.SetupRepositoryWrapper(toponymRepositoryMock);
            toponymRepositoryMock.SetupFindAllAsync<IToponymRepository, Toponym>(toponyms.AsQueryable());
            this.mapperMock.Setup(m => m.Map<IEnumerable<ToponymDto>>(It.IsAny<IEnumerable<Toponym>>()))
                .Returns((IEnumerable<Toponym> source) =>
                {
                    var filtered = source.Where(t => t.StreetName.Contains(filterTitle, StringComparison.OrdinalIgnoreCase));
                    return toponymDtos.Where(dto => filtered.Any(f => f.Id == dto.Id));
                });

            // Act
            var result = await this.handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Value);
            Assert.NotNull(result.Value.Toponyms);
            Assert.All(result.Value.Toponyms, toponym => 
                Assert.Contains(filterTitle, toponym.StreetName, StringComparison.OrdinalIgnoreCase));
            Assert.Equal(1, result.Value.Pages);

            toponymRepositoryMock.VerifyFindAllCalledOnce<IToponymRepository, Toponym>();
            this.mapperMock.VerifyMapCalledOnce<IEnumerable<ToponymDto>>();
            this.loggerMock.VerifyLogErrorCalledNever();
        }

    }
}