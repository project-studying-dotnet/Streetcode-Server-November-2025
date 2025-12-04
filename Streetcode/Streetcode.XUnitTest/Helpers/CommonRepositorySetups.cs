namespace Streetcode.XUnitTest.Helpers
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using System.Linq.Expressions;
    using System.Runtime.Intrinsics.X86;

    /// <summary>
    /// Provides extension methods for configuring mocked repository, mapper, and logger behavior
    /// when testing handlers.
    /// </summary>
    public static class CommonRepositorySetups
    {
        /// <summary>
        /// Configures the mocked repository to return the specified collection
        /// of entities when calling <c>GetAllAsync</c>.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        /// <param name="entities">The collection of entities to return, or null.</param>
        public static void SetupGetAllAsync<TRepo, TEntity>(
            this Mock<TRepo> repositoryMock,
            IEnumerable<TEntity>? entities)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock
                .Setup(r => r.GetAllAsync(
                    It.IsAny<Expression<Func<TEntity, bool>>>(),
                    It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>()))
                .ReturnsAsync(entities!);
        }

        /// <summary>
        /// Configures the mocked repository to return the specified entity
        /// when calling <c>GetFirstOrDefaultAsync</c>.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        /// <param name="entity">The entity to return, or null.</param>
        public static void SetupGetFirstOrDefaultAsync<TRepo, TEntity>(
            this Mock<TRepo> repositoryMock,
            TEntity? entity)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock
                .Setup(r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<TEntity, bool>>>(),
                    It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>()))
                .ReturnsAsync(entity!);
        }

        /// <summary>
        /// Configures the mocked repository to return the specified entity
        /// when calling <c>CreateAsync</c>.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        /// <param name="entity">The entity to return after creation.</param>
        public static void SetupCreateAsync<TRepo, TEntity>(
            this Mock<TRepo> repositoryMock,
            TEntity entity)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<TEntity>()))
                .ReturnsAsync(entity);
        }

        /// <summary>
        /// Configures the mocked repository to accept any Update call.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        /// /// <param name="entity">The entity to update.</param>
        public static void SetupUpdate<TRepo, TEntity>(
            this Mock<TRepo> repositoryMock,
            TEntity entity)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock
                .Setup(r => r.Update(It.IsAny<TEntity>()))
                .Returns(It.IsAny<EntityEntry<TEntity>>());
        }

        /// <summary>
        /// Configures the mocked repository to accept any Delete call.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void SetupDelete<TRepo, TEntity>(
            this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Setup(r => r.Delete(It.IsAny<TEntity>()));
        }

        /// <summary>
        /// Configures the mocked <see cref="IRepositoryWrapper"/> to accept any SaveChangesAsync call
        /// and return a successful result.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        public static void SetupSaveChangesAsync(this Mock<IRepositoryWrapper> repositoryWrapperMock)
        {
            repositoryWrapperMock
                .Setup(rw => rw.SaveChangesAsync())
                .ReturnsAsync(1);
        }

        /// <summary>
        /// Configures the mocked <see cref="ILoggerService"/> to accept any LogError call.
        /// This prevents logger usage from affecting test execution.
        /// </summary>
        /// <param name="loggerMock">The mocked logger service.</param>
        public static void SetupLogger(this Mock<ILoggerService> loggerMock)
        {
            loggerMock
                .Setup(l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()));
        }

        /// <summary>
        /// Configures the mocked<see cref = "IMapper" /> to map from source to a new destination object.
        /// This method should be used when the mapper creates a new instance of the destination type.
        /// For updating existing objects, use <see cref="SetupMapOnto{TSource, TDestination}"/> instead.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="mapperMock">The mocked mapper.</param>
        /// <param name="source">The source object.</param>
        /// <param name="destination">The destination object to return.</param>
        public static void SetupMapper<TSource, TDestination>(
            this Mock<IMapper> mapperMock,
            TSource source,
            TDestination destination)
        {
            mapperMock
                .Setup(m => m.Map<TDestination>(source))
                .Returns(destination);
        }
    }
}