using AutoMapper;

namespace Streetcode.XUnitTest.MediatR.Transactions.TransactionLink.Helpers
{
    using Moq;
    using Streetcode.BLL.DTO.Transactions;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.DAL.Entities.Transactions;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore.Query;
    using Streetcode.DAL.Entities.Streetcode;

    /// <summary>
    /// Provides helper methods for verifying mock interactions in TransactionLink handler tests.
    /// </summary>
    public static class TransactionLinkVerifications
    {
        /// <summary>
        /// Verifies that GetFirstOrDefaultAsync was called exactly once on the TransactLinks repository.
        /// </summary>
        /// <param name="repoMock">The repository wrapper mock.</param>
        public static void VerifyGetFirstOrDefaultAsyncCalledOnce(this Mock<IRepositoryWrapper> repoMock)
        {
            repoMock.Verify(
                r => r.TransactLinksRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<TransactionLink, bool>>>(),
                    It.IsAny<Func<IQueryable<TransactionLink>, IIncludableQueryable<TransactionLink, object>>>()),
                Times.Once,
                "GetFirstOrDefaultAsync should be called exactly once on TransactLinksRepository");
        }

        /// <summary>
        /// Verifies that GetAllAsync was called exactly once on the TransactLinks repository (no parameters version).
        /// </summary>
        /// <param name="repoMock">The repository wrapper mock.</param>
        public static void VerifyGetAllAsyncCalledOnce(this Mock<IRepositoryWrapper> repoMock)
        {
            repoMock.Verify(
                r => r.TransactLinksRepository.GetAllAsync(null, null),
                Times.Once,
                "GetAllAsync should be called exactly once on TransactLinksRepository");
        }

        /// <summary>
        /// Verifies that GetFirstOrDefaultAsync was called on the Streetcode repository.
        /// </summary>
        /// <param name="repoMock">The repository wrapper mock.</param>
        /// <param name="times">The expected number of calls.</param>
        public static void VerifyGetStreetcodeAsyncCalled(
            this Mock<IRepositoryWrapper> repoMock,
            Times times)
        {
            repoMock.Verify(
                r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()),
                times,
                "GetFirstOrDefaultAsync should be called on StreetcodeRepository");
        }

        /// <summary>
        /// Verifies that Map was called exactly once to map to TransactLinkDto.
        /// </summary>
        /// <param name="mapperMock">The mapper mock.</param>
        public static void VerifyMapCalledOnce(this Mock<IMapper> mapperMock)
        {
            mapperMock.Verify(
                m => m.Map<TransactLinkDto>(It.IsAny<TransactionLink>()),
                Times.Once,
                "Map should be called exactly once to TransactLinkDto");
        }

        /// <summary>
        /// Verifies that Map was called exactly once to map nullable types.
        /// </summary>
        /// <param name="mapperMock">The mapper mock.</param>
        public static void VerifyMapNullableCalledOnce(this Mock<IMapper> mapperMock)
        {
            mapperMock.Verify(
                m => m.Map<TransactLinkDto?>(It.IsAny<TransactionLink?>()),
                Times.Once,
                "Map should be called exactly once to TransactLinkDto?");
        }

        /// <summary>
        /// Verifies that Map was called exactly once to map collection to IEnumerable of TransactLinkDto.
        /// </summary>
        /// <param name="mapperMock">The mapper mock.</param>
        public static void VerifyMapCollectionCalledOnce(this Mock<IMapper> mapperMock)
        {
            mapperMock.Verify(
                m => m.Map<IEnumerable<TransactLinkDto>>(It.IsAny<IEnumerable<TransactionLink>>()),
                Times.Once,
                "Map should be called exactly once to IEnumerable<TransactLinkDto>");
        }

        /// <summary>
        /// Verifies that LogError was called exactly once.
        /// </summary>
        /// <param name="loggerMock">The logger service mock.</param>
        public static void VerifyLogErrorCalledOnce(this Mock<ILoggerService> loggerMock)
        {
            loggerMock.Verify(
                l => l.LogError(It.IsAny<object>(), It.IsAny<string>()),
                Times.Once,
                "LogError should be called exactly once");
        }

        /// <summary>
        /// Verifies that LogError was never called.
        /// </summary>
        /// <param name="loggerMock">The logger service mock.</param>
        public static void VerifyLogErrorNeverCalled(this Mock<ILoggerService> loggerMock)
        {
            loggerMock.Verify(
                l => l.LogError(It.IsAny<object>(), It.IsAny<string>()),
                Times.Never,
                "LogError should not be called");
        }
    }
}