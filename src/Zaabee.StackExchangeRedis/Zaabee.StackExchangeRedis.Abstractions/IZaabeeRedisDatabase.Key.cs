namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisDatabase
{
    bool Delete(string key);

    long DeleteAll(IEnumerable<string> keys, bool isBatch = false);

    bool Exists(string key);

    bool Expire(string key, TimeSpan? timeSpan);
}
