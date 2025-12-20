namespace Streetcode.XUnitTest.MediatR.Toponym.StreetcodeToponym.Helpers
{
    using AutoMapper;
    using Moq;
    using Streetcode.BLL.DTO.Toponyms;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.DAL.Repositories.Interfaces.Base;

    /// <summary>
    /// Base class for StreetcodeToponym handler tests providing common mock setup functionality.
    /// </summary>
    public abstract class StreetcodeToponymHandlerTestsBase
    {
        /// <summary>
        /// Gets the mock repository wrapper for testing.
        /// </summary>
        protected Mock<IRepositoryWrapper> MockRepository { get; }

        /// <summary>
        /// Gets the mock mapper for testing.
        /// </summary>
        protected Mock<IMapper> MockMapper { get; }

        /// <summary>
        /// Gets the mock logger service for testing.
        /// </summary>
        protected Mock<ILoggerService> MockLogger { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreetcodeToponymHandlerTestsBase"/> class.
        /// </summary>
        protected StreetcodeToponymHandlerTestsBase()
        {
            this.MockRepository = new Mock<IRepositoryWrapper>();
            this.MockMapper = new Mock<IMapper>();
            this.MockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Sets up the mapper to return a specific StreetcodeToponymDto for any StreetcodeToponym entity.
        /// </summary>
        /// <param name="dto">The StreetcodeToponymDto to return.</param>
        protected void SetupMapperForStreetcodeToponymDto(StreetcodeToponymDto dto)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<StreetcodeToponymDto>(
                    It.IsAny<DAL.Entities.Toponyms.StreetcodeToponym>()))
                .Returns(dto);
        }

        /// <summary>
        /// Sets up the mapper to return a specific StreetcodeToponym entity for any StreetcodeToponymDto.
        /// </summary>
        /// <param name="entity">The StreetcodeToponym entity to return.</param>
        protected void SetupMapperForStreetcodeToponymEntity(DAL.Entities.Toponyms.StreetcodeToponym entity)
        {
            this.MockMapper
                .Setup(mapper => mapper.Map<DAL.Entities.Toponyms.StreetcodeToponym>(
                    It.IsAny<StreetcodeToponymDto>()))
                .Returns(entity);
        }

        /// <summary>
        /// Sets up the repository SaveChangesAsync to return success.
        /// </summary>
        protected void SetupSaveChangesAsyncSuccess()
        {
            this.MockRepository
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(1);
        }

        /// <summary>
        /// Sets up the repository SaveChangesAsync to return failure.
        /// </summary>
        protected void SetupSaveChangesAsyncFailure()
        {
            this.MockRepository
                .Setup(repo => repo.SaveChangesAsync())
                .ReturnsAsync(0);
        }

        /// <summary>
        /// Sets up the logger to accept LogError calls.
        /// </summary>
        protected void SetupLogger()
        {
            this.MockLogger
                .Setup(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()));
        }
    }
}