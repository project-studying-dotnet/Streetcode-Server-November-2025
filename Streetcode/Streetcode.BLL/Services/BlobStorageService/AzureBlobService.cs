using System.Security.Cryptography;
using System.Text;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Util;

namespace Streetcode.BLL.Services.BlobStorageService
{
    public class AzureBlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureBlobService(IOptions<BlobEnvironmentVariables> blobEnvironmentVariables)
        {
            var config = blobEnvironmentVariables.Value;
            _blobServiceClient = new BlobServiceClient(config.AzureStorageConnectionString);
            _containerName = config.AzureContainerName;
        }

        public string SaveFileInStorage(
            string base64,
            string name,
            string mimeType)
        {
            var container = GetContainer();

            var hashName = GenerateHashName(base64);

            var extension = BlobHelper.GetExtensionFromMimeType(mimeType);

            var fullBlobName = string.IsNullOrEmpty(extension) ? hashName : $"{hashName}{extension}";

            var blobClient = container.GetBlobClient(fullBlobName);

            var bytes = Convert.FromBase64String(base64);
            using var stream = new MemoryStream(bytes);

            var contentType = NormalizeMimeType(mimeType);

            blobClient.Upload(
                stream,
                new BlobHttpHeaders { ContentType = contentType },
                conditions: null);

            return fullBlobName;
        }

        public string FindFileInStorageAsBase64(string name)
        {
            try
            {
                using var memoryStream = FindFileInStorageAsMemoryStream(name);
                var imageArray = memoryStream.ToArray();
                return Convert.ToBase64String(imageArray);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Blob with name '{name}' was not found in storage", ex);
            }
        }

        public MemoryStream FindFileInStorageAsMemoryStream(string name)
        {
            var container = GetContainer();
            var blobClient = container.GetBlobClient(name);

            if (!blobClient.Exists())
            {
                throw new FileNotFoundException($"Blob with name '{name}' was not found");
            }

            var memoryStream = new MemoryStream();
            blobClient.DownloadTo(memoryStream);
            memoryStream.Position = 0;

            return memoryStream;
        }

        public string UpdateFileInStorage(
           string previousBlobName,
           string base64Format,
           string newBlobName,
           string mimeType)
        {
            DeleteFileInStorage(previousBlobName);
            return SaveFileInStorage(base64Format, newBlobName, mimeType);
        }

        public void DeleteFileInStorage(string name)
        {
            var container = GetContainer();
            var blobClient = container.GetBlobClient(name);

            if (!blobClient.DeleteIfExists())
            {
                throw new FileNotFoundException($"Blob with name '{name}' was not found");
            }
        }

        public bool BlobExists(string blobName)
        {
            var container = GetContainer();
            return container.GetBlobClient(blobName).Exists();
        }

        private BlobContainerClient GetContainer()
        {
            var container = _blobServiceClient.GetBlobContainerClient(_containerName);
            container.CreateIfNotExists(PublicAccessType.Blob);
            return container;
        }

        private static string GenerateHashName(string base64Content)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(base64Content));
            return Convert.ToBase64String(hashBytes).Replace("+", "-").Replace("/", "_").TrimEnd('=');
        }

        private static string NormalizeMimeType(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("MIME type cannot be null or empty");
            }

            if (input.Contains('/'))
            {
                return input.ToLowerInvariant();
            }

            var ext = input.StartsWith('.') ? input : $".{input}";

            if (!BlobHelper.MimeToExtension.TryGetValue(ext.ToLower(), out var mime))
            {
                throw new InvalidOperationException($"Unsupported file extension: {input}");
            }

            return mime;
        }
    }
}