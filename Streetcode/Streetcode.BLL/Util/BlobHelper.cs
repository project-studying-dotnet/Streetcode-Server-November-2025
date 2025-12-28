namespace Streetcode.BLL.Util
{
    public class BlobHelper
    {
        public static readonly Dictionary<string, string> MimeToExtension = new()
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
