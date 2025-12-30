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

        public async Task<string> SaveFileInStorageAsync(
            string base64,
            string name,
            string mimeType)
        {
            var container = await GetContainerAsync();

            var hashName = GenerateHashName(base64);

            var extension = BlobHelper.GetExtensionFromMimeType(mimeType);

            var fullBlobName = string.IsNullOrEmpty(extension) ? hashName : $"{hashName}{extension}";

            var blobClient = container.GetBlobClient(fullBlobName);

            var bytes = Convert.FromBase64String(base64);
            using var stream = new MemoryStream(bytes);

            var contentType = NormalizeMimeType(mimeType);

            await blobClient.UploadAsync(
                stream,
                new BlobHttpHeaders { ContentType = contentType },
                conditions: null);

            return fullBlobName;
        }

        public async Task<string> FindFileInStorageAsBase64Async(string name)
        {
            try
            {
                using var memoryStream = await FindFileInStorageAsMemoryStreamAsync(name);
                var imageArray = memoryStream.ToArray();
                return Convert.ToBase64String(imageArray);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException(string.Format(ErrorMessages.BlobNotFoundByName, name), ex);
            }
        }

        public async Task<MemoryStream> FindFileInStorageAsMemoryStreamAsync(string name)
        {
            var container = await GetContainerAsync();
            var blobClient = container.GetBlobClient(name);

            if (!await blobClient.ExistsAsync())
            {
                throw new FileNotFoundException(string.Format(ErrorMessages.BlobNotFoundByName, name));
            }

            var memoryStream = new MemoryStream();
            await blobClient.DownloadToAsync(memoryStream);
            memoryStream.Position = 0;

            return memoryStream;
        }

        public async Task<string> UpdateFileInStorageAsync(
           string previousBlobName,
           string base64Format,
           string newBlobName,
           string mimeType)
        {
            var newBlobNameInStorage = await SaveFileInStorageAsync(base64Format, newBlobName, mimeType);

            DeleteFileInStorageAsync(previousBlobName);

            return newBlobNameInStorage;
        }

        public async Task DeleteFileInStorageAsync(string name)
        {
            var container = await GetContainerAsync();
            var blobClient = container.GetBlobClient(name);

            if (!await blobClient.DeleteIfExistsAsync())
            {
                throw new FileNotFoundException(string.Format(ErrorMessages.BlobNotFoundByName, name));
            }
        }

        public async Task<bool> BlobExistsAsync(string blobName)
        {
            var container = await GetContainerAsync();
            return await container.GetBlobClient(blobName).ExistsAsync();
        }

        private async Task<BlobContainerClient> GetContainerAsync()
        {
            var container = _blobServiceClient.GetBlobContainerClient(_containerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob);
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
                throw new ArgumentException(ErrorMessages.MimeRequired);
            }

            if (input.Contains('/'))
            {
                return input.ToLowerInvariant();
            }

            var ext = input.StartsWith('.') ? input : $".{input}";

            if (!BlobHelper.MimeToExtension.TryGetValue(ext.ToLower(), out var mime))
            {
                throw new InvalidOperationException(string.Format(ErrorMessages.UnsupportedFileExtension, input));
            }

            return mime;
        }
    }
}