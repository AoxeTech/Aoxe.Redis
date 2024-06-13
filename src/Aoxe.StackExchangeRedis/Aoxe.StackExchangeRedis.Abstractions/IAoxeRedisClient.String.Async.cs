namespace Aoxe.StackExchangeRedis.Abstractions;

public partial interface IAoxeRedisClient
{
    ValueTask<bool> AddAsync<T>(string key, T? entity, TimeSpan? expiry = null);

    ValueTask<T?> GetAsync<T>(string key);

    ValueTask<List<T?>> GetAsync<T>(IEnumerable<string> keys);

    Task<bool> AddAsync(string key, long value, TimeSpan? expiry = null);

    Task<bool> AddAsync(string key, double value, TimeSpan? expiry = null);

    Task<double> IncrementAsync(string key, double value);

    Task<long> IncrementAsync(string key, long value);
}
