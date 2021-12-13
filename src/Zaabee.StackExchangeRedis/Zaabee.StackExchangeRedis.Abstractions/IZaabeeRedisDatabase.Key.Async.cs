namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisDatabase
{
    Task<bool> DeleteAsync(string key);

    Task<long> DeleteAllAsync(IEnumerable<string> keys, bool isBatch = false);

    Task<bool> ExistsAsync(string key);

    Task<bool> ExpireAsync(string key, TimeSpan? timeSpan);
}