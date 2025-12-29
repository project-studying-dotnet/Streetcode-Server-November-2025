using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Util;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.Services.BlobStorageService;

public class LocalBlobService : IBlobService
{
    private readonly BlobEnvironmentVariables _envirovment;
    private readonly string _keyCrypt;
    private readonly string _blobPath;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public LocalBlobService(IOptions<BlobEnvironmentVariables> environment, IRepositoryWrapper? repositoryWrapper = null)
    {
        _envirovment = environment.Value;
        _keyCrypt = _envirovment.BlobStoreKey;
        _blobPath = _envirovment.BlobStorePath;
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task<MemoryStream> FindFileInStorageAsMemoryStreamAsync(string name)
    {
        byte[] decodedBytes = await DecryptFileAsync(name);

        var image = new MemoryStream(decodedBytes);

        return image;
    }

    public async Task<string> FindFileInStorageAsBase64Async(string name)
    {
        byte[] decodedBytes = await DecryptFileAsync(name);

        string base64 = Convert.ToBase64String(decodedBytes);

        return base64;
    }

    public async Task<string> SaveFileInStorageAsync(string base64, string name, string mimeType)
    {
        byte[] fileBytes = Convert.FromBase64String(base64);

        var hashName = HashFunction($"{DateTime.UtcNow}{name}");

        var extension = BlobHelper.GetExtensionFromMimeType(mimeType);
        var fullName = $"{hashName}{extension}";

        Directory.CreateDirectory(_blobPath);
        await EncryptFileAsync(fileBytes, extension.TrimStart('.'), hashName);

        return fullName;
    }

    public async Task SaveFileInStorageBase64Async(string base64, string name, string mimeType)
    {
        byte[] imageBytes = Convert.FromBase64String(base64);
        Directory.CreateDirectory(_blobPath);
        await EncryptFileAsync(imageBytes, mimeType, name);
    }

    public Task DeleteFileInStorageAsync(string name)
    {
        File.Delete($"{_blobPath}{name}");
        return Task.CompletedTask;
    }

    public async Task<string> UpdateFileInStorageAsync(
        string previousBlobName,
        string base64Format,
        string newBlobName,
        string extension)
    {
        var hashBlobStorageName = await SaveFileInStorageAsync(
            base64Format,
            newBlobName,
            extension);

        await DeleteFileInStorageAsync(previousBlobName);

        return hashBlobStorageName;
    }

    public async Task CleanBlobStorageAsync()
    {
        var base64Files = GetAllBlobNames();

        var existingImagesInDatabase = await _repositoryWrapper.ImageRepository.GetAllAsync();
        var existingAudiosInDatabase = await _repositoryWrapper.AudioRepository.GetAllAsync();

        List<string> existingMedia = new ();
        existingMedia.AddRange(existingImagesInDatabase.Select(img => img.BlobName));
        existingMedia.AddRange(existingAudiosInDatabase.Select(img => img.BlobName));

        var filesToRemove = base64Files.Except(existingMedia).ToList();

        foreach (var file in filesToRemove)
        {
            Console.WriteLine($"Deleting {file}...");
            await DeleteFileInStorageAsync(file);
        }
    }

    public Task<bool> BlobExistsAsync(string blobName)
    {
        return Task.FromResult(File.Exists(Path.Combine(_blobPath, blobName)));
    }

    private IEnumerable<string> GetAllBlobNames()
    {
        var paths = Directory.EnumerateFiles(_blobPath);

        return paths.Select(p => Path.GetFileName(p));
    }

    private string HashFunction(string createdFileName)
    {
        using (var hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(createdFileName));
            return Convert.ToBase64String(result).Replace('/', '_');
        }
    }

    private async Task EncryptFileAsync(byte[] imageBytes, string type, string name)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(_keyCrypt);

        byte[] iv = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(iv);
        }

        byte[] encryptedBytes;
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Key = keyBytes;
            aes.IV = iv;
            ICryptoTransform encryptor = aes.CreateEncryptor();
            encryptedBytes = encryptor.TransformFinalBlock(imageBytes, 0, imageBytes.Length);
        }

        byte[] encryptedData = new byte[encryptedBytes.Length + iv.Length];
        Buffer.BlockCopy(iv, 0, encryptedData, 0, iv.Length);
        Buffer.BlockCopy(encryptedBytes, 0, encryptedData, iv.Length, encryptedBytes.Length);
        await File.WriteAllBytesAsync($"{_blobPath}{name}.{type}", encryptedData);
    }

    private async Task<byte[]> DecryptFileAsync(string fileName)
    {
        byte[] encryptedData = await File.ReadAllBytesAsync(Path.Combine(_blobPath, fileName));
        byte[] keyBytes = Encoding.UTF8.GetBytes(_keyCrypt);

        byte[] iv = new byte[16];
        Buffer.BlockCopy(encryptedData, 0, iv, 0, iv.Length);

        byte[] decryptedBytes;
        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.Key = keyBytes;
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor();
            decryptedBytes = decryptor.TransformFinalBlock(encryptedData, iv.Length, encryptedData.Length - iv.Length);
        }

        return decryptedBytes;
    }
}