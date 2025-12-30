namespace Streetcode.BLL.Util
{
    public static class BlobHelper
    {
        private static readonly Dictionary<string, string> _mimeToExtension = new()
        {
            // Images
            ["image/png"] = ".png",
            ["image/jpeg"] = ".jpg",
            ["image/jpg"] = ".jpg",
            ["image/gif"] = ".gif",
            ["image/webp"] = ".webp",

            // Audio
            ["audio/mpeg"] = ".mp3",
            ["audio/mp3"] = ".mp3",
            ["audio/wav"] = ".wav"
        };

        public static IReadOnlyDictionary<string, string> MimeToExtension => _mimeToExtension;

        public static string GetExtensionFromMimeType(string mimeType)
        {
            if (!MimeToExtension.TryGetValue(mimeType.ToLower(), out var ext))
            {
                throw new InvalidOperationException($"Unsupported MIME type: {mimeType}");
            }

            return ext;
        }
    }
}
