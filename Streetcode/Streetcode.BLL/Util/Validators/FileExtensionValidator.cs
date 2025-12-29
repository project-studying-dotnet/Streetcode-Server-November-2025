using System.Linq;

namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Utility class for file extension validation.
    /// </summary>
    public static class FileExtensionValidator
    {
        /// <summary>
        /// Allowed image file extensions.
        /// </summary>
        public static readonly string[] AllowedImageExtensions = { "png", "jpg", "jpeg", "webp", "gif" };

        /// <summary>
        /// Allowed audio file extensions.
        /// </summary>
        public static readonly string[] AllowedAudioExtensions = { "mp3", "wav", "ogg", "m4a" };

        /// <summary>
        /// Validates if an extension is in the allowed list.
        /// </summary>
        /// <param name="extension">The file extension to validate.</param>
        /// <param name="allowedExtensions">Array of allowed extensions.</param>
        /// <returns>True if extension is allowed, false otherwise.</returns>
        public static bool IsValidExtension(string? extension, string[] allowedExtensions)
        {
            if (string.IsNullOrWhiteSpace(extension))
            {
                return false;
            }

            return allowedExtensions.Contains(extension.ToLower());
        }
    }
}
