using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Async validator to check URL accessibility (optional feature).
    /// Performs HTTP HEAD requests to verify if URLs are reachable.
    /// </summary>
    public class UrlAccessibilityValidator
    {
        private const int DefaultMaxRedirects = 5;
        private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(10);

        private readonly HttpClient _httpClient;
        private readonly bool _followRedirects;
        private readonly int _maxRedirects;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlAccessibilityValidator"/> class.
        /// </summary>
        /// <param name="httpClient">Optional HttpClient instance. If not provided, a default one will be used.</param>
        /// <param name="followRedirects">Whether to follow HTTP redirects (default: true).</param>
        /// <param name="maxRedirects">Maximum number of redirects to follow (default: 5).</param>
        public UrlAccessibilityValidator(HttpClient? httpClient = null, bool followRedirects = true, int maxRedirects = DefaultMaxRedirects)
        {
            _httpClient = httpClient ?? new HttpClient { Timeout = DefaultTimeout };
            _followRedirects = followRedirects;
            _maxRedirects = maxRedirects;
        }

        /// <summary>
        /// Validates that a URL is accessible by sending a HEAD request.
        /// </summary>
        /// <param name="url">The URL to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the URL is accessible (returns 2xx or 3xx status), false otherwise.</returns>
        public async Task<bool> IsUrlAccessibleAsync(string? url, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                return false;
            }

            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Head, uri);
                using var response = await _httpClient.SendAsync(request, cancellationToken);

                // Consider 2xx and 3xx as accessible
                return response.IsSuccessStatusCode ||
                       ((int)response.StatusCode >= 300 && (int)response.StatusCode < 400);
            }
            catch (HttpRequestException)
            {
                // Network error, DNS failure, etc.
                return false;
            }
            catch (TaskCanceledException)
            {
                // Timeout or cancellation
                return false;
            }
            catch (OperationCanceledException)
            {
                // Explicit cancellation
                return false;
            }
        }

        /// <summary>
        /// Validates that a URL is accessible using GET request (for URLs that don't support HEAD).
        /// </summary>
        /// <param name="url">The URL to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if the URL is accessible, false otherwise.</returns>
        public async Task<bool> IsUrlAccessibleWithGetAsync(string? url, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                return false;
            }

            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, uri);
                using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }

        /// <summary>
        /// Validates multiple URLs for accessibility in parallel.
        /// </summary>
        /// <param name="urls">The URLs to validate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if all URLs are accessible, false if any fails.</returns>
        public async Task<bool> AreAllUrlsAccessibleAsync(string[] urls, CancellationToken cancellationToken = default)
        {
            if (urls == null || urls.Length == 0)
            {
                return true;
            }

            var tasks = new Task<bool>[urls.Length];
            for (int i = 0; i < urls.Length; i++)
            {
                tasks[i] = IsUrlAccessibleAsync(urls[i], cancellationToken);
            }

            var results = await Task.WhenAll(tasks);
            return Array.TrueForAll(results, result => result);
        }

        /// <summary>
        /// Gets the HTTP status code for a URL without throwing exceptions.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The HTTP status code, or null if request fails.</returns>
        public async Task<int?> GetStatusCodeAsync(string? url, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return null;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                return null;
            }

            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Head, uri);
                using var response = await _httpClient.SendAsync(request, cancellationToken);

                return (int)response.StatusCode;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Validates URL and checks if it returns a specific content type.
        /// </summary>
        /// <param name="url">The URL to validate.</param>
        /// <param name="expectedContentType">The expected content type (e.g., "image/jpeg", "application/json").</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if URL is accessible and returns expected content type, false otherwise.</returns>
        public async Task<bool> IsUrlAccessibleWithContentTypeAsync(
            string? url,
            string expectedContentType,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(expectedContentType))
            {
                return false;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                return false;
            }

            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Head, uri);
                using var response = await _httpClient.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }

                var contentType = response.Content.Headers.ContentType?.MediaType;
                return contentType != null && contentType.StartsWith(expectedContentType, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
