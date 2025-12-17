namespace Streetcode.XUnitTest.MediatR.Transactions.TransactionLink.Helpers
{
    using System.Linq.Expressions;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.DTO.Transactions;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Entities.Transactions;
    using Streetcode.DAL.Repositories.Interfaces.Base;

    /// <summary>
    /// Provides helper methods for setting up mocks in TransactionLink handler tests.
    /// </summary>
    public static class TransactionLinkSetups
    {
        /// <summary>
        /// Sets up the repository wrapper to return the specified transaction link when queried.
        /// </summary>
        /// <param name="repoMock">The repository wrapper mock.</param>
        /// <param name="transactionLink">The transaction link to return, or null if not found.</param>
        public static void SetupGetFirstOrDefaultAsync(
            this Mock<IRepositoryWrapper> repoMock,
            TransactionLink? transactionLink)
        {
            repoMock
                .Setup(r => r.TransactLinksRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<TransactionLink, bool>>>(),
                    It.IsAny<Func<IQueryable<TransactionLink>, IIncludableQueryable<TransactionLink, object>>>()))
                .ReturnsAsync(transactionLink);
        }

        /// <summary>
        /// Sets up the repository wrapper to return all transaction links (no parameters version).
        /// </summary>
        /// <param name="repoMock">The repository wrapper mock.</param>
        /// <param name="transactionLinks">The collection of transaction links to return.</param>
        public static void SetupGetAllAsync(
            this Mock<IRepositoryWrapper> repoMock,
            IEnumerable<TransactionLink>? transactionLinks)
        {
            repoMock
                .Setup(r => r.TransactLinksRepository.GetAllAsync(
                    It.IsAny<Expression<Func<TransactionLink, bool>>>(),
                    It.IsAny<Func<IQueryable<TransactionLink>, IIncludableQueryable<TransactionLink, object>>>()))
                .ReturnsAsync(transactionLinks);
        }

        /// <summary>
        /// Sets up the streetcode repository to return a streetcode entity.
        /// </summary>
        /// <param name="repoMock">The repository wrapper mock.</param>
        /// <param name="streetcode">The streetcode entity to return, or null if not found.</param>
        public static void SetupGetStreetcodeAsync(
            this Mock<IRepositoryWrapper> repoMock,
            StreetcodeContent? streetcode)
        {
            repoMock
                .Setup(r => r.StreetcodeRepository.GetFirstOrDefaultAsync(
                    It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                    It.IsAny<Func<IQueryable<StreetcodeContent>, IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcode);
        }

        /// <summary>
        /// Sets up the mapper to map from TransactionLink entity to TransactLinkDto.
        /// </summary>
        /// <param name="mapperMock">The mapper mock.</param>
        /// <param name="source">The source entity.</param>
        /// <param name="destination">The destination DTO.</param>
        public static void SetupMapper(
            this Mock<IMapper> mapperMock,
            TransactionLink source,
            TransactLinkDto destination)
        {
            mapperMock
                .Setup(m => m.Map<TransactLinkDto>(source))
                .Returns(destination);
        }

        /// <summary>
        /// Sets up the mapper to map from TransactionLink to nullable TransactLinkDto.
        /// </summary>
        /// <param name="mapperMock">The mapper mock.</param>
        /// <param name="source">The source entity.</param>
        /// <param name="destination">The destination DTO.</param>
        public static void SetupMapperNullable(
            this Mock<IMapper> mapperMock,
            TransactionLink? source,
            TransactLinkDto? destination)
        {
            mapperMock
                .Setup(m => m.Map<TransactLinkDto?>(source))
                .Returns(destination);
        }

        /// <summary>
        /// Sets up the mapper to map from collection of TransactionLink to collection of TransactLinkDto.
        /// </summary>
        /// <param name="mapperMock">The mapper mock.</param>
        /// <param name="source">The source entities.</param>
        /// <param name="destination">The destination DTOs.</param>
        public static void SetupMapper(
            this Mock<IMapper> mapperMock,
            IEnumerable<TransactionLink> source,
            IEnumerable<TransactLinkDto> destination)
        {
            mapperMock
                .Setup(m => m.Map<IEnumerable<TransactLinkDto>>(source))
                .Returns(destination);
        }

        /// <summary>
        /// Sets up the logger to accept LogError calls.
        /// </summary>
        /// <param name="loggerMock">The logger service mock.</param>
        public static void SetupLogger(this Mock<ILoggerService> loggerMock)
        {
            loggerMock
                .Setup(l => l.LogError(It.IsAny<object>(), It.IsAny<string>()));
        }
    }
}