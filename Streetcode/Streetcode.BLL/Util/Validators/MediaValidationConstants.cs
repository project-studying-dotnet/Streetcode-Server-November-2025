namespace Streetcode.BLL.Util.Validators
{
    /// <summary>
    /// Constants for media validation rules.
    /// </summary>
    public static class MediaValidationConstants
    {
        /// <summary>
        /// Maximum allowed size for images in bytes (5MB).
        /// </summary>
        public const long MaxImageSizeInBytes = 5 * 1024 * 1024;

        /// <summary>
        /// Maximum allowed size for audio files in bytes (10MB).
        /// </summary>
        public const long MaxAudioSizeInBytes = 10 * 1024 * 1024;
    }
}
