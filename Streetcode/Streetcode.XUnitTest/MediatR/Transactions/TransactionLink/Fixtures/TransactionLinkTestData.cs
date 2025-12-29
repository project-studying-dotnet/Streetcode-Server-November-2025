namespace Streetcode.XUnitTest.MediatR.Transactions.Fixtures
{
 using global::Streetcode.BLL.DTO.Transactions;
 using global::Streetcode.DAL.Entities.Transactions;

    /// <summary>
    /// Provides test data for TransactionLink entities and DTOs used in unit tests.
    /// </summary>
    public static class TransactionLinkTestData
    {
        /// <summary>
        /// Creates a sample TransactionLink entity with specified properties.
        /// </summary>
        /// <param name="id">The transaction link identifier.</param>
        /// <param name="streetcodeId">The streetcode identifier.</param>
        /// <param name="url">The transaction URL.</param>
        /// <param name="qrCodeUrl">The QR code URL.</param>
        /// <returns>A configured <see cref="TransactionLink"/> entity.</returns>
        public static TransactionLink CreateTransactionLink(
            int id = 1,
            int streetcodeId = 1,
            string url = "https://payment.example.com/donate/1",
            string? qrCodeUrl = null)
        {
            return new TransactionLink
            {
                Id = id,
                StreetcodeId = streetcodeId,
                Url = url,
            };
        }

        /// <summary>
        /// Creates a sample TransactLinkDto with specified properties.
        /// </summary>
        /// <param name="id">The transaction link identifier.</param>
        /// <param name="streetcodeId">The streetcode identifier.</param>
        /// <param name="url">The transaction URL.</param>
        /// <param name="qrCodeUrl">The QR code URL.</param>
        /// <returns>A configured <see cref="TransactLinkDto"/>.</returns>
        public static TransactLinkDto CreateTransactLinkDto(
            int id = 1,
            int streetcodeId = 1,
            string url = "https://payment.example.com/donate/1",
            string? qrCodeUrl = null)
        {
            return new TransactLinkDto
            {
                Id = id,
                StreetcodeId = streetcodeId,
                Url = url,
                QrCodeUrl = qrCodeUrl,
            };
        }

        /// <summary>
        /// Creates multiple TransactionLink entities for collection testing.
        /// </summary>
        /// <param name="count">Number of entities to create.</param>
        /// <returns>An enumerable of <see cref="TransactionLink"/> entities.</returns>
        public static IEnumerable<TransactionLink> CreateTransactionLinks(int count = 3)
        {
            return Enumerable.Range(1, count)
                .Select(i => CreateTransactionLink(
                    id: i,
                    streetcodeId: i,
                    url: $"https://payment.example.com/donate/{i}"))
                .ToList();
        }

        /// <summary>
        /// Creates multiple TransactLinkDto instances for collection testing.
        /// </summary>
        /// <param name="count">Number of DTOs to create.</param>
        /// <returns>An enumerable of <see cref="TransactLinkDto"/> instances.</returns>
        public static IEnumerable<TransactLinkDto> CreateTransactLinkDtos(int count = 3)
        {
            return Enumerable.Range(1, count)
                .Select(i => CreateTransactLinkDto(
                    id: i,
                    streetcodeId: i,
                    url: $"https://payment.example.com/donate/{i}"))
                .ToList();
        }
    }
}