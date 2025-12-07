using System;
using System.IO;

namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Validator for image size validation with base64 decoding support.
    /// Provides detailed size validation and information extraction.
    /// </summary>
    public static class ImageSizeValidator
    {
        /// <summary>
        /// Validates that a base64 image string is within the specified size limit.
        /// </summary>
        /// <param name="base64String">The base64-encoded image string.</param>
        /// <param name="maxSizeInBytes">Maximum allowed size in bytes.</param>
        /// <returns>True if the decoded image is within the size limit, false otherwise.</returns>
        public static bool IsWithinSizeLimit(string? base64String, long maxSizeInBytes)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return false;
            }

            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                return imageBytes.Length <= maxSizeInBytes;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the decoded size in bytes from a base64 image string.
        /// </summary>
        /// <param name="base64String">The base64-encoded image string.</param>
        /// <returns>The size in bytes, or -1 if decoding fails.</returns>
        public static long GetDecodedSize(string? base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return -1;
            }

            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                return imageBytes.Length;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets the decoded size in megabytes from a base64 image string.
        /// </summary>
        /// <param name="base64String">The base64-encoded image string.</param>
        /// <returns>The size in MB with 2 decimal places, or -1 if decoding fails.</returns>
        public static double GetDecodedSizeInMB(string? base64String)
        {
            long sizeInBytes = GetDecodedSize(base64String);
            if (sizeInBytes < 0)
            {
                return -1;
            }

            return Math.Round(sizeInBytes / (1024.0 * 1024.0), 2);
        }

        /// <summary>
        /// Validates that a base64 image string is within the default maximum image size.
        /// Uses MediaValidationConstants.MaxImageSizeInBytes (5MB).
        /// </summary>
        /// <param name="base64String">The base64-encoded image string.</param>
        /// <returns>True if the decoded image is within the default limit, false otherwise.</returns>
        public static bool IsWithinDefaultImageLimit(string? base64String)
        {
            return IsWithinSizeLimit(base64String, MediaValidationConstants.MaxImageSizeInBytes);
        }

        /// <summary>
        /// Decodes a base64 string to byte array.
        /// </summary>
        /// <param name="base64String">The base64-encoded string.</param>
        /// <returns>The decoded byte array, or null if decoding fails.</returns>
#pragma warning disable SA1011 // Closing square bracket should be followed by a space
        public static byte[]? DecodeBase64(string? base64String)
#pragma warning restore SA1011 // Closing square bracket should be followed by a space
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return null;
            }

            try
            {
                return Convert.FromBase64String(base64String);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Validates that the decoded image size is between minimum and maximum bounds.
        /// </summary>
        /// <param name="base64String">The base64-encoded image string.</param>
        /// <param name="minSizeInBytes">Minimum allowed size in bytes (0 for no minimum).</param>
        /// <param name="maxSizeInBytes">Maximum allowed size in bytes.</param>
        /// <returns>True if the size is within bounds, false otherwise.</returns>
        public static bool IsWithinSizeRange(string? base64String, long minSizeInBytes, long maxSizeInBytes)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return false;
            }

            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                long size = imageBytes.Length;
                return size >= minSizeInBytes && size <= maxSizeInBytes;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates base64 string and checks if it represents a non-empty image.
        /// </summary>
        /// <param name="base64String">The base64-encoded image string.</param>
        /// <returns>True if valid and non-empty, false otherwise.</returns>
        public static bool IsValidNonEmptyImage(string? base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return false;
            }

            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                return imageBytes.Length > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
