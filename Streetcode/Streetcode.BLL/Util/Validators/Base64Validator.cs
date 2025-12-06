using System;

namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Utility class for Base64 string validation.
    /// </summary>
    public static class Base64Validator
    {
        /// <summary>
        /// Validates if a string is valid Base64 format.
        /// </summary>
        /// <param name="base64String">The Base64 string to validate.</param>
        /// <returns>True if valid Base64, false otherwise.</returns>
        public static bool IsValidBase64(string? base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates if a Base64 string, when decoded, does not exceed the specified size in bytes.
        /// </summary>
        /// <param name="base64String">The Base64 string to validate.</param>
        /// <param name="maxSizeInBytes">Maximum allowed size in bytes after decoding.</param>
        /// <returns>True if decoded size is within limit, false otherwise.</returns>
        public static bool IsWithinSizeLimit(string? base64String, long maxSizeInBytes)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
                return false;
            }

            try
            {
                byte[] decodedBytes = Convert.FromBase64String(base64String);
                return decodedBytes.Length <= maxSizeInBytes;
            }
            catch
            {
                return false;
            }
        }
    }
}
