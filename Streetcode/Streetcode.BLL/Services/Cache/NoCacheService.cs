using Streetcode.BLL.Interfaces.Cache;

namespace Streetcode.BLL.Services.Cache;

public class NoCacheService : ICacheService
{
    public Task<T?> GetAsync<T>(string key)
    {
        return Task.FromResult<T?>(default);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        return Task.CompletedTask;
    }
}
