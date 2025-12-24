namespace Streetcode.XUnitTest.Helpers
{
    using System.Linq.Expressions;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.DAL.Repositories.Interfaces.Base;

    /// <summary>
    /// Provides generic extension methods for verifying interactions with mocked repository,
    /// mapper, and logger components that can be used across all entity types.
    /// </summary>
    public static class CommonRepositoryVerifications
    {
        // -------------------------- Verify Repository -------------------------------

        /// <summary>
        /// Verifies that <c>GetAllAsync</c> was called exactly once on the mocked repository.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void VerifyGetAllAsyncCalledOnce<TRepo, TEntity>(this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Verify(
                r => r.GetAllAsync(
                    It.IsAny<Expression<Func<TEntity, bool>>>(),
                    It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>()),
                Times.Once(),
                $"GetAllAsync method should be called exactly once for {typeof(TEntity).Name}");
        }

        /// <summary>
        /// Verifies that <c>GetFirstOrDefaultAsync</c> was called exactly once on the mocked repository.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void VerifyGetFirstOrDefaultCalledOnce<TRepo, TEntity>(this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<TEntity, bool>>>(),
                    It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>()),
                Times.Once(),
                $"GetFirstOrDefaultAsync method should be called exactly once for {typeof(TEntity).Name}");
        }

        /// <summary>
        /// Verifies that <c>GetFirstOrDefaultAsync</c> was never called on the mocked repository.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void VerifyGetFirstOrDefaultCalledNever<TRepo, TEntity>(this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Verify(
                r => r.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<TEntity, bool>>>(),
                    It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>()),
                Times.Never(),
                $"GetFirstOrDefaultAsync method should not be called for {typeof(TEntity).Name}");
        }

        /// <summary>
        /// Verifies that <c>CreateAsync</c> was called exactly once on the mocked repository.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void VerifyCreateAsyncCalledOnce<TRepo, TEntity>(this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Verify(
                r => r.CreateAsync(It.IsAny<TEntity>()),
                Times.Once(),
                $"CreateAsync method should be called exactly once for {typeof(TEntity).Name}");
        }

        /// <summary>
        /// Verifies that <c>CreateAsync</c> was never called on the mocked repository.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void VerifyCreateAsyncCalledNever<TRepo, TEntity>(this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Verify(
                r => r.CreateAsync(It.IsAny<TEntity>()),
                Times.Never(),
                $"CreateAsync method should not be called for {typeof(TEntity).Name}");
        }

        /// <summary>
        /// Verifies that <c>Update</c> was called exactly once on the mocked repository.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void VerifyUpdateCalledOnce<TRepo, TEntity>(this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Verify(
                r => r.Update(It.IsAny<TEntity>()),
                Times.Once(),
                $"Update method should be called exactly once for {typeof(TEntity).Name}");
        }

        /// <summary>
        /// Verifies that <c>Update</c> was never called on the mocked repository.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void VerifyUpdateCalledNever<TRepo, TEntity>(this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Verify(
                r => r.Update(It.IsAny<TEntity>()),
                Times.Never(),
                $"Update method should not be called for {typeof(TEntity).Name}");
        }

        /// <summary>
        /// Verifies that <c>Delete</c> was called exactly once on the mocked repository.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void VerifyDeleteCalledOnce<TRepo, TEntity>(this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Verify(
                r => r.Delete(It.IsAny<TEntity>()),
                Times.Once(),
                $"Delete method should be called exactly once for {typeof(TEntity).Name}");
        }

        /// <summary>
        /// Verifies that <c>Delete</c> was never called on the mocked repository.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void VerifyDeleteCalledNever<TRepo, TEntity>(this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Verify(
                r => r.Delete(It.IsAny<TEntity>()),
                Times.Never(),
                $"Delete method should not be called for {typeof(TEntity).Name}");
        }

        /// <summary>
        /// Verifies that <c>SaveChangesAsync</c> was called exactly once on the mocked
        /// <see cref="IRepositoryWrapper"/>.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        public static void VerifySaveChangesAsyncCalledOnce(this Mock<IRepositoryWrapper> repositoryWrapperMock)
        {
            repositoryWrapperMock.Verify(
                rw => rw.SaveChangesAsync(),
                Times.Once(),
                "SaveChangesAsync method should be called exactly once");
        }

        /// <summary>
        /// Verifies that <c>SaveChangesAsync</c> was never called on the mocked
        /// <see cref="IRepositoryWrapper"/>.
        /// </summary>
        /// <param name="repositoryWrapperMock">The mocked repository wrapper.</param>
        public static void VerifySaveChangesAsyncCalledNever(this Mock<IRepositoryWrapper> repositoryWrapperMock)
        {
            repositoryWrapperMock.Verify(
                rw => rw.SaveChangesAsync(),
                Times.Never(),
                "SaveChangesAsync method should not be called");
        }

        // -------------------------- Verify Logger -------------------------------

        /// <summary>
        /// Verifies that <c>LogError</c> was called exactly once on the mocked
        /// <see cref="ILoggerService"/>.
        /// </summary>
        /// <param name="loggerMock">The mocked logger service.</param>
        public static void VerifyLogErrorCalledOnce(this Mock<ILoggerService> loggerMock)
        {
            loggerMock.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Once(),
                "LogError method should be called exactly once");
        }

        /// <summary>
        /// Verifies that <c>LogError</c> was never called on the mocked
        /// <see cref="ILoggerService"/>.
        /// </summary>
        /// <param name="loggerMock">The mocked logger service.</param>
        public static void VerifyLogErrorCalledNever(this Mock<ILoggerService> loggerMock)
        {
            loggerMock.Verify(
                l => l.LogError(
                    It.IsAny<object>(),
                    It.IsAny<string>()),
                Times.Never,
                "LogError method should not be called");
        }

        /// <summary>
        /// Verifies that <c>LogDebug</c> was called exactly once on the mocked.
        /// </summary>
        /// <param name="loggerMock">The mocked logger service.</param>
        public static void VerifyLogDebugCalledOnce(this Mock<ILoggerService> loggerMock)
        {
            loggerMock.Verify(
                l => l.LogDebug(It.IsAny<string>()),
                Times.Once,
                "LogDebug method should be called exactly once");
        }

        /// <summary>
        /// Verifies that <c>LogDebug</c> was never called on the mocked.
        /// </summary>
        /// <param name="loggerMock">The mocked logger service.</param>
        public static void VerifyLogDebugCalledNever(this Mock<ILoggerService> loggerMock)
        {
            loggerMock.Verify(
                l => l.LogDebug(It.IsAny<string>()),
                Times.Never,
                "LogDebug method should not be called");
        }

        // -------------------------- Verify Mapper -------------------------------

        /// <summary>
        /// Verifies that the mapper was called exactly once to map from source type to destination type.
        /// </summary>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="mapperMock">The mocked mapper.</param>
        public static void VerifyMapCalledOnce<TDestination>(
            this Mock<IMapper> mapperMock)
        {
            mapperMock.Verify(
                m => m.Map<TDestination>(It.IsAny<object>()),
                Times.Once,
                $"Map method should be called exactly once to {typeof(TDestination).Name}");
        }

        /// <summary>
        /// Verifies that no mapping operation was performed on the mocked mapper for the specified destination type.
        /// </summary>
        /// <typeparam name="TDestination">The destination type to verify.</typeparam>
        /// <param name="mapperMock">The mocked mapper.</param>
        public static void VerifyMapCalledNever<TDestination>(this Mock<IMapper> mapperMock)
        {
            mapperMock.Verify(
                m => m.Map<TDestination>(It.IsAny<object>()),
                Times.Never,
                $"Map method to {typeof(TDestination).Name} should not be called at all");
        }

        /// <summary>
        /// Verifies that <c>FindAll</c> was called exactly once on the mocked repository.
        /// </summary>
        /// <typeparam name="TRepo">The repository interface type inheriting <see cref="IRepositoryBase{TEntity}"/>.</typeparam>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="repositoryMock">The mocked repository.</param>
        public static void VerifyFindAllCalledOnce<TRepo, TEntity>(this Mock<TRepo> repositoryMock)
            where TEntity : class
            where TRepo : class, IRepositoryBase<TEntity>
        {
            repositoryMock.Verify(
                r => r.FindAll(It.IsAny<Expression<Func<TEntity, bool>>>()),
                Times.Once(),
                $"FindAll method should be called exactly once for {typeof(TEntity).Name}");
        }
    }
}