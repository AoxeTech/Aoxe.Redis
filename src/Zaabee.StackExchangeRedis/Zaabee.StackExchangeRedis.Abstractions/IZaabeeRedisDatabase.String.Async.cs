namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisDatabase
{
    ValueTask<bool> AddAsync<T>(string key, T? entity, TimeSpan? expiry = null);

    ValueTask<T?> GetAsync<T>(string key);

    ValueTask<List<T?>> GetAsync<T>(IEnumerable<string> keys);
}
