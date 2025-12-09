using System;

namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Utility class for URL validation.
    /// </summary>
    public static class UrlValidator
    {
        /// <summary>
        /// Validates if a string is a valid absolute HTTP/HTTPS URL.
        /// </summary>
        /// <param name="url">The URL to validate.</param>
        /// <param name="isRequired">Whether the URL is required (false allows null/empty).</param>
        /// <returns>True if valid URL or allowed to be empty, false otherwise.</returns>
        public static bool IsValidAbsoluteUrl(string? url, bool isRequired = true)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return !isRequired;
            }

            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
