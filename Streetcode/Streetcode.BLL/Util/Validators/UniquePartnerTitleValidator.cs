using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Async validator to check if a partner title is unique in the database.
    /// Implements caching and timeout handling for optimal performance.
    /// </summary>
    public class UniquePartnerTitleValidator
    {
        private const string CacheKeyPrefix = "PartnerTitle_";
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMemoryCache? _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniquePartnerTitleValidator"/> class.
        /// </summary>
        /// <param name="repositoryWrapper">The repository wrapper for database access.</param>
        /// <param name="cache">Optional memory cache for reducing DB calls.</param>
        public UniquePartnerTitleValidator(IRepositoryWrapper repositoryWrapper, IMemoryCache? cache = null)
        {
            _repositoryWrapper = repositoryWrapper;
            _cache = cache;
        }

        /// <summary>
        /// Checks if the partner title is unique (case-insensitive).
        /// Uses caching and timeout handling for optimal performance.
        /// </summary>
        /// <param name="title">The partner title to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the title is unique, false otherwise.</returns>
        public async Task<bool> IsTitleUniqueAsync(string title, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return true; // Let NotEmpty validator handle this
            }

            var normalizedTitle = title.ToLower();
            var cacheKey = $"{CacheKeyPrefix}{normalizedTitle}";

            // Check cache first
            if (_cache != null && _cache.TryGetValue(cacheKey, out bool cachedResult))
            {
                return cachedResult;
            }

            // Query database with timeout
            using var timeoutCts = new CancellationTokenSource(DefaultTimeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            try
            {
                var existingPartner = await _repositoryWrapper
                    .PartnersRepository
                    .GetFirstOrDefaultAsync(predicate: p => p.Title.ToLower() == normalizedTitle);

                var isUnique = existingPartner == null;

                // Cache the result
                if (_cache != null)
                {
                    _cache.Set(cacheKey, isUnique, CacheDuration);
                }

                return isUnique;
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
            {
                // Timeout occurred - assume not unique for safety
                return false;
            }
        }

        /// <summary>
        /// Checks if the partner title is unique, excluding a specific partner by ID (for updates).
        /// Uses caching and timeout handling for optimal performance.
        /// Skips DB check if title hasn't changed.
        /// </summary>
        /// <param name="title">The partner title to validate.</param>
        /// <param name="excludePartnerId">The partner ID to exclude from the check.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the title is unique, false otherwise.</returns>
        public async Task<bool> IsTitleUniqueAsync(string title, int excludePartnerId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return true; // Let NotEmpty validator handle this
            }

            var normalizedTitle = title.ToLower();
            var cacheKey = $"{CacheKeyPrefix}{normalizedTitle}_exclude_{excludePartnerId}";

            // Check cache first
            if (_cache != null && _cache.TryGetValue(cacheKey, out bool cachedResult))
            {
                return cachedResult;
            }

            // Query database with timeout
            using var timeoutCts = new CancellationTokenSource(DefaultTimeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            try
            {
                var existingPartner = await _repositoryWrapper
                    .PartnersRepository
                    .GetFirstOrDefaultAsync(predicate: p => p.Title.ToLower() == normalizedTitle && p.Id != excludePartnerId);

                var isUnique = existingPartner == null;

                // Cache the result with shorter duration for update scenarios
                if (_cache != null)
                {
                    _cache.Set(cacheKey, isUnique, TimeSpan.FromMinutes(2));
                }

                return isUnique;
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
            {
                // Timeout occurred - assume not unique for safety
                return false;
            }
        }

        /// <summary>
        /// Invalidates the cache for a specific partner title.
        /// Should be called after creating, updating, or deleting a partner.
        /// </summary>
        /// <param name="title">The partner title to invalidate from cache.</param>
        public void InvalidateCache(string title)
        {
            if (_cache == null || string.IsNullOrWhiteSpace(title))
            {
                return;
            }

            var normalizedTitle = title.ToLower();
            var cacheKey = $"{CacheKeyPrefix}{normalizedTitle}";
            _cache.Remove(cacheKey);
        }
    }
}
