namespace Streetcode.BLL.Services.BlobStorageService;

public enum BlobStorageType
{
    Local,
    Azure
}

public class BlobEnvironmentVariables
{
    public BlobStorageType BlobStorageType { get; set; }

    // Local
    public string BlobStoreKey { get; set; }
    public string BlobStorePath { get; set; }

    // Azure
    public string AzureStorageConnectionString { get; set; }
    public string AzureContainerName { get; set; }
}