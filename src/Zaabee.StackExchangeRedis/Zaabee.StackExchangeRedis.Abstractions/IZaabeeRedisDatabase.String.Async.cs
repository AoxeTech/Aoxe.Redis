namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisDatabase
{
    ValueTask<bool> AddAsync<T>(string key, T? entity, TimeSpan? expiry = null);

    ValueTask AddRangeAsync<T>(
        IDictionary<string, T?> entities,
        TimeSpan? expiry = null,
        bool isBatch = false
    );

    ValueTask<T?> GetAsync<T>(string key);

    ValueTask<List<T>> GetAsync<T>(IEnumerable<string> keys, bool isBatch = false);

    ValueTask<bool> AddAsync(string key, long value, TimeSpan? expiry = null);

    ValueTask<bool> AddAsync(string key, double value, TimeSpan? expiry = null);

    ValueTask<double> IncrementAsync(string key, double value);

    ValueTask<long> IncrementAsync(string key, long value);
}
