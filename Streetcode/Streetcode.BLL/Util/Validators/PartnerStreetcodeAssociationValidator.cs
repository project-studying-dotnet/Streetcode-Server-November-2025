using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Async validator to validate Partner-Streetcode associations.
    /// Ensures that all referenced Streetcode IDs exist in the database.
    /// </summary>
    public class PartnerStreetcodeAssociationValidator
    {
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);

        private readonly IRepositoryWrapper _repositoryWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerStreetcodeAssociationValidator"/> class.
        /// </summary>
        /// <param name="repositoryWrapper">The repository wrapper for database access.</param>
        public PartnerStreetcodeAssociationValidator(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Validates that all Streetcode IDs in the list exist in the database.
        /// </summary>
        /// <param name="streetcodeIds">The list of Streetcode IDs to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if all Streetcode IDs exist, false otherwise.</returns>
        public async Task<bool> AreStreetcodesValidAsync(IEnumerable<int>? streetcodeIds, CancellationToken cancellationToken = default)
        {
            if (streetcodeIds == null || !streetcodeIds.Any())
            {
                return true; // Empty list is valid
            }

            var distinctIds = streetcodeIds.Distinct().ToList();

            // Query database with timeout
            using var timeoutCts = new CancellationTokenSource(DefaultTimeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            try
            {
                // Get all streetcodes that match the provided IDs
                var existingStreetcodes = await _repositoryWrapper
                    .StreetcodeRepository
                    .GetAllAsync(predicate: s => distinctIds.Contains(s.Id));

                // Check if all provided IDs exist in the database
                var existingIds = existingStreetcodes.Select(s => s.Id).ToHashSet();
                return distinctIds.All(id => existingIds.Contains(id));
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
            {
                // Timeout occurred - assume invalid for safety
                return false;
            }
        }

        /// <summary>
        /// Validates that a single Streetcode ID exists in the database.
        /// </summary>
        /// <param name="streetcodeId">The Streetcode ID to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the Streetcode ID exists, false otherwise.</returns>
        public async Task<bool> IsStreetcodeValidAsync(int streetcodeId, CancellationToken cancellationToken = default)
        {
            if (streetcodeId <= 0)
            {
                return false;
            }

            // Query database with timeout
            using var timeoutCts = new CancellationTokenSource(DefaultTimeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            try
            {
                var streetcode = await _repositoryWrapper
                    .StreetcodeRepository
                    .GetFirstOrDefaultAsync(predicate: s => s.Id == streetcodeId);

                return streetcode != null;
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
            {
                // Timeout occurred - assume invalid for safety
                return false;
            }
        }

        /// <summary>
        /// Validates that all Partner IDs in the list exist in the database.
        /// </summary>
        /// <param name="partnerIds">The list of Partner IDs to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if all Partner IDs exist, false otherwise.</returns>
        public async Task<bool> ArePartnersValidAsync(IEnumerable<int>? partnerIds, CancellationToken cancellationToken = default)
        {
            if (partnerIds == null || !partnerIds.Any())
            {
                return true; // Empty list is valid
            }

            var distinctIds = partnerIds.Distinct().ToList();

            // Query database with timeout
            using var timeoutCts = new CancellationTokenSource(DefaultTimeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            try
            {
                // Get all partners that match the provided IDs
                var existingPartners = await _repositoryWrapper
                    .PartnersRepository
                    .GetAllAsync(predicate: p => distinctIds.Contains(p.Id));

                // Check if all provided IDs exist in the database
                var existingIds = existingPartners.Select(p => p.Id).ToHashSet();
                return distinctIds.All(id => existingIds.Contains(id));
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
            {
                // Timeout occurred - assume invalid for safety
                return false;
            }
        }
    }
}
